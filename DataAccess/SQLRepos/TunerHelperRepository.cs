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
    public class TunerHelperRepository : ITunerHelperRepository
    {
        private readonly string _connectionString;

        public TunerHelperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TunerHelperDomain GetTunerHelper(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM TunerHelper WHERE rowid = @id";

                    var TunerHelperPoco = cn.QueryFirstOrDefault<TunerHelperPoco>(query, new { id = id }) ?? new TunerHelperPoco();

                    return TunerHelperPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<TunerHelperDomain> GetTunerHelpers(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM TunerHelper WHERE @criteria";
                    List<TunerHelperPoco> pocos = cn.Query<TunerHelperPoco>(sql).ToList();
                    List<TunerHelperDomain> domains = new List<TunerHelperDomain>();

                    foreach (TunerHelperPoco poco in pocos)
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

        public int DeleteTunerHelper(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM TunerHelper WHERE rowid = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertTunerHelper(TunerHelperDomain domain)
        {
            int insertedId = 0;

            try
            {
                TunerHelperPoco poco = new TunerHelperPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[TunerHelper] " +
                        "([rowid], [ProblemDateLastActivatedRemoved], [MigratedRecordsReleased], [MigratedReferrals2], " +
                        "[MigratedAllergies2], [ListImmunizationsDepricated], [MigratedInHouseTests], [MedsMigrated], " +
                        "[RefillsMigrated], [MigratedAllergies], [PracticeErxFixed], [UpdatedSpouseAndEmergencyContact], " +
                        "[UpdatedTakesNoMeds], [UpdatedRaceMissingValues], [UpdatedSoapICD9Data], [UpdatedEthnicity], " +
                        "[PayorMigrationMessageShown], [ClaimStatusReset], [UpdatedVitalComments], " +
                        "[MigratedCPT1CodesEdited], [SetObGynPregControl], [UpdateProviderCodeInBilling], " +
                        "[SetPMStartDate], [RemitFinalPayor], [BillingConversion], [MigratedSuperbillAccount], " +
                        "[MigratedSuperbillAccountFix], [PMEmailSent], [UpdatedRegistryUploads], [MigratedPatientRace], " +
                        "[MigratedPatientLanguage], [RegistryPatientInfoCreation], [MigrateExistingSOAPIcd9s]) " +
                        "VALUES " +
                        "(@rowid, @ProblemDateLastActivatedRemoved, @MigratedRecordsReleased, @MigratedReferrals2, " +
                        "@MigratedAllergies2, @ListImmunizationsDepricated, @MigratedInHouseTests, @MedsMigrated, " +
                        "@RefillsMigrated, @MigratedAllergies, @PracticeErxFixed, @UpdatedSpouseAndEmergencyContact, " +
                        "@UpdatedTakesNoMeds, @UpdatedRaceMissingValues, @UpdatedSoapICD9Data, @UpdatedEthnicity, " +
                        "@PayorMigrationMessageShown, @ClaimStatusReset, @UpdatedVitalComments, @MigratedCPT1CodesEdited, " +
                        "@SetObGynPregControl, @UpdateProviderCodeInBilling, @SetPMStartDate, @RemitFinalPayor, " +
                        "@BillingConversion, @MigratedSuperbillAccount, @MigratedSuperbillAccountFix, @PMEmailSent, " +
                        "@UpdatedRegistryUploads, @MigratedPatientRace, @MigratedPatientLanguage, " +
                        "@RegistryPatientInfoCreation, @MigrateExistingSOAPIcd9s); " +
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

        public int UpdateTunerHelper(TunerHelperDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                TunerHelperPoco poco = new TunerHelperPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE TunerHelper " +
                        "SET [ProblemDateLastActivatedRemoved] = @ProblemDateLastActivatedRemoved, " +
                        "[MigratedRecordsReleased] = @MigratedRecordsReleased, [MigratedReferrals2] = @MigratedReferrals2, " +
                        "[MigratedAllergies2] = @MigratedAllergies2, " +
                        "[ListImmunizationsDepricated] = @ListImmunizationsDepricated, " +
                        "[MigratedInHouseTests] = @MigratedInHouseTests, [MedsMigrated] = @MedsMigrated, " +
                        "[RefillsMigrated] = @RefillsMigrated, [MigratedAllergies] = @MigratedAllergies, " +
                        "[PracticeErxFixed] = @PracticeErxFixed, " +
                        "[UpdatedSpouseAndEmergencyContact] = @UpdatedSpouseAndEmergencyContact, " +
                        "[UpdatedTakesNoMeds] = @UpdatedTakesNoMeds, [UpdatedRaceMissingValues] = @UpdatedRaceMissingValues, " +
                        "[UpdatedSoapICD9Data] = @UpdatedSoapICD9Data, [UpdatedEthnicity] = @UpdatedEthnicity, " +
                        "[PayorMigrationMessageShown] = @PayorMigrationMessageShown, " +
                        "[ClaimStatusReset] = @ClaimStatusReset, [UpdatedVitalComments] = @UpdatedVitalComments, " +
                        "[MigratedCPT1CodesEdited] = @MigratedCPT1CodesEdited, [SetObGynPregControl] = @SetObGynPregControl, " +
                        "[UpdateProviderCodeInBilling] = @UpdateProviderCodeInBilling, [SetPMStartDate] = @SetPMStartDate, " +
                        "[RemitFinalPayor] = @RemitFinalPayor, [BillingConversion] = @BillingConversion, " +
                        "[MigratedSuperbillAccount] = @MigratedSuperbillAccount, " +
                        "[MigratedSuperbillAccountFix] = @MigratedSuperbillAccountFix, [PMEmailSent] = @PMEmailSent, " +
                        "[UpdatedRegistryUploads] = @UpdatedRegistryUploads, [MigratedPatientRace] = @MigratedPatientRace, " +
                        "[MigratedPatientLanguage] = @MigratedPatientLanguage, " +
                        "[RegistryPatientInfoCreation] = @RegistryPatientInfoCreation, " +
                        "[MigrateExistingSOAPIcd9s] = @MigrateExistingSOAPIcd9s " +
                        "WHERE rowid = @rowid;";

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
