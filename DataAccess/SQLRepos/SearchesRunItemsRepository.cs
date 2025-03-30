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
    public class SearchesRunItemsRepository : ISearchesRunItemsRepository
    {
        private readonly string _connectionString;

        public SearchesRunItemsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SearchesRunItemsDomain GetSearchesRunItems(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SearchesRunItems WHERE SearchesRunItemID = @id";

                    var SearchesRunItemsPoco = cn.QueryFirstOrDefault<SearchesRunItemsPoco>(query, new { id = id }) ?? new SearchesRunItemsPoco();

                    return SearchesRunItemsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SearchesRunItemsDomain> GetSearchesRunItems(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SearchesRunItems WHERE @criteria";
                    List<SearchesRunItemsPoco> pocos = cn.Query<SearchesRunItemsPoco>(sql).ToList();
                    List<SearchesRunItemsDomain> domains = new List<SearchesRunItemsDomain>();

                    foreach (SearchesRunItemsPoco poco in pocos)
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

        public int DeleteSearchesRunItems(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SearchesRunItems WHERE SearchesRunItemID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSearchesRunItems(SearchesRunItemsDomain domain)
        {
            int insertedId = 0;

            try
            {
                SearchesRunItemsPoco poco = new SearchesRunItemsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SearchesRunItems] " +
                        "([SearchesRunItemID], [SearchRunID], [GroupGUID], [GroupCategoryID], [FieldCategoryID], " +
                        "[FieldID], [ComparisonOpID], [Value], [ValueID], [IsOr], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@SearchesRunItemID, @SearchRunID, @GroupGUID, @GroupCategoryID, @FieldCategoryID, " +
                        "@FieldID, @ComparisonOpID, @Value, @ValueID, @IsOr, @DateLastTouched, " +
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

        public int UpdateSearchesRunItems(SearchesRunItemsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SearchesRunItemsPoco poco = new SearchesRunItemsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SearchesRunItems " +
                        "SET [SearchRunID] = @SearchRunID, [GroupGUID] = @GroupGUID, " +
                        "[GroupCategoryID] = @GroupCategoryID, [FieldCategoryID] = @FieldCategoryID, " +
                        "[FieldID] = @FieldID, [ComparisonOpID] = @ComparisonOpID, [Value] = @Value, " +
                        "[ValueID] = @ValueID, [IsOr] = @IsOr, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE SearchesRunItemID = @SearchesRunItemID;";

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
