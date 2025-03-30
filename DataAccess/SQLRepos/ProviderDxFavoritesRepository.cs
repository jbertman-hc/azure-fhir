using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using POC.DataAccess.SQLPocos;
using POC.Domain.DomainModels;
using POC.Domain.RepositoryDefinitions;

namespace POC.DataAccess.SQLRepos
{
    public class ProviderDxFavoritesRepository : IProviderDxFavoritesRepository
    {
        private readonly string _connectionString;

        public ProviderDxFavoritesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProviderDxFavoritesDomain GetProviderDxFavorites(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ProviderDxFavorites WHERE ProviderCode = @id";

                    var ProviderDxFavoritesPoco = cn.QueryFirstOrDefault<ProviderDxFavoritesPoco>(query, new { id = id }) ?? new ProviderDxFavoritesPoco();

                    return ProviderDxFavoritesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ProviderDxFavoritesDomain> GetProviderDxFavorites(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ProviderDxFavorites WHERE @criteria";
                    List<ProviderDxFavoritesPoco> pocos = cn.Query<ProviderDxFavoritesPoco>(sql).ToList();
                    List<ProviderDxFavoritesDomain> domains = new List<ProviderDxFavoritesDomain>();

                    foreach (ProviderDxFavoritesPoco poco in pocos)
                    {
                        domains.Add(poco.MapToDomainModel());
                    }

                    return domains;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteProviderDxFavorites(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ProviderDxFavorites WHERE ProviderCode = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertProviderDxFavorites(ProviderDxFavoritesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ProviderDxFavoritesPoco poco = new ProviderDxFavoritesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO ProviderDxFavorites (** column list **) VALUES (** column list prefixed with @ **); SELECT CAST(SCOPE_IDENTITY() AS INT)";

                    insertedId = cn.Query(sql, poco).Single();
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return insertedId;
        }

        public int UpdateProviderDxFavorites(ProviderDxFavoritesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ProviderDxFavoritesPoco poco = new ProviderDxFavoritesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ProviderDxFavorites SET col1 = @col1, col2 = @col2, col3 = @col3, etc WHERE id = @id";

                    rowsAffected = cn.Execute(sql, poco);
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return rowsAffected;
        }
    }
}
