using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2;

namespace TCIS_Inventory3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose(); 
        }
    }
}
