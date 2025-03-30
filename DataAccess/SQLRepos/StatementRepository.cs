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
    public class StatementRepository : IStatementRepository
    {
        private readonly string _connectionString;

        public StatementRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public StatementDomain GetStatement(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Statement WHERE StatementID = @id";

                    var StatementPoco = cn.QueryFirstOrDefault<StatementPoco>(query, new { id = id }) ?? new StatementPoco();

                    return StatementPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<StatementDomain> GetStatements(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Statement WHERE @criteria";
                    List<StatementPoco> pocos = cn.Query<StatementPoco>(sql).ToList();
                    List<StatementDomain> domains = new List<StatementDomain>();

                    foreach (StatementPoco poco in pocos)
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

        public int DeleteStatement(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Statement WHERE StatementID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertStatement(StatementDomain domain)
        {
            int insertedId = 0;

            try
            {
                StatementPoco poco = new StatementPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Statement] " +
                        "([StatementID], [PersonalizedMessage], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[DateSent], [PatientID], [BillingPeriod], [Printed], [Emailed], [Closed], [Subscriber]) " +
                        "VALUES " +
                        "(@StatementID, @PersonalizedMessage, @DateLastTouched, @LastTouchedBy, @DateRowAdded, " +
                        "@DateSent, @PatientID, @BillingPeriod, @Printed, @Emailed, @Closed, @Subscriber); " +
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

        public int UpdateStatement(StatementDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                StatementPoco poco = new StatementPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Statement " +
                        "SET [PersonalizedMessage] = @PersonalizedMessage, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [DateSent] = @DateSent, " +
                        "[PatientID] = @PatientID, [BillingPeriod] = @BillingPeriod, [Printed] = @Printed, " +
                        "[Emailed] = @Emailed, [Closed] = @Closed, [Subscriber] = @Subscriber " +
                        "WHERE StatementID = @StatementID;";

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
