namespace EasyErpTool
{
    partial class EasyErpTool
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SalesOutput = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.InputProducts = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker.Location = new System.Drawing.Point(6, 28);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(159, 20);
            this.dateTimePicker.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SalesOutput);
            this.groupBox1.Controls.Add(this.dateTimePicker);
            this.groupBox1.Location = new System.Drawing.Point(17, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 115);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "销售记录";
            // 
            // SalesOutput
            // 
            this.SalesOutput.Location = new System.Drawing.Point(6, 70);
            this.SalesOutput.Name = "SalesOutput";
            this.SalesOutput.Size = new System.Drawing.Size(75, 23);
            this.SalesOutput.TabIndex = 0;
            this.SalesOutput.Text = "导出月销售记录";
            this.SalesOutput.UseVisualStyleBackColor = true;
            this.SalesOutput.Click += new System.EventHandler(this.SalesOutput_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.InputProducts);
            this.groupBox4.Location = new System.Drawing.Point(17, 133);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(373, 117);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "商品导入";
            // 
            // InputProducts
            // 
            this.InputProducts.Location = new System.Drawing.Point(6, 32);
            this.InputProducts.Name = "InputProducts";
            this.InputProducts.Size = new System.Drawing.Size(75, 23);
            this.InputProducts.TabIndex = 1;
            this.InputProducts.Text = "导入商品目录";
            this.InputProducts.UseVisualStyleBackColor = true;
            this.InputProducts.Click += new System.EventHandler(this.InputProducts_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // EasyErpTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 262);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Name = "EasyErpTool";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button SalesOutput;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button InputProducts;
        private System.Windows.Forms.OpenFileDialog openFileDialog;

    }
}

