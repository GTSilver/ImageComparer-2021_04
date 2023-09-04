using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_comparison
{
    public partial class Form1 : Form
    {
        static int progressValue;
        static int progressMaxValue;
        static Thread progressThread;
        static Thread calculationThread;

        public Form1()
        {
            InitializeComponent();
        }

        public class ImagePixel
        {
            public float R;
            public float G;
            public float B;
            public float A;

            public float GetSum()
            {
                return R + G + B + A;
            }

            public float GetAverage()
            {
                return (R + G + B + A) / 4;
            }

            public bool IsDifferent()
            {
                if (R != 0 || G != 0 || B != 0 || A != 0)
                    return true;
                else
                    return false;
            }

            public static ImagePixel operator -(ImagePixel pixel1, ImagePixel pixel2)
            {
                return new ImagePixel
                {
                    R = pixel1.R - pixel2.R,
                    G = pixel1.G - pixel2.G,
                    B = pixel1.B - pixel2.B,
                    A = pixel1.A - pixel2.A,
                };
            }

            public static ImagePixel operator *(ImagePixel pixel1, ImagePixel pixel2)
            {
                return new ImagePixel
                {
                    R = pixel1.R * pixel2.R,
                    G = pixel1.G * pixel2.G,
                    B = pixel1.B * pixel2.B,
                    A = pixel1.A * pixel2.A,
                };
            }
        }

        public class Сhannel
        {
            public string Name;
            public int Number;
            public float Percente;
            public float Sum;
            public float SumSquares;
            public float Dispersion;
            public float StandardDeviation;

            public void GetPercente(int pixelsNumber, int digits)
            {
                Percente = (float)Math.Round(Number * 100f / pixelsNumber, digits);
            }

            public void GetDispersion(int pixelsNumber, int digits)
            {
                Dispersion = (float)Math.Round((SumSquares / pixelsNumber) - (Sum / pixelsNumber) * (Sum / pixelsNumber), digits);
            }

            public void GetStandardDeviation(int digits)
            {
                StandardDeviation = (float)Math.Round(Math.Sqrt(Dispersion), digits);
            }

            public void CalculateParameters(int pixelsNumber, int digits)
            {
                GetPercente(pixelsNumber, digits);
                GetDispersion(pixelsNumber, digits);
                GetStandardDeviation(digits);
            }
        }

        public ImagePixel[,] GetImage(Bitmap bmp)    //Использование object выполнено для передачи объекта Bitmap в отдельный поток
        {
            //Кастует Bitmap обратно в Bitmap из object для дальнейшей работы
            Bitmap bmap = (Bitmap)bmp;

            //Создаём массив для хранения и передачи изображения (массива пикселей) для заполнения в следующем цикле
            var result = new ImagePixel[bmap.Width, bmap.Height];

            //Требуется для использования указателей * (необходимо включить в свойствах сборки "Небезопасный код")
            unsafe
            {
                //Фиксирует каждый пиксель прямоугольной области, равной размеру изображения, на режим чтения и записи соответствующего формата пикселей (Format24bppRgb или Format32bppArgb), в памяти
                BitmapData bitmapData = bmap.LockBits(new Rectangle(0, 0, bmap.Width, bmap.Height), ImageLockMode.ReadWrite, bmap.PixelFormat);

                //Определяем формат пикселей
                var pixelFormat = bmap.PixelFormat;

                //Определяем сколько байт тратиться на один пиксель
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bmap.PixelFormat) / 8;

                //Определяем высоту изображения в пикселях
                int heightInPixels = bitmapData.Height;

                //Определяем ширину изображения в байтах
                int widthInBytes = bitmapData.Width * bytesPerPixel;

                //Создаём указатель указывающий на первый пиксель изображения (на первый байт первого пикселя, в фиксированной области данных)
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                
                //Выполняем цикл аналогичный for (var y = 0; y < heightInPixels; y++) c автоматическим распаралеливанием каждой итерации
                Parallel.For(0, heightInPixels, y =>
                {
                    progressValue++;
                    //Создаём указатель указывающий на первый байт y-ой строки (.Stride - возвращает ширину строки в байтах)
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);

                    /*Выполняем цикл шагающий по каждому пикселю (каждому байту каждого пикселя) y-ой строки
                    При использовании указателя типа byte порядок сохранения цветов будет BGR для Format24bppRgb и BGRA для Format32bppArgb соответственно.
                    При касте указателя к int порядок байт будет соответствовать RGB и ARGB соответственно
                    (https://stackoverflow.com/questions/8104461/pixelformat-format32bppargb-seems-to-have-wrong-byte-order).
                    Для доступа к каждому цвету, при byte указателе, используется следующий код: currentLine[x] - Blue; currentLine[x + 1] - Green; и т.д.*/
                    if (pixelFormat == PixelFormat.Format24bppRgb || pixelFormat == PixelFormat.Format32bppRgb)
                        for (var x = 0; x < widthInBytes; x = x + bytesPerPixel)
                        {
                            var xIndex = x / bytesPerPixel;
                            result[xIndex, y] = new ImagePixel();

                            result[xIndex, y].B = currentLine[x];
                            result[xIndex, y].G = currentLine[x + 1];
                            result[xIndex, y].R = currentLine[x + 2];
                            result[xIndex, y].A = 255;
                        }
                    else if (pixelFormat == PixelFormat.Format32bppArgb)
                        for (var x = 0; x < widthInBytes; x = x + bytesPerPixel)
                        {
                            var xIndex = x / bytesPerPixel;
                            result[xIndex, y] = new ImagePixel();

                            result[xIndex, y].B = currentLine[x];
                            result[xIndex, y].G = currentLine[x + 1];
                            result[xIndex, y].R = currentLine[x + 2];
                            result[xIndex, y].A = currentLine[x + 3];
                        }
                    else
                        MessageBox.Show("Error:\tIncorrect pixel format.\tOnly the following formats are available:\tRGB 24 bit per pixel;\tRGB 32 bit per pixel;\t ARGB 32 bit per pixel.");
                });

                //Разблокирует указанную область памяти Bitmap
                bmap.UnlockBits(bitmapData);
            }
            return result;
        }

        public void UIOff()
        {
            Invoke((MethodInvoker)delegate ()
            {
                Start_B.Enabled = false;
                Start_B.Visible = false;
                Stop_B.Enabled = true;
                Stop_B.Visible = true;
                Browse1_B.Enabled = false;
                Browse2_B.Enabled = false;
                Path1_TB.Enabled = false;
                Path2_TB.Enabled = false;
                progressBar1.Value = 0;
            });
        }

        public void UIOn()
        {
            Invoke((MethodInvoker)delegate ()
            {
                Start_B.Enabled = true;
                Start_B.Visible = true;
                Stop_B.Enabled = false;
                Stop_B.Visible = false;
                Browse1_B.Enabled = true;
                Browse2_B.Enabled = true;
                Path1_TB.Enabled = true;
                Path2_TB.Enabled = true;
                progressBar1.Value = 0;
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UIOn();
            Result_DGV.Rows.Add("  R");
            Result_DGV.Rows.Add("  G");
            Result_DGV.Rows.Add("  B");
            Result_DGV.Rows.Add("  A");
            Result_DGV.Rows.Add("  Σ");
            Result_DGV.Rows.Add("<Σ>");
        }

        private void Browse1_B_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "images| *.jpg; *.png; *.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                    Path1_TB.Text = ofd.FileName;
            }
        }

        private void Browse2_B_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "images| *.jpg; *.png; *.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                    Path2_TB.Text = ofd.FileName;
            }
        }

        private void CalculationProcedure()
        {
            var i1 = Image.FromFile(Path1_TB.Text);
            var i2 = Image.FromFile(Path2_TB.Text);
            var inputImage1 = new Bitmap(i1);
            var inputImage2 = new Bitmap(i2);

            int imageWidth = inputImage1.Width;
            int imageHeight = inputImage1.Height;
            int imagePixelsNumber = imageWidth * imageHeight;

            if (inputImage1.Height == inputImage2.Height && inputImage1.Width == inputImage2.Width)
            {
                progressValue = 0;
                progressMaxValue = imageHeight * 3;

                Invoke((MethodInvoker)delegate ()
                {
                    progressBar1.Maximum = progressMaxValue;
                });

                var image1 = GetImage(inputImage1);

                progressValue = inputImage1.Height;

                var image2 = GetImage(inputImage2);

                progressValue = inputImage2.Height * 2;

                var red = new Сhannel() { Name = "Red"};
                var green = new Сhannel() { Name = "Green" };
                var blue = new Сhannel() { Name = "Blue" };
                var alpha = new Сhannel() { Name = "Alpha" };
                var sum = new Сhannel() { Name = "Sum" };
                var average = new Сhannel() { Name = "Average" };

                for (var y = 0; y < imageHeight; y++)
                {
                    progressValue++;
                    for (var x = 0; x < imageWidth; x++)
                    {
                        var pixel = image1[x, y] - image2[x, y];
                        float sumDelta = pixel.GetSum();
                        float averageDelta = pixel.GetAverage();

                        if (pixel.R != 0) red.Number++;
                        if (pixel.G != 0) green.Number++;
                        if (pixel.B != 0) blue.Number++;
                        if (pixel.A != 0) alpha.Number++;
                        if (pixel.IsDifferent()) sum.Number++;

                        red.Sum += pixel.R;
                        green.Sum += pixel.G;
                        blue.Sum += pixel.B;
                        alpha.Sum += pixel.A;
                        sum.Sum += sumDelta;
                        average.Sum += averageDelta;

                        var pixelSquares = pixel * pixel;

                        red.SumSquares += pixelSquares.R;
                        green.SumSquares += pixelSquares.G;
                        blue.SumSquares += pixelSquares.B;
                        alpha.SumSquares += pixelSquares.A;
                        sum.SumSquares += sumDelta * sumDelta;
                        average.SumSquares += averageDelta * averageDelta;
                    }
                }
                average.Number = sum.Number / 4;

                var channelList = new List<Сhannel>();

                channelList.Add(red);
                channelList.Add(green);
                channelList.Add(blue);
                channelList.Add(alpha);
                channelList.Add(sum);
                channelList.Add(average);

                foreach (var unit in channelList)
                    unit.CalculateParameters(imagePixelsNumber, 4);

                try { progressThread.Abort(); }
                catch { }

                Invoke((MethodInvoker)delegate ()
                {
                    Result_DGV.Rows.Clear();
                    progressBar1.Value = 0;
                    foreach (var unit in channelList)
                        Result_DGV.Rows.Add(unit.Name, unit.Number, unit.Percente, unit.StandardDeviation);

                    inputImage1.Dispose();
                    inputImage2.Dispose();
                    i1.Dispose();
                    i2.Dispose();

                    channelList = null;
                    inputImage1 = null;
                    inputImage2 = null;
                    image1 = null;
                    image2 = null;
                    red = null;
                    green = null;
                    blue = null;
                    alpha = null;
                    sum = null;
                    average = null;

                    imageWidth = 0;
                    imageHeight = 0;
                    imagePixelsNumber = 0;
                    progressValue = 0;
                    progressMaxValue = 0;

                    GC.Collect();

                    UIOn();
                });

                try { calculationThread.Abort(); }
                catch { }
            }
            else
                MessageBox.Show("Error:\tImages differ in height and/or width.\tFor comparison, images of equal dimensions are required.");
        }

        private void ProgressProcedure()
        {
            while (true)
            {
                Thread.Sleep(50);
                Invoke((MethodInvoker)delegate ()
                {
                    progressBar1.Value = progressValue;
                });
            }
        }

        private void Start_B_Click(object sender, EventArgs e)
        {
            UIOff();

            calculationThread = new Thread(new ThreadStart(CalculationProcedure));
            progressThread = new Thread(new ThreadStart(ProgressProcedure));

            calculationThread.Start();
            progressThread.Start();
        }

        private void Stop_B_Click(object sender, EventArgs e)
        {
            try
            {
                calculationThread.Abort();
                progressThread.Abort();
            }
            catch { }

            UIOn();
        }
    }
}
