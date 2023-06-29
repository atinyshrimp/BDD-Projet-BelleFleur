using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private MainForm mainForm1;

        public Form1()
        {
            InitializeComponent();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string username = usernameField.Text;
            string password = passwordField.Text;

            string connectionString = $"SERVER=localhost;PORT=3306;DATABASE=fleurs;UID={username};PASSWORD={password};";
            MySqlConnection connection = null;

            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                MessageBox.Show("Connexion réussie !");
                this.Hide();
                mainForm1 = new MainForm(this, connection);
                mainForm1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connexion : " + ex.Message);
                usernameField.Clear();
                passwordField.Clear();
                usernameField.Focus();
            }
            finally
            {
                connection?.Close();
            }

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
