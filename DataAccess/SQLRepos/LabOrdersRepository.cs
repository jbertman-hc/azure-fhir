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
    public class LabOrdersRepository : ILabOrdersRepository
    {
        private readonly string _connectionString;

        public LabOrdersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LabOrdersDomain GetLabOrders(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM LabOrders WHERE LabResultID = @id";

                    var LabOrdersPoco = cn.QueryFirstOrDefault<LabOrdersPoco>(query, new { id = id }) ?? new LabOrdersPoco();

                    return LabOrdersPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LabOrdersDomain> GetLabOrders(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM LabOrders WHERE @criteria";
                    List<LabOrdersPoco> pocos = cn.Query<LabOrdersPoco>(sql).ToList();
                    List<LabOrdersDomain> domains = new List<LabOrdersDomain>();

                    foreach (LabOrdersPoco poco in pocos)
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

        public int DeleteLabOrders(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM LabOrders WHERE LabResultID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLabOrders(LabOrdersDomain domain)
        {
            int insertedId = 0;

            try
            {
                LabOrdersPoco poco = new LabOrdersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[LabOrders] " +
                        "([AccessionNbrAC], [LabTestID], [CreatedDt], [CreatedBy], [LastUpdDt], [LastUpdBy], " +
                        "[OrderingProviderID], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@AccessionNbrAC, @LabTestID, @CreatedDt, @CreatedBy, @LastUpdDt, @LastUpdBy, " +
                        "@OrderingProviderID, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateLabOrders(LabOrdersDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LabOrdersPoco poco = new LabOrdersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE LabOrders " +
                        "SET [LabTestID] = @LabTestID, [CreatedDt] = @CreatedDt, [CreatedBy] = @CreatedBy, " +
                        "[LastUpdDt] = @LastUpdDt, [LastUpdBy] = @LastUpdBy, " +
                        "[OrderingProviderID] = @OrderingProviderID, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE AccessionNbrAC = @AccessionNbrAC;";

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
