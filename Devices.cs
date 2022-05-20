using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
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
        private void UpdateItem()
        {
            try
            {
                int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                using(MySqlConnection conn = new MySqlConnection(connection))
                {
                    string updoot = "UPDATE devices SET manufacturer = @manufacturer, information = @information, device_type = @device_type, purchase_price = @purchase_price," +
                        "mac_address = @mac_address, model = @model, serial_number = @serial_number, asset_tag = @asset_tag, location = @location, date_added = @date_added, image_src = @image_src WHERE id = @id;"; 
                    conn.Open();
                    using (MySqlCommand deviceUpdate = new MySqlCommand(updoot, conn))
                    {
                        deviceUpdate.Parameters.AddWithValue("@manufacturer", textBox13.Text.Trim());
                        deviceUpdate.Parameters.AddWithValue("@information", textBox14.Text.Trim());
                        deviceUpdate.Parameters.AddWithValue("@device_type", textBox15.Text.Trim());
                        deviceUpdate.Parameters.AddWithValue("@purchase_price", Convert.ToDouble(textBox16.Text.Trim()));
                        deviceUpdate.Parameters.AddWithValue("@mac_address", textBox17.Text.Trim());
                        deviceUpdate.Parameters.AddWithValue("@model", textBox18.Text.Trim());
                        deviceUpdate.Parameters.AddWithValue("@serial_number", textBox19.Text.Trim());
                        deviceUpdate.Parameters.AddWithValue("@asset_tag", textBox20.Text.Trim());
                        deviceUpdate.Parameters.AddWithValue("@location", textBox21.Text.Trim());
                        deviceUpdate.Parameters.AddWithValue("@date_added", textBox22.Text.Trim());
                        deviceUpdate.Parameters.AddWithValue("@image_src", textBox23.Text.Trim());
                        deviceUpdate.Parameters.AddWithValue("@id", id);
                        deviceUpdate.Prepare();
                        deviceUpdate.ExecuteNonQuery();
                    }
                    conn.Close();
                    MessageBox.Show("Device Data Updated", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Sql Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveAsCsv()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "CSV (*.csv)|*.csv";
                save.FileName = "Device_List.csv";
                bool fileError = false;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int colCount = dataGridView1.Columns.Count;
                            string colNames = "";
                            string[] outputCsv = new string[dataGridView1.Rows.Count + 1];
                            for (int i = 0; i < colCount; i++)
                            {
                                colNames += dataGridView1.Columns[i].HeaderText + ",";
                            }
                            outputCsv[0] += colNames;

                            for (int i = 1; (i - 1) < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < colCount; j++)
                                {
                                    outputCsv[i] += dataGridView1.Rows[i - 1].Cells[j].Value + ",";
                                }
                            }
                            File.WriteAllLines(save.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("Data Exported Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
            }
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
                                                reader2.GetString(2),
                                                reader2.GetString(3),
                                                reader2.GetString(4),
                                                reader2.GetString(5),
                                                reader2.GetString(6),
                                                reader2.GetString(8),
                                                reader2.GetString(7),
                                                reader2.GetString(9),
                                                reader2.GetString(10),
                                                reader2.GetString(11)
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
                                                reader2.GetString(2),
                                                reader2.GetString(3),
                                                reader2.GetString(4),
                                                reader2.GetString(5),
                                                reader2.GetString(6),
                                                reader2.GetString(8),
                                                reader2.GetString(7),
                                                reader2.GetString(9),
                                                reader2.GetString(10),
                                                reader2.GetString(11)
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
                                                reader2.GetString(2),
                                                reader2.GetString(3),
                                                reader2.GetString(4),
                                                reader2.GetString(5),
                                                reader2.GetString(6),
                                                reader2.GetString(8),
                                                reader2.GetString(7),
                                                reader2.GetString(9),
                                                reader2.GetString(10),
                                                reader2.GetString(11)
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
                                                reader2.GetString(2),
                                                reader2.GetString(3),
                                                reader2.GetString(4),
                                                reader2.GetString(5),
                                                reader2.GetString(6),
                                                reader2.GetString(8),
                                                reader2.GetString(7),
                                                reader2.GetString(9),
                                                reader2.GetString(10),
                                                reader2.GetString(11)
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
                    read.GetString(2),
                    read.GetString(3),
                    read.GetString(4),
                    read.GetString(5),
                    read.GetString(6),
                    read.GetString(8),
                    read.GetString(7),
                    read.GetString(9),
                    read.GetString(10),
                    read.GetString(11)
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
                                textBox13.Text = reader.GetValue(1).ToString();
                                textBox14.Text = reader.GetValue(2).ToString();
                                textBox15.Text = reader.GetValue(3).ToString();
                                textBox16.Text = reader.GetValue(4).ToString();
                                textBox17.Text = reader.GetValue(5).ToString();
                                textBox18.Text = reader.GetValue(6).ToString();
                                textBox19.Text = reader.GetValue(7).ToString();
                                textBox20.Text = reader.GetValue(8).ToString();
                                textBox21.Text = reader.GetValue(9).ToString();
                                textBox22.Text = reader.GetValue(10).ToString();
                                textBox23.Text = reader.GetValue(11).ToString();

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

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateItem();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            //Export to CSV
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
                    read.GetString(2),
                    read.GetString(3),
                    read.GetString(4),
                    read.GetString(5),
                    read.GetString(6),
                    read.GetString(8),
                    read.GetString(7),
                    read.GetString(9),
                    read.GetString(10),
                    read.GetString(11)
                    });
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {

            }
            SaveAsCsv();
        }
    }
}
