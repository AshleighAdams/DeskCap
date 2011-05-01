using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Specialized;

namespace DeskCap
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.0;
            Timer tmr = new Timer();
            tmr.Interval = 100;
            tmr.Tick += delegate
            {
                tmr.Enabled = false;
                this.Visible = false;
                this.Opacity = 1.0;
            };
            tmr.Enabled = true;
            tbAPIKey.Text = Properties.Settings.Default.APIKey;
            cbUseDirectLink.Checked = Properties.Settings.Default.DirectLink;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.APIKey = tbAPIKey.Text;
            Properties.Settings.Default.DirectLink = cbUseDirectLink.Checked;
            Properties.Settings.Default.Save();
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            btnApply_Click(null, null);
            this.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Filter = "Supported Images (*.jpg;*.jpeg;*.png;*.gif;*.apng;*.tiff;*.tif;*.bmp;*.pdf;*.xcf;*.JPG;)|*.jpg;*.jpeg;*.png;*.gif;*.apng;*.tiff;*.tif;*.bmp;*.pdf;*.xcf;*.JPG;";
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                    UploadFile(File.ReadAllBytes(ofd.FileName));
            }
            catch
            {
                MessageBox.Show("Error uploading", "Error");
            }
        }

        string UploadFile(Bitmap Img)
        {
            MemoryStream s = new MemoryStream();
            Img.Save(s, System.Drawing.Imaging.ImageFormat.Png);
            byte[] bytes = s.ToArray();
            return UploadFile(bytes);
        }

        string UploadFile(byte[] Img)
        {

            WebClient w = new WebClient();

            var values = new NameValueCollection
            {
                { "key", "433a1bf4743dd8d7845629b95b5ca1b4" },
                { "image", Convert.ToBase64String(Img) }
            };

            byte[] response = w.UploadValues("http://imgur.com/api/upload.xml", values);

            XDocument x = XDocument.Parse(System.Text.Encoding.ASCII.GetString(response));

            string url = "";
            foreach (XElement element in x.Descendants("imgur_page"))
                url = element.Value;
            Clipboard.SetDataObject(url, true);
            this.TrayIcon.BalloonTipTitle = "Image uploaded";
            this.TrayIcon.ShowBalloonTip(2, "Image Uploaded!", "Your image has been uploaded (" + url + ")", ToolTipIcon.Info);
            return url;
        }
    }
}
