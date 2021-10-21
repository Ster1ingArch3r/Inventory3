using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TCIS_Inventory3
{
    public partial class ViewEdits : Form
    {
        private string connection;
        public ViewEdits(string a)
        {
            InitializeComponent();
            connection = a;
        }
        private void SaveAsCsv()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DateTime dateTime = DateTime.Now;
                dateTime.ToShortDateString();   
                
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "CSV (*.csv)|*.csv";
                save.FileName = $"Edits_Date_{dateTime}.csv";
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
        private void button1_Click(object sender, EventArgs e)
        {
            SaveAsCsv();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "Truncate changes;";
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewEdits_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                MySqlConnection conn = new MySqlConnection(connection);
                conn.Open();
                string show = "Select * from changes;";
                using (MySqlCommand cmd = new MySqlCommand(show, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(new object[]
                            {
                                reader.GetValue(0),
                                reader.GetValue(1),
                                reader.GetValue(2),
                                reader.GetValue(3),
                                reader.GetValue(4)
                                
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
