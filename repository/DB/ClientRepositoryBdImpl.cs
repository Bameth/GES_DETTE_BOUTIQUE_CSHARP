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
                const string query = "SELECT * FROM \"client\" WHERE \"userid\" IS NOT NULL";
                return db.Query<Client>(query).ToList();
            }
        }

        public bool Insert(Client objet)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = @"
                INSERT INTO client (address, phone, surname, createdat, updatedat)
                VALUES (@Address, @Phone, @Surname, @CreatedAt, @UpdatedAt);";
                objet.CreatedAt = DateTime.UtcNow;
                objet.UpdatedAt = DateTime.UtcNow;

                var parameters = new
                {
                    Address = objet.Address,  // Correcting the parameter name to Address
                    Phone = objet.Phone,
                    Surname = objet.Surname,
                    CreatedAt = objet.CreatedAt,
                    UpdatedAt = objet.UpdatedAt
                };
                var result = db.Execute(query, objet);
                return result > 0;
            }
        }

        // Insérer un nouveau client
        public bool InsertClientUser(Client client)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                // Vérifier si l'utilisateur existe, si non, on l'insère d'abord
                if (client.User == null)
                {
                    throw new InvalidOperationException("L'utilisateur n'est pas créé. Vous devez d'abord créer l'utilisateur.");
                }

                // 1. Insérer l'utilisateur dans la table "user" et récupérer l'ID généré
                const string insertUserQuery = @"
            INSERT INTO ""user""(name, login, password, role)
            VALUES(@Name, @Login, @Password, @Role) RETURNING id;";

                var userParameters = new
                {
                    Name = client.User.Name,
                    Login = client.User.Login,
                    Password = client.User.Password,
                    Role = client.User.Role
                };

                var userId = db.ExecuteScalar<int>(insertUserQuery, userParameters);
                client.UserId = userId;  // Assigner l'ID généré à l'objet client

                // 2. Insérer le client dans la table "client" en utilisant l'ID de l'utilisateur
                const string insertClientQuery = @"
            INSERT INTO client (address, phone, surname, userid, createdat, updatedat)
            VALUES(@Address, @Phone, @Surname, @UserId, @CreatedAt, @UpdatedAt);";

                client.CreatedAt = DateTime.UtcNow;
                client.UpdatedAt = DateTime.UtcNow;

                var clientParameters = new
                {
                    Address = client.Address,
                    Phone = client.Phone,
                    Surname = client.Surname,
                    UserId = client.UserId,  // Utiliser l'ID de l'utilisateur pour lier le client
                    CreatedAt = client.CreatedAt,
                    UpdatedAt = client.UpdatedAt
                };

                // Insérer le client avec l'ID de l'utilisateur associé
                var result = db.Execute(insertClientQuery, clientParameters);

                return result > 0;  // Retourner vrai si l'insertion a réussi
            }
        }



        public List<Client> SelectAll()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = @"
            SELECT c.*, u.* 
            FROM ""client"" c
            LEFT JOIN ""user"" u ON c.""userid"" = u.""id""";

                var clientDict = new Dictionary<int, Client>();

                var clients = db.Query<Client, User, Client>(
                    query,
                    (client, user) =>
                    {
                        if (!clientDict.TryGetValue(client.Id, out var currentClient))
                        {
                            currentClient = client;
                            clientDict.Add(client.Id, currentClient);
                        }
                        currentClient.User = user;
                        return currentClient;
                    },
                    splitOn: "id"
                ).Distinct().ToList();

                return clients;
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
                const string query = "SELECT * FROM \"client\" WHERE \"userid\" = @UserId";
                return db.QueryFirstOrDefault<Client>(query, new { UserId = userId });
            }
        }

        public bool Update(Client client)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = @"
                    UPDATE client
                    SET address = @Address, phone = @Phone, surname = @Surname, updatedat = @UpdatedAt
                    WHERE id = @Id";

                client.UpdatedAt = DateTime.Now;

                var result = db.Execute(query, client);
                return result > 0;
            }
        }
    }
}
