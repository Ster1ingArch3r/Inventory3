using System;
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
            Hide();
            Inventory inventory = new Inventory(sql);
            inventory.ShowDialog();
            inventory = null;
            Show();
        }

        private void ExitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Environment.Exit(10);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string version = "0.8.2";
            const string title = "About Program";
            string message = $"This program was designed and developed by Matt Brown\n" +
                $"for the exclusive use by Tuscola County Information Systems.\n" +
                $"                                 {version} ";
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Hide();
            Audit audit = new Audit(sql);
            audit.ShowDialog();
            audit = null;
            Show();
        }
    }
}
