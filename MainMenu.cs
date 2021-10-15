using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCIS_Inventory3
{
    public partial class MainMenu : Form
    {
        private string sql;
        public MainMenu(string a)
        {
            InitializeComponent();
            sql = a;
            //MessageBox.Show(a);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            const string question = "Are you sure you want to log out?";
            const string title = "Log Out?";

            var result = MessageBox.Show(question, title, MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                
            }
            else
            {
                this.Close();
            }
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory(sql);
            inventory.Show();
        }

        private void ExitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Environment.Exit(10);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string title = "About Program";
            const string message = "This program was designed and developed by Matt Brown\n" +
                "for the exclusive use by Tuscola County Information Systems.\n" +
                "                                 Version 1.0.0.3 ";
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}
