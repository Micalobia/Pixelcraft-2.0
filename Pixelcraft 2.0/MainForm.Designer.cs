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
            _work.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pcb_Original = new System.Windows.Forms.PictureBox();
            this.btn_LoadImage = new System.Windows.Forms.Button();
            this.btn_LoadTexture = new System.Windows.Forms.Button();
            this.btn_Preview = new System.Windows.Forms.Button();
            this.ofd_LoadImage = new System.Windows.Forms.OpenFileDialog();
            this.pnl_menuBar = new System.Windows.Forms.Panel();
            this.btn_Maximize = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.ofd_LoadTexture = new System.Windows.Forms.OpenFileDialog();
            this.btn_ConvertImage = new System.Windows.Forms.Button();
            this.pbx_Convert = new Pixelcraft_2.ProgressBarEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nud_input = new System.Windows.Forms.NumericUpDown();
            this.lbl_input = new System.Windows.Forms.Label();
            this.rad_maxRes = new System.Windows.Forms.RadioButton();
            this.rad_useWidth = new System.Windows.Forms.RadioButton();
            this.rad_useHeight = new System.Windows.Forms.RadioButton();
            this.tot_expand = new System.Windows.Forms.ToolTip(this.components);
            this.btn_cancel = new System.Windows.Forms.Button();
            this.pnl_checks = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Original)).BeginInit();
            this.pnl_menuBar.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_input)).BeginInit();
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
            this.btn_LoadTexture.Click += new System.EventHandler(this.Btn_LoadTexture_Click);
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
            // ofd_LoadImage
            // 
            this.ofd_LoadImage.Filter = "Image Files|*.bmp;*.jpg;*.png;*.gif|All files|*.*";
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
            this.pnl_menuBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pnl_menuBar_MouseDown);
            this.pnl_menuBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pnl_menuBar_MouseMove);
            this.pnl_menuBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Pnl_menuBar_MouseUp);
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
            this.btn_Maximize.Click += new System.EventHandler(this.Btn_Maximize_Click);
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
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // ofd_LoadTexture
            // 
            this.ofd_LoadTexture.Filter = "Zip Files|*.zip|All files|*.*";
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
            // pbx_Convert
            // 
            this.pbx_Convert.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbx_Convert.Location = new System.Drawing.Point(512, 176);
            this.pbx_Convert.Maximum = 1000;
            this.pbx_Convert.Name = "pbx_Convert";
            this.pbx_Convert.Size = new System.Drawing.Size(280, 23);
            this.pbx_Convert.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbx_Convert.TabIndex = 12;
            this.pbx_Convert.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nud_input);
            this.panel1.Controls.Add(this.lbl_input);
            this.panel1.Controls.Add(this.rad_maxRes);
            this.panel1.Controls.Add(this.rad_useWidth);
            this.panel1.Controls.Add(this.rad_useHeight);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(512, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 88);
            this.panel1.TabIndex = 13;
            // 
            // nud_input
            // 
            this.nud_input.Dock = System.Windows.Forms.DockStyle.Top;
            this.nud_input.Location = new System.Drawing.Point(0, 64);
            this.nud_input.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nud_input.Name = "nud_input";
            this.nud_input.Size = new System.Drawing.Size(280, 20);
            this.nud_input.TabIndex = 4;
            // 
            // lbl_input
            // 
            this.lbl_input.AutoSize = true;
            this.lbl_input.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_input.Location = new System.Drawing.Point(0, 51);
            this.lbl_input.Name = "lbl_input";
            this.lbl_input.Size = new System.Drawing.Size(60, 13);
            this.lbl_input.TabIndex = 3;
            this.lbl_input.Text = "Use Height";
            // 
            // rad_maxRes
            // 
            this.rad_maxRes.AutoSize = true;
            this.rad_maxRes.Dock = System.Windows.Forms.DockStyle.Top;
            this.rad_maxRes.Location = new System.Drawing.Point(0, 34);
            this.rad_maxRes.Name = "rad_maxRes";
            this.rad_maxRes.Size = new System.Drawing.Size(280, 17);
            this.rad_maxRes.TabIndex = 2;
            this.rad_maxRes.Text = "Max Resolution";
            this.rad_maxRes.UseVisualStyleBackColor = true;
            // 
            // rad_useWidth
            // 
            this.rad_useWidth.AutoSize = true;
            this.rad_useWidth.Dock = System.Windows.Forms.DockStyle.Top;
            this.rad_useWidth.Location = new System.Drawing.Point(0, 17);
            this.rad_useWidth.Name = "rad_useWidth";
            this.rad_useWidth.Size = new System.Drawing.Size(280, 17);
            this.rad_useWidth.TabIndex = 1;
            this.rad_useWidth.Text = "Use Width";
            this.rad_useWidth.UseVisualStyleBackColor = true;
            // 
            // rad_useHeight
            // 
            this.rad_useHeight.AutoSize = true;
            this.rad_useHeight.Checked = true;
            this.rad_useHeight.Dock = System.Windows.Forms.DockStyle.Top;
            this.rad_useHeight.Location = new System.Drawing.Point(0, 0);
            this.rad_useHeight.Name = "rad_useHeight";
            this.rad_useHeight.Size = new System.Drawing.Size(280, 17);
            this.rad_useHeight.TabIndex = 0;
            this.rad_useHeight.TabStop = true;
            this.rad_useHeight.Text = "Use Height";
            this.rad_useHeight.UseVisualStyleBackColor = true;
            // 
            // tot_expand
            // 
            this.tot_expand.ToolTipTitle = "Image Expansion";
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_cancel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_cancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Location = new System.Drawing.Point(512, 153);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(280, 23);
            this.btn_cancel.TabIndex = 15;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Visible = false;
            this.btn_cancel.Click += new System.EventHandler(this.Btn_cancel_Click);
            // 
            // pnl_checks
            // 
            this.pnl_checks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_checks.Location = new System.Drawing.Point(512, 199);
            this.pnl_checks.Name = "pnl_checks";
            this.pnl_checks.Size = new System.Drawing.Size(280, 286);
            this.pnl_checks.TabIndex = 16;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(792, 531);
            this.Controls.Add(this.pnl_checks);
            this.Controls.Add(this.pbx_Convert);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Preview);
            this.Controls.Add(this.btn_LoadTexture);
            this.Controls.Add(this.btn_ConvertImage);
            this.Controls.Add(this.btn_LoadImage);
            this.Controls.Add(this.pcb_Original);
            this.Controls.Add(this.pnl_menuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Original)).EndInit();
            this.pnl_menuBar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_input)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcb_Original;
        private System.Windows.Forms.Button btn_LoadImage;
        private System.Windows.Forms.Button btn_LoadTexture;
        private System.Windows.Forms.Button btn_Preview;
        private System.Windows.Forms.OpenFileDialog ofd_LoadImage;
        private System.Windows.Forms.Panel pnl_menuBar;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Maximize;
        private System.Windows.Forms.OpenFileDialog ofd_LoadTexture;
        private System.Windows.Forms.Button btn_ConvertImage;
        private ProgressBarEx pbx_Convert;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nud_input;
        private System.Windows.Forms.Label lbl_input;
        private System.Windows.Forms.RadioButton rad_maxRes;
        private System.Windows.Forms.RadioButton rad_useWidth;
        private System.Windows.Forms.RadioButton rad_useHeight;
        private System.Windows.Forms.ToolTip tot_expand;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Panel pnl_checks;
    }
}

