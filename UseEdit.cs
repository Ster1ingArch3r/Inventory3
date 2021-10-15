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
                textBox1.Text = setTextBox;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Title", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateItem()
        {
            try
            {
                string update = "Update inventory Set category = @category, decript = @description, quantity = @quantity, cost = @cost, reorder = @reorder," +
                    " shelf = @shelf Where id = @id;";
                MySqlConnection conn = new MySqlConnection(connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@category", comboBox1.Text);
                cmd.Parameters.AddWithValue("@description", textBox1.Text);
                cmd.Parameters.AddWithValue("@quantity", Convert.ToInt32(textBox2.Text));
                cmd.Parameters.AddWithValue("@cost", Convert.ToInt32(textBox3.Text));
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
            string logChange = "Insert into newinventory.changes";
        }
    }
}
