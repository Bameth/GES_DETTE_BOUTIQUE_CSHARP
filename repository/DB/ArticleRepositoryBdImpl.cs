using csharp.entities;
using csharp.core.config;
using csharp.core.interfaces;
using Dapper;
using System.Data;
using System.Collections.Generic;
using Npgsql;

namespace csharp.repository.DB
{
    public class ArticlesRepositoryBdImpl : IArticleRepository
    {
        private readonly IDataAccess dataAccess;

        public ArticlesRepositoryBdImpl(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public int Count()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT COUNT(*) FROM  \"article\"";
                return db.ExecuteScalar<int>(query);
            }
        }

        // Supprimer un article par son ID
        public bool Delete(int id)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "DELETE FROM  \"article\" WHERE \"id\" = @Id";
                var result = db.Execute(query, new { Id = id });
                return result > 0; 
            }
        }

        // Insérer un nouvel article
        public bool Insert(Articles article)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = @"
                    INSERT INTO article (libelle, prix, qteStock, reference, createdat, updatedat)
                    VALUES (@Libelle, @Prix, @QteStock, @Reference, @CreatedAt, @UpdatedAt)";
                
                article.CreatedAt = DateTime.Now;
                article.UpdatedAt = DateTime.Now;

                var result = db.Execute(query, article);
                return result > 0;
            }
        }

        // Sélectionner tous les articles
        public List<Articles> SelectAll()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM article";
                return db.Query<Articles>(query).ToList();
            }
        }

        // Sélectionner les articles par disponibilité (QteStock > 0)
        public List<Articles> SelectByAvailability()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM article WHERE qteStock > 0";
                return db.Query<Articles>(query).ToList();
            }
        }

        // Sélectionner un article par son ID
        public Articles SelectById(int id)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM article WHERE id = @Id";
                return db.QueryFirstOrDefault<Articles>(query, new { Id = id });
            }
        }

        // Sélectionner un article par sa référence
        public Articles? SelectByReference(string reference)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM article WHERE reference = @Reference";
                return db.QueryFirstOrDefault<Articles>(query, new { Reference = reference });
            }
        }

        // Mettre à jour un article
        public bool Update(Articles article)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = @"
                    UPDATE article
                    SET libelle = @Libelle, prix = @Prix, qteStock = @QteStock, 
                        reference = @Reference, updatedat = @UpdatedAt
                    WHERE id = @Id";

                article.UpdatedAt = DateTime.Now;

                var result = db.Execute(query, article);
                return result > 0;
            }
        }
    }
}
