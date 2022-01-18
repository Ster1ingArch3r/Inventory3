using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Net;
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
            try
            {
                string checkID = "SELECT * FROM devices WHERE id LIKE @ID;";
                using (MySqlConnection IDConn = new MySqlConnection(connection))
                {
                    IDConn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(checkID, IDConn))
                    {
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(c));
                        cmd.Prepare();

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                using (MySqlConnection getID = new MySqlConnection(connection))
                                {
                                    getID.Open();
                                    string IDGrab = "SELECT * FROM devices where id = @ID;";
                                    using (MySqlCommand cmdc = new MySqlCommand(IDGrab, getID))
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
            catch (Exception ex)
            {
                MessageBox.Show("That isn't a valid ID number. Try Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Please Enter a valid asset tag number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connection))
                {
                    string mysql = "INSERT INTO devices(manufacturer, information, device_type, purchase_price, mac_address, model, serial_number, asset_tag, Location, date_added, image_src) VALUES(" +
                        "@manufacturer, @information, @device_type, @purchase_price, @mac_address, @model, @serial_number, @asset_tag,@location, @date_added, @image_src);";
                    conn.Open();
                    using (MySqlCommand deviceInsert = new MySqlCommand(mysql, conn))
                    {
                        deviceInsert.Parameters.AddWithValue("@manufacturer", textBox2.Text.Trim());
                        deviceInsert.Parameters.AddWithValue("@information", textBox3.Text.Trim());
                        deviceInsert.Parameters.AddWithValue("@device_type", textBox4.Text.Trim());
                        deviceInsert.Parameters.AddWithValue("@purchase_price", Convert.ToDouble(textBox5.Text.Trim()));
                        deviceInsert.Parameters.AddWithValue("@mac_address", textBox6.Text.Trim());
                        deviceInsert.Parameters.AddWithValue("@model", textBox7.Text.Trim());
                        deviceInsert.Parameters.AddWithValue("@serial_number", textBox8.Text.Trim());
                        deviceInsert.Parameters.AddWithValue("@asset_tag", textBox9.Text.Trim());
                        deviceInsert.Parameters.AddWithValue("@location", textBox10.Text.Trim());
                        deviceInsert.Parameters.AddWithValue("@date_added", textBox11.Text.Trim());
                        deviceInsert.Parameters.AddWithValue("@image_src", textBox12.Text.Trim());
                        deviceInsert.Prepare();
                        deviceInsert.ExecuteNonQuery();
                    }
                    conn.Close();
                    string message = "Device Added Successfully!";
                    MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox11.Text = "";
                    textBox12.Text = "";
                }
            }
            catch(MySqlException MSE)
            {
                string errorMsg = MSE.Message;
                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                string getDevice = "SELECT * FROM devices WHERE id = @id;";

                using (MySqlConnection conn = new MySqlConnection(connection))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(getDevice, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Prepare();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                label15.Text = reader.GetValue(1).ToString();
                                label16.Text = reader.GetValue(2).ToString();
                                label17.Text = reader.GetValue(3).ToString();
                                label18.Text = reader.GetValue(4).ToString();
                                label19.Text = reader.GetValue(5).ToString();
                                label20.Text = reader.GetValue(6).ToString();
                                label21.Text = reader.GetValue(7).ToString();
                                label22.Text = reader.GetValue(8).ToString();
                                label23.Text = reader.GetValue(9).ToString();
                                label24.Text = reader.GetValue(10).ToString();
                                string image = reader.GetValue(11).ToString();
                                var request = WebRequest.Create(image);
                                using (var response = request.GetResponse())
                                {
                                    using (var stream = response.GetResponseStream())
                                    {

                                        pictureBox1.Image = Bitmap.FromStream(stream);
                                        response.Close();
                                        response.Dispose();
                                    }                           
                                }                                
                            }
                        }
                    }
                    conn.Close(); 
                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops", "Fire", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
