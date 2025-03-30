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
    public class HmConditionsForRulesEditedRepository : IHmConditionsForRulesEditedRepository
    {
        private readonly string _connectionString;

        public HmConditionsForRulesEditedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public HmConditionsForRulesEditedDomain GetHmConditionsForRulesEdited(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM HmConditionsForRulesEdited WHERE HmConditionForRuleID = @id";

                    var HmConditionsForRulesEditedPoco = cn.QueryFirstOrDefault<HmConditionsForRulesEditedPoco>(query, new { id = id }) ?? new HmConditionsForRulesEditedPoco();

                    return HmConditionsForRulesEditedPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<HmConditionsForRulesEditedDomain> GetHmConditionsForRulesEditeds(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM HmConditionsForRulesEdited WHERE @criteria";
                    List<HmConditionsForRulesEditedPoco> pocos = cn.Query<HmConditionsForRulesEditedPoco>(sql).ToList();
                    List<HmConditionsForRulesEditedDomain> domains = new List<HmConditionsForRulesEditedDomain>();

                    foreach (HmConditionsForRulesEditedPoco poco in pocos)
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

        public int DeleteHmConditionsForRulesEdited(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM HmConditionsForRulesEdited WHERE HmConditionForRuleID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertHmConditionsForRulesEdited(HmConditionsForRulesEditedDomain domain)
        {
            int insertedId = 0;

            try
            {
                HmConditionsForRulesEditedPoco poco = new HmConditionsForRulesEditedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[HmConditionsForRulesEdited] " +
                        "([HmConditionForRuleID], [HmRuleID], [HmConditionID], [Comments], [Value], " +
                        "[ComparisonOp], [GroupID], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[CriteriaType], [Deleted]) " +
                        "VALUES " +
                        "(@HmConditionForRuleID, @HmRuleID, @HmConditionID, @Comments, @Value, " +
                        "@ComparisonOp, @GroupID, @DateLastTouched, @LastTouchedBy, @DateRowAdded, " +
                        "@CriteriaType, @Deleted); " +
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

        public int UpdateHmConditionsForRulesEdited(HmConditionsForRulesEditedDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                HmConditionsForRulesEditedPoco poco = new HmConditionsForRulesEditedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE HmConditionsForRulesEdited " +
                        "SET [HmRuleID] = @HmRuleID, [HmConditionID] = @HmConditionID, [Comments] = @Comments, " +
                        "[Value] = @Value, [ComparisonOp] = @ComparisonOp, [GroupID] = @GroupID, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [CriteriaType] = @CriteriaType, [Deleted] = @Deleted " +
                        "WHERE HmConditionForRuleID = @HmConditionForRuleID;";

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
