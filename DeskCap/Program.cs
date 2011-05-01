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
