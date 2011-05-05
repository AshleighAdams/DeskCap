namespace DeskCap
{
    partial class frmCaptureImage
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
            this.SuspendLayout();
            // 
            // frmCaptureImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCaptureImage";
            this.Opacity = 0.01;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Capturing Image";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmCaptureImage_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmCaptureImage_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmCaptureImage_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmCaptureImage_MouseDown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCaptureImage_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCaptureImage_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}