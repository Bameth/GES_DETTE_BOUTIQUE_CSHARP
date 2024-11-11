using csharp.entities;
using csharp.core.config;
using csharp.core.interfaces;
using Dapper;
using System.Data;
using System.Collections.Generic;
using Npgsql;

namespace csharp.repository.DB
{
    public class ClientRepositoryBdImpl : IClientRepository
    {
        private readonly IDataAccess dataAccess;

        public ClientRepositoryBdImpl(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public int Count()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT COUNT(*) FROM \"client\"";
                return db.ExecuteScalar<int>(query);
            }
        }

        // Supprimer un client par son ID
        public bool Delete(int id)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "DELETE FROM \"client\" WHERE \"id\" = @Id";
                var result = db.Execute(query, new { Id = id });
                return result > 0;
            }
        }

        public List<Client> FindAllClientWithAccount()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM \"client\" WHERE \"user_id\" IS NOT NULL";
                return db.Query<Client>(query).ToList();
            }
        }

        // Insérer un nouveau client
        public bool Insert(Client client)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = @"
                    INSERT INTO client (adresse, phone, surname, user_id, createdat, updatedat)
                    VALUES (@Addresse, @Phone, @Surname, @User, @CreatedAt, @UpdatedAt)";

                client.CreatedAt = DateTime.Now;
                client.UpdatedAt = DateTime.Now;


                var result = db.Execute(query, client);
                return result > 0;
            }
        }

        public List<Client> SelectAll()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM \"client\"";
                return db.Query<Client>(query).ToList();
            }
        }

        public Client SelectById(int id)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM \"client\" WHERE \"id\" = @Id";
                return db.QueryFirstOrDefault<Client>(query, new { Id = id });
            }
        }

        public Client SelectByPhone(string phone)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM \"client\" WHERE \"phone\" = @Phone";
                return db.QueryFirstOrDefault<Client>(query, new { Phone = phone });
            }
        }

        public Client SelectByUserId(int userId)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM \"client\" WHERE \"user_id\" = @UserId";
                return db.QueryFirstOrDefault<Client>(query, new { UserId = userId });
            }
        }

        public bool Update(Client client)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = @"
                    UPDATE client
                    SET adresse = @Adresse, phone = @Phone, surname = @Surname, updatedat = @UpdatedAt
                    WHERE id = @Id";

                client.UpdatedAt = DateTime.Now;

                var result = db.Execute(query, client);
                return result > 0;
            }
        }
    }
}
