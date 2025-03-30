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
    public class PatientCharges_BackupRepository : IPatientCharges_BackupRepository
    {
        private readonly string _connectionString;

        public PatientCharges_BackupRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PatientCharges_BackupDomain GetPatientCharges_Backup(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PatientCharges_Backup WHERE PatientChargesID = @id";

                    var PatientCharges_BackupPoco = cn.QueryFirstOrDefault<PatientCharges_BackupPoco>(query, new { id = id }) ?? new PatientCharges_BackupPoco();

                    return PatientCharges_BackupPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PatientCharges_BackupDomain> GetPatientCharges_Backups(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PatientCharges_Backup WHERE @criteria";
                    List<PatientCharges_BackupPoco> pocos = cn.Query<PatientCharges_BackupPoco>(sql).ToList();
                    List<PatientCharges_BackupDomain> domains = new List<PatientCharges_BackupDomain>();

                    foreach (PatientCharges_BackupPoco poco in pocos)
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

        public int DeletePatientCharges_Backup(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PatientCharges_Backup WHERE PatientChargesID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPatientCharges_Backup(PatientCharges_BackupDomain domain)
        {
            int insertedId = 0;

            try
            {
                PatientCharges_BackupPoco poco = new PatientCharges_BackupPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PatientCharges_Backup] " +
                        "([PatientChargesID], [PatientID], [ChargeTypeID], [Amount], [FullyPaid], [DateEntered], " +
                        "[Comments], [RemitServiceLinesID], [BillingCPTsID], [BillingID], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [Ledger], [ReconciledToRemit], [Historical], [ChargeText], " +
                        "[ChargeName], [OriginalKey]) " +
                        "VALUES " +
                        "(@PatientChargesID, @PatientID, @ChargeTypeID, @Amount, @FullyPaid, @DateEntered, " +
                        "@Comments, @RemitServiceLinesID, @BillingCPTsID, @BillingID, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @Ledger, @ReconciledToRemit, @Historical, @ChargeText, " +
                        "@ChargeName, @OriginalKey); " +
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

        public int UpdatePatientCharges_Backup(PatientCharges_BackupDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PatientCharges_BackupPoco poco = new PatientCharges_BackupPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PatientCharges_Backup " +
                        "SET [PatientID] = @PatientID, [ChargeTypeID] = @ChargeTypeID, [Amount] = @Amount, " +
                        "[FullyPaid] = @FullyPaid, [DateEntered] = @DateEntered, [Comments] = @Comments, " +
                        "[RemitServiceLinesID] = @RemitServiceLinesID, [BillingCPTsID] = @BillingCPTsID, " +
                        "[BillingID] = @BillingID, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [Ledger] = @Ledger, " +
                        "[ReconciledToRemit] = @ReconciledToRemit, [Historical] = @Historical, " +
                        "[ChargeText] = @ChargeText, [ChargeName] = @ChargeName, [OriginalKey] = @OriginalKey " +
                        "WHERE PatientChargesID = @PatientChargesID;";

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
