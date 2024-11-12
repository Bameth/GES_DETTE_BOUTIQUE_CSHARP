using csharp.entities;
using csharp.core.config;
using csharp.core.interfaces;
using csharp.enums;
using System.Data;
using Dapper;
using BCrypt.Net;
using Npgsql;

namespace csharp.repository.DB
{
    public class UserRepositoryBdImpl : IUserRepository
    {
        private readonly IDataAccess dataAccess;

        public UserRepositoryBdImpl(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public int Count()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT COUNT(*) FROM \"user\"";
                return db.ExecuteScalar<int>(query);
            }
        }

        public bool Delete(int id)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "DELETE FROM \"user\" WHERE \"id\" = @Id";
                var result = db.Execute(query, new { Id = id });
                return result > 0;
            }
        }

        public bool Insert(User user)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                const string query = @"
                INSERT INTO ""user"" (name, login, password, role, typeEtat, createdAt, updatedAt)
                VALUES (@Name, @Login, @Password, @Role, @TypeEtat, @CreatedAt, @UpdatedAt)";

                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;

                var result = db.Execute(query, new
                {
                    user.Name,
                    user.Login,
                    user.Password,
                    Role = user.Role.ToString(),   // Utilisation de ToString pour insérer comme chaîne
                    TypeEtat = user.TypeEtat.ToString(),   // Idem pour TypeEtat
                    user.CreatedAt,
                    user.UpdatedAt
                });

                return result > 0;

            }
        }

        public List<User> SelectAll()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM \"user\"";
                return db.Query<User>(query).ToList();
            }
        }

        public List<User> SelectByEtat(TypeEtat etat)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM \"user\" WHERE \"typeetat\" = @Etat::TEXT";
                return db.Query<User>(query, new { Etat = etat.ToString() }).ToList();
            }
        }


        public User SelectById(int id)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM \"user\" WHERE \"id\" = @Id";
                return db.QueryFirstOrDefault<User>(query, new { Id = id });
            }
        }

        public User? SelectByLogin(string login, string password)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM \"user\" WHERE \"login\" = @Login";
                var user = db.QueryFirstOrDefault<User>(query, new { Login = login });

                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return user;
                }
                return null;
            }
        }

        public User SelectByName(string name)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM \"user\" WHERE \"name\" = @Name";
                return db.QueryFirstOrDefault<User>(query, new { Name = name });
            }
        }

        public List<User> SelectByRole(Role role)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM \"user\" WHERE \"role\" = @Role::TEXT";
                return db.Query<User>(query, new { Role = role.ToString() }).ToList();
            }
        }


        public bool Update(User user)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                if (!string.IsNullOrEmpty(user.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // Hash password if changed
                }

                const string query = @"
                UPDATE ""user""
                SET name = @Name, login = @Login, password = @Password, role = @Role, typeetat = @TypeEtat, updatedAt = @UpdatedAt
                WHERE id = @Id";

                user.UpdatedAt = DateTime.Now;

                var result = db.Execute(query, user);
                return result > 0;
            }
        }
    }
}
