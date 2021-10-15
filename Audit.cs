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
    public partial class Audit : Form
    {
        public Audit()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void abutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string title = "About Program";
            const string message = "This program was designed and developed by Matt Brown\n" +
                "for the exclusive use by Tuscola County Information Systems.\n" +
                "                                 Version 1.0.0.3 ";
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bonusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bonus bonus = new Bonus();  
            bonus.Show();
        }



    }
}
