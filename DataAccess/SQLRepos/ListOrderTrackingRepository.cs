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
    public class ListOrderTrackingRepository : IListOrderTrackingRepository
    {
        private readonly string _connectionString;

        public ListOrderTrackingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListOrderTrackingDomain GetListOrderTracking(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListOrderTracking WHERE RowID = @id";

                    var ListOrderTrackingPoco = cn.QueryFirstOrDefault<ListOrderTrackingPoco>(query, new { id = id }) ?? new ListOrderTrackingPoco();

                    return ListOrderTrackingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListOrderTrackingDomain> GetListOrderTrackings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListOrderTracking WHERE @criteria";
                    List<ListOrderTrackingPoco> pocos = cn.Query<ListOrderTrackingPoco>(sql).ToList();
                    List<ListOrderTrackingDomain> domains = new List<ListOrderTrackingDomain>();

                    foreach (ListOrderTrackingPoco poco in pocos)
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

        public int DeleteListOrderTracking(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListOrderTracking WHERE RowID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListOrderTracking(ListOrderTrackingDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListOrderTrackingPoco poco = new ListOrderTrackingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListOrderTracking] " +
                        "([RowID], [ListOrderID], [OrderStatus], [DateDone], [Comments], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [DoneBy], [AssignedTo], [OriginalOrderText]) " +
                        "VALUES " +
                        "(@RowID, @ListOrderID, @OrderStatus, @DateDone, @Comments, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @DoneBy, @AssignedTo, @OriginalOrderText); " +
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

        public int UpdateListOrderTracking(ListOrderTrackingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListOrderTrackingPoco poco = new ListOrderTrackingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListOrderTracking " +
                        "SET [ListOrderID] = @ListOrderID, [OrderStatus] = @OrderStatus, [DateDone] = @DateDone, " +
                        "[Comments] = @Comments, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[DoneBy] = @DoneBy, [AssignedTo] = @AssignedTo, [OriginalOrderText] = @OriginalOrderText " +
                        "WHERE RowID = @RowID;";

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
