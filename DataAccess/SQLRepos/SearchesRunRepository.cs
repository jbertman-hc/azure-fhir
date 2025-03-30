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
    public class SearchesRunRepository : ISearchesRunRepository
    {
        private readonly string _connectionString;

        public SearchesRunRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SearchesRunDomain GetSearchesRun(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SearchesRun WHERE SearchRunID = @id";

                    var SearchesRunPoco = cn.QueryFirstOrDefault<SearchesRunPoco>(query, new { id = id }) ?? new SearchesRunPoco();

                    return SearchesRunPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SearchesRunDomain> GetSearchesRuns(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SearchesRun WHERE @criteria";
                    List<SearchesRunPoco> pocos = cn.Query<SearchesRunPoco>(sql).ToList();
                    List<SearchesRunDomain> domains = new List<SearchesRunDomain>();

                    foreach (SearchesRunPoco poco in pocos)
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

        public int DeleteSearchesRun(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SearchesRun WHERE SearchRunID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSearchesRun(SearchesRunDomain domain)
        {
            int insertedId = 0;

            try
            {
                SearchesRunPoco poco = new SearchesRunPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SearchesRun] " +
                        "([SearchRunID], [DateRan], [CategoryID], [SQL], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@SearchRunID, @DateRan, @CategoryID, @SQL, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateSearchesRun(SearchesRunDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SearchesRunPoco poco = new SearchesRunPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SearchesRun " +
                        "SET [DateRan] = @DateRan, [CategoryID] = @CategoryID, [SQL] = @SQL, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE SearchRunID = @SearchRunID;";

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
