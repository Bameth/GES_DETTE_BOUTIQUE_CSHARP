using csharp.entities;
using csharp.core.config;
using csharp.core.interfaces;
using csharp.enums;
using System.Data;
using Dapper;
using Npgsql;

namespace csharp.repository.DB
{
    public class DetteRepositoryBdImpl : IDeptRepository
    {
        private readonly IDataAccess dataAccess;

        public DetteRepositoryBdImpl(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public int Count()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT COUNT(*) FROM dept";
                return db.ExecuteScalar<int>(query);
            }
        }

        // Supprimer un client par son ID
        public bool Delete(int id)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "DELETE FROM dept WHERE id = @Id";
                var result = db.Execute(query, new { Id = id });
                return result > 0;
            }
        }

        // Insérer une dette
        public bool Insert(Dept dept)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = @"
                    INSERT INTO dept (montant, montantrestant, montantverser, client_id, typedette, etat, createdat, updatedat)
                    VALUES (@Montant, @MontantRestant, @MontantVerser, @ClientId, @TypeDette, @Etat, @CreatedAt, @UpdatedAt)";

                dept.CreatedAt = DateTime.Now;
                dept.UpdatedAt = DateTime.Now;

                var result = db.Execute(query, dept);
                return result > 0;
            }
        }

        // Sélectionner toutes les dettes
        public List<Dept> SelectAll()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM dept";
                return db.Query<Dept>(query).ToList();
            }
        }

        // Sélectionner toutes les dettes non soldées
        public List<Dept> SelectAllDeptNonSoldees()
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM dept WHERE montantrestant > 0";
                return db.Query<Dept>(query).ToList();
            }
        }

        // Sélectionner les dettes d'un client spécifique
        public List<Dept> SelectAllMyDept(int clientId)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM dept WHERE client_id = @ClientId";
                return db.Query<Dept>(query, new { ClientId = clientId }).ToList();
            }
        }

        // Sélectionner les dettes annulées d'un client
        public List<Dept> SelectAllMyDeptAnnuler(int clientId)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM dept WHERE client_id = @ClientId AND typedette = @TypeDetteAnnuler";
                return db.Query<Dept>(query, new { ClientId = clientId, TypeDetteAnnuler = TypeDette.ANNULER }).ToList();
            }
        }

        // Sélectionner les dettes non soldées d'un client
        public List<Dept> SelectAllMyDeptNonSoldees(int clientId)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM dept WHERE client_id = @ClientId AND montantrestant > 0";
                return db.Query<Dept>(query, new { ClientId = clientId }).ToList();
            }
        }
        public List<Dept> SelectByClientId(int clientId)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM dept WHERE client_id = @ClientId";
                return db.Query<Dept>(query, new { ClientId = clientId }).ToList();
            }
        }

        // Sélectionner une dette par son ID
        public Dept SelectById(int id)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM dept WHERE id = @Id";
                return db.QueryFirstOrDefault<Dept>(query, new { Id = id });
            }
        }

        // Sélectionner des dettes par leur état
        public List<Dept> SelectByEtat(TypeDette etat)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = "SELECT * FROM dept WHERE typedette = @TypeDette";
                return db.Query<Dept>(query, new { TypeDette = etat }).ToList();
            }
        }

        // Mettre à jour une dette
        public bool Update(Dept dept)
        {
            using (NpgsqlConnection db = dataAccess.GetConnections())
            {
                const string query = @"
                    UPDATE dept
                    SET montant = @Montant, montantrestant = @MontantRestant, etat = @Etat, typedette = @TypeDette, montantverser = @MontantVerser, client_id = @ClientId, updatedat = @UpdatedAt
                    WHERE id = @Id";

                dept.UpdatedAt = DateTime.Now;

                var result = db.Execute(query, dept);
                return result > 0;
            }
        }
    }
}
