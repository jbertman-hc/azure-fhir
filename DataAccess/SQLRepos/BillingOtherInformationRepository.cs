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
    public class BillingOtherInformationRepository : IBillingOtherInformationRepository
    {
        private readonly string _connectionString;

        public BillingOtherInformationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BillingOtherInformationDomain GetBillingOtherInformation(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM BillingOtherInformation WHERE BillingOtherInformationID = @id";

                    var BillingOtherInformationPoco = cn.QueryFirstOrDefault<BillingOtherInformationPoco>(query, new { id = id }) ?? new BillingOtherInformationPoco();

                    return BillingOtherInformationPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BillingOtherInformationDomain> GetBillingOtherInformations(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM BillingOtherInformation WHERE @criteria";
                    List<BillingOtherInformationPoco> pocos = cn.Query<BillingOtherInformationPoco>(sql).ToList();
                    List<BillingOtherInformationDomain> domains = new List<BillingOtherInformationDomain>();

                    foreach (BillingOtherInformationPoco poco in pocos)
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

        public int DeleteBillingOtherInformation(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM BillingOtherInformation WHERE BillingOtherInformationID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertBillingOtherInformation(BillingOtherInformationDomain domain)
        {
            int insertedId = 0;

            try
            {
                BillingOtherInformationPoco poco = new BillingOtherInformationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[BillingOtherInformation] " +
                        "([BillingOtherInformationID], [BillingID], [PatientID], [PriorAuthorizationNumber], " +
                        "[ReferProviderID], [ServiceAuthExceptionCode], [AccidentDate], " +
                        "[DisabilityBeginDate], [DisabilityEndDate], [LastWorkedDate], " +
                        "[AuthorizedReturnToWorkDate], [InitialTreatmentDate], [AcuteManifestationDate], " +
                        "[LastXrayDate], [SpinalManipulationConditionID], [IsPatientHomebound], " +
                        "[TotalVisitsRendered], [ProjectedVisitCount], [VisitQuantity], [FrequencyCount], " +
                        "[FrequencyPeriod], [NumberOfPeriods], [PeriodUnit], [HospitalAdmissionDate], " +
                        "[HospitalDischargeDate], [CasualtyClaimNumber], [FirstSymptomDate], " +
                        "[IsEmergencyVisit], [DateLastTouched], [LastTouchedBy], [DateRowAdded], [ReferralNumber]) " +
                        "VALUES " +
                        "(@BillingOtherInformationID, @BillingID, @PatientID, @PriorAuthorizationNumber, " +
                        "@ReferProviderID, @ServiceAuthExceptionCode, @AccidentDate, @DisabilityBeginDate, " +
                        "@DisabilityEndDate, @LastWorkedDate, @AuthorizedReturnToWorkDate, " +
                        "@InitialTreatmentDate, @AcuteManifestationDate, @LastXrayDate, " +
                        "@SpinalManipulationConditionID, @IsPatientHomebound, @TotalVisitsRendered, " +
                        "@ProjectedVisitCount, @VisitQuantity, @FrequencyCount, @FrequencyPeriod, " +
                        "@NumberOfPeriods, @PeriodUnit, @HospitalAdmissionDate, @HospitalDischargeDate, " +
                        "@CasualtyClaimNumber, @FirstSymptomDate, @IsEmergencyVisit, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @ReferralNumber); " +
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

        public int UpdateBillingOtherInformation(BillingOtherInformationDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                BillingOtherInformationPoco poco = new BillingOtherInformationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE BillingOtherInformation " +
                        "SET [BillingID] = @BillingID, [PatientID] = @PatientID, " +
                        "[PriorAuthorizationNumber] = @PriorAuthorizationNumber, " +
                        "[ReferProviderID] = @ReferProviderID, " +
                        "[ServiceAuthExceptionCode] = @ServiceAuthExceptionCode, " +
                        "[AccidentDate] = @AccidentDate, [DisabilityBeginDate] = @DisabilityBeginDate, " +
                        "[DisabilityEndDate] = @DisabilityEndDate, [LastWorkedDate] = @LastWorkedDate, " +
                        "[AuthorizedReturnToWorkDate] = @AuthorizedReturnToWorkDate, " +
                        "[InitialTreatmentDate] = @InitialTreatmentDate, " +
                        "[AcuteManifestationDate] = @AcuteManifestationDate, " +
                        "[LastXrayDate] = @LastXrayDate, " +
                        "[SpinalManipulationConditionID] = @SpinalManipulationConditionID, " +
                        "[IsPatientHomebound] = @IsPatientHomebound, " +
                        "[TotalVisitsRendered] = @TotalVisitsRendered, " +
                        "[ProjectedVisitCount] = @ProjectedVisitCount, [VisitQuantity] = @VisitQuantity, " +
                        "[FrequencyCount] = @FrequencyCount, [FrequencyPeriod] = @FrequencyPeriod, " +
                        "[NumberOfPeriods] = @NumberOfPeriods, [PeriodUnit] = @PeriodUnit, " +
                        "[HospitalAdmissionDate] = @HospitalAdmissionDate, " +
                        "[HospitalDischargeDate] = @HospitalDischargeDate, " +
                        "[CasualtyClaimNumber] = @CasualtyClaimNumber, " +
                        "[FirstSymptomDate] = @FirstSymptomDate, [IsEmergencyVisit] = @IsEmergencyVisit, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [ReferralNumber] = @ReferralNumber " +
                        "WHERE BillingOtherInformationID = @BillingOtherInformationID;";

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
