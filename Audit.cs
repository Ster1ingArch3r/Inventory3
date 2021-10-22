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
        private void setAudit()
        {
            try
            {
                string sql = "Truncate test_audit.inventory; Insert into test_audit.inventory select * from inventory;";
                MySqlConnection conn = new MySqlConnection(connection);
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void abutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string version = "0.8.2";
            const string title = "About Program";
            string message = $"This program was designed and developed by Matt Brown\n" +
                $"for the exclusive use by Tuscola County Information Systems.\n" +
                $"                                 {version} ";
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bonusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bonus bonus = new Bonus();  
            bonus.Show();
        }

        private void Audit_Load(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
            if(DateTime.Now.Day == 28 && DateTime.Now.Month == 2)
            {
                setAudit();
            }
            else if(DateTime.Now.Day == 30 || DateTime.Now.Day == 31)
            {
                setAudit();
            }
            else
            {
                button1.Enabled = true;
            }


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

        private void button1_Click(object sender, EventArgs e)
        {
            setAudit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewEdits viewEdits = new ViewEdits(connection);
            viewEdits.Show();   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
            UseEdit useEdit = new UseEdit(connection, ID);
            useEdit.Show(); 
        }
    }
}
