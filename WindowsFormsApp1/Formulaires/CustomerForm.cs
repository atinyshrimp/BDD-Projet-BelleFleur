using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp1.Pages;

namespace WindowsFormsApp1.Formulaires
{
    public partial class CustomerForm : Form
    {
        #region Attributs
        private MySqlCommand _command;
        private MySqlConnection _connection;
        private MySqlDataReader _reader;

        private string _query;
        #endregion

        #region Constructeur
        public CustomerForm(CustomerModule module, MySqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;

            cbFidelity.Items.AddRange(module.fidelityLevels);
            FillComboBox();
        }
        #endregion

        #region Méthodes
        private bool CheckFields()
        {
            return (tbMail.Text != "" && IsEmail(tbMail.Text) && tbSurname.Text != "" && tbName.Text != "" && tbAddress.Text != "" && tbCardNb.Text != "" && IsCreditCard(tbCardNb.Text) 
                && tbPassword.Text != "" && tbPhone.Text != "" && IsPhoneNumber(tbPhone.Text) && cbFidelity.Text != "" && cbShop.Text != "");
        }

        private void FillComboBox()
        {
            _query = $"SELECT nom_boutique, ville_boutique FROM boutique;";
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _command = new MySqlCommand(_query, _connection);
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                cbShop.Items.Add($"{_reader.GetString(0)} : {_reader.GetString(1)}");
            }
            _reader.Close();
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }

        private bool IsCreditCard(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return false;

            // Expression régulière pour vérifier si le numéro de carte bancaire est valide
            string pattern = @"^(?:4[0-9]{12}(?:[0-9]{3})?          # Visa
                    | 5[1-5][0-9]{14}                  # Mastercard
                    | 3[47][0-9]{13}                   # American Express
                    )$";

            // Vérifier si le numéro de carte bancaire correspond à l'expression régulière
            return Regex.IsMatch(cardNumber, pattern, RegexOptions.IgnorePatternWhitespace);
        }

        private bool IsEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Expression régulière pour vérifier si l'email est valide
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Vérifier si l'email correspond à l'expression régulière
            return Regex.IsMatch(email, pattern);
        }

        private bool IsPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Expression régulière pour vérifier si le numéro de téléphone est valide
            string pattern = @"^(0|\+33|0033)(6|7)([-. ]?[0-9]{2}){4}$";

            // Vérifier si le numéro de téléphone correspond à l'expression régulière
            return Regex.IsMatch(phoneNumber, pattern);
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
                    if (MessageBox.Show("Êtes-vous sûr(e) de vouloir ajouter ce client ?", "Ajout d'un client", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _query = "INSERT INTO client(courriel, nom, prenom, no_tel, mdp, adresse_facture, no_carte, fidelite, id_boutique)VALUES(@courriel, @nom, @prenom, @no_tel, @mdp, @adresse_facture, @no_carte, @fidelite, @id_boutique);";
                        _command = new MySqlCommand(_query, _connection);
                        _command.Parameters.AddWithValue("@courriel", tbMail.Text);
                        _command.Parameters.AddWithValue("@nom", tbSurname.Text);
                        _command.Parameters.AddWithValue("@prenom", tbName.Text);
                        _command.Parameters.AddWithValue("@no_tel", tbPhone.Text);
                        _command.Parameters.AddWithValue("@mdp", tbPassword.Text);
                        _command.Parameters.AddWithValue("@adresse_facture", tbAddress.Text);
                        _command.Parameters.AddWithValue("@no_carte", tbCardNb.Text);
                        _command.Parameters.AddWithValue("@fidelite", cbFidelity.Text);
                        _command.Parameters.AddWithValue("@id_boutique", cbShop.SelectedIndex + 1);

                        if (_connection.State == ConnectionState.Closed) _connection.Open();
                        _command.ExecuteNonQuery();
                        if(_connection.State == ConnectionState.Open) _connection.Close();

                        MessageBox.Show("Le client a été ajouté à la base de données.", "BelleFleur");

                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Les champs doivent être remplis ou au bon format (email et/ou numéro de téléphone).", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BelleFleur");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckFields())
                {
                    if (MessageBox.Show("Êtes-vous sûr(e) de vouloir mettre à jour les infos de ce client ?", "Modification d'un client", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _query = $"UPDATE client SET nom=@nom, prenom=@prenom, no_tel=@no_tel, mdp=@mdp, adresse_facture=@adresse_facture, no_carte=@no_carte, fidelite=\"{cbFidelity.Text}\", id_boutique=@id_boutique WHERE courriel=\"{tbMail.Text}\";";
                        _command = new MySqlCommand(_query, _connection);
                        //_command.Parameters.AddWithValue("@courriel", tbMail.Text);
                        _command.Parameters.AddWithValue("@nom", tbSurname.Text);
                        _command.Parameters.AddWithValue("@prenom", tbName.Text);
                        _command.Parameters.AddWithValue("@no_tel", tbPhone.Text);
                        _command.Parameters.AddWithValue("@mdp", tbPassword.Text);
                        _command.Parameters.AddWithValue("@adresse_facture", tbAddress.Text);
                        _command.Parameters.AddWithValue("@no_carte", tbCardNb.Text);
                        //_command.Parameters.AddWithValue("@fidelite", cbFidelity.Text);
                        _command.Parameters.AddWithValue("@id_boutique", cbShop.SelectedIndex + 1);

                        if (_connection.State == ConnectionState.Closed) _connection.Open();
                        _command.ExecuteNonQuery();
                        if (_connection.State == ConnectionState.Open) _connection.Close();

                        MessageBox.Show("Les informations ont été mises à jour.", "BelleFleur");

                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Les champs doivent être remplis", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BelleFleur");
            }

        }
        #endregion
    }
}
