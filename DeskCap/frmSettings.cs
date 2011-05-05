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
using System.Diagnostics;

namespace DeskCap
{

    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }
        private bool Capturing = false;
        KeyboardHook hook;
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

            hook = new KeyboardHook();
            hook.RegisterHotKey(DeskCap.ModifierKeys.Shift, Keys.F11);
            hook.KeyPressed += delegate
            {
                captureToolStripMenuItem_Click(null, null);
            };
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
                { "key", Properties.Settings.Default.APIKey },
                { "image", Convert.ToBase64String(Img) }
            };
            // original_image "imgur_page"

            try
            {
                byte[] response = w.UploadValues("http://imgur.com/api/upload.xml", values);

                string tag = Properties.Settings.Default.DirectLink ? "original_image" : "imgur_page";

                XDocument x = XDocument.Parse(System.Text.Encoding.ASCII.GetString(response));

                string url = "";
                foreach (XElement element in x.Descendants(tag))
                    url = element.Value;
                Clipboard.SetDataObject(url, true);
                this.URL = url;
                this.TrayIcon.ShowBalloonTip(2, "Image Uploaded!", "Your image has been uploaded and the URL has been copied to the clipbaord. (" + url + ")", ToolTipIcon.Info);
                return url;
            }
            catch (WebException ex)
            {
                if (ex.Message.Contains("(400)"))
                    MessageBox.Show("Error uploading image, API key is invalid or has exceeded the maximum uploads this day.", "DeskCap - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "";
        }
        string URL;

        private void captureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Capturing) return;
            this.Capturing = true;
            frmCaptureImage img = new frmCaptureImage();
            if (img.ShowDialog() == DialogResult.OK)
            {
                UploadFile(img.Result);
            }
            this.Capturing = false;
        }

        private void TrayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo(this.URL);
            Process.Start(info);
        }
    }
}
