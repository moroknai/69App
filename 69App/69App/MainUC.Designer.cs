
namespace _69App
{
    partial class MainUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxLo = new System.Windows.Forms.PictureBox();
            this.pictureBoxUp = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUp)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLo
            // 
            this.pictureBoxLo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBoxLo.Location = new System.Drawing.Point(0, 458);
            this.pictureBoxLo.Name = "pictureBoxLo";
            this.pictureBoxLo.Size = new System.Drawing.Size(612, 450);
            this.pictureBoxLo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLo.TabIndex = 6;
            this.pictureBoxLo.TabStop = false;
            // 
            // pictureBoxUp
            // 
            this.pictureBoxUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxUp.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxUp.Name = "pictureBoxUp";
            this.pictureBoxUp.Size = new System.Drawing.Size(612, 450);
            this.pictureBoxUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxUp.TabIndex = 5;
            this.pictureBoxUp.TabStop = false;
            // 
            // MainUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxLo);
            this.Controls.Add(this.pictureBoxUp);
            this.Name = "MainUC";
            this.Size = new System.Drawing.Size(612, 908);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLo;
        private System.Windows.Forms.PictureBox pictureBoxUp;
    }
}
