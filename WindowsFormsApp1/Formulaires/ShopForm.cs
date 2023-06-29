using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1.Formulaires
{
    public partial class ShopForm : Form
    {
        #region Attributs
        private MySqlConnection _connection;
        private MySqlCommand _command;

        public int idShop;

        private string _query;
        #endregion

        #region Constructeur
        public ShopForm(MySqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;
        }
        #endregion

        #region Méthodes
        private bool CheckFields()
        {
            return tbShopName.Text != "" && tbShopAddress.Text != "" && tbShopCity.Text != "";
        }

        private int NumberOfShops()
        {
            int count;
            string query = "SELECT COUNT(*) FROM boutique;";
            MySqlCommand command = new MySqlCommand(query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            count = Convert.ToInt32(command.ExecuteScalar());
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return count;
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
                    if (MessageBox.Show("Êtes-vous sûr(e) de vouloir ajouter cette boutique ?", "Ajout d'une boutique", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _query = "INSERT INTO boutique(id_boutique, nom_boutique, adresse_boutique, ville_boutique)VALUES(@id_boutique, @nom_boutique, @adresse_boutique, @ville_boutique);";
                        _command = new MySqlCommand(_query, _connection);
                        _command.Parameters.AddWithValue("@id_boutique", NumberOfShops() + 1);
                        _command.Parameters.AddWithValue("@nom_boutique", tbShopName.Text);
                        _command.Parameters.AddWithValue("@adresse_boutique", tbShopAddress.Text);
                        _command.Parameters.AddWithValue("@ville_boutique", tbShopCity.Text);

                        if (_connection.State == ConnectionState.Closed) _connection.Open();
                        _command.ExecuteNonQuery();
                        if (_connection.State == ConnectionState.Open) _connection.Close();

                        MessageBox.Show("La boutique a été ajoutée à la base de données.", "BelleFleur");

                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else MessageBox.Show("Les champs doivent être remplis", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "BelleFleur"); }
            finally { _connection.Close(); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckFields())
                {
                    if (MessageBox.Show("Êtes-vous sûr(e) de mettre à jour les infos de cette boutique ?", "Modification d'une boutique", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _query = $"UPDATE boutique SET nom_boutique=@nom_boutique, adresse_boutique=@adresse_boutique, ville_boutique=@ville_boutique WHERE id_boutique={idShop};";
                        _command = new MySqlCommand(_query, _connection);
                        _command.Parameters.AddWithValue("@nom_boutique", tbShopName.Text);
                        _command.Parameters.AddWithValue("@adresse_boutique", tbShopAddress.Text);
                        _command.Parameters.AddWithValue("@ville_boutique", tbShopCity.Text);

                        if (_connection.State == ConnectionState.Closed) _connection.Open();
                        _command.ExecuteNonQuery();
                        if (_connection.State == ConnectionState.Open) _connection.Close();

                        MessageBox.Show("Les informations ont été mises à jour.", "BelleFleur");

                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else MessageBox.Show("Les champs doivent être remplis", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "BelleFleur"); }
            finally { _connection.Close(); }
        }
        #endregion
    }
}
