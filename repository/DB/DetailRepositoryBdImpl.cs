using csharp.entities;
using csharp.core.config;
using csharp.core.interfaces;
using csharp.enums;
using System.Data;
using Dapper;
using Npgsql;

namespace csharp.repository.DB
{
    public class DetailRepositoryBdImpl : IDetailRepository
    {
        private readonly IDataAccess dataAccess;

        public DetailRepositoryBdImpl(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public int Count()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT COUNT(*) FROM detail";
                return db.ExecuteScalar<int>(query);
            }
        }

        // Supprimer un client par son ID
        public bool Delete(int id)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "DELETE FROM detail WHERE id = @Id";
                var result = db.Execute(query, new { Id = id });
                return result > 0;
            }
        }

        public bool Insert(Detail objet)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = @"
                    INSERT INTO detail (qte, article_id, dept_id, createdat, updatedat)
                    VALUES (@qte, @article_id, @dept_id, @CreatedAt, @UpdatedAt)";

                objet.CreatedAt = DateTime.Now;
                objet.UpdatedAt = DateTime.Now;

                var result = db.Execute(query, objet);
                return result > 0;
            }
        }

        public List<Detail> SelectAll()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM detail";
                return db.Query<Detail>(query).ToList();
            }
        }

        public Detail SelectById(int id)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM detail WHERE id = @Id";
                return db.QueryFirstOrDefault<Detail>(query, new { Id = id });
            }
        }

        public bool Update(Detail objet)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = @"
                    UPDATE detail
                    SET qte = @qte, article_id = @article_id, dept_id = @dept_id, updatedat = @UpdatedAt
                    WHERE id = @Id";

                objet.UpdatedAt = DateTime.Now;

                var result = db.Execute(query, objet);
                return result > 0;
            }
        }
    }
}