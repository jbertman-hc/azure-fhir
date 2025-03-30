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
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;

namespace POC.DataAccess.SQLRepos
{
    public class DemographicsRepository : IDemographicsRepository
    {
        private readonly string _connectionString;

        public DemographicsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DemographicsDomain GetDemographics(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Demographics WHERE PatientId = @id";

                    var DemographicsPoco = cn.QueryFirstOrDefault<DemographicsPoco>(query, new { id = id }) ?? new DemographicsPoco();

                    return DemographicsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<DemographicsDomain> GetDemographics(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Demographics WHERE @criteria";
                    List<DemographicsPoco> pocos = cn.Query<DemographicsPoco>(sql).ToList();
                    List<DemographicsDomain> domains = new List<DemographicsDomain>();

                    foreach (DemographicsPoco poco in pocos)
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

        public int DeleteDemographics(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE FROM Demographics WHERE PatientId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertDemographics(DemographicsDomain domain)
        {
            int insertedId = 0;

            try
            {
                DemographicsPoco poco = new DemographicsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Demographics] " +
                        "([ChartID], [Salutation], [First], [Middle], [Last], [Suffix], [Gender], " +
                        "[BirthDate], [SS], [PatientAddress], [City], [State], [Zip], [Phone], " +
                        "[WorkPhone], [Fax], [Email], [EmployerName], [EmergencyContactName], " +
                        "[EmergencyContactPhone], [SpouseName], [InsuranceType], [PatientRel], " +
                        "[InsuredPlanName], [InsuredIDNo], [InsuredName], [InsuredGroupNo], [Copay], " +
                        "[InsuraceNotes], [InsuredPlanName2], [InsuredIDNo2], [InsuredName2], " +
                        "[InsuredGroupNo2], [Copay2], [Comments], [RecordsReleased], [Referredby], " +
                        "[ReferredbyMore], [Inactive], [ReasonInactive], [PreferredPhysician], " +
                        "[PreferredPharmacy], [UserLog], [DateAdded], [Photo], [Picture], " +
                        "[ReferringDoc], [ReferringNumber], [InsuredsDOB], [InsAddL1], [InsAddL2], " +
                        "[InsAddCity], [InsAddState], [InsAddZip], [InsAddPhone], [Insureds2DOB], " +
                        "[Ins2AddL1], [Ins2AddL2], [Ins2AddCity], [Ins2AddState], [Ins2AddZip], " +
                        "[Ins2AddPhone], [Miscellaneous1], [Miscellaneous2], [Miscellaneous3], " +
                        "[Miscellaneous4], [MaritalStatus], [AllergiesDemo], [ImageName], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [ExemptFromReporting], " +
                        "[TakesNoMeds], [PatientRace], [ExemptFromReportingReason], [InsuranceNotes2], " +
                        "[PatientRel2], [PatientAddress2], [PatientGUID], [LanguagePreference], " +
                        "[BarriersToCommunication], [MiddleName], [ContactPreference], [EthnicityID], " +
                        "[HasNoActiveDiagnoses], [VFCInitialScreen], [VFCLastScreen], [VFCReasonID], " +
                        "[DateOfDeath], [StatementDeliveryMethod], [IsPayorConverted], " +
                        "[IsPayorConverted2], [MothersMaidenName], [BirthOrder], [DateTimePatientInactivated], " +
                        "[PublicityCode], [PublicityCodeEffectiveDate], [MothersFirstName], [AcPmAccountId]) " +
                        "VALUES " +
                        "(@ChartID, @Salutation, @First, @Middle, @Last, @Suffix, @Gender, " +
                        "@BirthDate, @SS, @PatientAddress, @City, @State, @Zip, @Phone, " +
                        "@WorkPhone, @Fax, @Email, @EmployerName, @EmergencyContactName, " +
                        "@EmergencyContactPhone, @SpouseName, @InsuranceType, @PatientRel, " +
                        "@InsuredPlanName, @InsuredIDNo, @InsuredName, @InsuredGroupNo, @Copay, " +
                        "@InsuraceNotes, @InsuredPlanName2, @InsuredIDNo2, @InsuredName2, " +
                        "@InsuredGroupNo2, @Copay2, @Comments, @RecordsReleased, @Referredby, " +
                        "@ReferredbyMore, @Inactive, @ReasonInactive, @PreferredPhysician, " +
                        "@PreferredPharmacy, @UserLog, @DateAdded, @Photo, @Picture, " +
                        "@ReferringDoc, @ReferringNumber, @InsuredsDOB, @InsAddL1, @InsAddL2, " +
                        "@InsAddCity, @InsAddState, @InsAddZip, @InsAddPhone, @Insureds2DOB, " +
                        "@Ins2AddL1, @Ins2AddL2, @Ins2AddCity, @Ins2AddState, @Ins2AddZip, " +
                        "@Ins2AddPhone, @Miscellaneous1, @Miscellaneous2, @Miscellaneous3, " +
                        "@Miscellaneous4, @MaritalStatus, @AllergiesDemo, @ImageName, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @ExemptFromReporting, " +
                        "@TakesNoMeds, @PatientRace, @ExemptFromReportingReason, @InsuranceNotes2, " +
                        "@PatientRel2, @PatientAddress2, @PatientGUID, @LanguagePreference, " +
                        "@BarriersToCommunication, @MiddleName, @ContactPreference, @EthnicityID, " +
                        "@HasNoActiveDiagnoses, @VFCInitialScreen, @VFCLastScreen, @VFCReasonID, " +
                        "@DateOfDeath, @StatementDeliveryMethod, @IsPayorConverted, " +
                        "@IsPayorConverted2, @MothersMaidenName, @BirthOrder, @DateTimePatientInactivated, " +
                        "@PublicityCode, @PublicityCodeEffectiveDate, @MothersFirstName, @AcPmAccountId); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT)";

                    insertedId = cn.Query<int>(sql, poco).Single();
                    return insertedId;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateDemographics(DemographicsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                DemographicsPoco poco = new DemographicsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Demographics SET" +
                        "[ChartID] = @ChartID, [Salutation] = @Salutation, [First] = @First, " +
                        "[Middle] = @Middle, [Last] = @Last, [Suffix] = @Suffix, " +
                        "[Gender] = @Gender, [BirthDate] = @BirthDate, [SS] = @SS, " +
                        "[PatientAddress] = @PatientAddress, [City] = @City, [State] = @State, " +
                        "[Zip] = @Zip, [Phone] = @Phone, [WorkPhone] = @WorkPhone, [Fax] = @Fax, " +
                        "[Email] = @Email, [EmployerName] = @EmployerName, " +
                        "[EmergencyContactName] = @EmergencyContactName, " +
                        "[EmergencyContactPhone] = @EmergencyContactPhone, [SpouseName] = @SpouseName, " +
                        "[InsuranceType] = @InsuranceType, [PatientRel] = @PatientRel, " +
                        "[InsuredPlanName] = @InsuredPlanName, [InsuredIDNo] = @InsuredIDNo, " +
                        "[InsuredName] = @InsuredName, [InsuredGroupNo] = @InsuredGroupNo, " +
                        "[Copay] = @Copay, [InsuraceNotes] = @InsuraceNotes, [InsuredPlanName2] = @InsuredPlanName2, " +
                        "[InsuredIDNo2] = @InsuredIDNo2, [InsuredName2] = @InsuredName2, " +
                        "[InsuredGroupNo2] = @InsuredGroupNo2, [Copay2] = @Copay2, [Comments] = @Comments, " +
                        "[RecordsReleased] = @RecordsReleased, [Referredby] = @Referredby, " +
                        "[ReferredbyMore] = @ReferredbyMore, [Inactive] = @Inactive, " +
                        "[ReasonInactive] = @ReasonInactive, [PreferredPhysician] = @PreferredPhysician, " +
                        "[PreferredPharmacy] = @PreferredPharmacy, [UserLog] = @UserLog, " +
                        "[DateAdded] = @DateAdded, [Photo] = @Photo, [Picture] = @Picture, " +
                        "[ReferringDoc] = @ReferringDoc, [ReferringNumber] = @ReferringNumber, " +
                        "[InsuredsDOB] = @InsuredsDOB, [InsAddL1] = @InsAddL1, [InsAddL2] = @InsAddL2, " +
                        "[InsAddCity] = @InsAddCity, [InsAddState] = @InsAddState, [InsAddZip] = @InsAddZip, " +
                        "[InsAddPhone] = @InsAddPhone, [Insureds2DOB] = @Insureds2DOB, [Ins2AddL1] = @Ins2AddL1, " +
                        "[Ins2AddL2] = @Ins2AddL2, [Ins2AddCity] = @Ins2AddCity, [Ins2AddState] = @Ins2AddState, " +
                        "[Ins2AddZip] = @Ins2AddZip, [Ins2AddPhone] = @Ins2AddPhone, " +
                        "[Miscellaneous1] = @Miscellaneous1, [Miscellaneous2] = @Miscellaneous2, " +
                        "[Miscellaneous3] = @Miscellaneous3, [Miscellaneous4] = @Miscellaneous4, " +
                        "[MaritalStatus] = @MaritalStatus, [AllergiesDemo] = @AllergiesDemo, " +
                        "[ImageName] = @ImageName, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[ExemptFromReporting] = @ExemptFromReporting, [TakesNoMeds] = @TakesNoMeds, " +
                        "[PatientRace] = @PatientRace, [ExemptFromReportingReason] = @ExemptFromReportingReason, " +
                        "[InsuranceNotes2] = @InsuranceNotes2, [PatientRel2] = @PatientRel2, " +
                        "[PatientAddress2] = @PatientAddress2, [PatientGUID] = @PatientGUID, " +
                        "[LanguagePreference] = @LanguagePreference, [BarriersToCommunication] = @BarriersToCommunication, " +
                        "[MiddleName] = @MiddleName, [ContactPreference] = @ContactPreference, " +
                        "[EthnicityID] = @EthnicityID, [HasNoActiveDiagnoses] = @HasNoActiveDiagnoses, " +
                        "[VFCInitialScreen] = @VFCInitialScreen, [VFCLastScreen] = @VFCLastScreen, " +
                        "[VFCReasonID] = @VFCReasonID, [DateOfDeath] = @DateOfDeath, " +
                        "[StatementDeliveryMethod] = @StatementDeliveryMethod, " +
                        "[IsPayorConverted] = @IsPayorConverted, [IsPayorConverted2] = @IsPayorConverted2, " +
                        "[MothersMaidenName] = @MothersMaidenName, [BirthOrder] = @BirthOrder, " +
                        "[DateTimePatientInactivated] = @DateTimePatientInactivated, " +
                        "[PublicityCode] = @PublicityCode, [PublicityCodeEffectiveDate] = @PublicityCodeEffectiveDate, " +
                        "[MothersFirstName] = @MothersFirstName, [AcPmAccountId] = @AcPmAccountId " +
                        "WHERE PatientID = @PatientID;";

                    rowsAffected = cn.Execute(sql, poco);
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
