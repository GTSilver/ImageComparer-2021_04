namespace Image_comparison
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Path1_TB = new System.Windows.Forms.TextBox();
            this.Path2_TB = new System.Windows.Forms.TextBox();
            this.Browse1_B = new System.Windows.Forms.Button();
            this.Browse2_B = new System.Windows.Forms.Button();
            this.Result_DGV = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Start_B = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Stop_B = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Result_DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // Path1_TB
            // 
            this.Path1_TB.Location = new System.Drawing.Point(12, 41);
            this.Path1_TB.Name = "Path1_TB";
            this.Path1_TB.Size = new System.Drawing.Size(156, 20);
            this.Path1_TB.TabIndex = 0;
            // 
            // Path2_TB
            // 
            this.Path2_TB.Location = new System.Drawing.Point(12, 67);
            this.Path2_TB.Name = "Path2_TB";
            this.Path2_TB.Size = new System.Drawing.Size(156, 20);
            this.Path2_TB.TabIndex = 1;
            // 
            // Browse1_B
            // 
            this.Browse1_B.Location = new System.Drawing.Point(174, 39);
            this.Browse1_B.Name = "Browse1_B";
            this.Browse1_B.Size = new System.Drawing.Size(81, 23);
            this.Browse1_B.TabIndex = 2;
            this.Browse1_B.Text = "Open";
            this.Browse1_B.UseVisualStyleBackColor = true;
            this.Browse1_B.Click += new System.EventHandler(this.Browse1_B_Click);
            // 
            // Browse2_B
            // 
            this.Browse2_B.Location = new System.Drawing.Point(174, 65);
            this.Browse2_B.Name = "Browse2_B";
            this.Browse2_B.Size = new System.Drawing.Size(81, 23);
            this.Browse2_B.TabIndex = 3;
            this.Browse2_B.Text = "Open";
            this.Browse2_B.UseVisualStyleBackColor = true;
            this.Browse2_B.Click += new System.EventHandler(this.Browse2_B_Click);
            // 
            // Result_DGV
            // 
            this.Result_DGV.AllowUserToAddRows = false;
            this.Result_DGV.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Result_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Result_DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.Result_DGV.Location = new System.Drawing.Point(12, 107);
            this.Result_DGV.Name = "Result_DGV";
            this.Result_DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Result_DGV.RowHeadersVisible = false;
            this.Result_DGV.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Result_DGV.ShowCellErrors = false;
            this.Result_DGV.ShowCellToolTips = false;
            this.Result_DGV.ShowEditingIcon = false;
            this.Result_DGV.ShowRowErrors = false;
            this.Result_DGV.Size = new System.Drawing.Size(243, 154);
            this.Result_DGV.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Collor";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 48;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Number";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 64;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Percent, %";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 64;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "SD";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 64;
            // 
            // Start_B
            // 
            this.Start_B.Location = new System.Drawing.Point(174, 12);
            this.Start_B.Name = "Start_B";
            this.Start_B.Size = new System.Drawing.Size(81, 23);
            this.Start_B.TabIndex = 5;
            this.Start_B.Text = "Start";
            this.Start_B.UseVisualStyleBackColor = true;
            this.Start_B.Click += new System.EventHandler(this.Start_B_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Paths to Images for Comparison";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 93);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(243, 8);
            this.progressBar1.TabIndex = 7;
            // 
            // Stop_B
            // 
            this.Stop_B.Location = new System.Drawing.Point(174, 12);
            this.Stop_B.Name = "Stop_B";
            this.Stop_B.Size = new System.Drawing.Size(81, 23);
            this.Stop_B.TabIndex = 8;
            this.Stop_B.Text = "Stop";
            this.Stop_B.UseVisualStyleBackColor = true;
            this.Stop_B.Click += new System.EventHandler(this.Stop_B_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 273);
            this.Controls.Add(this.Stop_B);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Start_B);
            this.Controls.Add(this.Result_DGV);
            this.Controls.Add(this.Browse2_B);
            this.Controls.Add(this.Browse1_B);
            this.Controls.Add(this.Path2_TB);
            this.Controls.Add(this.Path1_TB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Image Comparer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Result_DGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Path1_TB;
        private System.Windows.Forms.TextBox Path2_TB;
        private System.Windows.Forms.Button Browse1_B;
        private System.Windows.Forms.Button Browse2_B;
        private System.Windows.Forms.DataGridView Result_DGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button Start_B;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button Stop_B;
    }
}

