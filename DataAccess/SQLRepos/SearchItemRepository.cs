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
    public class SearchItemRepository : ISearchItemRepository
    {
        private readonly string _connectionString;

        public SearchItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SearchItemDomain GetSearchItem(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SearchItem WHERE SearchItemID = @id";

                    var SearchItemPoco = cn.QueryFirstOrDefault<SearchItemPoco>(query, new { id = id }) ?? new SearchItemPoco();

                    return SearchItemPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SearchItemDomain> GetSearchItems(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SearchItem WHERE @criteria";
                    List<SearchItemPoco> pocos = cn.Query<SearchItemPoco>(sql).ToList();
                    List<SearchItemDomain> domains = new List<SearchItemDomain>();

                    foreach (SearchItemPoco poco in pocos)
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

        public int DeleteSearchItem(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SearchItem WHERE SearchItemID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSearchItem(SearchItemDomain domain)
        {
            int insertedId = 0;

            try
            {
                SearchItemPoco poco = new SearchItemPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SearchItem] " +
                        "([SearchItemID], [GroupName], [GroupGUID], [Category], [CategoryID], [Fields], " +
                        "[FieldsID], [ComparisonOp], [ComparisonOpID], [Value], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded], [ValueID], [IsOr], [GroupCategoryID]) " +
                        "VALUES " +
                        "(@SearchItemID, @GroupName, @GroupGUID, @Category, @CategoryID, @Fields, @FieldsID, " +
                        "@ComparisonOp, @ComparisonOpID, @Value, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @ValueID, @IsOr, @GroupCategoryID); " +
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

        public int UpdateSearchItem(SearchItemDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SearchItemPoco poco = new SearchItemPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SearchItem " +
                        "SET [GroupName] = @GroupName, [GroupGUID] = @GroupGUID, [Category] = @Category, " +
                        "[CategoryID] = @CategoryID, [Fields] = @Fields, [FieldsID] = @FieldsID, " +
                        "[ComparisonOp] = @ComparisonOp, [ComparisonOpID] = @ComparisonOpID, [Value] = @Value, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [ValueID] = @ValueID, [IsOr] = @IsOr, " +
                        "[GroupCategoryID] = @GroupCategoryID " +
                        "WHERE SearchItemID = @SearchItemID;";

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
