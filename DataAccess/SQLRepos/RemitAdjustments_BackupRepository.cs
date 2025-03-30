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
    public class RemitAdjustments_BackupRepository : IRemitAdjustments_BackupRepository
    {
        private readonly string _connectionString;

        public RemitAdjustments_BackupRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RemitAdjustments_BackupDomain GetRemitAdjustments_Backup(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM RemitAdjustments_Backup WHERE AdjustmentID = @id";

                    var RemitAdjustments_BackupPoco = cn.QueryFirstOrDefault<RemitAdjustments_BackupPoco>(query, new { id = id }) ?? new RemitAdjustments_BackupPoco();

                    return RemitAdjustments_BackupPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RemitAdjustments_BackupDomain> GetRemitAdjustments_Backups(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM RemitAdjustments_Backup WHERE @criteria";
                    List<RemitAdjustments_BackupPoco> pocos = cn.Query<RemitAdjustments_BackupPoco>(sql).ToList();
                    List<RemitAdjustments_BackupDomain> domains = new List<RemitAdjustments_BackupDomain>();

                    foreach (RemitAdjustments_BackupPoco poco in pocos)
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

        public int DeleteRemitAdjustments_Backup(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM RemitAdjustments_Backup WHERE AdjustmentID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertRemitAdjustments_Backup(RemitAdjustments_BackupDomain domain)
        {
            int insertedId = 0;

            try
            {
                RemitAdjustments_BackupPoco poco = new RemitAdjustments_BackupPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[RemitAdjustments_Backup] " +
                        "([AdjustmentID], [RemitClaimsID], [RemitServiceLinesID], [AdjustmentAMT], [AdjustmentCode], " +
                        "[AdjustmentReason], [DateLastTouched], [LastTouchedBy], [DateRowAdded], [OriginalKey]) " +
                        "VALUES " +
                        "(@AdjustmentID, @RemitClaimsID, @RemitServiceLinesID, @AdjustmentAMT, @AdjustmentCode, " +
                        "@AdjustmentReason, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @OriginalKey); " +
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

        public int UpdateRemitAdjustments_Backup(RemitAdjustments_BackupDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                RemitAdjustments_BackupPoco poco = new RemitAdjustments_BackupPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE RemitAdjustments_Backup " +
                        "SET [RemitClaimsID] = @RemitClaimsID, [RemitServiceLinesID] = @RemitServiceLinesID, " +
                        "[AdjustmentAMT] = @AdjustmentAMT, [AdjustmentCode] = @AdjustmentCode, " +
                        "[AdjustmentReason] = @AdjustmentReason, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [OriginalKey] = @OriginalKey " +
                        "WHERE AdjustmentID = @AdjustmentID;";

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
