using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using WindowsFormsApp1.Formulaires;

namespace WindowsFormsApp1.Pages
{
    public partial class ProductModule : Form
    {
        #region Attributs
        private MySqlCommand _command;
        private MySqlConnection _connection;
        private MySqlDataReader _reader;

        private string _searchFilter;
        private string _query;

        private CultureInfo _culture = CultureInfo.CreateSpecificCulture("fr-FR");
        #endregion

        #region Constructeur
        public ProductModule(MySqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;

            _query = $"SELECT p.id_produit, p.nom_produit, p.occasion, p.dispo_debut, p.dispo_fin, p.prix_produit, COALESCE(s.qte_inv, 0) FROM produit p LEFT JOIN stocke s ON p.id_produit = s.id_produit AND s.id_boutique = 1 ORDER BY p.id_produit;";
            LoadProducts(_query);

            string[] choices = { "Tout", "Accessoires", "Fleurs", "Bouquets" };
            cbProduct.Items.AddRange(choices);

            LoadShopOptions();

            _searchFilter = "";
        }
        #endregion

        #region Méthodes
        private void AddProduct()
        {
            if (cbProduct.SelectedIndex != 3)
            {
                ProductForm form = new ProductForm(_connection);
                form.Text = "Ajout d'un produit";
                form.checkGenInfo.Visible = false;

                form.tbProductName.Enabled = true;
                form.cbOccasion.Enabled = true;
                form.cbShop.Enabled = false;
                form.numBegMonth.Enabled = true;
                form.numEndMonth.Enabled = true;
                form.numPrice.Enabled = true;
                form.numStock.Enabled = false;
                form.numThreshold.Enabled = true;

                form.btnSave.Visible = true;
                form.btnUpdate.Visible = false;

                if (form.ShowDialog() == DialogResult.OK) LoadProducts(_query);
            }
            else
            {
                BouquetForm form = new BouquetForm(_connection, BouquetForm.State.Add);
                form.Text = "Ajout d'un bouquet";
                form.btnSave.Visible = true;
                form.btnUpdate.Visible = false;
                if (form.ShowDialog() == DialogResult.OK) LoadProducts(_query);
            }
        }

        private void EditProduct(DataGridViewCellEventArgs e)
        {
            // Si on ne se trouve pas sur la page des bouquets
            if (cbProduct.SelectedIndex != 3)
            {
                string productId = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
                ProductForm form = new ProductForm(_connection, productId);
                form.lblTitle.Text = "Modification d'un produit";
                form.Text = form.lblTitle.Text;

                int typeIndex = 0;
                switch (productId[0])
                {
                    case 'A':
                        typeIndex = 0; break;
                    case 'F': typeIndex = 1; break;
                }
                form.cbProductType.SelectedIndex = typeIndex;
                form.cbProductType.Enabled = false;

                form.cbShop.SelectedIndex = cbStock.SelectedIndex;

                form.tbProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();

                form.cbOccasion.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();

                form.numPrice.Value = Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString());

                form.numStock.Value = GetStock(productId);

                form.numThreshold.Value = GetThreshold(productId);

                DateTime beg = DateTime.ParseExact(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString(), "MMMM", _culture);
                DateTime end = DateTime.ParseExact(dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString(), "MMMM", _culture);
                form.numBegMonth.Value = beg.Month;
                form.numEndMonth.Value = end.Month;

                form.btnSave.Visible = false;
                form.btnUpdate.Visible = true;

                form.checkGenInfo.Enabled = true;

                if (form.ShowDialog() == DialogResult.OK) LoadProducts(_query);
            }
            else
            {
                string bouquetId = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
                BouquetForm form = new BouquetForm(_connection, BouquetForm.State.Edit, bouquetId);
                form.Text = "Modification d'un bouquet";

                form.cbShop.SelectedIndex = cbStock.SelectedIndex;

                form.tbBouquetName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();

                form.cbOccasion.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();

                form.numPrice.Value = Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString());

                form.numStock.Value = GetStock(bouquetId);

                form.numThreshold.Value = GetThreshold(bouquetId);

                DateTime beg = DateTime.ParseExact(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString(), "MMMM", _culture);
                DateTime end = DateTime.ParseExact(dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString(), "MMMM", _culture);
                form.numBegMonth.Value = beg.Month;
                form.numEndMonth.Value = end.Month;

                form.btnSave.Visible = false;
                form.btnUpdate.Visible = true;

                form.checkGenInfo.Enabled = true;

                if (form.ShowDialog() == DialogResult.OK) LoadProducts(_query);
            }

        }

        private void EditProduct(DataGridViewCellMouseEventArgs e)
        {
            // Si on ne se trouve pas sur la page des bouquets
            if (cbProduct.SelectedIndex != 3)
            {
                string productId = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
                ProductForm form = new ProductForm(_connection, productId);
                form.lblTitle.Text = "Modification d'un produit";
                form.Text = form.lblTitle.Text;

                int typeIndex = 0;
                switch (productId[0])
                {
                    case 'A':
                        typeIndex = 0; break;
                    case 'F': typeIndex = 1; break;
                }
                form.cbProductType.SelectedIndex = typeIndex;
                form.cbProductType.Enabled = false;

                form.cbShop.SelectedIndex = cbStock.SelectedIndex;

                form.tbProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();

                form.cbOccasion.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();

                form.numPrice.Value = Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString());

                form.numStock.Value = GetStock(productId);

                form.numThreshold.Value = GetThreshold(productId);

                DateTime beg = DateTime.ParseExact(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString(), "MMMM", _culture);
                DateTime end = DateTime.ParseExact(dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString(), "MMMM", _culture);
                form.numBegMonth.Value = beg.Month;
                form.numEndMonth.Value = end.Month;

                form.btnSave.Visible = false;
                form.btnUpdate.Visible = true;

                form.checkGenInfo.Enabled = true;

                if (form.ShowDialog() == DialogResult.OK) LoadProducts(_query);
            }
            else
            {
                string bouquetId = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
                BouquetForm form = new BouquetForm(_connection, BouquetForm.State.Edit, bouquetId);
                form.Text = "Modification d'un bouquet";

                form.cbShop.SelectedIndex = cbStock.SelectedIndex;

                form.tbBouquetName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();

                form.cbOccasion.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();

                form.numPrice.Value = Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString());

                form.numStock.Value = GetStock(bouquetId);

                form.numThreshold.Value = GetThreshold(bouquetId);

                DateTime beg = DateTime.ParseExact(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString(), "MMMM", _culture);
                DateTime end = DateTime.ParseExact(dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString(), "MMMM", _culture);
                form.numBegMonth.Value = beg.Month;
                form.numEndMonth.Value = end.Month;

                form.btnSave.Visible = false;
                form.btnUpdate.Visible = true;

                form.checkGenInfo.Enabled = true;

                if (form.ShowDialog() == DialogResult.OK) LoadProducts(_query);
            }

        }

        private int GetStock(string id)
        {
            int stock;
            string query = $"SELECT qte_inv FROM stocke WHERE id_boutique={cbStock.SelectedIndex + 1} AND id_produit=\"{id}\"";
            _command = new MySqlCommand(query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            stock = Convert.ToInt32(_command.ExecuteScalar());
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return stock;
        }

        private int GetThreshold(string id)
        {
            int threshold;
            string query = $"SELECT seuil_produit FROM produit WHERE id_produit=\"{id}\";";
            _command = new MySqlCommand(query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            threshold = Convert.ToInt32(_command.ExecuteScalar());
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return threshold;
        }

        private void LoadShopOptions()
        {
            _query = "SELECT nom_boutique FROM boutique;";
            _command = new MySqlCommand(_query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _reader = _command.ExecuteReader();
            while (_reader.Read())
                cbStock.Items.Add(_reader.GetString("nom_boutique"));
            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
            cbStock.SelectedIndex = 0;
            cbProduct.SelectedIndex = 0;
        }

        private void LoadProducts(string query)
        {
            dgvProduct.Rows.Clear();
            _query = query;
            _command = new MySqlCommand(_query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            { 
                dgvProduct.Rows.Add(_reader.GetString(0), _reader.GetString(1), _reader.GetString(2), _culture.DateTimeFormat.GetMonthName(_reader.GetInt32(3)), _culture.DateTimeFormat.GetMonthName(_reader.GetInt32(4)), _reader.GetString(5), _reader.GetString(6));
            }
            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }
        #endregion

        #region Evenements
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProduct();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("L'ajout d'un produit initialise le stock de ce dernier à 0 pour chaque boutique.\nLa quantité par boutique est à modifier ultérieurement.\n\n" +
                            "La gestion des bouquets se fait à part des autres produits. Veuillez sélectionner l'onglet \"Bouquets\" pour ajouter et/ou modifier les différents bouquets.", 
                            "Informations sur les produits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbProduct.SelectedIndex)
            {
                case 1:
                    _query = $"SELECT p.id_produit, p.nom_produit, p.occasion, p.dispo_debut, p.dispo_fin, p.prix_produit, " +
                        $"COALESCE(s.qte_inv, 0) FROM produit p LEFT JOIN stocke s ON p.id_produit = s.id_produit AND " +
                        $"s.id_boutique ={cbStock.SelectedIndex + 1} WHERE p.id_produit LIKE \"A%\" AND CONCAT(p.id_produit, nom_produit) LIKE \"%{_searchFilter}%\" ORDER BY p.id_produit;";
                    LoadProducts(_query);
                    break;

                case 2:
                    _query = $"SELECT p.id_produit, p.nom_produit, p.occasion, p.dispo_debut, p.dispo_fin, p.prix_produit, " +
                        $"COALESCE(s.qte_inv, 0) FROM produit p LEFT JOIN stocke s ON p.id_produit = s.id_produit AND " +
                        $"s.id_boutique ={cbStock.SelectedIndex + 1} WHERE p.id_produit LIKE \"F%\" AND CONCAT(p.id_produit, nom_produit) LIKE \"%{_searchFilter}%\" ORDER BY p.id_produit;";
                    LoadProducts(_query);
                    break;

                case 3:
                    _query = $"SELECT p.id_produit, nom_produit, occasion, dispo_debut, dispo_fin, prix_produit, IFNULL(qte_inv, 0) FROM produit p LEFT JOIN stocke s ON p.id_produit=s.id_produit WHERE s.id_boutique={cbStock.SelectedIndex + 1} AND p.id_produit LIKE \"B%\" AND CONCAT(p.id_produit, nom_produit) LIKE \"%{_searchFilter}%\" ORDER BY p.id_produit;";
                    LoadProducts(_query);
                    break;

                default:
                    _query = $"SELECT p.id_produit, p.nom_produit, p.occasion, p.dispo_debut, p.dispo_fin, p.prix_produit, COALESCE(s.qte_inv, 0) FROM produit p LEFT JOIN stocke s ON p.id_produit = s.id_produit AND s.id_boutique ={cbStock.SelectedIndex + 1} WHERE CONCAT(p.id_produit, nom_produit) LIKE \"%{_searchFilter}%\" AND p.id_produit NOT LIKE \"B%\" ORDER BY p.id_produit;";
                    LoadProducts(_query);
                    break;
            }
        }

        private void cbStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbProduct_SelectedIndexChanged(sender, e);
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                EditProduct(e);
            }
            else if (colName == "Delete")
            {
                try
                {
                    if (MessageBox.Show($"Êtes-vous sûr(e) de vouloir supprimer ce produit : {dgvProduct.Rows[e.RowIndex].Cells[1].Value} ?",
                        "Supprimer un produit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string query = $"DELETE FROM produit WHERE id_produit=\"{dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString()}\";";

                        if (_connection.State == ConnectionState.Closed) _connection.Open();
                        _command = new MySqlCommand(query, _connection);
                        _command.ExecuteNonQuery();
                        if (_connection.State == ConnectionState.Open) _connection.Close();

                        MessageBox.Show("Le produit a bien été supprimé.", "BelleFleur");
                        LoadProducts(_query);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Erreur : " + ex.Message); }
            }
        }

        private void dgvProduct_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EditProduct(e);
        }

        private void dgvProduct_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit" || colName == "Delete") { Cursor = Cursors.Hand; }
        }

        private void dgvProduct_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit" || colName == "Delete") { Cursor = Cursors.Default; }
        }

        private void tbProduct_TextChanged(object sender, EventArgs e)
        {
            _searchFilter = tbProduct.Text;
            cbProduct_SelectedIndexChanged(sender, e);
        }
        #endregion
    }
}
