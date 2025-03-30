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
    public class ListOrdersRepository : IListOrdersRepository
    {
        private readonly string _connectionString;

        public ListOrdersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListOrdersDomain GetListOrders(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListOrders WHERE RowID = @id";

                    var ListOrdersPoco = cn.QueryFirstOrDefault<ListOrdersPoco>(query, new { id = id }) ?? new ListOrdersPoco();

                    return ListOrdersPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListOrdersDomain> GetListOrders(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = $"SELECT * FROM ListOrders WHERE {@criteria}";

                    List<ListOrdersPoco> pocos = cn.Query<ListOrdersPoco>(sql).ToList();
                    List<ListOrdersDomain> domains = new List<ListOrdersDomain>();

                    foreach (ListOrdersPoco poco in pocos)
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

        public int DeleteListOrders(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListOrders WHERE RowID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListOrders(ListOrdersDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListOrdersPoco poco = new ListOrdersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListOrders] " +
                        "([RowID], [PatientID], [OrderID], [OrderType], [OrderText], [CPTs], [ICDs], [Comments], " +
                        "[SentBy], [SentTo], [StatusID], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[DateSent], [OrderGUID], [IsPartialResult], [IsSigned]) " +
                        "VALUES " +
                        "(@RowID, @PatientID, @OrderID, @OrderType, @OrderText, @CPTs, @ICDs, @Comments, " +
                        "@SentBy, @SentTo, @StatusID, @DateLastTouched, @LastTouchedBy, @DateRowAdded, " +
                        "@DateSent, @OrderGUID, @IsPartialResult, @IsSigned); " +
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

        public int UpdateListOrders(ListOrdersDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListOrdersPoco poco = new ListOrdersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListOrders " +
                        "SET [PatientID] = @PatientID, [OrderID] = @OrderID, [OrderType] = @OrderType, " +
                        "[OrderText] = @OrderText, [CPTs] = @CPTs, [ICDs] = @ICDs, [Comments] = @Comments, " +
                        "[SentBy] = @SentBy, [SentTo] = @SentTo, [StatusID] = @StatusID, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [DateSent] = @DateSent, [OrderGUID] = @OrderGUID, " +
                        "[IsPartialResult] = @IsPartialResult, [IsSigned] = @IsSigned " +
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
