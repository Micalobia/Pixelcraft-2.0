namespace Pixelcraft_2
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
            this.pcb_Original = new System.Windows.Forms.PictureBox();
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
            this.ofd_LoadImage = new System.Windows.Forms.OpenFileDialog();
            this.pnl_menuBar = new System.Windows.Forms.Panel();
            this.btn_Maximize = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.pgs_Convert = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Height)).BeginInit();
            this.pnl_menuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcb_Original
            // 
            this.pcb_Original.Dock = System.Windows.Forms.DockStyle.Left;
            this.pcb_Original.Location = new System.Drawing.Point(0, 19);
            this.pcb_Original.Name = "pcb_Original";
            this.pcb_Original.Size = new System.Drawing.Size(512, 512);
            this.pcb_Original.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcb_Original.TabIndex = 0;
            this.pcb_Original.TabStop = false;
            // 
            // btn_LoadImage
            // 
            this.btn_LoadImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_LoadImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_LoadImage.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_LoadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LoadImage.Location = new System.Drawing.Point(512, 19);
            this.btn_LoadImage.Name = "btn_LoadImage";
            this.btn_LoadImage.Size = new System.Drawing.Size(280, 23);
            this.btn_LoadImage.TabIndex = 1;
            this.btn_LoadImage.Text = "Load Image";
            this.btn_LoadImage.UseVisualStyleBackColor = false;
            this.btn_LoadImage.Click += new System.EventHandler(this.Btn_LoadImage_Click);
            // 
            // btn_ConvertImage
            // 
            this.btn_ConvertImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_ConvertImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_ConvertImage.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_ConvertImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ConvertImage.Location = new System.Drawing.Point(512, 42);
            this.btn_ConvertImage.Name = "btn_ConvertImage";
            this.btn_ConvertImage.Size = new System.Drawing.Size(280, 23);
            this.btn_ConvertImage.TabIndex = 2;
            this.btn_ConvertImage.Text = "Convert Image";
            this.btn_ConvertImage.UseVisualStyleBackColor = false;
            this.btn_ConvertImage.Click += new System.EventHandler(this.Btn_ConvertImage_Click);
            // 
            // btn_LoadTexture
            // 
            this.btn_LoadTexture.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_LoadTexture.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_LoadTexture.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_LoadTexture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LoadTexture.Location = new System.Drawing.Point(512, 508);
            this.btn_LoadTexture.Name = "btn_LoadTexture";
            this.btn_LoadTexture.Size = new System.Drawing.Size(280, 23);
            this.btn_LoadTexture.TabIndex = 3;
            this.btn_LoadTexture.Text = "Load Texture";
            this.btn_LoadTexture.UseVisualStyleBackColor = false;
            this.btn_LoadTexture.Click += new System.EventHandler(this.btn_LoadTexture_Click);
            // 
            // btn_Preview
            // 
            this.btn_Preview.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_Preview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_Preview.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_Preview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Preview.Location = new System.Drawing.Point(512, 485);
            this.btn_Preview.Name = "btn_Preview";
            this.btn_Preview.Size = new System.Drawing.Size(280, 23);
            this.btn_Preview.TabIndex = 4;
            this.btn_Preview.Text = "Preview";
            this.btn_Preview.UseVisualStyleBackColor = false;
            this.btn_Preview.Click += new System.EventHandler(this.Btn_Preview_Click);
            // 
            // nud_Width
            // 
            this.nud_Width.Dock = System.Windows.Forms.DockStyle.Top;
            this.nud_Width.Location = new System.Drawing.Point(512, 80);
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
            this.lbl_Width.Location = new System.Drawing.Point(512, 65);
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
            this.chk_Width.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_Width.Location = new System.Drawing.Point(512, 100);
            this.chk_Width.Name = "chk_Width";
            this.chk_Width.Size = new System.Drawing.Size(280, 17);
            this.chk_Width.TabIndex = 7;
            this.chk_Width.Text = "Use Width";
            this.chk_Width.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chk_Width.UseVisualStyleBackColor = true;
            this.chk_Width.CheckedChanged += new System.EventHandler(this.Chk_Width_CheckedChanged);
            // 
            // chk_Height
            // 
            this.chk_Height.AutoSize = true;
            this.chk_Height.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Height.Dock = System.Windows.Forms.DockStyle.Top;
            this.chk_Height.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.chk_Height.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_Height.Location = new System.Drawing.Point(512, 152);
            this.chk_Height.Name = "chk_Height";
            this.chk_Height.Size = new System.Drawing.Size(280, 17);
            this.chk_Height.TabIndex = 10;
            this.chk_Height.Text = "Use Height";
            this.chk_Height.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chk_Height.UseVisualStyleBackColor = true;
            this.chk_Height.CheckedChanged += new System.EventHandler(this.Chk_Height_CheckedChanged);
            // 
            // nud_Height
            // 
            this.nud_Height.Dock = System.Windows.Forms.DockStyle.Top;
            this.nud_Height.Location = new System.Drawing.Point(512, 132);
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
            this.lbl_Height.Location = new System.Drawing.Point(512, 117);
            this.lbl_Height.Name = "lbl_Height";
            this.lbl_Height.Size = new System.Drawing.Size(280, 15);
            this.lbl_Height.TabIndex = 9;
            this.lbl_Height.Text = "Height (Blocks)";
            this.lbl_Height.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ofd_LoadImage
            // 
            this.ofd_LoadImage.Filter = "Image Files|*.bmp;*.jpg;*.png|All files|*.*";
            this.ofd_LoadImage.Title = "Load Image";
            // 
            // pnl_menuBar
            // 
            this.pnl_menuBar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnl_menuBar.Controls.Add(this.btn_Maximize);
            this.pnl_menuBar.Controls.Add(this.btn_Close);
            this.pnl_menuBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_menuBar.Location = new System.Drawing.Point(0, 0);
            this.pnl_menuBar.Name = "pnl_menuBar";
            this.pnl_menuBar.Size = new System.Drawing.Size(792, 19);
            this.pnl_menuBar.TabIndex = 11;
            this.pnl_menuBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_menuBar_MouseDown);
            this.pnl_menuBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_menuBar_MouseMove);
            this.pnl_menuBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnl_menuBar_MouseUp);
            // 
            // btn_Maximize
            // 
            this.btn_Maximize.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btn_Maximize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Maximize.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btn_Maximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Maximize.Location = new System.Drawing.Point(696, 0);
            this.btn_Maximize.Name = "btn_Maximize";
            this.btn_Maximize.Size = new System.Drawing.Size(48, 19);
            this.btn_Maximize.TabIndex = 1;
            this.btn_Maximize.UseVisualStyleBackColor = false;
            this.btn_Maximize.Click += new System.EventHandler(this.btn_Maximize_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btn_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Close.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Location = new System.Drawing.Point(744, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(48, 19);
            this.btn_Close.TabIndex = 0;
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // pgs_Convert
            // 
            this.pgs_Convert.Dock = System.Windows.Forms.DockStyle.Top;
            this.pgs_Convert.Location = new System.Drawing.Point(512, 169);
            this.pgs_Convert.Maximum = 1000;
            this.pgs_Convert.Name = "pgs_Convert";
            this.pgs_Convert.Size = new System.Drawing.Size(280, 23);
            this.pgs_Convert.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgs_Convert.TabIndex = 12;
            this.pgs_Convert.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(792, 531);
            this.Controls.Add(this.pgs_Convert);
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
            this.Controls.Add(this.pcb_Original);
            this.Controls.Add(this.pnl_menuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.GotFocus += new System.EventHandler(this.MainForm_GotFocus);
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Height)).EndInit();
            this.pnl_menuBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcb_Original;
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
        private System.Windows.Forms.OpenFileDialog ofd_LoadImage;
        private System.Windows.Forms.Panel pnl_menuBar;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Maximize;
        private System.Windows.Forms.ProgressBar pgs_Convert;
    }
}

