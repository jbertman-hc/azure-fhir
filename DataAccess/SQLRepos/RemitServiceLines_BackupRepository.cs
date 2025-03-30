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
    public class RemitServiceLines_BackupRepository : IRemitServiceLines_BackupRepository
    {
        private readonly string _connectionString;

        public RemitServiceLines_BackupRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RemitServiceLines_BackupDomain GetRemitServiceLines_Backup(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM RemitServiceLines_Backup WHERE RemitServiceLinesID = @id";

                    var RemitServiceLines_BackupPoco = cn.QueryFirstOrDefault<RemitServiceLines_BackupPoco>(query, new { id = id }) ?? new RemitServiceLines_BackupPoco();

                    return RemitServiceLines_BackupPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RemitServiceLines_BackupDomain> GetRemitServiceLines_Backups(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM RemitServiceLines_Backup WHERE @criteria";
                    List<RemitServiceLines_BackupPoco> pocos = cn.Query<RemitServiceLines_BackupPoco>(sql).ToList();
                    List<RemitServiceLines_BackupDomain> domains = new List<RemitServiceLines_BackupDomain>();

                    foreach (RemitServiceLines_BackupPoco poco in pocos)
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

        public int DeleteRemitServiceLines_Backup(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM RemitServiceLines_Backup WHERE RemitServiceLinesID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertRemitServiceLines_Backup(RemitServiceLines_BackupDomain domain)
        {
            int insertedId = 0;

            try
            {
                RemitServiceLines_BackupPoco poco = new RemitServiceLines_BackupPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[RemitServiceLines_Backup] " +
                        "([RemitServiceLinesID], [RemitClaimsID], [CPTCode], [Units], [Charge], [Payment], " +
                        "[DeniedAmount], [DeniedCodes], [AllowedAmt], [RemarkCode], [BillingCPTsID], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [Bundled], [PatientChargesID], [OriginalKey]) " +
                        "VALUES " +
                        "(@RemitServiceLinesID, @RemitClaimsID, @CPTCode, @Units, @Charge, @Payment, @DeniedAmount, " +
                        "@DeniedCodes, @AllowedAmt, @RemarkCode, @BillingCPTsID, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @Bundled, @PatientChargesID, @OriginalKey); " +
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

        public int UpdateRemitServiceLines_Backup(RemitServiceLines_BackupDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                RemitServiceLines_BackupPoco poco = new RemitServiceLines_BackupPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE RemitServiceLines_Backup " +
                        "SET [RemitClaimsID] = @RemitClaimsID, [CPTCode] = @CPTCode, [Units] = @Units, " +
                        "[Charge] = @Charge, [Payment] = @Payment, [DeniedAmount] = @DeniedAmount, " +
                        "[DeniedCodes] = @DeniedCodes, [AllowedAmt] = @AllowedAmt, [RemarkCode] = @RemarkCode, " +
                        "[BillingCPTsID] = @BillingCPTsID, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [Bundled] = @Bundled, " +
                        "[PatientChargesID] = @PatientChargesID, [OriginalKey] = @OriginalKey " +
                        "WHERE RemitServiceLinesID = @RemitServiceLinesID;";

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
