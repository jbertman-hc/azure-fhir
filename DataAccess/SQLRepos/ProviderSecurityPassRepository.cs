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
    public class ProviderSecurityPassRepository : IProviderSecurityPassRepository
    {
        private readonly string _connectionString;

        public ProviderSecurityPassRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProviderSecurityPassDomain GetProviderSecurityPass(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ProviderSecurityPass WHERE id = @id";

                    var ProviderSecurityPassPoco = cn.QueryFirstOrDefault<ProviderSecurityPassPoco>(query, new { id = id }) ?? new ProviderSecurityPassPoco();

                    return ProviderSecurityPassPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ProviderSecurityPassDomain> GetProviderSecurityPass(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ProviderSecurityPass WHERE @criteria";
                    List<ProviderSecurityPassPoco> pocos = cn.Query<ProviderSecurityPassPoco>(sql).ToList();
                    List<ProviderSecurityPassDomain> domains = new List<ProviderSecurityPassDomain>();

                    foreach (ProviderSecurityPassPoco poco in pocos)
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

        public int DeleteProviderSecurityPass(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ProviderSecurityPass WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertProviderSecurityPass(ProviderSecurityPassDomain domain)
        {
            int insertedId = 0;

            try
            {
                ProviderSecurityPassPoco poco = new ProviderSecurityPassPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ProviderSecurityPass] " +
                        "([ID], [ProviderID], [ProviderPass], [PassStartDate], [PassEndDate], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @ProviderID, @ProviderPass, @PassStartDate, @PassEndDate, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    insertedId = cn.Query(sql, poco).Single();
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return insertedId;
        }

        public int UpdateProviderSecurityPass(ProviderSecurityPassDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ProviderSecurityPassPoco poco = new ProviderSecurityPassPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ProviderSecurityPass " +
                        "SET [ProviderID] = @ProviderID, [ProviderPass] = @ProviderPass, [PassStartDate] = @PassStartDate, " +
                        "[PassEndDate] = @PassEndDate, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE ID = @ID;";

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
