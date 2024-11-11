using System.Data;
using Npgsql;

namespace csharp.core.config
{
    public class DataAccess : IDataAccess
    {
        private readonly string connectionString = "Host=localhost;Port=5432;Database=ges_dette_boutique_csharp;Username=postgres;Password=Rd5jm7qshp;";
        private NpgsqlConnection connection;

        public NpgsqlConnection GetConnections()
        {
            if (connection == null || connection.State == ConnectionState.Closed)
            {
                try
                {
                    connection = new NpgsqlConnection(connectionString);
                    connection.Open();
                    Console.WriteLine("Connexion à la base de données réussie.");
                    return connection;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur de connexion à la base de données : {ex.Message}");
                    return null;
                }
            }
            else
            {
                return connection;
            }
        }

        public void CloseConnections()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public NpgsqlConnection Connection => connection;
    }

}
