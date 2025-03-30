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
    public class StatementAgeMessagesRepository : IStatementAgeMessagesRepository
    {
        private readonly string _connectionString;

        public StatementAgeMessagesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public StatementAgeMessagesDomain GetStatementAgeMessages(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM StatementAgeMessages WHERE StatementAgeMessagesID = @id";

                    var StatementAgeMessagesPoco = cn.QueryFirstOrDefault<StatementAgeMessagesPoco>(query, new { id = id }) ?? new StatementAgeMessagesPoco();

                    return StatementAgeMessagesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<StatementAgeMessagesDomain> GetStatementAgeMessages(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM StatementAgeMessages WHERE @criteria";
                    List<StatementAgeMessagesPoco> pocos = cn.Query<StatementAgeMessagesPoco>(sql).ToList();
                    List<StatementAgeMessagesDomain> domains = new List<StatementAgeMessagesDomain>();

                    foreach (StatementAgeMessagesPoco poco in pocos)
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

        public int DeleteStatementAgeMessages(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM StatementAgeMessages WHERE StatementAgeMessagesID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertStatementAgeMessages(StatementAgeMessagesDomain domain)
        {
            int insertedId = 0;

            try
            {
                StatementAgeMessagesPoco poco = new StatementAgeMessagesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[StatementAgeMessages] " +
                        "([StatementAgeMessagesID], [StatementSettingsID], [Message], [Age], [SortOrder], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [MinDays], [MaxDays]) " +
                        "VALUES " +
                        "(@StatementAgeMessagesID, @StatementSettingsID, @Message, @Age, @SortOrder, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @MinDays, @MaxDays); " +
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

        public int UpdateStatementAgeMessages(StatementAgeMessagesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                StatementAgeMessagesPoco poco = new StatementAgeMessagesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE StatementAgeMessages " +
                        "SET [StatementSettingsID] = @StatementSettingsID, [Message] = @Message, [Age] = @Age, " +
                        "[SortOrder] = @SortOrder, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [MinDays] = @MinDays, " +
                        "[MaxDays] = @MaxDays " +
                        "WHERE StatementAgeMessagesID = @StatementAgeMessagesID;";

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
