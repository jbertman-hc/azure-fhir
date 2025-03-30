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
    public class ProviderIndexRepository : IProviderIndexRepository
    {
        private readonly string _connectionString;

        public ProviderIndexRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProviderIndexDomain GetProviderIndex(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ProviderIndex WHERE ProviderIndexId = @id";

                    var ProviderIndexPoco = cn.QueryFirstOrDefault<ProviderIndexPoco>(query, new { id = id }) ?? new ProviderIndexPoco();

                    return ProviderIndexPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ProviderIndexDomain> GetProviderIndexs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ProviderIndex WHERE @criteria";
                    List<ProviderIndexPoco> pocos = cn.Query<ProviderIndexPoco>(sql).ToList();
                    List<ProviderIndexDomain> domains = new List<ProviderIndexDomain>();

                    foreach (ProviderIndexPoco poco in pocos)
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

        public int DeleteProviderIndex(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ProviderIndex WHERE ProviderIndexId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertProviderIndex(ProviderIndexDomain domain)
        {
            int insertedId = 0;

            try
            {
                ProviderIndexPoco poco = new ProviderIndexPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ProviderIndex] " +
                        "([ProviderIndexId], [ProviderCode], [ExternalProviderName], [ExternalProviderPassword], " +
                        "[Source], [DateLastTouched], [LastTouchedBy], [DateRowAdded], [ExternalProviderID], " +
                        "[ExternalProviderData]) " +
                        "VALUES " +
                        "(@ProviderIndexId, @ProviderCode, @ExternalProviderName, @ExternalProviderPassword, @Source, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @ExternalProviderID, @ExternalProviderData); SELECT CAST(SCOPE_IDENTITY() AS INT);\r\n";

                    insertedId = cn.Query(sql, poco).Single();
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return insertedId;
        }

        public int UpdateProviderIndex(ProviderIndexDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ProviderIndexPoco poco = new ProviderIndexPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ProviderIndex " +
                        "SET [ProviderCode] = @ProviderCode, [ExternalProviderName] = @ExternalProviderName, " +
                        "[ExternalProviderPassword] = @ExternalProviderPassword, [Source] = @Source, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [ExternalProviderID] = @ExternalProviderID, " +
                        "[ExternalProviderData] = @ExternalProviderData " +
                        "WHERE ProviderIndexId = @ProviderIndexId;";

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
