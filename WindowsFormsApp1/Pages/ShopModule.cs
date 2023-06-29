using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.Formulaires;

namespace WindowsFormsApp1.Pages
{
    public partial class ShopModule : Form
    {
        #region Attributs
        private MySqlConnection _connection;
        private MySqlCommand _command;
        private MySqlDataReader _reader;

        private string _searchFilter = "";
        private string _query;
        #endregion

        #region Constructeur
        public ShopModule(MySqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;
            LoadShops();
        }
        #endregion

        #region Méthodes
        private void EditShops(DataGridViewCellEventArgs e)
        {
            ShopForm form = new ShopForm(_connection);
            form.Text = "Modification d'une boutique";

            form.idShop = Convert.ToInt32(dgvShop.Rows[e.RowIndex].Cells[0].Value.ToString());

            form.lblTitle.Text = "Modification d'une boutique";

            form.tbShopName.Text = dgvShop.Rows[e.RowIndex].Cells[1].Value.ToString();
            form.tbShopAddress.Text = dgvShop.Rows[e.RowIndex].Cells[2].Value.ToString();
            form.tbShopCity.Text = dgvShop.Rows[e.RowIndex].Cells[3].Value.ToString();

            form.btnSave.Visible = false;
            form.btnUpdate.Visible = true;

            if (form.ShowDialog() == DialogResult.OK) LoadShops();
        }

        private void EditShops(DataGridViewCellMouseEventArgs e)
        {
            ShopForm form = new ShopForm(_connection);
            form.Text = "Modification d'une boutique";

            form.idShop = Convert.ToInt32(dgvShop.Rows[e.RowIndex].Cells[0].Value.ToString());

            form.lblTitle.Text = "Modification d'une boutique";

            form.tbShopName.Text = dgvShop.Rows[e.RowIndex].Cells[1].Value.ToString();
            form.tbShopAddress.Text = dgvShop.Rows[e.RowIndex].Cells[2].Value.ToString();
            form.tbShopCity.Text = dgvShop.Rows[e.RowIndex].Cells[3].Value.ToString();

            form.btnSave.Visible = false;
            form.btnUpdate.Visible = true;

            if (form.ShowDialog() == DialogResult.OK) LoadShops();
        }

        private void LoadShops(string query = "SELECT b.id_boutique, nom_boutique, adresse_boutique, ville_boutique, COUNT(courriel) FROM boutique b LEFT JOIN client c ON b.id_boutique=c.id_boutique GROUP BY b.id_boutique;")
        {
            dgvShop.Rows.Clear();
            _query = query;
            _command = new MySqlCommand(_query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _reader = _command.ExecuteReader();
            while (_reader.Read())
                dgvShop.Rows.Add(_reader.GetString(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4));

            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }
        #endregion

        #region Evenements
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ShopForm form = new ShopForm(_connection);
            form.Text = "Ajout d'une boutique";
            form.btnSave.Visible = true;
            form.btnUpdate.Visible = false;
            if (form.ShowDialog() == DialogResult.OK) LoadShops();
        }

        private void dgvShop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvShop.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                EditShops(e);
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show($"Êtes-vous sûr(e) de vouloir supprimer cette boutique : {dgvShop.Rows[e.RowIndex].Cells[1].Value} ?",
                    "Supprimer une boutique", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    _query = $"DELETE FROM boutique WHERE id_boutique={dgvShop.Rows[e.RowIndex].Cells[0].Value.ToString()};";

                    if (_connection.State == ConnectionState.Closed) _connection.Open();
                    _command = new MySqlCommand(_query, _connection);
                    _command.ExecuteNonQuery();
                    if (_connection.State == ConnectionState.Open) _connection.Close();

                    MessageBox.Show("La boutique a bien été supprimée.", "BelleFleur");
                    LoadShops();
                }
            }
        }

        private void dgvShop_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EditShops(e);
        }

        private void dgvShop_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvShop.Columns[e.ColumnIndex].Name;
            if (colName == "Edit" || colName == "Delete") { Cursor = Cursors.Hand; } 
        }

        private void dgvShop_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvShop.Columns[e.ColumnIndex].Name;
            if (colName == "Edit" || colName == "Delete") { Cursor = Cursors.Default; }
        }

        private void tbShop_TextChanged(object sender, EventArgs e)
        {
            _searchFilter = tbShop.Text;
            _query = $"SELECT b.id_boutique, nom_boutique, adresse_boutique, ville_boutique, COUNT(courriel) FROM boutique b LEFT JOIN client c ON b.id_boutique=c.id_boutique WHERE CONCAT(nom_boutique, adresse_boutique, ville_boutique) LIKE \"%{_searchFilter}%\" GROUP BY b.id_boutique;";
            LoadShops(_query);
        }
        #endregion
    }
}
