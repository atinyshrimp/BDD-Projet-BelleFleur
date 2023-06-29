using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1.Formulaires
{
    public partial class ProductForm : Form
    {
        #region Attributs
        private readonly MySqlConnection _connection;
        private MySqlCommand _command;
        private MySqlDataReader _reader;

        private string _query, _productId;
        private readonly string[] productTypes = { "Accessoire", "Fleur" };
        private readonly string[] _occasions = { "Baptèmes", "Fête des Mères", "Funérailles", "Mariage", "St Valentin", "Toute occasion" };

        private readonly int nbShops;

        #endregion

        #region Constructeur
        public ProductForm(MySqlConnection connection, string id="")
        {
            InitializeComponent();
            _connection = connection;
            _productId = id;
            // GetProductType();

            cbProductType.Items.AddRange(productTypes);
            cbOccasion.Items.AddRange(_occasions);
            LoadShopsOptions();

            checkGenInfo.Checked = false;
            nbShops = NumberOfShops();
        }
        #endregion

        #region Méthodes
        private bool CheckFields()
        {
            bool res = true;
            if (checkGenInfo.Visible)
            {
                if (!checkGenInfo.Checked) { res = numStock.Value > 0; }
            }
            else { res = cbProductType.Text != "" && tbProductName.Text != "" && cbOccasion.Text != "" && numPrice.Value > 0 && numThreshold.Value > 0; }
            return res;
        }

        private void LoadShopsOptions()
        {
            _query = "SELECT nom_boutique FROM boutique;";
            _command = new MySqlCommand(_query, _connection);
            _connection.Open();
            _reader = _command.ExecuteReader();
            while (_reader.Read())
                cbShop.Items.Add(_reader.GetString("nom_boutique"));
            _reader.Close();
            _connection.Close();
            cbShop.SelectedIndex = 0;
        }

        private int NumberOfShops()
        {
            int num;
            MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM boutique;", _connection);
            if(_connection.State == ConnectionState.Closed) _connection.Open();
            num = Convert.ToInt32(command.ExecuteScalar());
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return num;
        }

        private string ParseInt(int n)
        {
            string result = n.ToString();
            while (result.Length < 3) { result = "0" + result;}
            return result;
        }

        private void SetProductType()
        {
            switch (cbProductType.SelectedIndex)
            {
                case 0:
                    _productId = "A";
                    break;

                case 1:
                    _productId = "B";
                    break;

                case 2: 
                    _productId = "F";
                    break;
            }

            int count;
            string query = $"SELECT COUNT(*) FROM produit WHERE id_produit LIKE \"{_productId}%\";";
            _command = new MySqlCommand(query, _connection);
            _connection.Open();
            count = Convert.ToInt32(_command.ExecuteScalar());
            _connection.Close();

            _productId += ParseInt(count + 1);
        }
        #endregion

        #region Evenements
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckFields())
                {
                    if (MessageBox.Show("Êtes-vous sûr(e) de vouloir ajouter ce produit ?", "Ajout d'un produit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SetProductType();
                        _query = "INSERT INTO produit(id_produit, nom_produit, occasion, prix_produit, dispo_debut, dispo_fin, seuil_produit)VALUES(@id_produit, @nom_produit, @occasion, @prix_produit, @dispo_debut, @dispo_fin, @seuil_produit);";
                        _command = new MySqlCommand(_query, _connection);
                        _command.Parameters.AddWithValue("@id_produit", _productId);
                        _command.Parameters.AddWithValue("@nom_produit", tbProductName.Text);
                        _command.Parameters.AddWithValue("@occasion", cbOccasion.Text);
                        _command.Parameters.AddWithValue("@prix_produit", numPrice.Value);
                        _command.Parameters.AddWithValue("@dispo_debut", numBegMonth.Value);
                        _command.Parameters.AddWithValue("@dispo_fin", numEndMonth.Value);
                        _command.Parameters.AddWithValue("@seuil_produit", numThreshold.Value);

                        _connection.Open();
                        _command.ExecuteNonQuery();
                        _connection.Close();

                        for (int i = 1; i <= nbShops; i++)
                        {
                            _query = "INSERT INTO stocke(id_boutique, id_produit, qte_inv)VALUES(@id_boutique, @id_produit, 0);";
                            _command = new MySqlCommand(_query, _connection);
                            _command.Parameters.AddWithValue("@id_boutique", i);
                            _command.Parameters.AddWithValue("@id_produit", _productId);

                            if (_connection.State == ConnectionState.Closed) _connection.Open();
                            _command.ExecuteNonQuery();
                            if (_connection.State == ConnectionState.Open) _connection.Close();
                        }

                        MessageBox.Show("Le produit a été ajouté à la base de données.", "BelleFleur");

                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else MessageBox.Show("Tous les champs doivent être remplis", "BelleFleur");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckFields())
                {
                    if (MessageBox.Show("Êtes-vous sûr(e) de vouloir mettre à jour les infos de ce produit ?", "Modification d'un produit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (checkGenInfo.Checked)
                        {
                            _query = $"UPDATE produit SET nom_produit=@nom_produit, prix_produit=@prix_produit, occasion=@occasion, dispo_debut=@dispo_debut, dispo_fin=@dispo_fin, " +
                                    $"seuil_produit=@seuil_produit WHERE id_produit=\"{_productId}\";";
                        
                            _command = new MySqlCommand(_query, _connection);
                            _command.Parameters.AddWithValue("@nom_produit", tbProductName.Text);
                            _command.Parameters.AddWithValue("@occasion", cbOccasion.Text);
                            _command.Parameters.AddWithValue("@prix_produit", numPrice.Value);
                            _command.Parameters.AddWithValue("@dispo_debut", numBegMonth.Value);
                            _command.Parameters.AddWithValue("@dispo_fin", numEndMonth.Value);
                            _command.Parameters.AddWithValue("@seuil_produit", numThreshold.Value);
                        }
                        else
                        {
                            _query = $"UPDATE stocke SET qte_inv=@qte_inv WHERE id_produit=\"{_productId}\" AND id_boutique={cbShop.SelectedIndex + 1};";

                            _command = new MySqlCommand(_query, _connection);
                            _command.Parameters.AddWithValue("@qte_inv", numStock.Value);
                        }
                        
                            _connection.Open();
                            _command.ExecuteNonQuery();
                            _connection.Close();

                            MessageBox.Show("Les informations ont été mises à jour", "BelleFleur");

                            this.Close();
                            this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void checkGenInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkGenInfo.Checked)
            {
                tbProductName.Enabled = true;
                cbOccasion.Enabled = true;
                numPrice.Enabled = true;
                numBegMonth.Enabled = true;
                numEndMonth.Enabled = true;
                numStock.Enabled = false;
                numThreshold.Enabled = true;
            }
            else
            {
                tbProductName.Enabled = false;
                cbOccasion.Enabled = false;
                numPrice.Enabled = false;
                numBegMonth.Enabled = false;
                numEndMonth.Enabled = false;
                numStock.Enabled = true;
                numThreshold.Enabled = false;
            }
        }
        #endregion
    }
}
