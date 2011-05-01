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
        private frmFakeBox Box;
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
            this.Box = new frmFakeBox();
            Box.Show();
        }

        private void frmCaptureImage_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.Left)
                this.Close();

            this.StartPoint = e.Location;
            frmCaptureImage_MouseMove(sender, e);
            Box.Opacity = 0.1;
        }

        private void frmCaptureImage_MouseUp(object sender, MouseEventArgs e)
        {
            Point Start = new Point();
            Point End = new Point();

            Start.X = StartPoint.X <= e.Location.X ? StartPoint.X : e.Location.X;
            Start.Y = StartPoint.Y <= e.Location.Y ? StartPoint.Y : e.Location.Y;

            End.X = StartPoint.X >= e.Location.X ? StartPoint.X : e.Location.X;
            End.Y = StartPoint.Y >= e.Location.Y ? StartPoint.Y : e.Location.Y;

            Size sSize = new Size(End.X - Start.X, End.Y - Start.Y);

            try
            {

                this.Result = new Bitmap(sSize.Width, sSize.Height);
                Graphics gfx = Graphics.FromImage(this.Result);

                gfx.CopyFromScreen(Start, new Point(0, 0), sSize);

                this.DialogResult = DialogResult.OK;
            }
            catch { }
            this.Close();
        }

        private void frmCaptureImage_MouseMove(object sender, MouseEventArgs e)
        {
            Point Start = new Point();
            Point End = new Point();

            Start.X = StartPoint.X <= e.Location.X ? StartPoint.X : e.Location.X;
            Start.Y = StartPoint.Y <= e.Location.Y ? StartPoint.Y : e.Location.Y;

            End.X = StartPoint.X >= e.Location.X ? StartPoint.X : e.Location.X;
            End.Y = StartPoint.Y >= e.Location.Y ? StartPoint.Y : e.Location.Y;

            Size sSize = new Size(End.X - Start.X, End.Y - Start.Y);

            Box.Location = Start;
            Box.Size = sSize;
        }

        private void frmCaptureImage_Paint(object sender, PaintEventArgs e)
        {       }

        private void frmCaptureImage_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Box.Close();
        }
    }
}
