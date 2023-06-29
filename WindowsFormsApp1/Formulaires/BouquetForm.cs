using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1.Formulaires
{
    public partial class BouquetForm : Form
    {
        #region Attributs
        private MySqlCommand _command;
        private readonly MySqlConnection _connection;

        private readonly int nbShops;

        private readonly string[] _occasions = { "Baptèmes", "Fête des Mères", "Funérailles", "Mariage", "St Valentin", "Toute occasion" };

        private readonly string _idBouquet;
        #endregion

        #region Propriétés
        public MySqlConnection Connection { get => _connection; }
        public enum State { Add, Edit }
        private readonly State _state;
        #endregion

        #region Constructeur
        public BouquetForm(MySqlConnection connection, State state, string idBouquet=null)
        {
            InitializeComponent();

            _connection = connection;
            _state = state;

            nbShops = NumberOfShops();
            cbOccasion.Items.AddRange(_occasions);
            LoadShopOptions();

            // Mise en page de l'entête du form
            _idBouquet = idBouquet ?? CreateBouquetId();
            lblTitle.Text = state == State.Add ? "Création d'un bouquet" : "Modification d'un bouquet";
            lblOrderNo.Text = _idBouquet;

            // Masquer la Checkbox et l'option d'ajouter des produits si on est en mode création
            checkGenInfo.Visible = _state != State.Add;
            btnAddProduct.Visible = _state == State.Add;

            if (_state == State.Add) ToggleControls(true);
            else LoadBouquet();
        }
        #endregion

        #region Méthodes
        private bool CheckFields()
        {
            bool res = true;
            if (checkGenInfo.Visible)
                if (!checkGenInfo.Checked) res = numStock.Value > 0;
                else res = tbBouquetName.Text != "" && cbOccasion.Text != "" && numPrice.Value > 0 && dgvBouquet.Rows.Count > 0;
            return res;
        }

        private string CreateBouquetId()
        {
            string res = "B";
            string query = "SELECT COUNT(*) FROM produit WHERE id_produit LIKE \"B%\"";
            int count;
            _command = new MySqlCommand(query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            count = Convert.ToInt32(_command.ExecuteScalar());
            if (_connection.State == ConnectionState.Open) _connection.Close();
            res += ParseInt(count + 1);
            return res;
        }

        private void LoadBouquet()
        {
            dgvBouquet.Rows.Clear();
            string query = $"SELECT id_composant, nom_produit, qte, prix_produit FROM compose c JOIN produit p ON c.id_composant=p.id_produit WHERE c.id_bouquet=\"{_idBouquet}\" ORDER BY id_composant;";
            _command = new MySqlCommand(query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            MySqlDataReader reader = _command.ExecuteReader();
            while (reader.Read()) { dgvBouquet.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)); }
            reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }

        private void LoadShopOptions()
        {
            string query = "SELECT nom_boutique FROM boutique;";
            _command = new MySqlCommand(query, _connection);
            _connection.Open();
            MySqlDataReader reader = _command.ExecuteReader();
            while (reader.Read())
                cbShop.Items.Add(reader.GetString("nom_boutique"));
            reader.Close();
            _connection.Close();
        }

        private int NumberOfShops()
        {
            int num = 0;
            MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM boutique;", _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            num = Convert.ToInt32(command.ExecuteScalar());
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return num;
        }

        private string ParseInt(int n)
        {
            string result = n.ToString();
            while (result.Length < 3) result = "0" + result;
            return result;
        }

        private void ToggleControls(bool isChecked)
        {
            if (isChecked)
            {
                tbBouquetName.Enabled = true;
                cbOccasion.Enabled = true;
                numPrice.Enabled = true;
                numBegMonth.Enabled = true;
                numEndMonth.Enabled = true;
                numStock.Enabled = false;
                numThreshold.Enabled = true;
            }
            else
            {
                tbBouquetName.Enabled = false;
                cbOccasion.Enabled = false;
                numPrice.Enabled = false;
                numBegMonth.Enabled = false;
                numEndMonth.Enabled = false;
                numStock.Enabled = true;
                numThreshold.Enabled = false;
            }
        }
        #endregion

        #region Evenements
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
/*            if (MessageBox.Show("Veillez à bien entrer la période de disponibilité voulue, les produits affichés pour composer le bouquet seront affichés en conséquence.", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                OrderProductForm form = new OrderProductForm(this);
                form.ShowDialog();
            }
*/
            OrderProductForm form = new OrderProductForm(this);
            form.ShowDialog();
        }

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
                    if (MessageBox.Show("Êtes-vous sûr(e) de vouloir créer ce bouquet ?", "Validation de la création du bouquet", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string query = "INSERT INTO produit(id_produit, nom_produit, occasion, prix_produit, dispo_debut, dispo_fin, seuil_produit)VALUES" +
                                        "(@id_produit, @nom_produit, @occasion, @prix_produit, @dispo_debut, @dispo_fin, @seuil_produit);";
                        _command = new MySqlCommand(query, _connection);
                        _command.Parameters.AddWithValue("@id_produit", _idBouquet);
                        _command.Parameters.AddWithValue("@nom_produit", tbBouquetName.Text);
                        _command.Parameters.AddWithValue("@occasion", cbOccasion.Text);
                        _command.Parameters.AddWithValue("@prix_produit", numPrice.Value);
                        _command.Parameters.AddWithValue("@dispo_debut", numBegMonth.Value);
                        _command.Parameters.AddWithValue("@dispo_fin", numEndMonth.Value);
                        _command.Parameters.AddWithValue("@seuil_produit", numThreshold.Value);


                        if (_connection.State == ConnectionState.Closed) _connection.Open();
                        _command.ExecuteNonQuery();
                        if (_connection.State == ConnectionState.Open) _connection.Close();


                        query = "INSERT INTO compose(id_bouquet, id_composant, qte)VALUES(@id_bouquet, @id_composant, @qte);";
                        if (_connection.State == ConnectionState.Closed) _connection.Open();
                        foreach (DataGridViewRow row in dgvBouquet.Rows)
                        {
                            _command = new MySqlCommand(query, _connection);
                            _command.Parameters.AddWithValue("@id_bouquet", _idBouquet);
                            _command.Parameters.AddWithValue("@id_composant", row.Cells[0].Value.ToString());
                            _command.Parameters.AddWithValue("@qte", Convert.ToInt32(row.Cells[2].Value.ToString()));
                            _command.ExecuteNonQuery();
                        }
                        if (_connection.State == ConnectionState.Open) _connection.Close();

                        // Mise à zéro du stock du bouquet dans chaque boutique
                        query = $"INSERT INTO stocke (id_boutique, id_produit, qte_inv)VALUES(@id_boutique, @id_produit, 0);";
                        if (_connection.State == ConnectionState.Closed) _connection.Open();
                        for (int i = 1; i <= nbShops; i++)
                        {
                            _command = new MySqlCommand(query, _connection);
                            _command.Parameters.AddWithValue("@id_boutique", i);
                            _command.Parameters.AddWithValue("@id_produit", _idBouquet);
                            _command.ExecuteNonQuery();
                        }
                        if (_connection.State == ConnectionState.Open) _connection.Close();

                        // Ajouter le fait de décrémenter la qte de l'inventaire des produits correspondants

                        MessageBox.Show("Le bouquet a été ajouté à la base de données.", "BelleFleur");
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else MessageBox.Show("Les champs doivent être remplis et/ou le contenu du bouquet doit contenir au moins un produit", "BelleFleur");
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
                            string query = $"UPDATE produit SET nom_produit=@nom_produit, prix_produit=@prix_produit, occasion=@occasion, dispo_debut=@dispo_debut, " +
                                            $"dispo_fin=@dispo_fin, seuil_produit=@seuil_produit WHERE id_produit=\"{_idBouquet}\";";

                            _command = new MySqlCommand(query, _connection);
                            _command.Parameters.AddWithValue("@nom_produit", tbBouquetName.Text);
                            _command.Parameters.AddWithValue("@occasion", cbOccasion.Text);
                            _command.Parameters.AddWithValue("@prix_produit", numPrice.Value);
                            _command.Parameters.AddWithValue("@dispo_debut", numBegMonth.Value);
                            _command.Parameters.AddWithValue("@dispo_fin", numEndMonth.Value);
                            _command.Parameters.AddWithValue("@seuil_produit", numThreshold.Value);
                        }
                        else
                        {
                            string query = $"UPDATE stocke SET qte_inv=@qte_inv WHERE id_produit=\"{_idBouquet}\" AND id_boutique={cbShop.SelectedIndex + 1};";

                            _command = new MySqlCommand(query, _connection);
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
                else MessageBox.Show("Tous les champs doivent être remplis", "BelleFleur");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void checkGenInfo_CheckedChanged(object sender, EventArgs e)
        {
            ToggleControls(checkGenInfo.Checked);
        }
        #endregion
    }
}