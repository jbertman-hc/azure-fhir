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
    public class PatientChargesRepository : IPatientChargesRepository
    {
        private readonly string _connectionString;

        public PatientChargesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PatientChargesDomain GetPatientCharges(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PatientCharges WHERE PatientChargesID = @id";

                    var PatientChargesPoco = cn.QueryFirstOrDefault<PatientChargesPoco>(query, new { id = id }) ?? new PatientChargesPoco();

                    return PatientChargesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PatientChargesDomain> GetPatientCharges(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PatientCharges WHERE @criteria";
                    List<PatientChargesPoco> pocos = cn.Query<PatientChargesPoco>(sql).ToList();
                    List<PatientChargesDomain> domains = new List<PatientChargesDomain>();

                    foreach (PatientChargesPoco poco in pocos)
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

        public int DeletePatientCharges(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PatientCharges WHERE PatientChargesID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPatientCharges(PatientChargesDomain domain)
        {
            int insertedId = 0;

            try
            {
                PatientChargesPoco poco = new PatientChargesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PatientCharges] " +
                        "([PatientChargesID], [PatientID], [ChargeTypeID], [Amount], [FullyPaid], [DateEntered], " +
                        "[Comments], [RemitServiceLinesID], [BillingCPTsID], [BillingID], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [Ledger], [ReconciledToRemit], [Historical], [ChargeText], " +
                        "[ChargeName]) " +
                        "VALUES " +
                        "(@PatientChargesID, @PatientID, @ChargeTypeID, @Amount, @FullyPaid, @DateEntered, " +
                        "@Comments, @RemitServiceLinesID, @BillingCPTsID, @BillingID, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @Ledger, @ReconciledToRemit, @Historical, @ChargeText, @ChargeName); " +
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

        public int UpdatePatientCharges(PatientChargesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PatientChargesPoco poco = new PatientChargesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PatientCharges " +
                        "SET [PatientID] = @PatientID, [ChargeTypeID] = @ChargeTypeID, [Amount] = @Amount, " +
                        "[FullyPaid] = @FullyPaid, [DateEntered] = @DateEntered, [Comments] = @Comments, " +
                        "[RemitServiceLinesID] = @RemitServiceLinesID, [BillingCPTsID] = @BillingCPTsID, " +
                        "[BillingID] = @BillingID, [DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [Ledger] = @Ledger, [ReconciledToRemit] = @ReconciledToRemit, " +
                        "[Historical] = @Historical, [ChargeText] = @ChargeText, [ChargeName] = @ChargeName " +
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
