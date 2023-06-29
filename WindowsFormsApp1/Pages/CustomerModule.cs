using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApp1.Formulaires;

namespace WindowsFormsApp1.Pages
{
    public partial class CustomerModule : Form
    {
        #region Attributs
        private MySqlConnection _connection;
        private MySqlCommand _command;
        private MySqlDataReader _reader;

        private string _query;
        private string _fidelity = "None";
        private string _searchFilter = "";

        public string[] fidelityLevels = { "None", "Bronze", "Or" };
        #endregion

        #region Constructeur
        public CustomerModule(MySqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;
            cbFidelity.SelectedIndex = 0;
            LoadShopOptions();
            LoadCustomers();
        }
        #endregion

        #region Méthodes
        private void CheckFidelity()
        {
            string query = "UPDATE client SET fidelite = " +
                           "(CASE " +
                           "    WHEN (SELECT COUNT(*) FROM commande WHERE courriel = client.courriel AND MONTH(date_commande) = MONTH(CURDATE())) > 5 THEN 'Or' " +
                           "    WHEN (SELECT COUNT(*) FROM commande WHERE courriel = client.courriel AND YEAR(date_commande) = YEAR(CURDATE())) / " +
                           "         (SELECT GREATEST(TIMESTAMPDIFF(MONTH, MIN(date_commande), MAX(date_commande)), 1) FROM commande WHERE courriel = client.courriel) >= 1 THEN 'Bronze' " +
                           "    ELSE 'None' " +
                           "END)";

            
            _command = new MySqlCommand(query, _connection);
            if(_connection.State == ConnectionState.Closed) _connection.Open();
            _command.ExecuteNonQuery();
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }

        private void EditCustomer(DataGridViewCellEventArgs e)
        {
            CustomerForm form = new CustomerForm(this, _connection);

            form.lblTitle.Text = "Modification d'un client";
            form.Text = form.lblTitle.Text;

            form.tbMail.Text = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
            form.tbMail.Enabled = false;

            form.tbSurname.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            form.tbName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
            form.tbPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
            form.tbPassword.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();
            form.tbAddress.Text = dgvCustomer.Rows[e.RowIndex].Cells[5].Value.ToString();
            form.tbCardNb.Text = dgvCustomer.Rows[e.RowIndex].Cells[6].Value.ToString();
            form.cbFidelity.Text = dgvCustomer.Rows[e.RowIndex].Cells[7].Value.ToString();
            form.cbShop.SelectedIndex = GetShopID(form.tbMail.Text) - 1;

            form.btnSave.Visible = false;
            form.btnUpdate.Visible = true;

            if (form.ShowDialog() == DialogResult.OK) LoadCustomers();
        }

        private void EditCustomer(DataGridViewCellMouseEventArgs e)
        {
            CustomerForm form = new CustomerForm(this, _connection);

            form.lblTitle.Text = "Modification d'un client";
            form.Text = form.lblTitle.Text;

            form.tbMail.Text = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
            form.tbMail.Enabled = false;

            form.tbSurname.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            form.tbName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
            form.tbPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
            form.tbPassword.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();
            form.tbAddress.Text = dgvCustomer.Rows[e.RowIndex].Cells[5].Value.ToString();
            form.tbCardNb.Text = dgvCustomer.Rows[e.RowIndex].Cells[6].Value.ToString();
            form.cbFidelity.Text = dgvCustomer.Rows[e.RowIndex].Cells[7].Value.ToString();
            form.cbShop.SelectedIndex = GetShopID(form.tbMail.Text) - 1;

            form.btnSave.Visible = false;
            form.btnUpdate.Visible = true;

            if (form.ShowDialog() == DialogResult.OK) LoadCustomers();
        }

        public void ExportClientsNotOrderedLast6MonthsToJSON()
        {
            // Récupérer la date d'il y a 6 mois
            DateTime last6Months = DateTime.Now.AddMonths(-6);

            // Requête pour récupérer les clients n'ayant pas commandé depuis plus de 6 mois
            string query = "SELECT courriel, nom, prenom " +
                           "FROM Client " +
                           "WHERE courriel NOT IN (SELECT DISTINCT courriel FROM Commande WHERE date_commande >= @last6Months)";

            using (_connection)
            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                // Ajouter le paramètre "last6Months" à la commande
                command.Parameters.AddWithValue("@last6Months", last6Months);

                if (_connection.State == ConnectionState.Closed) _connection.Open();

                // Récupérer les résultats de la requête
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Créer la liste de clients en format JSON
                    List<object> clients = new List<object>();
                    while (reader.Read())
                    {
                        clients.Add(new
                        {
                            Courriel = reader.GetString("courriel"),
                            Nom = reader.GetString("nom"),
                            Prenom = reader.GetString("prenom")
                        });
                    }

                    // Convertir la liste de clients en format JSON
                    string json = JsonConvert.SerializeObject(clients, Newtonsoft.Json.Formatting.Indented);

                    // Sauvegarder le fichier JSON avec le dialogue de sauvegarde de fichier
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "JSON file (*.json)|*.json|All files (*.*)|*.*";
                    saveFileDialog.Title = "Enregistrer le fichier JSON";
                    saveFileDialog.FileName = "clients_not_ordered_last_6_months.json";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                        {
                            sw.Write(json);
                        }
                    }
                }
            }
        }

        public void ExportClientsWithMultipleOrdersLastMonthToXML()
        {
            // Requête pour récupérer les clients ayant commandé plusieurs fois durant le dernier mois
            string query = "SELECT c.courriel, c.nom, c.prenom " +
                           "FROM Client c " +
                           "INNER JOIN Commande cmd ON c.courriel = cmd.courriel " +
                           "WHERE cmd.date_commande >= DATE_ADD(NOW(), INTERVAL -1 MONTH) " +
                           "GROUP BY c.courriel, c.nom, c.prenom " +
                           "HAVING COUNT(*) > 1";

            using (_connection)
            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                if (_connection.State == ConnectionState.Closed) _connection.Open();

                // Récupérer les résultats de la requête
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Créer le document XML
                    XDocument xmlDocument = new XDocument(new XElement("Clients"));

                    while (reader.Read())
                    {
                        // Ajouter chaque client récupéré à l'élément XML "Clients"
                        XElement clientElement = new XElement("Client",
                            new XElement("Courriel", reader.GetString("courriel")),
                            new XElement("Nom", reader.GetString("nom")),
                            new XElement("Prenom", reader.GetString("prenom"))
                        );
                        xmlDocument.Root.Add(clientElement);
                    }

                    // Demander à l'utilisateur de spécifier l'emplacement et le nom du fichier
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Fichiers XML (*.xml)|*.xml";
                    saveFileDialog.FileName = "clients_multiple_orders_last_month.xml";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Sauvegarder le document XML dans le fichier spécifié
                        xmlDocument.Save(saveFileDialog.FileName);
                    }
                }
            }
        }

        private int GetShopID(string customerID)
        {
            int index = 0;
            _query = $"SELECT id_boutique FROM client WHERE courriel=\"{customerID}\";";
            _command = new MySqlCommand(_query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            index = Convert.ToInt32(_command.ExecuteScalar());
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return index;
        }

        private string GetUserName()
        {
            string userName;
            string query = "SELECT USER();";
            MySqlCommand command = new MySqlCommand(query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            userName = Convert.ToString(command.ExecuteScalar());
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return userName.Split('@')[0];
        }

        private void LoadCustomers(string query = "SELECT * FROM client;")
        {
            dgvCustomer.Rows.Clear();

            // Vérification du nombre de commandes et attribution des statuts de fidélité en conséquence
            // if (GetUserName() == "root") CheckFidelity();

            _query = query;
            _command = new MySqlCommand(_query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _reader = _command.ExecuteReader();
            while (_reader.Read())
                dgvCustomer.Rows.Add(_reader.GetString(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4), _reader.GetString(5), _reader.GetString(6), _reader.GetString(7));

            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }

        private void LoadShopOptions()
        {
            cbShop.SelectedIndex = 0;
            _query = "SELECT nom_boutique FROM boutique;";
            _command = new MySqlCommand(_query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _reader = _command.ExecuteReader();
            while (_reader.Read()) 
                cbShop.Items.Add(_reader.GetString("nom_boutique"));

            if (_connection.State == ConnectionState.Open) _connection.Close();
        }
        #endregion

        #region Evenements
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CustomerForm form = new CustomerForm(this, _connection);
            form.Text = "Ajout d'un client";
            form.btnSave.Visible = true;
            form.btnUpdate.Visible = false;
            if (form.ShowDialog() == DialogResult.OK) LoadCustomers();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous exporter les clients ayant commandé plusieurs fois durant le dernier mois ?",
                "Export des informations de la clientèle",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            { ExportClientsWithMultipleOrdersLastMonthToXML(); }

            if (MessageBox.Show("Voulez-vous exporter les clients n’ayant pas commandé depuis plus de 6 mois ?",
                "Export des informations de la clientèle",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            { ExportClientsNotOrderedLast6MonthsToJSON(); }
        }

        private void cbFidelity_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFidelity.SelectedIndex)
            {
                case 0:
                    _fidelity = "%%";
                    break;

                case 1:
                    _fidelity = "None";
                    break;
                case 2:
                    _fidelity = "Bronze";
                    break;
                case 3:
                    _fidelity = "Or";
                    break;
            }

            if (cbShop.SelectedIndex != 0) LoadCustomers($"SELECT * FROM client WHERE fidelite LIKE \"{_fidelity}\" AND id_boutique={cbShop.SelectedIndex};");
            else LoadCustomers($"SELECT * FROM client WHERE fidelite LIKE \"{_fidelity}\";");
        }

        private void cbShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFidelity.SelectedIndex != 0)
            {
                _query = $"SELECT * FROM client WHERE fidelite LIKE \"{_fidelity}\" AND id_boutique={cbShop.SelectedIndex};";
                LoadCustomers(_query);
            }
            else
            {
                if (cbShop.SelectedIndex != 0) { LoadCustomers($"SELECT * FROM client WHERE id_boutique={cbShop.SelectedIndex}"); }
                else LoadCustomers();
            }
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                EditCustomer(e);
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show($"Êtes-vous sûr(e) de vouloir supprimer ce client : {dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString()} {dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString().ToUpper()} ?",
                    "Supprimer un client", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _query = $"DELETE FROM client WHERE courriel=\"{dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString()}\";";

                    if (_connection.State == ConnectionState.Closed) _connection.Open();
                    _command = new MySqlCommand(_query, _connection);
                    _command.ExecuteNonQuery();
                    if (_connection.State == ConnectionState.Open) _connection.Close();

                    MessageBox.Show("Le client a bien été supprimée.", "BelleFleur");
                    LoadCustomers();
                }
            }
        }

        private void dgvCustomer_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EditCustomer(e);
        }

        private void dgvCustomer_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit" || colName == "Delete") { Cursor = Cursors.Hand; }
        }

        private void dgvCustomer_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit" || colName == "Delete") { Cursor = Cursors.Default; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _searchFilter = tbCustomer.Text;
            LoadCustomers($"SELECT * FROM client WHERE CONCAT(courriel, nom, prenom, no_tel, mdp, adresse_facture, no_carte) LIKE \"%{_searchFilter}%\";");
        }
        #endregion
    }
}
