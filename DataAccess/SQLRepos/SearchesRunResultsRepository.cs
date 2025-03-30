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
    public class SearchesRunResultsRepository : ISearchesRunResultsRepository
    {
        private readonly string _connectionString;

        public SearchesRunResultsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SearchesRunResultsDomain GetSearchesRunResults(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SearchesRunResults WHERE SearchesRunResultID = @id";

                    var SearchesRunResultsPoco = cn.QueryFirstOrDefault<SearchesRunResultsPoco>(query, new { id = id }) ?? new SearchesRunResultsPoco();

                    return SearchesRunResultsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SearchesRunResultsDomain> GetSearchesRunResults(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SearchesRunResults WHERE @criteria";
                    List<SearchesRunResultsPoco> pocos = cn.Query<SearchesRunResultsPoco>(sql).ToList();
                    List<SearchesRunResultsDomain> domains = new List<SearchesRunResultsDomain>();

                    foreach (SearchesRunResultsPoco poco in pocos)
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

        public int DeleteSearchesRunResults(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SearchesRunResults WHERE SearchesRunResultID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSearchesRunResults(SearchesRunResultsDomain domain)
        {
            int insertedId = 0;

            try
            {
                SearchesRunResultsPoco poco = new SearchesRunResultsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SearchesRunResults] " +
                        "([SearchesRunResultID], [SearchRunID], [PatientID], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@SearchesRunResultID, @SearchRunID, @PatientID, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateSearchesRunResults(SearchesRunResultsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SearchesRunResultsPoco poco = new SearchesRunResultsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SearchesRunResults " +
                        "SET [SearchRunID] = @SearchRunID, [PatientID] = @PatientID, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE SearchesRunResultID = @SearchesRunResultID;";

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
