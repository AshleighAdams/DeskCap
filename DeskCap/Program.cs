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
	static class Program
	{
		static string UploadFile(Bitmap Img)
		{

            WebClient w = new WebClient();
            MemoryStream s = new MemoryStream();
            Img.Save(s, System.Drawing.Imaging.ImageFormat.Png);

            byte[] bytes = s.ToArray();
            var values = new NameValueCollection
            {
                { "key", "433a1bf4743dd8d7845629b95b5ca1b4" },
                { "image", Convert.ToBase64String(bytes) }
            };

            byte[] response = w.UploadValues("http://imgur.com/api/upload.xml", values);

            XDocument x = XDocument.Parse(System.Text.Encoding.ASCII.GetString(response));

            foreach (XElement element in x.Descendants("imgur_page"))
                return element.Value;
            return "";

		}
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmSettings());
		}
	}
}
