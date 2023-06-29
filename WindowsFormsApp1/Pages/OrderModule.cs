using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp1.Formulaires;

namespace WindowsFormsApp1.Pages
{
    public partial class OrderModule : Form
    {
        #region Attributs
        // MySQL
        private readonly MySqlConnection _connection;
        private MySqlCommand _command;
        private MySqlDataReader _reader;

        // Data
        private string _query;
        private string _searchFilter;

        public string[] orderTypes = { "standard", "personnalisé" };
        public string[] orderStates = { "CAL", "CC", "CL", "CPAV", "VINV" };
        #endregion

        #region Constructeur
        public OrderModule(MySqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;
            _searchFilter = "";

            LoadOrders();
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
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _command.ExecuteNonQuery();
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }

        private void EditOrder(DataGridViewCellEventArgs e)
        {
            OrderForm form = new OrderForm(this, OrderForm.State.Edit, _connection, dgvOrder.Rows[e.RowIndex].Cells[0].Value.ToString());
            form.Text = "Modification d'une commande";

            form.tbAddress.Text = dgvOrder.Rows[e.RowIndex].Cells[4].Value.ToString();
            form.cbOrderType.SelectedIndex = GetIndex(orderTypes, dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString());
            form.tbOrderMsg.Text = dgvOrder.Rows[e.RowIndex].Cells[2].Value.ToString();
            form.cbOrderState.SelectedIndex = GetIndex(orderStates, dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString());
            form.dateTimePicker1.Text = dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString();
            form.cbCustomer.Text = dgvOrder.Rows[e.RowIndex].Cells[7].Value.ToString();

            form.btnSave.Visible = false;
            form.btnUpdate.Visible = true;
            form.cbCustomer.Enabled = false;
            form.btnAddProduct.Visible = false;

            if (form.ShowDialog() == DialogResult.OK) LoadOrders();
        }

        private void EditOrder(DataGridViewCellMouseEventArgs e)
        {
            OrderForm form = new OrderForm(this, OrderForm.State.Edit, _connection, dgvOrder.Rows[e.RowIndex].Cells[0].Value.ToString());
            form.Text = "Modification d'une commande";

            form.tbAddress.Text = dgvOrder.Rows[e.RowIndex].Cells[4].Value.ToString();
            form.cbOrderType.SelectedIndex = GetIndex(orderTypes, dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString());
            form.tbOrderMsg.Text = dgvOrder.Rows[e.RowIndex].Cells[2].Value.ToString();
            form.cbOrderState.SelectedIndex = GetIndex(orderStates, dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString());
            form.dateTimePicker1.Text = dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString();
            form.cbCustomer.Text = dgvOrder.Rows[e.RowIndex].Cells[7].Value.ToString();

            form.btnSave.Visible = false;
            form.btnUpdate.Visible = true;
            form.cbCustomer.Enabled = false;
            form.btnAddProduct.Visible = false;

            if (form.ShowDialog() == DialogResult.OK) LoadOrders();
        }

        private int GetIndex(string[] tab, string s)
        {
            int res = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] == s)
                {
                    res = i;
                    break;
                }
            }
            return res;
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

        private void LoadOrders()
        {
            dgvOrder.Rows.Clear();
            _query = $"SELECT no_commande, type_commande, message, date_livraison, adresse, etat, prix_commande, courriel, SUM(nb_produit) AS nb_produit_total FROM commande NATURAL JOIN inclut " +
                        $"WHERE CONCAT(no_commande, adresse, message, courriel, etat) LIKE \"%{_searchFilter}%\" GROUP BY no_commande ORDER BY no_commande DESC;";
            _command = new MySqlCommand(_query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                dgvOrder.Rows.Add(_reader.GetString(0),_reader.GetString(1), _reader.GetString(2), _reader.GetDateTime(3).ToString("dd/MM/yyyy"),
                                _reader.GetString(4), _reader.GetString(5), _reader.GetString(6), _reader.GetString(7), _reader.GetString(8));
            }
            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }
        #endregion

        #region Evenements
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OrderForm form = new OrderForm(this, OrderForm.State.Add, _connection);
            form.Text = "Ajout d'une commande";
            form.btnSave.Visible = true;
            form.btnUpdate.Visible = false;
            form.cbCustomer.Enabled = true;
            if (form.ShowDialog() == DialogResult.OK)
            {
                // on met à jour la fidélité des clients (surtout celui qui vient de commander); pour ne pas avoir à retourner sur la page "Clients" pour actualiser
                if (GetUserName() == "root") CheckFidelity();
                LoadOrders();
            }
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                EditOrder(e);
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show($"Êtes-vous sûr(e) de vouloir supprimer cette commande : {dgvOrder.Rows[e.RowIndex].Cells[0].Value.ToString()} ?",
                    "Supprimer une commande", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _query = $"DELETE FROM commande WHERE no_commande=\"{dgvOrder.Rows[e.RowIndex].Cells[0].Value.ToString()}\";";

                    if (_connection.State == ConnectionState.Closed) _connection.Open();
                    _command = new MySqlCommand(_query, _connection);
                    _command.ExecuteNonQuery();
                    if (_connection.State == ConnectionState.Open) _connection.Close();

                    MessageBox.Show($"La commande {dgvOrder.Rows[e.RowIndex].Cells[0].Value.ToString()} a bien été supprimée.", "BelleFleur");
                    LoadOrders();
                }
            }
        }

        private void dgvOrder_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EditOrder(e);
        }

        private void dgvOrder_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
            if (colName == "Edit" || colName == "Delete") { Cursor = Cursors.Hand; }
        }

        private void dgvOrder_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
            if (colName == "Edit" || colName == "Delete") { Cursor = Cursors.Default; }
        }

        private void tbOrder_TextChanged(object sender, EventArgs e)
        {
            _searchFilter = tbOrder.Text;
            LoadOrders();
        }
        #endregion
    }
}
