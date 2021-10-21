using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace TCIS_Inventory3
{
    public partial class UseEdit : Form
    {
        string connection = "";
        int ID = 0;
        public UseEdit(string a, int b)
        {
            InitializeComponent();
            connection = a;
            ID = b;
        }

        string setTextBox = "";
        //Get user info. Set username to info

        private void UseEdit_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            dateTime.ToShortDateString();
            dateTimePicker1.Text = dateTime.ToString();

            try
            {
                MySqlConnection conn = new MySqlConnection(connection);
                string getItem = $"Select * from inventory where id = @id";
                conn.Open();
                MySqlCommand command = new MySqlCommand(getItem, conn);
                command.Parameters.AddWithValue("@id", ID);
                command.Prepare();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    comboBox1.Text = reader.GetValue(1).ToString();
                    textBox1.Text = reader.GetValue(2).ToString();
                    textBox2.Text = reader.GetValue(3).ToString();
                    textBox3.Text = reader.GetValue(4).ToString();
                    textBox4.Text = reader.GetValue(5).ToString();
                    textBox5.Text = reader.GetValue(6).ToString();

                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                MySqlConnection conn = new MySqlConnection(connection);
                string getUser = "Select Current_User();";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(getUser, conn);
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    setTextBox = read.GetString(0);
                }
                conn.Close();
                textBox6.Text = setTextBox;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void UpdateItem()
        {
            try
            {
                string update = "Update inventory Set category = @category, descript = @description, quantity = @quantity, cost = @cost, reorder = @reorder, shelf = @shelf Where id = @id;";
                MySqlConnection conn = new MySqlConnection(connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@category", comboBox1.Text);
                cmd.Parameters.AddWithValue("@description", textBox1.Text);
                cmd.Parameters.AddWithValue("@quantity", Convert.ToInt32(textBox2.Text));
                cmd.Parameters.AddWithValue("@cost", Convert.ToDouble(textBox3.Text));
                cmd.Parameters.AddWithValue("@reorder", Convert.ToInt32(textBox4.Text));
                cmd.Parameters.AddWithValue("@shelf", textBox5.Text);
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
            }catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Sql Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LogEdit()
        {
            try
            {
                string logChange = "Insert into changes(username, itemchanged, datechanged, ticket) values(@username, @item, @date, @ticket);";
                MySqlConnection conn = new MySqlConnection(connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(logChange, conn);
                cmd.Parameters.AddWithValue("@username", textBox6.Text);
                cmd.Parameters.AddWithValue("@item", textBox7.Text);
                cmd.Parameters.AddWithValue("@date", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@ticket", Convert.ToInt32(textBox8.Text));
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateItem();
                LogEdit();
                MessageBox.Show("Inventory Updated!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
