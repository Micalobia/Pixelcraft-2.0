namespace Pixelcraft_2._0
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
            Hide();
            /*
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);*/
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_SaveImage = new System.Windows.Forms.Button();
            this.btn_SaveSchematic = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // btn_SaveImage
            // 
            this.btn_SaveImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_SaveImage.Location = new System.Drawing.Point(512, 0);
            this.btn_SaveImage.Name = "btn_SaveImage";
            this.btn_SaveImage.Size = new System.Drawing.Size(98, 23);
            this.btn_SaveImage.TabIndex = 1;
            this.btn_SaveImage.Text = "Save Image";
            this.btn_SaveImage.UseVisualStyleBackColor = true;
            this.btn_SaveImage.Click += new System.EventHandler(this.btn_SaveImage_Click);
            // 
            // btn_SaveSchematic
            // 
            this.btn_SaveSchematic.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_SaveSchematic.Location = new System.Drawing.Point(512, 23);
            this.btn_SaveSchematic.Name = "btn_SaveSchematic";
            this.btn_SaveSchematic.Size = new System.Drawing.Size(98, 23);
            this.btn_SaveSchematic.TabIndex = 2;
            this.btn_SaveSchematic.Text = "Save Schematic";
            this.btn_SaveSchematic.UseVisualStyleBackColor = true;
            this.btn_SaveSchematic.Click += new System.EventHandler(this.btn_SaveSchematic_Click);
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 512);
            this.Controls.Add(this.btn_SaveSchematic);
            this.Controls.Add(this.btn_SaveImage);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PreviewForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_SaveImage;
        private System.Windows.Forms.Button btn_SaveSchematic;
    }
}

