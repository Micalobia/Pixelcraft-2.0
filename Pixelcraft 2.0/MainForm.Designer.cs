namespace Pixelcraft_2._0
{
    partial class MainForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_LoadImage = new System.Windows.Forms.Button();
            this.btn_ConvertImage = new System.Windows.Forms.Button();
            this.btn_LoadTexture = new System.Windows.Forms.Button();
            this.btn_Preview = new System.Windows.Forms.Button();
            this.nud_Width = new System.Windows.Forms.NumericUpDown();
            this.lbl_Width = new System.Windows.Forms.Label();
            this.chk_Width = new System.Windows.Forms.CheckBox();
            this.chk_Height = new System.Windows.Forms.CheckBox();
            this.nud_Height = new System.Windows.Forms.NumericUpDown();
            this.lbl_Height = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Height)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btn_LoadImage
            // 
            this.btn_LoadImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_LoadImage.Location = new System.Drawing.Point(512, 0);
            this.btn_LoadImage.Name = "btn_LoadImage";
            this.btn_LoadImage.Size = new System.Drawing.Size(280, 23);
            this.btn_LoadImage.TabIndex = 1;
            this.btn_LoadImage.Text = "Load Image";
            this.btn_LoadImage.UseVisualStyleBackColor = true;
            this.btn_LoadImage.Click += new System.EventHandler(this.btn_LoadImage_Click);
            // 
            // btn_ConvertImage
            // 
            this.btn_ConvertImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_ConvertImage.Location = new System.Drawing.Point(512, 23);
            this.btn_ConvertImage.Name = "btn_ConvertImage";
            this.btn_ConvertImage.Size = new System.Drawing.Size(280, 23);
            this.btn_ConvertImage.TabIndex = 2;
            this.btn_ConvertImage.Text = "Convert Image";
            this.btn_ConvertImage.UseVisualStyleBackColor = true;
            this.btn_ConvertImage.Click += new System.EventHandler(this.btn_ConvertImage_Click);
            // 
            // btn_LoadTexture
            // 
            this.btn_LoadTexture.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_LoadTexture.Location = new System.Drawing.Point(512, 489);
            this.btn_LoadTexture.Name = "btn_LoadTexture";
            this.btn_LoadTexture.Size = new System.Drawing.Size(280, 23);
            this.btn_LoadTexture.TabIndex = 3;
            this.btn_LoadTexture.Text = "Load Texture";
            this.btn_LoadTexture.UseVisualStyleBackColor = true;
            this.btn_LoadTexture.Click += new System.EventHandler(this.btn_LoadTexture_Click);
            // 
            // btn_Preview
            // 
            this.btn_Preview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_Preview.Location = new System.Drawing.Point(512, 466);
            this.btn_Preview.Name = "btn_Preview";
            this.btn_Preview.Size = new System.Drawing.Size(280, 23);
            this.btn_Preview.TabIndex = 4;
            this.btn_Preview.Text = "Preview";
            this.btn_Preview.UseVisualStyleBackColor = true;
            this.btn_Preview.Click += new System.EventHandler(this.btn_Preview_Click);
            // 
            // nud_Width
            // 
            this.nud_Width.Dock = System.Windows.Forms.DockStyle.Top;
            this.nud_Width.Location = new System.Drawing.Point(512, 61);
            this.nud_Width.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_Width.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_Width.Name = "nud_Width";
            this.nud_Width.Size = new System.Drawing.Size(280, 20);
            this.nud_Width.TabIndex = 5;
            this.nud_Width.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lbl_Width
            // 
            this.lbl_Width.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Width.Location = new System.Drawing.Point(512, 46);
            this.lbl_Width.Name = "lbl_Width";
            this.lbl_Width.Size = new System.Drawing.Size(280, 15);
            this.lbl_Width.TabIndex = 6;
            this.lbl_Width.Text = "Width (Blocks)";
            this.lbl_Width.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chk_Width
            // 
            this.chk_Width.AutoSize = true;
            this.chk_Width.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Width.Dock = System.Windows.Forms.DockStyle.Top;
            this.chk_Width.Location = new System.Drawing.Point(512, 81);
            this.chk_Width.Name = "chk_Width";
            this.chk_Width.Size = new System.Drawing.Size(280, 17);
            this.chk_Width.TabIndex = 7;
            this.chk_Width.Text = "Use Width";
            this.chk_Width.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chk_Width.UseVisualStyleBackColor = true;
            this.chk_Width.CheckedChanged += new System.EventHandler(this.chk_Width_CheckedChanged);
            // 
            // chk_Height
            // 
            this.chk_Height.AutoSize = true;
            this.chk_Height.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Height.Dock = System.Windows.Forms.DockStyle.Top;
            this.chk_Height.Location = new System.Drawing.Point(512, 133);
            this.chk_Height.Name = "chk_Height";
            this.chk_Height.Size = new System.Drawing.Size(280, 17);
            this.chk_Height.TabIndex = 10;
            this.chk_Height.Text = "Use Height";
            this.chk_Height.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chk_Height.UseVisualStyleBackColor = true;
            this.chk_Height.CheckedChanged += new System.EventHandler(this.chk_Height_CheckedChanged);
            // 
            // nud_Height
            // 
            this.nud_Height.Dock = System.Windows.Forms.DockStyle.Top;
            this.nud_Height.Location = new System.Drawing.Point(512, 113);
            this.nud_Height.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_Height.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_Height.Name = "nud_Height";
            this.nud_Height.Size = new System.Drawing.Size(280, 20);
            this.nud_Height.TabIndex = 8;
            this.nud_Height.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lbl_Height
            // 
            this.lbl_Height.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Height.Location = new System.Drawing.Point(512, 98);
            this.lbl_Height.Name = "lbl_Height";
            this.lbl_Height.Size = new System.Drawing.Size(280, 15);
            this.lbl_Height.TabIndex = 9;
            this.lbl_Height.Text = "Height (Blocks)";
            this.lbl_Height.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 512);
            this.Controls.Add(this.chk_Height);
            this.Controls.Add(this.nud_Height);
            this.Controls.Add(this.lbl_Height);
            this.Controls.Add(this.chk_Width);
            this.Controls.Add(this.nud_Width);
            this.Controls.Add(this.btn_Preview);
            this.Controls.Add(this.btn_LoadTexture);
            this.Controls.Add(this.lbl_Width);
            this.Controls.Add(this.btn_ConvertImage);
            this.Controls.Add(this.btn_LoadImage);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_LoadImage;
        private System.Windows.Forms.Button btn_ConvertImage;
        private System.Windows.Forms.Button btn_LoadTexture;
        private System.Windows.Forms.Button btn_Preview;
        private System.Windows.Forms.NumericUpDown nud_Width;
        private System.Windows.Forms.Label lbl_Width;
        private System.Windows.Forms.CheckBox chk_Width;
        private System.Windows.Forms.CheckBox chk_Height;
        private System.Windows.Forms.NumericUpDown nud_Height;
        private System.Windows.Forms.Label lbl_Height;
    }
}

