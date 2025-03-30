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
    public class SearchLastReminderRepository : ISearchLastReminderRepository
    {
        private readonly string _connectionString;

        public SearchLastReminderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SearchLastReminderDomain GetSearchLastReminder(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SearchLastReminder WHERE SearchLastReminderID = @id";

                    var SearchLastReminderPoco = cn.QueryFirstOrDefault<SearchLastReminderPoco>(query, new { id = id }) ?? new SearchLastReminderPoco();

                    return SearchLastReminderPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SearchLastReminderDomain> GetSearchLastReminders(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SearchLastReminder WHERE @criteria";
                    List<SearchLastReminderPoco> pocos = cn.Query<SearchLastReminderPoco>(sql).ToList();
                    List<SearchLastReminderDomain> domains = new List<SearchLastReminderDomain>();

                    foreach (SearchLastReminderPoco poco in pocos)
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

        public int DeleteSearchLastReminder(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SearchLastReminder WHERE SearchLastReminderID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSearchLastReminder(SearchLastReminderDomain domain)
        {
            int insertedId = 0;

            try
            {
                SearchLastReminderPoco poco = new SearchLastReminderPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SearchLastReminder] " +
                        "([SearchLastReminderID], [PatientID], [GroupGUID], [IsPreferredMethod], [LastReminder], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@SearchLastReminderID, @PatientID, @GroupGUID, @IsPreferredMethod, @LastReminder, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateSearchLastReminder(SearchLastReminderDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SearchLastReminderPoco poco = new SearchLastReminderPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SearchLastReminder " +
                        "SET [PatientID] = @PatientID, [GroupGUID] = @GroupGUID, " +
                        "[IsPreferredMethod] = @IsPreferredMethod, [LastReminder] = @LastReminder, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE SearchLastReminderID = @SearchLastReminderID;";

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
