using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1.Pages
{
    public partial class StatsModule : Form
    {
        #region Attributs
        private MySqlConnection _connection;
        private MySqlDataReader _reader;
        private MySqlCommand _command;

        private int _currentShop;
        private List<string> _shops;

        private DateTime _startDate;
        private DateTime _endDate;
        private DateMode _dateMode;

        private CheckBox _currentCheckBox;
        #endregion
        public enum DateMode { Custom, Standard }

        public StatsModule(MySqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;

            //SetDatePeriod();

            _currentShop = 0;
            _shops = new List<string>();
            LoadShopOptions();


            // Configurer le Chart control
            chart2.Series.Clear();
            chart2.Series.Add("Ventes");
            chart2.Series["Ventes"].ChartType = SeriesChartType.Pie;

            cb30LastDays.Checked = true;

            UpdateFigures();
        }

        #region Méthodes
        private void CheckDate(CheckBox button, DateMode dateMode)
        {
            _dateMode = dateMode;
            ToggleCustomControls();

            if (_currentCheckBox == null) _currentCheckBox = button;

            if (_currentCheckBox.Checked && _currentCheckBox != button)
            {
                _currentCheckBox.Checked = false;
                _currentCheckBox = button;
            }
        }

        private void LoadShopOptions()
        {
            _shops.Add("Toutes les boutiques");

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
                _startDate = start;
                dtpStart.Value = _startDate;

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
                btnCustomPeriod.Cursor = Cursors.Default;
            }
            else
            {
                dtpEnd.Enabled = true;
                dtpStart.Enabled = true;
                btnCustomPeriod.Enabled = true;
                btnCustomPeriod.Cursor = Cursors.Hand;
            }
        }

        private void UpdateFigures()
        {
            UpdateShopLabel();
            UpdateShopData();
            UpdatePieChart();
        }

        private void UpdateShopData()
        {
            // Nombre de commandes sur une période de temps
            string query = _currentShop != 0 ? $"SELECT COUNT(*) FROM commande cmd NATURAL JOIN client c WHERE id_boutique={_currentShop} AND date_commande BETWEEN @date_debut AND @date_fin;" :
                                                "SELECT COUNT(*) FROM commande cmd WHERE date_commande BETWEEN @date_debut AND @date_fin;";
            int count;
            _command = new MySqlCommand(query, _connection);
            _command.Parameters.AddWithValue("@date_debut", _startDate);
            _command.Parameters.AddWithValue("@date_fin", _endDate);

            if (_connection.State == ConnectionState.Closed) _connection.Open();
            count = Convert.ToInt32(_command.ExecuteScalar());
            if (_connection.State == ConnectionState.Open) _connection.Close();
            lblOrders.Text = count.ToString();

            // Prix moyen du bouquet
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

            // 3 meilleurs clients sur une période de temps
            List<string> top3Customers = new List<string>();
            query = _currentShop != 0 ?
                    "" :
                    "";
        }

        private void UpdatePieChart()
        {
            // Récupérer les données de vente des 5 produits les plus vendus
            string query;
            query = _currentShop != 0 ? "SELECT p.nom_produit, SUM(i.nb_produit) AS quantite_vendue FROM commande c NATURAL JOIN client cl NATURAL JOIN inclut i " +
                                        $"NATURAL JOIN produit p WHERE cl.id_boutique={_currentShop} AND p.id_produit NOT LIKE \"B%\" AND c.etat=\"CL\" " +
                                        $"AND date_commande BETWEEN @date_debut AND @date_fin GROUP BY p.id_produit ORDER BY quantite_vendue DESC LIMIT 5" :
                                        "SELECT p.nom_produit, SUM(i.nb_produit) AS quantite_vendue FROM commande c NATURAL JOIN inclut i NATURAL JOIN produit p WHERE p.id_produit NOT LIKE \"B%\" AND c.etat=\"CL\" " +
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

        private void UpdateShopLabel()
        {
            if (_currentShop == -1) _currentShop = _shops.Count - 1;
            else if (_currentShop >= _shops.Count) _currentShop = 0;
            lblCurrentShop.Text = _shops[_currentShop];
        }
        #endregion

        #region requêtes
        // Boutique avec le meilleur CA
        /*
         SELECT nom_boutique, SUM(prix_commande) AS chiffre_affaires 
        FROM boutique b
        NATURAL JOIN client c
        NATURAL JOIN commande cmd
        WHERE date_commande BETWEEN @date_debut AND @date_fin
        GROUP BY nom_boutique
        ORDER BY chiffre_affaires DESC
        LIMIT 1;
         */


        // Nombre total de commandes pour une boutique
        /*
         SELECT COUNT(*) FROM commande
        NATURAL JOIN client 
        WHERE id_boutique=1 AND date_commande BETWEEN @date_debut AND @date_fin;
         */


        // 3 meilleurs clients d'une boutique
        /*
         SELECT nom, prenom, SUM(prix_commande) AS chiffre_affaires
        FROM client NATURAL JOIN commande
        WHERE etat='CL'
        GROUP BY nom, prenom
        ORDER BY chiffre_affaires DESC
        LIMIT 3;
         */
        #endregion

        #region Evenements
        private void btnCustomPeriod_Click(object sender, EventArgs e)
        {
            SetDatePeriod(dtpStart.Value, dtpEnd.Value);
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
            if (cbCustom.Checked)
            {
                CheckDate(cbCustom, DateMode.Custom);
            }
        }

        private void cbThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (cbThisMonth.Checked)
            {
                CheckDate(cbThisMonth, DateMode.Standard);
                SetDatePeriod(DateTime.Now.AddDays(-(DateTime.Now.Day - 1)), DateTime.Now);
            }
        }
        #endregion
    }
}
