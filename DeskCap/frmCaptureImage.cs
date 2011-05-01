using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeskCap
{
    public partial class frmCaptureImage : Form
    {
        public Bitmap Result;
        private Point StartPoint;
        public frmCaptureImage()
        {
            InitializeComponent();
        }

        private void frmCaptureImage_Load(object sender, EventArgs e)
        {
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Left = 0;
            this.Top = 0;
        }

        private void frmCaptureImage_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.Left)
                this.Close();

            this.StartPoint = e.Location;
        }

        private void frmCaptureImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.StartPoint == null) return;

            Point Start = new Point();
            Point End = new Point();

            Start.X = StartPoint.X <= e.Location.X ? StartPoint.X : e.Location.X;
            Start.Y = StartPoint.Y <= e.Location.Y ? StartPoint.Y : e.Location.Y;

            End.X = StartPoint.X >= e.Location.X ? StartPoint.X : e.Location.X;
            End.Y = StartPoint.Y >= e.Location.Y ? StartPoint.Y : e.Location.Y;

            Size sSize = new Size(End.X - Start.X, End.Y - Start.Y);


            this.Result = new Bitmap(sSize.Width, sSize.Height);
            Graphics gfx = Graphics.FromImage(this.Result);

            gfx.CopyFromScreen(Start, new Point(0,0), sSize);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
