namespace Pixelcraft_2
{
    partial class PreviewForm
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
            this.pcb_Preview = new System.Windows.Forms.PictureBox();
            this.btn_SaveImage = new System.Windows.Forms.Button();
            this.btn_SaveSchematic = new System.Windows.Forms.Button();
            this.pnl_menuBar = new System.Windows.Forms.Panel();
            this.btn_Maximize = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.sfg_SaveImage = new System.Windows.Forms.SaveFileDialog();
            this.sfg_SaveSchem = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Preview)).BeginInit();
            this.pnl_menuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcb_Preview
            // 
            this.pcb_Preview.Dock = System.Windows.Forms.DockStyle.Left;
            this.pcb_Preview.Location = new System.Drawing.Point(0, 19);
            this.pcb_Preview.Name = "pcb_Preview";
            this.pcb_Preview.Size = new System.Drawing.Size(512, 512);
            this.pcb_Preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcb_Preview.TabIndex = 0;
            this.pcb_Preview.TabStop = false;
            // 
            // btn_SaveImage
            // 
            this.btn_SaveImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_SaveImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SaveImage.Location = new System.Drawing.Point(512, 19);
            this.btn_SaveImage.Name = "btn_SaveImage";
            this.btn_SaveImage.Size = new System.Drawing.Size(98, 23);
            this.btn_SaveImage.TabIndex = 1;
            this.btn_SaveImage.Text = "Save Image";
            this.btn_SaveImage.UseVisualStyleBackColor = true;
            this.btn_SaveImage.Click += new System.EventHandler(this.Btn_SaveImage_Click);
            // 
            // btn_SaveSchematic
            // 
            this.btn_SaveSchematic.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_SaveSchematic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SaveSchematic.Location = new System.Drawing.Point(512, 42);
            this.btn_SaveSchematic.Name = "btn_SaveSchematic";
            this.btn_SaveSchematic.Size = new System.Drawing.Size(98, 23);
            this.btn_SaveSchematic.TabIndex = 2;
            this.btn_SaveSchematic.Text = "Save Schematic";
            this.btn_SaveSchematic.UseVisualStyleBackColor = true;
            this.btn_SaveSchematic.Click += new System.EventHandler(this.btn_SaveSchematic_Click);
            // 
            // pnl_menuBar
            // 
            this.pnl_menuBar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnl_menuBar.Controls.Add(this.btn_Maximize);
            this.pnl_menuBar.Controls.Add(this.btn_Close);
            this.pnl_menuBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_menuBar.Location = new System.Drawing.Point(0, 0);
            this.pnl_menuBar.Name = "pnl_menuBar";
            this.pnl_menuBar.Size = new System.Drawing.Size(610, 19);
            this.pnl_menuBar.TabIndex = 12;
            this.pnl_menuBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pnl_menuBar_MouseDown);
            this.pnl_menuBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pnl_menuBar_MouseMoving);
            this.pnl_menuBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Pnl_menuBar_MouseUp);
            // 
            // btn_Maximize
            // 
            this.btn_Maximize.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btn_Maximize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Maximize.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btn_Maximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Maximize.Location = new System.Drawing.Point(514, 0);
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
            this.btn_Close.Location = new System.Drawing.Point(562, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(48, 19);
            this.btn_Close.TabIndex = 0;
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // sfg_SaveImage
            // 
            this.sfg_SaveImage.Filter = "Image Files|*.bmp;*.jpg;*.png|All files|*.*";
            // 
            // sfg_SaveSchem
            // 
            this.sfg_SaveSchem.FileName = "Schematic Files|*.schematic|All Files|*.*";
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(610, 531);
            this.Controls.Add(this.btn_SaveSchematic);
            this.Controls.Add(this.btn_SaveImage);
            this.Controls.Add(this.pcb_Preview);
            this.Controls.Add(this.pnl_menuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PreviewForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Preview)).EndInit();
            this.pnl_menuBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_SaveImage;
        private System.Windows.Forms.Button btn_SaveSchematic;
        public System.Windows.Forms.PictureBox pcb_Preview;
        private System.Windows.Forms.Panel pnl_menuBar;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Maximize;
        private System.Windows.Forms.SaveFileDialog sfg_SaveImage;
        private System.Windows.Forms.SaveFileDialog sfg_SaveSchem;
    }
}

