using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TCIS_Inventory3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
                stringBuilder.UserID = textBox1.Text;
                stringBuilder.Password = textBox2.Text;
                stringBuilder.Server = textBox3.Text;
                stringBuilder.Database = comboBox1.Text;

                string sql = stringBuilder.ToString();
                MySqlConnection conn = new MySqlConnection(sql);
                conn.Open();

                MessageBox.Show($"Welcome to the TCIS Inventory Program!\nServer Version: {conn.ServerVersion}.");

                conn.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.Text = "";

                MainMenu mm = new MainMenu(sql);
                mm.Show();



            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
