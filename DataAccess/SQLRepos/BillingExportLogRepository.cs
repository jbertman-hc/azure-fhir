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
    public class BillingExportLogRepository : IBillingExportLogRepository
    {
        private readonly string _connectionString;

        public BillingExportLogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BillingExportLogDomain GetBillingExportLog(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM BillingExportLog WHERE BillingExportLogID = @id";

                    var BillingExportLogPoco = cn.QueryFirstOrDefault<BillingExportLogPoco>(query, new { id = id }) ?? new BillingExportLogPoco();

                    return BillingExportLogPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BillingExportLogDomain> GetBillingExportLogs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM BillingExportLog WHERE @criteria";
                    List<BillingExportLogPoco> pocos = cn.Query<BillingExportLogPoco>(sql).ToList();
                    List<BillingExportLogDomain> domains = new List<BillingExportLogDomain>();

                    foreach (BillingExportLogPoco poco in pocos)
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

        public int DeleteBillingExportLog(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM BillingExportLog WHERE BillingExportLogID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertBillingExportLog(BillingExportLogDomain domain)
        {
            int insertedId = 0;

            try
            {
                BillingExportLogPoco poco = new BillingExportLogPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[BillingExportLog] " +
                        "([BillingExportLogID], [BillingID], [ExportFormatCode], [InterfaceName], " +
                        "[ExportDate], [PayorsID], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@BillingExportLogID, @BillingID, @ExportFormatCode, @InterfaceName, " +
                        "@ExportDate, @PayorsID, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateBillingExportLog(BillingExportLogDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                BillingExportLogPoco poco = new BillingExportLogPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE BillingExportLog " +
                        "SET [BillingID] = @BillingID, [ExportFormatCode] = @ExportFormatCode, " +
                        "[InterfaceName] = @InterfaceName, [ExportDate] = @ExportDate, " +
                        "[PayorsID] = @PayorsID, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE BillingExportLogID = @BillingExportLogID;";

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
