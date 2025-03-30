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
    public class ListOrderResultsRepository : IListOrderResultsRepository
    {
        private readonly string _connectionString;

        public ListOrderResultsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListOrderResultsDomain GetListOrderResults(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListOrderResults WHERE RowId = @id";

                    var ListOrderResultsPoco = cn.QueryFirstOrDefault<ListOrderResultsPoco>(query, new { id = id }) ?? new ListOrderResultsPoco();

                    return ListOrderResultsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListOrderResultsDomain> GetListOrderResults(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListOrderResults WHERE @criteria";
                    List<ListOrderResultsPoco> pocos = cn.Query<ListOrderResultsPoco>(sql).ToList();
                    List<ListOrderResultsDomain> domains = new List<ListOrderResultsDomain>();

                    foreach (ListOrderResultsPoco poco in pocos)
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

        public int DeleteListOrderResults(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListOrderResults WHERE RowId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListOrderResults(ListOrderResultsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListOrderResultsPoco poco = new ListOrderResultsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListOrderResults] " +
                        "([RowId], [OrderID], [ResultItemID], [ResultType], [DateAssigned], [ResultText], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [IncludesRadiologyImage]) " +
                        "VALUES " +
                        "(@RowId, @OrderID, @ResultItemID, @ResultType, @DateAssigned, @ResultText, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @IncludesRadiologyImage); " +
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

        public int UpdateListOrderResults(ListOrderResultsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListOrderResultsPoco poco = new ListOrderResultsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListOrderResults " +
                        "SET [OrderID] = @OrderID, [ResultItemID] = @ResultItemID, [ResultType] = @ResultType, " +
                        "[DateAssigned] = @DateAssigned, [ResultText] = @ResultText, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [IncludesRadiologyImage] = @IncludesRadiologyImage " +
                        "WHERE RowId = @RowId;";

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
