using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace TCIS_Inventory3
{
    public partial class Audit : Form
    {
        private string connection;
        public Audit(string a)
        {
            InitializeComponent();
            connection = a;
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

        private void Audit_Load(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            try
            {
                MySqlConnection conn = new MySqlConnection(connection);
                conn.Open();
                string show = "Select * from inventory;";
                using (MySqlCommand cmd = new MySqlCommand(show, conn))
                {
                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(new object[]
                            {
                                reader.GetValue(0),
                                reader.GetValue(1),
                                reader.GetValue(2),
                                reader.GetValue(3),
                                reader.GetValue(4),
                                reader.GetValue(5),
                                reader.GetValue(6)
                            });
                        }
                    }
                } 
                string show2 = "Select * from test_audit.inventory;";
                using(MySqlCommand command = new MySqlCommand(show2, conn))
                {
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView2.Rows.Add(new object[]
                            {
                                reader.GetValue(0),
                                reader.GetValue(1),
                                reader.GetValue(2),
                                reader.GetValue(3),
                                reader.GetValue(4),
                                reader.GetValue(5),
                                reader.GetValue(6)
                            });
                        }
                    }
                }

                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
