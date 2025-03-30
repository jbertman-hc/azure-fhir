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
    public class AutoEligibilityRepository : IAutoEligibilityRepository
    {
        private readonly string _connectionString;

        public AutoEligibilityRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AutoEligibilityDomain GetAutoEligibility(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM AutoEligibility WHERE AutoEligibilityID = @id";

                    var AutoEligibilityPoco = cn.QueryFirstOrDefault<AutoEligibilityPoco>(query, new { id = id }) ?? new AutoEligibilityPoco();

                    return AutoEligibilityPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AutoEligibilityDomain> GetAutoEligibilitys(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM AutoEligibility WHERE @criteria";
                    List<AutoEligibilityPoco> pocos = cn.Query<AutoEligibilityPoco>(sql).ToList();
                    List<AutoEligibilityDomain> domains = new List<AutoEligibilityDomain>();

                    foreach (AutoEligibilityPoco poco in pocos)
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

        public int DeleteAutoEligibility(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM AutoEligibility WHERE AutoEligibilityID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertAutoEligibility(AutoEligibilityDomain domain)
        {
            int insertedId = 0;

            try
            {
                AutoEligibilityPoco poco = new AutoEligibilityPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[AutoEligibility] " +
                        "([AutoEligibilityID], [AutoEligibilityCheck], [RunSameDay], [TimeToRun], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@AutoEligibilityID, @AutoEligibilityCheck, @RunSameDay, @TimeToRun, @DateLastTouched, " +
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

        public int UpdateAutoEligibility(AutoEligibilityDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                AutoEligibilityPoco poco = new AutoEligibilityPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE AutoEligibility " +
                        "SET [AutoEligibilityCheck] = @AutoEligibilityCheck, [RunSameDay] = @RunSameDay, " +
                        "[TimeToRun] = @TimeToRun, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE AutoEligibilityID = @AutoEligibilityID;";

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
