using System;
using MySql.Data.MySqlClient;

namespace ConsoleApp1
{
    internal class Program
    {
        static void idk(MySqlConnection conn)
        {
            string username = "", password = "";

            Console.Write("Entrer un nom d'utilisateur : ");
            username = Console.ReadLine();
            Console.Write("Entrer votre mot de passe : ");
            password = Console.ReadLine();

            //MySqlConnection conn = new MySqlConnection();
            string connectionString = $"SERVER=localhost;PORT=3306;DATABASE=fleurs;UID={username};PASSWORD={password};";
            try
            {
                conn = new MySqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Open();
            string commandText = "SELECT * FROM client;";
            MySqlCommand command = new MySqlCommand(commandText, conn);
            MySqlDataReader reader = command.ExecuteReader();
            string courriel, nom, prenom, no_tel, mdp, adresse, no_carte;
            while (reader.Read())
            {
                courriel = reader.GetString(0);
                nom = reader.GetString(1);
                prenom = reader.GetString(2);
                no_tel = reader.GetString(3);
                mdp = reader.GetString(4);
                adresse = reader.GetString(5);
                no_carte = reader.GetString(6);
                Console.WriteLine($"{courriel}\t{nom}\t{prenom}\t{no_tel}\t{mdp}\t{adresse}\t{no_carte}");
            }
            conn.Close();
        }

        static void fill_db(string path, MySqlConnection connection)
        {
            string[] lines = File.ReadAllLines(path);
            foreach(string line in lines)
            {
                string[] columns = line.Split(';');
                MySqlCommand command = new MySqlCommand(
                    $"INSERT INTO `fleurs`.`client` (`courriel`,`nom`,`prenom`,`no_tel`,`mdp`,`adresse_facture`,`no_carte`,`fidelite`) VALUES (\'{columns[0]}\',\'{columns[1]}\',\'{columns[2]}\',\'{columns[3]}\',\'{columns[4]}\',\'{columns[5]}\',\'{columns[6]}\', \'{columns[7]}\');",
                    connection);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Nom d'utilisateur : ");
            string username = Console.ReadLine();

            Console.Write("Mot de passe : ");
            string password = Console.ReadLine();

            string connectionString = $"SERVER=localhost;PORT=3306;DATABASE=fleurs;UID={username};PASSWORD={password};";
            MySqlConnection? connection = null;

            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connexion réussie !");
                try
                {
                    fill_db("clients.csv", connection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur de connexion : " + e.Message);
            }
            finally
            {
                connection?.Close();
            }


            Console.WriteLine("fin des opérations");
            Console.WriteLine();

        }
    }
}