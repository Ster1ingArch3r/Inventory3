using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.IO;
using System.Text;


namespace TCIS_Inventory3
{
    public partial class Inventory : Form
    {
        private string connection;
        public Inventory(string a)
        {
            InitializeComponent();
            connection = a;
            //MessageBox.Show(a);
        }
        private void Onload(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                MySqlConnection conn = new MySqlConnection(connection);
                conn.Open();
                string show = "Select * from inventory;";
                MySqlCommand cmd1 = new MySqlCommand(show, conn);
                MySqlDataReader read = cmd1.ExecuteReader();
                while (read.Read())
                {
                    dataGridView1.Rows.Add(new object[]
                    {
                        read.GetValue(0),
                        read.GetValue(1),
                        read.GetValue(2),
                        read.GetValue(3),
                        read.GetValue(4),
                        read.GetValue(5),
                        read.GetValue(6)
                    });

                }

                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string version = "0.8.2";
            const string title = "About Program";
            string message = $"This program was designed and developed by Matt Brown\n" +
                $"for the exclusive use by Tuscola County Information Systems.\n" +
                $"                                 {version} ";
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string insert = "INSERT INTO inventory(Category, Descript, Quantity, Cost, Reorder, Shelf) values(@Category, @Description, @Quantity, @Cost, @Reorder, @Shelf);";
            try
            {
                MySqlConnection conn = new MySqlConnection(connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@Category", comboBox1.Text);
                cmd.Parameters.AddWithValue("@Description", textBox1.Text);
                cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(textBox2.Text));
                cmd.Parameters.AddWithValue("@Cost", Convert.ToDouble(textBox3.Text));
                cmd.Parameters.AddWithValue("@Reorder", Convert.ToInt32(textBox4.Text));
                cmd.Parameters.AddWithValue("@Shelf", comboBox2.Text + comboBox3.Text);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                
                conn.Close();

                const string message1= "Item Added Successfully!";
                const string title1 = "Success!";
                MessageBox.Show(message1, title1, MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                comboBox1.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                
            }
            catch(MySqlException ex)
            {
                string message2 = ex.Message;
                const string title2 = "Error!";
                MessageBox.Show(message2, title2, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exirtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
            UseEdit ue = new UseEdit(connection, ID);
            ue.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string description = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            string message3 = "Are you sure that you wish to delete this item?";
            string title2 = "Delete";
            var result = MessageBox.Show(message3, title2, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {

            }
            else
            {
                string message4 = "Are you really sure you want to delete this?";
                string title3 = "Are you sure?";
                var result2 = MessageBox.Show(message4, title3, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result2 == DialogResult.No)
                {

                }
                else
                {
                    try
                    {
                        string UID = "";
                        MySqlConnection conn = new MySqlConnection(connection);
                        conn.Open();
                        string selectuser = "Select current_user();";
                        MySqlCommand cmd2 = new MySqlCommand(selectuser, conn);
                        MySqlDataReader read2 = cmd2.ExecuteReader();
                        while (read2.Read())
                        {
                            
                            UID = read2.GetString(0);
                        }
                        conn.Close();

                        conn.Open();
                        //Building changes command from data in table.
                        string change = "insert into changes(username, itemchanged, datechanged, ticket) " +
                            "values(@Username, @Item, @Date, @ticket);";
                        MySqlCommand cmd3 = new MySqlCommand(change, conn);
                        cmd3.Parameters.AddWithValue("@Username", UID);
                        cmd3.Parameters.AddWithValue("@Item", description);
                        cmd3.Parameters.AddWithValue("@Date", DateTime.Now);
                        cmd3.Parameters.AddWithValue("@ticket", -100);
                        cmd3.Prepare();
                        cmd3.ExecuteNonQuery();
                        conn.Close();   

                        conn.Open();
                        string delete = $"Delete from inventory where ID = {ID}";
                        MySqlCommand cmd = new MySqlCommand(delete, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Item Deleted!", "Deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                MySqlConnection conn = new MySqlConnection(connection);
                conn.Open();
                string show = "Select * from inventory;";
                MySqlCommand cmd1 = new MySqlCommand(show, conn);
                MySqlDataReader read = cmd1.ExecuteReader();
                while (read.Read())
                {
                    dataGridView1.Rows.Add(new object[]
                    {
                        read.GetValue(0),
                        read.GetValue(1),
                        read.GetValue(2),
                        read.GetValue(3),
                        read.GetValue(4),
                        read.GetValue(5),
                        read.GetValue(6)
                    });
                    
                }
                
                conn.Close();
            }
            catch(MySqlException ex) 
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        //getinfo from data grid view and convert to CSV. 
        //Then save as in windows.

        private void SaveAsCsv()
        {
            if(dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "CSV (*.csv)|*.csv";
                save.FileName = "TotalInventory.csv";
                bool fileError = false;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }catch (IOException ex)
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
                            for(int i = 0; i < colCount; i++)
                            {
                                colNames += dataGridView1.Columns[i].HeaderText + ",";
                            }
                            outputCsv[0] += colNames;

                            for(int i = 1; (i-1) < dataGridView1.Rows.Count; i++)
                            {
                                for(int j = 0; j < colCount; j++)
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

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SaveAsCsv();
        }

        private void bonusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bonus bonus= new Bonus();
            bonus.Show();
        }

        private void totalValueReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                
            string totalValue = "Select Sum(cost * quantity) From Inventory;";
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(totalValue, conn);
            MySqlDataReader read = cmd.ExecuteReader();
            double t = 0;

            if (read.Read())
            {
                {
                    t = read.GetDouble(0);              
                }
                MessageBox.Show(Convert.ToString(t), "Total Value", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            conn.Close();
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
