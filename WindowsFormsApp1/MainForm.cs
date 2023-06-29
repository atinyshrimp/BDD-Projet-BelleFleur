using FontAwesome.Sharp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsFormsApp1.Pages;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        #region Propriétés
        // Forms
        private Form1 loginForm;
        private Form currentChildForm;

        // MySql
        private MySqlConnection conn;

        // Form objects
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Color focusColor = Color.ForestGreen;
        #endregion

        #region Constructeur
        public MainForm(Form1 form1, MySqlConnection connection)
        {
            InitializeComponent();
            loginForm = form1;
            conn = connection;
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
        }
        #endregion

        #region Méthodes
        private void ActivateButton(object sender, Color color)
        {
            if (sender != null) 
            {
                DisableButton();

                // Button
                currentBtn = (IconButton)sender;
                currentBtn.BackColor = Color.FromArgb(128, 64, 0);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                // Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                // Current Child form
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                labelTitleChildForm.Text = currentBtn.Text;
            }
        }

        private void CheckFidelity()
        {
            string query = "UPDATE client SET fidelite = " +
                           "(CASE " +
                           "    WHEN (SELECT COUNT(*) FROM commande WHERE courriel = client.courriel AND MONTH(date_commande) = MONTH(CURDATE())) > 5 THEN 'Or' " +
                           "    WHEN (SELECT COUNT(*) FROM commande WHERE courriel = client.courriel AND YEAR(date_commande) = YEAR(CURDATE())) / " +
                           "         (SELECT GREATEST(TIMESTAMPDIFF(MONTH, MIN(date_commande), MAX(date_commande)), 1) FROM commande WHERE courriel = client.courriel) >= 1 THEN 'Bronze' " +
                           "    ELSE 'None' " +
            "END)";


            MySqlCommand command = new MySqlCommand(query, conn);
            if (conn.State == ConnectionState.Closed) conn.Open();
            command.ExecuteNonQuery();
            if (conn.State == ConnectionState.Open) conn.Close();
        }

        private void CheckProducts()
        {
            List<string> alerts = new List<string>();

            // Retrieve all products and their thresholds
            MySqlCommand command = new MySqlCommand("SELECT id_produit, seuil_produit FROM produit;", conn);
            MySqlDataReader reader = command.ExecuteReader();
            Dictionary<string, int> thresholds = new Dictionary<string, int>();
            while (reader.Read())
            {
                thresholds[reader.GetString(0)] = reader.GetInt32(1);
            }
            reader.Close();

            // Check the inventory for each product in each boutique
            for (int i = 1; i <= NumberOfShops(); i++)
            {
                command = new MySqlCommand($"SELECT p.id_produit, p.nom_produit, b.nom_boutique, s.qte_inv, p.seuil_produit FROM produit p NATURAL JOIN stocke s NATURAL JOIN boutique b WHERE s.id_boutique={i};", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int threshold = thresholds[reader.GetString(0)];
                    int quantity = reader.GetInt32(3);
                    if (quantity < threshold)
                    {
                        string message = string.Format("Le produit '{0}' est en-dessous du seuil d'alerte dans la boutique '{1}' ({2} en stock ; seuil à {3})", reader.GetString(1), reader.GetString(2), quantity, reader.GetString(4));
                        alerts.Add(message);
                    }
                }
                reader.Close();
            }

            // Display any alerts that were generated
            if (alerts.Count > 0)
            {
                string msg = "";
                for (int i = 0; i < alerts.Count; i++)
                {
                    msg += alerts[i];
                    if (i < alerts.Count - 1) msg += "\n\n";
                }
                MessageBox.Show(msg, "Alertes de stock !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(128, 64, 0);
                currentBtn.ForeColor = Color.SeaShell;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.SeaShell;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private string GetUserName()
        {
            string userName;
            string query = "SELECT USER();";
            MySqlCommand command = new MySqlCommand(query, conn);
            if (conn.State == ConnectionState.Closed) conn.Open();
            userName = Convert.ToString(command.ExecuteScalar());
            if (conn.State == ConnectionState.Open) conn.Close();
            return userName.Split('@')[0];
        }

        private int NumberOfShops()
        {
            int num = 0;
            MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM boutique;", conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) { num = Convert.ToInt32(reader.GetString(0)); }
            reader.Close();
            return num;
        }

        private void OpenChildForm(Form childForm)
        {
            currentChildForm?.Close();

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.RosyBrown;
            labelTitleChildForm.Text = "Home";
        }
        #endregion

        #region Evenements
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, focusColor);
            OpenChildForm(new CustomerModule(conn));
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vous déconnecter ?", "Déconnexion de l'utilisateur", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Hide();
            }
        }

        private void btnMaximize_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal) WindowState = FormWindowState.Maximized;
            else WindowState = FormWindowState.Normal;
        }

        private void btnMinimize_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, focusColor);
            OpenChildForm(new OrderModule(conn));
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, focusColor);
            OpenChildForm(new ProductModule(conn));
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, focusColor);
            OpenChildForm(new ShopModule(conn));
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, focusColor);
            OpenChildForm(new StatsModule(conn));
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            loginForm.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
        }

        #region Drag form without borders
        // Drag form
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckProducts();
            if (GetUserName() == "root") CheckFidelity();
        }
        #endregion
    }
}
