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
    public class ReportingSavedQueriesRepository : IReportingSavedQueriesRepository
    {
        private readonly string _connectionString;

        public ReportingSavedQueriesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ReportingSavedQueriesDomain GetReportingSavedQueries(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ReportingSavedQueries WHERE SavedQueryID = @id";

                    var ReportingSavedQueriesPoco = cn.QueryFirstOrDefault<ReportingSavedQueriesPoco>(query, new { id = id }) ?? new ReportingSavedQueriesPoco();

                    return ReportingSavedQueriesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ReportingSavedQueriesDomain> GetReportingSavedQueries(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ReportingSavedQueries WHERE @criteria";
                    List<ReportingSavedQueriesPoco> pocos = cn.Query<ReportingSavedQueriesPoco>(sql).ToList();
                    List<ReportingSavedQueriesDomain> domains = new List<ReportingSavedQueriesDomain>();

                    foreach (ReportingSavedQueriesPoco poco in pocos)
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

        public int DeleteReportingSavedQueries(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ReportingSavedQueries WHERE SavedQueryID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertReportingSavedQueries(ReportingSavedQueriesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ReportingSavedQueriesPoco poco = new ReportingSavedQueriesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ReportingSavedQueries] " +
                        "([SavedQueryID], [QueryName], [QueryType], [QuerySQL], [QueryFilter], [SavedByProviderID], " +
                        "[LastTouchedBy], [DateLastTouched], [DateRowAdded]) " +
                        "VALUES " +
                        "(@SavedQueryID, @QueryName, @QueryType, @QuerySQL, @QueryFilter, @SavedByProviderID, " +
                        "@LastTouchedBy, @DateLastTouched, @DateRowAdded); " +
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

        public int UpdateReportingSavedQueries(ReportingSavedQueriesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ReportingSavedQueriesPoco poco = new ReportingSavedQueriesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ReportingSavedQueries " +
                        "SET [QueryName] = @QueryName, [QueryType] = @QueryType, [QuerySQL] = @QuerySQL, " +
                        "[QueryFilter] = @QueryFilter, [SavedByProviderID] = @SavedByProviderID, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateLastTouched] = @DateLastTouched, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE SavedQueryID = @SavedQueryID;";

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
