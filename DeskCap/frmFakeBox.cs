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
    public partial class frmFakeBox : Form
    {
        public frmFakeBox()
        {
            InitializeComponent();
        }

        private void frmFakeBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
