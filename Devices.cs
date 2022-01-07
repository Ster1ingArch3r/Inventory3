using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCIS_Inventory3
{
    public partial class Devices : Form
    {
        private string connection;
        public Devices(string a)
        {
            InitializeComponent();
            connection = a;
        }

        private void eyeDee(string c)
        {
            string checkID = "SELECT * FROM devices WHERE id LIKE @ID;";
            using (MySqlConnection IDConn = new MySqlConnection(connection))
            {
                IDConn.Open();
                using(MySqlCommand cmd = new MySqlCommand(checkID, IDConn))
                {
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(c));
                    cmd.Prepare();
                    
                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            using(MySqlConnection getID = new MySqlConnection(connection))
                            {
                                getID.Open();
                                string IDGrab = "SELECT * FROM devices where id = @ID;";
                                using(MySqlCommand cmdc = new MySqlCommand(IDGrab, getID))
                                {
                                    cmdc.Parameters.AddWithValue("@ID", Convert.ToInt32(c));
                                    cmdc.Prepare();
                                    using (MySqlDataReader reader2 = cmdc.ExecuteReader())
                                    {
                                        dataGridView1.Rows.Clear();
                                        while (reader2.Read())
                                        {
                                            dataGridView1.Rows.Add(new object[]
                                            {
                                                reader2.GetString(0),
                                                reader2.GetString(1),
                                                reader2.GetString(8),
                                                reader2.GetString(7)
                                            });
                                        }
                                    } 
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("There are no Items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }
        private void manufact(string c)
        {
            string checkID = "SELECT * FROM devices WHERE manufacturer LIKE @Manufacturer;";
            using (MySqlConnection manConn = new MySqlConnection(connection))
            {
                manConn.Open();
                using (MySqlCommand cmd = new MySqlCommand(checkID, manConn))
                {
                    cmd.Parameters.AddWithValue("@Manufacturer", c);
                    cmd.Prepare();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            using (MySqlConnection manu = new MySqlConnection(connection))
                            {
                                manu.Open();
                                string ManuGrab = "SELECT * FROM devices where manufacturer = @Manufacturer;";
                                using (MySqlCommand cmdc = new MySqlCommand(ManuGrab, manu))
                                {
                                    cmdc.Parameters.AddWithValue("@Manufacturer", c);
                                    cmdc.Prepare();
                                    using (MySqlDataReader reader2 = cmdc.ExecuteReader())
                                    {
                                        dataGridView1.Rows.Clear();
                                        while (reader2.Read())
                                        {
                                            dataGridView1.Rows.Add(new object[]
                                            {
                                                reader2.GetString(0),
                                                reader2.GetString(1),
                                                reader2.GetString(8),
                                                reader2.GetString(7)
                                            });
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("There are no Items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }
        private void Asset_Tag(string c)
        {
            string checkAT = "SELECT * FROM devices WHERE asset_tag LIKE @AT;";
            using (MySqlConnection atConn = new MySqlConnection(connection))
            {
                atConn.Open();
                using (MySqlCommand cmd = new MySqlCommand(checkAT, atConn))
                {
                    cmd.Parameters.AddWithValue("@AT", Convert.ToInt32(c));
                    cmd.Prepare();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            using (MySqlConnection at = new MySqlConnection(connection))
                            {
                                at.Open();
                                string atGrab = "SELECT * FROM devices where asset_tag = @AT;";
                                using (MySqlCommand cmdat = new MySqlCommand(atGrab, at))
                                {
                                    cmdat.Parameters.AddWithValue("@AT", Convert.ToInt32(c));
                                    cmdat.Prepare();
                                    using (MySqlDataReader reader2 = cmdat.ExecuteReader())
                                    {
                                        dataGridView1.Rows.Clear();
                                        while (reader2.Read())
                                        {
                                            dataGridView1.Rows.Add(new object[]
                                            {
                                                reader2.GetString(0),
                                                reader2.GetString(1),
                                                reader2.GetString(8),
                                                reader2.GetString(7)
                                            });
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("There are no Items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }
        private void Serial(string c)
        {
            string checkSer = "SELECT * FROM devices WHERE serial_number LIKE @Serial;";
            using (MySqlConnection serConn = new MySqlConnection(connection))
            {
                serConn.Open();
                using (MySqlCommand cmd = new MySqlCommand(checkSer, serConn))
                {
                    cmd.Parameters.AddWithValue("@Serial", c);
                    cmd.Prepare();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            using (MySqlConnection snconn = new MySqlConnection(connection))
                            {
                                snconn.Open();
                                string snGrab = "SELECT * FROM devices where serial_number = @Serial;";
                                using (MySqlCommand cmdc = new MySqlCommand(snGrab, snconn))
                                {
                                    cmdc.Parameters.AddWithValue("@Serial", c);
                                    cmdc.Prepare();
                                    using (MySqlDataReader reader2 = cmdc.ExecuteReader())
                                    {
                                        dataGridView1.Rows.Clear();
                                        while (reader2.Read())
                                        {
                                            dataGridView1.Rows.Add(new object[]
                                            {
                                                reader2.GetString(0),
                                                reader2.GetString(1),
                                                reader2.GetString(8),
                                                reader2.GetString(7)
                                            });
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("There are no Items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                string searchbar = textBox1.Text;
                if (searchbar == "")
                {
                    MessageBox.Show("The search box must not be empty!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (checkBox1.Checked == true)
                    {
                        eyeDee(searchbar);
                    }
                    else if (checkBox2.Checked == true)
                    {
                        manufact(searchbar);
                    }
                    else if (checkBox3.Checked == true)
                    {
                        Asset_Tag(searchbar);   
                    }
                    else if (checkBox4.Checked == true)
                    {
                        Serial(searchbar);  
                    }
                    else
                    {
                        dataGridView1.Rows.Clear();
                        MySqlConnection conn = new MySqlConnection(connection);
                        conn.Open();
                        string show = $"Select * from devices;";
                        MySqlCommand cmd1 = new MySqlCommand(show, conn);
                        MySqlDataReader read = cmd1.ExecuteReader();

                        while (read.Read())
                        {
                            dataGridView1.Rows.Add(new object[]
                            {
                                read.GetString(0),
                                read.GetString(1),
                                read.GetString(8),
                                read.GetString(7)
                            });
                        }
                        conn.Close();
                    }


                }
            }
            catch (MySqlException x)
            {
                MessageBox.Show(x.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }
           

        private void Devices_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connection);
                conn.Open();
                string show = $"Select * from devices;";
                MySqlCommand cmd1 = new MySqlCommand(show, conn);
                MySqlDataReader read = cmd1.ExecuteReader();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(new object[]
                    {
                    read.GetString(0),
                    read.GetString(1),
                    read.GetString(8),
                    read.GetString(7)
                    });
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox3.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox4.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
