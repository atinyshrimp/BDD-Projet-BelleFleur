using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp1.Pages;

namespace WindowsFormsApp1.Formulaires
{
    public partial class OrderForm : Form
    {
        #region Attributs
        private MySqlCommand _command;
        private readonly MySqlConnection _connection;
        private MySqlDataReader _reader;

        private string _query;
        private readonly string _orderNo;
        private readonly string _orderType;
        private string _courriel;

        private readonly State _formState;

        private float _price;
        private float _finalPrice;
        #endregion

        #region Propriétés
        public MySqlConnection Connection { get => _connection; }

        public Dictionary<string, float> fidelityDiscounts = new Dictionary<string, float> { { "None", 0f }, { "Bronze", 0.05f }, { "Or", 0.15f } };

        public string OrderNo { get => _orderNo; }

        public enum State { Add, Edit }

        #endregion

        #region Constructeur
        public OrderForm(OrderModule module, State state, MySqlConnection connection, string orderNo=null)
        {
            InitializeComponent();
            _connection = connection;
            _formState = state;

            if (_formState == State.Add)
            {
                lblTitle.Text = "Création de la commande";
                Text = "Création d'une commande";
            }
            else Text = "Modification d'une commande";

            _orderNo = GetOrderNo(orderNo);
            lblOrderNo.Text = _orderNo;
            _orderType = GetOrderType();

            cbOrderType.Items.AddRange(module.orderTypes);
            cbOrderState.Items.AddRange(module.orderStates);
            FillComboBox();

            LoadBouquetOptions();
            LoadOrderContent();

            DisplayPrice();
        }
        #endregion

        #region Méthodes
        private bool CheckFields()
        {
            return tbAddress.Text != "" && cbOrderState.Text != "" && cbCustomer.Text != "" && cbOrderType.Text != "" && dgvOrderContent.Rows.Count > 0;
        }

        public void DisplayPrice()
        {
            if (_formState == State.Edit)
            {
                _query = $"SELECT prix_sans_reduc FROM commande WHERE no_commande=\"{_orderNo}\";";
                _command = new MySqlCommand(_query, _connection);
                if (_connection.State == ConnectionState.Closed ) _connection.Open();
                _price = (float)Convert.ToDouble(_command.ExecuteScalar());
                if (_connection.State == ConnectionState.Open) _connection.Close();

                _query = $"SELECT prix_commande FROM commande WHERE no_commande=\"{_orderNo}\";";
                _command = new MySqlCommand(_query, _connection);
                if (_connection.State == ConnectionState.Closed) _connection.Open();
                _finalPrice = (float)Convert.ToDouble(_command.ExecuteScalar());
                if (_connection.State == ConnectionState.Open) _connection.Close();

            }
            else
            {
                _price = 0;
                _finalPrice = 0;

                if (cbOrderType.SelectedIndex == 1) 
                {
                    if (dgvOrderContent.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow row in dgvOrderContent.Rows) _price += (float)Convert.ToDouble(row.Cells[3].Value);
                        _finalPrice = _price * (1 - fidelityDiscounts[GetCustomerFidelity(_courriel)]);
                    }
                }
                else
                {
                    string idBouquet = "B";
                    idBouquet += ParseInt(cbBouquet.SelectedIndex + 1);
                    string query = $"SELECT prix_produit FROM produit WHERE id_produit=\"{idBouquet}\";";
                    _command = new MySqlCommand(query, _connection);
                    if (_connection.State == ConnectionState.Closed) _connection.Open();
                    _price = (float)Convert.ToDouble(_command.ExecuteScalar());
                    if (_connection.State == ConnectionState.Closed) _connection.Open();
                    _finalPrice = _price * (1 - fidelityDiscounts[GetCustomerFidelity(_courriel)]);   
                }
            }

            lblPrice.Text = _price.ToString() + " €";
            lblRealPrice.Text = $"Prix : {_finalPrice} €";

            // Si une réduction a été appliquée, on affiche le deuxième label
            if (_price == _finalPrice) lblPrice.Visible = false;
            else lblPrice.Visible = true;
        }

        private void FillComboBox()
        {
            cbCustomer.Items.Clear();
            _query = "SELECT courriel FROM client;";
            _command = new MySqlCommand(_query, _connection);
            try
            {
                _connection.Open();
                _reader = _command.ExecuteReader();
                while (_reader.Read()) { cbCustomer.Items.Add(_reader.GetString(0)); }
                _reader.Close();
            }
            catch (Exception ex) { MessageBox.Show("Erreur : " + ex.Message); }
            finally { _connection.Close(); }
        }

        private int GetBouquetIndex()
        {
            string productId = "";
            _query = $"SELECT id_produit FROM commande NATURAL JOIN inclut WHERE no_commande = \"{_orderNo}\";";
            _command = new MySqlCommand(_query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _reader = _command.ExecuteReader();
            while (_reader.Read()) { productId = _reader.GetString(0); }
            if (_connection.State == ConnectionState.Open) _connection.Close();
            int res = int.Parse(productId.Substring(1));
            return res;
        }


        private string GetCustomerFidelity(string courriel=null)
        {
            string fidelity = "None";
            if (_formState == State.Edit)
            {
                _query = $"SELECT fidelite FROM client WHERE courriel = (SELECT courriel FROM commande WHERE no_commande = \"{_orderNo}\");";
            }
            else if (_formState == State.Add)
            {
                if (courriel == "") return "None";
                _query = $"SELECT fidelite FROM client WHERE courriel = \"{courriel}\";";
            }
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _command = new MySqlCommand(_query, _connection);
            _reader = _command.ExecuteReader();
            while (_reader.Read()) { fidelity = _reader.GetString(0); }
            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return fidelity;
        }

        private string GetOrderNo(string orderNo)
        {
            string res = "CMD-";
            if (orderNo != null) res = orderNo;
            else
            {
                int nbOrder = 0;
                DateTime dateTime = DateTime.Now;
                res += dateTime.ToString("yyyyMMdd-");
                _query = $"SELECT COUNT(*) FROM commande WHERE no_commande LIKE \"{res}%\";";
                _connection.Open();
                _command = new MySqlCommand(_query, _connection);
                _reader = _command.ExecuteReader();
                while (_reader.Read()) { nbOrder = Convert.ToInt32(_reader.GetString(0)) + 1; }
                _reader.Close();
                _connection.Close();
                res += ParseInt(nbOrder);
            }
            return res;
        }

        private string GetOrderType()
        {
            string res = "";
            _query = $"SELECT type_commande FROM commande WHERE no_commande=\"{_orderNo}\";";
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _command = new MySqlCommand(_query, _connection);
            _reader = _command.ExecuteReader();
            while (_reader.Read()) { res = _reader.GetString(0); }
            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return res;
        }

        private void LoadBouquetOptions()
        {
            _query = "SELECT nom_produit, prix_produit FROM produit WHERE id_produit LIKE \"B%\";";
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _command = new MySqlCommand(_query, _connection);
            _reader = _command.ExecuteReader();
            while (_reader.Read()) { cbBouquet.Items.Add($"{_reader.GetString(0)} : {_reader.GetString(1)} €"); }
            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }

        public void LoadOrderContent()
        {
            dgvOrderContent.Rows.Clear();
            string query;
            query = (_orderType == "standard") ?
                $"SELECT id_composant, nom_produit, qte, qte * prix_produit FROM compose c JOIN produit p ON p.id_produit=c.id_composant WHERE id_bouquet = (SELECT id_produit FROM commande NATURAL JOIN inclut WHERE no_commande = \"{_orderNo}\");" :
                $"SELECT id_produit, nom_produit, nb_produit, nb_produit * prix_produit FROM inclut NATURAL JOIN produit WHERE no_commande=\"{_orderNo}\";";
            
            if (_formState == State.Add) { query = $"SELECT id_composant, nom_produit, qte, prix_produit FROM compose c JOIN produit p ON p.id_produit=c.id_composant WHERE " +
                    $"id_bouquet=\"B{ParseInt(cbBouquet.SelectedIndex + 1)}\";"; }
            _command = new MySqlCommand(query, _connection);

            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _reader = _command.ExecuteReader();
            while (_reader.Read()) { dgvOrderContent.Rows.Add(_reader.GetString(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3)); }
            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }

        private string ParseInt(int n)
        {
            string result = n.ToString();
            while (result.Length < 3) result = "0" + result;
            return result;
        }

        private void UpdateInventory()
        {
            // dans le cas d'un bouquet standard, le TRIGGER se chargera d'annuler le processus de commande s'il n'y en a pas assez en stock
            if (cbOrderType.Text == "standard")
            {
                _query = $"UPDATE stocke SET qte_inv = qte_inv - @qte_cmd WHERE id_boutique=(SELECT id_boutique FROM client WHERE courriel=\"{_courriel}\") AND id_produit=\"B{ParseInt(cbBouquet.SelectedIndex + 1)}\";";
                _command = new MySqlCommand(_query, _connection);
                _command.Parameters.AddWithValue("@qte_cmd", 1);
                if (_connection.State == ConnectionState.Closed) _connection.Open();
                _command.ExecuteNonQuery();
                if (_connection.State == ConnectionState.Open) _connection.Close();
            }
            else
            {
                // dans le cas d'une commande personnalisée, une méthode se charge de gérer le stock
                if (ValidateOrder())
                {
                    foreach (DataGridViewRow row in dgvOrderContent.Rows)
                    {
                        _query = $"UPDATE stocke SET qte_inv = qte_inv - @qte_cmd WHERE id_boutique=(SELECT id_boutique FROM client WHERE courriel=\"{_courriel}\") AND id_produit=\"{row.Cells[0].Value.ToString()}\";";
                        _command = new MySqlCommand(_query, _connection);
                        _command.Parameters.AddWithValue("@qte_cmd", Convert.ToInt32(row.Cells[2].Value.ToString()));
                        if (_connection.State == ConnectionState.Closed) _connection.Open();
                        _command.ExecuteNonQuery();
                        if (_connection.State == ConnectionState.Open) _connection.Close();
                    }
                }
            }
        }

        private bool ValidateOrder()
        {
            Dictionary<string, int> orderContent = new Dictionary<string, int>();
            foreach (DataGridViewRow row in dgvOrderContent.Rows) orderContent.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[2].Value.ToString()));

            // Vérifier la disponibilité du stock pour chaque produit inclus
            foreach (KeyValuePair<string, int> product in orderContent)
            {
                string query = $"SELECT qte_inv FROM stocke WHERE id_produit = \"{ product.Key }\" AND id_boutique = (SELECT id_boutique FROM client WHERE courriel=\"{cbCustomer.Text}\");";
                using (MySqlCommand command = new MySqlCommand(query, _connection))
                {
                    if (_connection.State == ConnectionState.Closed) _connection.Open();
                    int quantiteStock = Convert.ToInt32(command.ExecuteScalar());
                    if (_connection.State == ConnectionState.Open) _connection.Close();

                    // Vérifier si la quantité demandée est supérieure à la quantité en stock . 
                    if (product.Value > quantiteStock) throw new Exception("La commande ne peut pas être effectuée. Stock insuffisant pour le produit : " + product.Key);
                }
            }
            return true;
        }

        #endregion

        #region Evenements
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
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
                    if (MessageBox.Show("Êtes-vous sûr(e) de vouloir valider cette commande ?", "Validation de la commande", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Ajouter le fait de décrémenter la qte de l'inventaire des produits correspondants
                        UpdateInventory();

                        _query = "INSERT INTO commande(no_commande, adresse, type_commande, message, date_commande, date_livraison, etat, prix_sans_reduc, prix_commande, courriel)" +
                            "VALUES(@no_commande, @adresse, @type_commande, @message, @date_commande, @date_livraison, @etat, @prix_sans_reduc, @prix_commande, @courriel);";
                        _command = new MySqlCommand(_query, _connection);
                        _command.Parameters.AddWithValue("@no_commande", _orderNo);
                        _command.Parameters.AddWithValue("@adresse", tbAddress.Text);
                        _command.Parameters.AddWithValue("@type_commande", cbOrderType.Text);
                        _command.Parameters.AddWithValue("@message", tbOrderMsg.Text);
                        _command.Parameters.AddWithValue("@date_commande", DateTime.ParseExact(_orderNo.Split('-')[1], "yyyyMMdd", null));
                        _command.Parameters.AddWithValue("@date_livraison", dateTimePicker1.Value);
                        _command.Parameters.AddWithValue("@etat", cbOrderState.Text);
                        _command.Parameters.AddWithValue("@prix_sans_reduc", _price);
                        _command.Parameters.AddWithValue("@prix_commande", _finalPrice);
                        _command.Parameters.AddWithValue("@courriel", cbCustomer.Text);

                        if (_connection.State == ConnectionState.Closed) _connection.Open();
                        _command.ExecuteNonQuery();
                        if (_connection.State == ConnectionState.Open) _connection.Close();


                        _query = "INSERT INTO inclut(no_commande, id_produit, nb_produit)VALUES(@no_commande, @id_produit, @nb_produit);";
                        if (_connection.State == ConnectionState.Closed) _connection.Open();
                        if (cbOrderType.Text != "standard")
                        {
                            foreach (DataGridViewRow row in dgvOrderContent.Rows)
                            {
                                _command = new MySqlCommand(_query, _connection);
                                _command.Parameters.AddWithValue("@no_commande", _orderNo);
                                _command.Parameters.AddWithValue("@id_produit", row.Cells[0].Value.ToString());
                                _command.Parameters.AddWithValue("@nb_produit", Convert.ToInt32(row.Cells[2].Value.ToString()));
                                _command.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            _command = new MySqlCommand(_query, _connection);
                            _command.Parameters.AddWithValue("@no_commande", _orderNo);
                            _command.Parameters.AddWithValue("@id_produit", "B" + ParseInt(cbBouquet.SelectedIndex + 1));
                            _command.Parameters.AddWithValue("@nb_produit", 1);
                            _command.ExecuteNonQuery();
                        }
                        if (_connection.State == ConnectionState.Open) _connection.Close();


                        MessageBox.Show("La commande a été ajoutée à la base de données.", "BelleFleur");
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else MessageBox.Show("Veuillez remplir tous les champs et/ou vérifier que le contenu de la commande ne soit pas vide.", "BelleFleur");
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
                    if (MessageBox.Show("Êtes-vous sûr(e) de mettre à jour les infos de cette commande ?", 
                        "Modification d'une commande", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _query = $"UPDATE commande SET adresse=@adresse, type_commande=@type_commande, message=@message, date_livraison=@date_livraison," +
                            $" etat=@etat WHERE no_commande=\"{_orderNo}\";";
                        _command = new MySqlCommand(_query, _connection);
                        _command.Parameters.AddWithValue("@adresse", tbAddress.Text);
                        _command.Parameters.AddWithValue("@type_commande", cbOrderType.Text);
                        _command.Parameters.AddWithValue("@message", tbOrderMsg.Text);
                        _command.Parameters.AddWithValue("@date_livraison", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        _command.Parameters.AddWithValue("@etat", cbOrderState.Text);

                        if (_connection.State == ConnectionState.Closed) _connection.Open();
                        _command.ExecuteNonQuery();
                        if (_connection.State == ConnectionState.Open) _connection.Close();

                        MessageBox.Show("Les informations ont été mises à jour.", "BelleFleur");

                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else { MessageBox.Show("Les champs doivent être remplis", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "BelleFleur"); }
        }

        private void cbBouquet_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOrderContent();
            DisplayPrice();
        }

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            _courriel = cbCustomer.Text;
            DisplayPrice();
        }

        private void cbOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvOrderContent.Rows.Clear();
            if (cbOrderType.SelectedIndex != 0) 
            {
                lblBouquet.Visible = false;
                cbBouquet.Visible = false;
                btnAddProduct.Visible = true;
            }
            else
            {
                dgvOrderContent.Rows.Clear();
                lblBouquet.Visible = true;
                cbBouquet.Visible = true;
                cbBouquet.SelectedIndex = 0;
                btnAddProduct.Visible = false;
            }
            LoadOrderContent();
            DisplayPrice();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            if (_formState == State.Edit)
            {
                if (_orderType == "standard") { cbBouquet.SelectedIndex = GetBouquetIndex() - 1; }
                else cbBouquet.SelectedIndex = 0;

                cbOrderType.Enabled = false;
            }
        }
        #endregion
    }
}
