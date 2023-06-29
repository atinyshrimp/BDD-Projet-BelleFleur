using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1.Pages
{
    public partial class StatsModule : Form
    {
        #region Attributs
        private MySqlConnection _connection;
        private MySqlDataReader _reader;
        private MySqlCommand _command;

        private int _currentShop;
        private readonly List<string> _shops;

        private int _currentProduct;
        private readonly List<string> _products;

        private DateTime _startDate;
        private DateTime _endDate;
        private DateMode _dateMode;

        private CheckBox _currentCheckBox;
        #endregion

        #region Propriétés
        public enum DateMode { Custom, Standard }
        #endregion

        #region Constructeur
        public StatsModule(MySqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;

            // Configurer les options pour la boutique choisie
            _currentShop = 0;
            _shops = new List<string>();
            LoadShopOptions();

            // Configurer les options pour les produits analysés
            _currentProduct = 0;
            _products = new List<string>();
            LoadProductOptions();

            // Configurer le graphique des ventes par période
            chart1.Series.Clear();
            chart1.Series.Add("Ventes (en €)");
            chart1.Series["Ventes (en €)"].ChartType = SeriesChartType.Column;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            // Configurer le Chart control
            chart2.Series.Clear();
            chart2.Series.Add("Ventes");
            chart2.Series["Ventes"].ChartType = SeriesChartType.Pie;

            // Période par défaut
            cb30LastDays.Checked = true;
            cb30LastDays.Enabled = false;

            UpdateFigures();
        }
        #endregion

        #region Méthodes
        private void CheckDate(CheckBox button, DateMode dateMode)
        {
            _dateMode = dateMode;
            ToggleCustomControls();

            if (_currentCheckBox == null) _currentCheckBox = button;

            if (_currentCheckBox.Checked && _currentCheckBox != button)
            {
                _currentCheckBox.Checked = false;
                _currentCheckBox.Enabled = true;
                _currentCheckBox = button;
                _currentCheckBox.Enabled = false;
            }
        }

        private void LoadProductOptions()
        {
            string[] products = { "produits", "accessoires", "fleurs", "bouquets" };
            _products.AddRange(products);
        }

        private void LoadShopOptions()
        {
            _shops.Add("Boutiques");

            string query = "SELECT nom_boutique FROM boutique;";
            _command = new MySqlCommand(query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _reader = _command.ExecuteReader();
            while (_reader.Read()) { _shops.Add(_reader.GetString(0)); }
            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }
 
        private void SetDatePeriod(DateTime start, DateTime end)
        {
            if (start < end)
            {
                dtpStart.Value = start;
                _startDate = start;

                _endDate = end;
                dtpEnd.Value = _endDate;
                UpdateFigures();
            }
            else MessageBox.Show("La date de début soit être antérieure à celle de fin", "Entrées invalides", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ToggleCustomControls()
        {
            if (_dateMode == DateMode.Standard)
            {
                dtpStart.Enabled = false;
                dtpEnd.Enabled = false;
                btnCustomPeriod.Enabled = false;
            }
            else
            {
                dtpEnd.Enabled = true;
                dtpStart.Enabled = true;
                btnCustomPeriod.Enabled = true;
            }
        }

        private void UpdateFigures()
        {
            UpdateShopLabel();
            UpdateShopData();
            UpdateRevenue();
            UpdateTopCustomers();
            UpdateOrderChart();
            UpdatePieChart();
        }

        private void UpdateOrderChart()
        {

            chart1.Series.Clear();
            chart1.Series.Add("Ventes (en €)");
            chart1.Series["Ventes (en €)"].ChartType = SeriesChartType.Column;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.Series["Ventes (en €)"].IsValueShownAsLabel = true;

            // Récupérer les données de vente par période de la boutique
            string query = _currentShop != 0 ? 
                            $"SELECT date_commande, SUM(prix_commande) AS total_sales FROM commande NATURAL JOIN client WHERE id_boutique={_currentShop} " +
                            "AND date_commande BETWEEN @date_debut AND @date_fin GROUP BY date_commande ORDER BY date_commande" :
                            "SELECT date_commande, SUM(prix_commande) AS total_sales FROM commande WHERE date_commande BETWEEN @date_debut AND @date_fin GROUP BY date_commande ORDER BY date_commande";

            MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@date_debut", _startDate.AddDays(-1));
            command.Parameters.AddWithValue("@date_fin", _endDate);

            if (_connection.State == ConnectionState.Closed) _connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                chart1.ChartAreas[0].Visible = true;
                while (reader.Read())
                {
                    DateTime date = reader.GetDateTime("date_commande");
                    double totalSales = reader.GetDouble("total_sales");

                    // Ajouter les points de données à la série
                    chart1.Series["Ventes (en €)"].Points.AddXY(date.ToOADate(), totalSales);
                }
            }
            else chart1.ChartAreas[0].Visible = false;
            reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();

            // Formater l'axe des dates
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy";
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
        }

        private void UpdatePieChart()
        {
            // Changer l'affichage
            if (_currentProduct == -1) _currentProduct = _products.Count - 1;
            else if (_currentProduct >= _products.Count) _currentProduct = 0;
            chart2.Titles[0].Text = "Top 5 des " + _products[_currentProduct];

            string filter;
            switch (_currentProduct)
            {
                case 1:
                    filter = "LIKE \"A%\"";
                    break;
                case 2:
                    filter = "LIKE \"F%\"";
                    break;
                case 3:
                    filter = "LIKE \"B%\"";
                    break;
                default:
                    filter = "NOT LIKE \"B%\"";
                    break;
            }


            // Récupérer les données de vente des 5 produits les plus vendus
            string query;
            query = _currentShop != 0 ? "SELECT p.nom_produit, SUM(i.nb_produit) AS quantite_vendue FROM commande c NATURAL JOIN client cl NATURAL JOIN inclut i " +
                                        $"NATURAL JOIN produit p WHERE cl.id_boutique={_currentShop} AND p.id_produit {filter}" +
                                        "AND date_commande BETWEEN @date_debut AND @date_fin GROUP BY p.id_produit ORDER BY quantite_vendue DESC LIMIT 5" :
                                        $"SELECT p.nom_produit, SUM(i.nb_produit) AS quantite_vendue FROM commande c NATURAL JOIN inclut i NATURAL JOIN produit p WHERE p.id_produit {filter}" +
                                        "AND date_commande BETWEEN @date_debut AND @date_fin GROUP BY p.id_produit ORDER BY quantite_vendue DESC LIMIT 5;";
            List<string> produits = new List<string>();
            List<int> ventes = new List<int>();
            using (_connection)
            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@date_debut", _startDate);
                command.Parameters.AddWithValue("@date_fin", _endDate);

                if (_connection.State == ConnectionState.Closed) _connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    produits.Add(reader.GetString("nom_produit"));
                    ventes.Add(reader.GetInt32("quantite_vendue"));
                }
                reader.Close();
            }

            // Ajouter les données au PieChart
            chart2.Series["Ventes"].Points.Clear();
            for (int i = 0; i < produits.Count; i++) chart2.Series["Ventes"].Points.AddXY(produits[i], ventes[i]);

            // Afficher les chiffres des ventes sur les parts du PieChart
            chart2.Series["Ventes"].IsValueShownAsLabel = true;
            chart2.Series["Ventes"].LabelForeColor = Color.SeaShell;
        }

        private void UpdateRevenue()
        {
            string query = _currentShop != 0 ? 
                            $"SELECT SUM(prix_commande) AS chiffre_affaires FROM boutique b NATURAL JOIN client c NATURAL JOIN commande cmd WHERE id_boutique={_currentShop} AND date_commande BETWEEN " +
                            $"@date_debut AND @date_fin GROUP BY id_boutique ORDER BY chiffre_affaires DESC LIMIT 1;" :
                            "SELECT IFNULL(SUM(prix_commande), 0) AS chiffre_affaires FROM commande cmd WHERE date_commande BETWEEN @date_debut AND @date_fin ORDER BY chiffre_affaires DESC LIMIT 1;";
            decimal revenue;
            _command = new MySqlCommand(query, _connection);
            _command.Parameters.AddWithValue("@date_debut", _startDate);
            _command.Parameters.AddWithValue("@date_fin", _endDate);

            if (_connection.State == ConnectionState.Closed) _connection.Open();
            revenue = Math.Round(Convert.ToDecimal(_command.ExecuteScalar()), 2);
            if (_connection.State == ConnectionState.Open) _connection.Close();
            lblRevenue.Text = $"{revenue} €";
        }

        private void UpdateShopData()
        {
            string query = _currentShop != 0 ? 
                            $"SELECT COUNT(*) FROM commande cmd NATURAL JOIN client c WHERE id_boutique={_currentShop} AND date_commande BETWEEN @date_debut AND @date_fin;" :
                            "SELECT COUNT(*) FROM commande cmd WHERE date_commande BETWEEN @date_debut AND @date_fin;";
            int count;
            _command = new MySqlCommand(query, _connection);
            _command.Parameters.AddWithValue("@date_debut", _startDate);
            _command.Parameters.AddWithValue("@date_fin", _endDate);

            if (_connection.State == ConnectionState.Closed) _connection.Open();
            count = Convert.ToInt32(_command.ExecuteScalar());
            if (_connection.State == ConnectionState.Open) _connection.Close();
            lblOrders.Text = count.ToString();

            decimal avg;
            query = _currentShop != 0 ? $"SELECT IFNULL(AVG(prix_commande), 0) FROM commande c NATURAL JOIN client cl NATURAL JOIN inclut i WHERE i.id_produit LIKE \"B%\" AND id_boutique={_currentShop } " +
                                        $"AND date_commande BETWEEN @date_debut AND @date_fin;" :
                                        "SELECT IFNULL(AVG(prix_commande), 0) FROM commande c NATURAL JOIN inclut i WHERE i.id_produit LIKE \"B%\" AND date_commande BETWEEN @date_debut AND @date_fin;";
            _command = new MySqlCommand(query, _connection);
            _command.Parameters.AddWithValue("@date_debut", _startDate);
            _command.Parameters.AddWithValue("@date_fin", _endDate);

            if (_connection.State == ConnectionState.Closed) _connection.Open();
            avg = Math.Round(Convert.ToDecimal(_command.ExecuteScalar()), 2);
            if (_connection.State == ConnectionState.Open) _connection.Close();
            lblAvgBouquet.Text = avg != 0 ? avg.ToString() + " €" : "- €";
        }

        private void UpdateShopLabel()
        {
            if (_currentShop == -1) _currentShop = _shops.Count - 1;
            else if (_currentShop >= _shops.Count) _currentShop = 0;
            lblCurrentShop.Text = _shops[_currentShop];

            string query = "SELECT nom_boutique, ville_boutique, SUM(prix_commande) AS chiffre_affaires FROM boutique b NATURAL JOIN client c NATURAL JOIN commande cmd WHERE date_commande BETWEEN @date_debut AND @date_fin " +
                            "GROUP BY nom_boutique, ville_boutique ORDER BY chiffre_affaires DESC LIMIT 1;";
            _command = new MySqlCommand(query, _connection);
            _command.Parameters.AddWithValue("@date_debut", _startDate);
            _command.Parameters.AddWithValue("@date_fin", _endDate);

            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _reader = _command.ExecuteReader();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    lblBestShop.Text = _reader.GetString(0) + $" ({_reader.GetString(1)})";
                    lblBestRevenue.Text = Math.Round(_reader.GetDecimal(2), 2).ToString() + " €";
                }
            }
            else
            {
                lblBestShop.Text = "-";
                lblBestRevenue.Text = "- €";
            }
            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }

        private void UpdateTopCustomers()
        {
            string query = _currentShop != 0 ? 
                            $"SELECT nom, prenom, nom_boutique, COUNT(*) AS total_orders FROM commande NATURAL JOIN client NATURAL JOIN boutique WHERE id_boutique={_currentShop} AND date_commande BETWEEN " +
                            "@date_debut AND @date_fin GROUP BY courriel ORDER BY total_orders DESC LIMIT 3;" :
                            "SELECT nom, prenom, nom_boutique, COUNT(*) AS total_orders FROM commande NATURAL JOIN client NATURAL JOIN boutique WHERE date_commande BETWEEN @date_debut AND @date_fin " +
                            "GROUP BY courriel ORDER BY total_orders DESC LIMIT 3;";
            _command = new MySqlCommand(query, _connection);
            _command.Parameters.AddWithValue("@date_debut", _startDate);
            _command.Parameters.AddWithValue("@date_fin", _endDate);

            int count = 0;
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                count++;
                switch (count)
                {
                    case 1:
                        lbl1stCustomer.Text = $"{_reader.GetString(0)?.ToUpper()} {_reader.GetString(1)} ({_reader.GetString(2)})";
                        lbl1stOrder.Text = $"{Math.Round(_reader.GetDecimal(3), 2).ToString()} commande(s)";
                        break;

                    case 2:
                        lbl2ndCustomer.Text = $"{_reader.GetString(0)?.ToUpper()} {_reader.GetString(1)} ({_reader.GetString(2)})";
                        lbl2ndOrder.Text = $"{Math.Round(_reader.GetDecimal(3), 2).ToString()} commande(s)";
                        break;
                    case 3:
                        lbl3rdCustomer.Text = $"{_reader.GetString(0)?.ToUpper()} {_reader.GetString(1)} ({_reader.GetString(2)})";
                        lbl3rdOrder.Text = $"{Math.Round(_reader.GetDecimal(3), 2).ToString()} commande(s)";
                        break;
                }
            }
            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();

            switch (count)
            {
                case 0:
                    panel1stCustomer.Visible = false;
                    panel2ndCustomer.Visible = false;
                    panel3rdCustomer.Visible = false;
                    break;

                case 1:
                    panel1stCustomer.Visible = true;
                    panel2ndCustomer.Visible = false;
                    panel3rdCustomer.Visible = false;
                    break;

                case 2:
                    panel1stCustomer.Visible = true;
                    panel2ndCustomer.Visible = true;
                    panel3rdCustomer.Visible = false;
                    break;

                case 3:
                    panel1stCustomer.Visible = true;
                    panel2ndCustomer.Visible = true;
                    panel3rdCustomer.Visible = true;
                    break;
            }
        }
        #endregion

        #region Evenements
        private void btnCustomPeriod_Click(object sender, EventArgs e)
        {
            SetDatePeriod(dtpStart.Value, dtpEnd.Value);
        }

        private void btnNextProduct_Click(object sender, EventArgs e)
        {
            _currentProduct++;
            UpdateFigures();
        }

        private void btnPrevProduct_Click(object sender, EventArgs e)
        {
            _currentProduct--;
            UpdateFigures();
        }

        private void btnNextShop_Click(object sender, EventArgs e)
        {
            _currentShop++;
            UpdateFigures();
        }

        private void btnPrevShop_Click(object sender, EventArgs e)
        {
            _currentShop--;
            UpdateFigures();
        }

        private void cb30LastDays_CheckedChanged(object sender, EventArgs e)
        {
            if (cb30LastDays.Checked)
            {
                CheckDate(cb30LastDays, DateMode.Standard);
                SetDatePeriod(DateTime.Now.AddDays(-30), DateTime.Now);
            }
        }

        private void cb6LastMonths_CheckedChanged(object sender, EventArgs e)
        {
            if (cb6LastMonths.Checked)
            {
                CheckDate(cb6LastMonths, DateMode.Standard);
                SetDatePeriod(DateTime.Now.AddMonths(-6), DateTime.Now);
            }
        }

        private void cb7LastDays_CheckedChanged(object sender, EventArgs e)
        {
            CheckDate(cb7LastDays, DateMode.Standard);
            SetDatePeriod(DateTime.Now.AddDays(-7), DateTime.Now);
        }

        private void cbCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCustom.Checked) CheckDate(cbCustom, DateMode.Custom);
            else ToggleCustomControls();
        }

        private void cbThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (cbThisMonth.Checked)
            {
                CheckDate(cbThisMonth, DateMode.Standard);
                SetDatePeriod(DateTime.Now.AddDays(-(DateTime.Now.Day)), DateTime.Now);
            }
        }
        #endregion
    }
}
