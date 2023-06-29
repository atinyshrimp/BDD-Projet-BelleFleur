using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1.Formulaires
{
    public partial class OrderProductForm : Form
    {
        #region Attributs
        private MySqlCommand _command;
        private readonly MySqlConnection _connection;
        private MySqlDataReader _reader;

        private string _productId;
        private string _query;

        private readonly BouquetForm _bouquetForm;
        private readonly OrderForm _orderForm;

        private enum Type { Bouquet, Order }
        private readonly Type _type;
        #endregion

        #region Constructeur
        public OrderProductForm(Form form)
        {
            InitializeComponent();
            if (form is OrderForm)
            {
                _orderForm = (OrderForm)form;
                _connection = _orderForm.Connection;
                _type = Type.Order;
            }
            else if (form is BouquetForm)
            {
                _bouquetForm = (BouquetForm)form;
                _connection = _bouquetForm.Connection;
                _type = Type.Bouquet;
                label1.Text = "Choix des produits du bouquet";
            }

            LoadProducts();
        }
        #endregion

        #region Méthodes
        private void Add()
        {
            if (cbProducts.Text != "")
            {
                _productId = cbProducts.Text.Substring(1,4);
                _query = $"SELECT nom_produit, prix_produit FROM produit WHERE id_produit=\"{_productId}\";";
                string _productName = "";
                decimal _productPrice = 0;
                _command = new MySqlCommand(_query, _connection);
                if (_connection.State != ConnectionState.Open) { _connection.Open(); }
                _reader = _command.ExecuteReader();
                while (_reader.Read())
                {
                    _productName = _reader.GetString(0);
                    _productPrice = _reader.GetDecimal(1);
                }
                _reader.Close();
                if (_connection.State == ConnectionState.Open) _connection.Close();
                if (_type == Type.Order)
                    _orderForm.dgvOrderContent.Rows.Add(_productId, _productName, numProducts.Value, numProducts.Value * _productPrice);
                else if (_type == Type.Bouquet)
                    _bouquetForm.dgvBouquet.Rows.Add(_productId, _productName, numProducts.Value, numProducts.Value * _productPrice);
            }
        }

        private bool CheckDisponibility(int month, int startMonth, int endMonth)
        {
            bool isAvailable;
            if (endMonth < startMonth) isAvailable = month <= endMonth || month >= startMonth;
            else isAvailable = month >= startMonth && month <= endMonth;
            return isAvailable;
        }

        private void LoadProducts()
        {
            int _currentMonth = DateTime.Now.Month;

            _query = _type == Type.Order ? 
                    "SELECT id_produit, nom_produit, prix_produit, dispo_debut, dispo_fin FROM produit WHERE id_produit NOT LIKE \"B%\";" :
                    "SELECT id_produit, nom_produit, prix_produit, dispo_debut, dispo_fin FROM produit WHERE id_produit LIKE \"F%\";";
            _command = new MySqlCommand(_query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                // afficher seulement les produits disponibles
                if (_type == Type.Order)
                {
                    if (CheckDisponibility(_currentMonth, _reader.GetInt32(3), _reader.GetInt32(4)))
                        cbProducts.Items.Add($"[{_reader.GetString(0)}] {_reader.GetString(1)} : {_reader.GetString(2)} €");
                }
                else
                {
                    // le bouquet ne peut être composé que de fleurs disponibles pendant la période de disponibilité choisie
                    //if (IsPeriodOverlapping((int)_bouquetForm.numBegMonth.Value, (int)_bouquetForm.numEndMonth.Value, _reader.GetInt32(3), _reader.GetInt32(4)))
                    cbProducts.Items.Add($"[{_reader.GetString(0)}] {_reader.GetString(1)} : {_reader.GetString(2)} €");
                }
            }
            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }
        #endregion

        #region Evenements
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
            numProducts.Value = 1;
            if (_type == Type.Order) _orderForm.DisplayPrice();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
