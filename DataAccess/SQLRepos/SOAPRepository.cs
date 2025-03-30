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
    public class SOAPRepository : ISOAPRepository
    {
        private readonly string _connectionString;

        public SOAPRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SOAPDomain GetSOAP(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SOAP WHERE VisitNumber = @id";

                    var SOAPPoco = cn.QueryFirstOrDefault<SOAPPoco>(query, new { id = id }) ?? new SOAPPoco();

                    return SOAPPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SOAPDomain> GetSOAPs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SOAP WHERE @criteria";
                    List<SOAPPoco> pocos = cn.Query<SOAPPoco>(sql).ToList();
                    List<SOAPDomain> domains = new List<SOAPDomain>();

                    foreach (SOAPPoco poco in pocos)
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

        public int DeleteSOAP(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SOAP WHERE VisitNumber = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSOAP(SOAPDomain domain)
        {
            int insertedId = 0;

            try
            {
                SOAPPoco poco = new SOAPPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SOAP] " +
                        "([VisitNumber], [PatientID], [PatientName], [EncounterDate], [CC], [HPI], [ROS], [PMH], " +
                        "[Meds], [Allergies], [SH], [FH], [PE], [Ass], [Plan], [BP], [Temp], [RR], [Pulse], " +
                        "[Weight], [Height], [BMI], [HC], [VitalComments], [VisitCode], [ProviderID], " +
                        "[ProviderSignature], [lblAgeGenderSentence], [CallBackComment], [CPTcode], [CPTcomments], " +
                        "[Tests], [Image1Location], [Image2Location], [Image1LocationOLD], [Image2LocationOLD], " +
                        "[Image1Desc], [Image2Desc], [Illustration1Loc], [Illustration2Loc], [Illustration1Desc], " +
                        "[Illustration2Desc], [DateLastTouched], [LastTouchedBy], [DateRowAdded], [Sat], [Pain], " +
                        "[PF], [OtherVS], [ConfidentialInformation], [SatAirSource], [SatSuppO2Amount], [PFPost], " +
                        "[SatSuppO2Type], [PacksPerDay], [YearsSmoked], [YearsQuit], [IsResourceProvided], " +
                        "[TobaccoCDCCode], [IsMedReconciled], [IsPatientTransfer], [LastMenstrualPeriod], " +
                        "[EstimatedDeliveryDate], [PregnancyComments], [VisionOS], [VisionOD], [Hearing], " +
                        "[HearingComments], [SBPSupine], [DBPSupine], [TobaccoStatusStartDate], [TobaccoStatusEndDate], " +
                        "[TobaccoPipeSmoker], [TobaccoPipeStartDate], [TobaccoPipeEndDate], [TobaccoCigarSmoker], " +
                        "[TobaccoCigarStartDate], [TobaccoCigarEndDate], [TobaccoChewing], [TobaccoChewingStartDate], " +
                        "[TobaccoChewingEndDate], [DeclinedClinicalSummary], [Instructions], [IsIcd10]) " +
                        "VALUES " +
                        "(@VisitNumber, @PatientID, @PatientName, @EncounterDate, @CC, @HPI, @ROS, @PMH, @Meds, " +
                        "@Allergies, @SH, @FH, @PE, @Ass, @Plan, @BP, @Temp, @RR, @Pulse, @Weight, @Height, @BMI, " +
                        "@HC, @VitalComments, @VisitCode, @ProviderID, @ProviderSignature, @lblAgeGenderSentence, " +
                        "@CallBackComment, @CPTcode, @CPTcomments, @Tests, @Image1Location, @Image2Location, " +
                        "@Image1LocationOLD, @Image2LocationOLD, @Image1Desc, @Image2Desc, @Illustration1Loc, " +
                        "@Illustration2Loc, @Illustration1Desc, @Illustration2Desc, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @Sat, @Pain, @PF, @OtherVS, @ConfidentialInformation, @SatAirSource, " +
                        "@SatSuppO2Amount, @PFPost, @SatSuppO2Type, @PacksPerDay, @YearsSmoked, @YearsQuit, " +
                        "@IsResourceProvided, @TobaccoCDCCode, @IsMedReconciled, @IsPatientTransfer, " +
                        "@LastMenstrualPeriod, @EstimatedDeliveryDate, @PregnancyComments, @VisionOS, @VisionOD, " +
                        "@Hearing, @HearingComments, @SBPSupine, @DBPSupine, @TobaccoStatusStartDate, " +
                        "@TobaccoStatusEndDate, @TobaccoPipeSmoker, @TobaccoPipeStartDate, @TobaccoPipeEndDate, " +
                        "@TobaccoCigarSmoker, @TobaccoCigarStartDate, @TobaccoCigarEndDate, @TobaccoChewing, " +
                        "@TobaccoChewingStartDate, @TobaccoChewingEndDate, @DeclinedClinicalSummary, @Instructions, @IsIcd10); " +
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

        public int UpdateSOAP(SOAPDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SOAPPoco poco = new SOAPPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SOAP " +
                        "SET [PatientID] = @PatientID, [PatientName] = @PatientName, [EncounterDate] = @EncounterDate, " +
                        "[CC] = @CC, [HPI] = @HPI, [ROS] = @ROS, [PMH] = @PMH, [Meds] = @Meds, [Allergies] = @Allergies, " +
                        "[SH] = @SH, [FH] = @FH, [PE] = @PE, [Ass] = @Ass, [Plan] = @Plan, [BP] = @BP, [Temp] = @Temp, " +
                        "[RR] = @RR, [Pulse] = @Pulse, [Weight] = @Weight, [Height] = @Height, [BMI] = @BMI, [HC] = @HC, " +
                        "[VitalComments] = @VitalComments, [VisitCode] = @VisitCode, [ProviderID] = @ProviderID, " +
                        "[ProviderSignature] = @ProviderSignature, [lblAgeGenderSentence] = @lblAgeGenderSentence, " +
                        "[CallBackComment] = @CallBackComment, [CPTcode] = @CPTcode, [CPTcomments] = @CPTcomments, " +
                        "[Tests] = @Tests, [Image1Location] = @Image1Location, [Image2Location] = @Image2Location, " +
                        "[Image1LocationOLD] = @Image1LocationOLD, [Image2LocationOLD] = @Image2LocationOLD, " +
                        "[Image1Desc] = @Image1Desc, [Image2Desc] = @Image2Desc, [Illustration1Loc] = @Illustration1Loc, " +
                        "[Illustration2Loc] = @Illustration2Loc, [Illustration1Desc] = @Illustration1Desc, " +
                        "[Illustration2Desc] = @Illustration2Desc, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [Sat] = @Sat, [Pain] = @Pain, " +
                        "[PF] = @PF, [OtherVS] = @OtherVS, [ConfidentialInformation] = @ConfidentialInformation, " +
                        "[SatAirSource] = @SatAirSource, [SatSuppO2Amount] = @SatSuppO2Amount, [PFPost] = @PFPost, " +
                        "[SatSuppO2Type] = @SatSuppO2Type, [PacksPerDay] = @PacksPerDay, [YearsSmoked] = @YearsSmoked, " +
                        "[YearsQuit] = @YearsQuit, [IsResourceProvided] = @IsResourceProvided, " +
                        "[TobaccoCDCCode] = @TobaccoCDCCode, [IsMedReconciled] = @IsMedReconciled, " +
                        "[IsPatientTransfer] = @IsPatientTransfer, [LastMenstrualPeriod] = @LastMenstrualPeriod, " +
                        "[EstimatedDeliveryDate] = @EstimatedDeliveryDate, [PregnancyComments] = @PregnancyComments, " +
                        "[VisionOS] = @VisionOS, [VisionOD] = @VisionOD, [Hearing] = @Hearing, " +
                        "[HearingComments] = @HearingComments, [SBPSupine] = @SBPSupine, [DBPSupine] = @DBPSupine, " +
                        "[TobaccoStatusStartDate] = @TobaccoStatusStartDate, [TobaccoStatusEndDate] = @TobaccoStatusEndDate, " +
                        "[TobaccoPipeSmoker] = @TobaccoPipeSmoker, [TobaccoPipeStartDate] = @TobaccoPipeStartDate, " +
                        "[TobaccoPipeEndDate] = @TobaccoPipeEndDate, [TobaccoCigarSmoker] = @TobaccoCigarSmoker, " +
                        "[TobaccoCigarStartDate] = @TobaccoCigarStartDate, [TobaccoCigarEndDate] = @TobaccoCigarEndDate, " +
                        "[TobaccoChewing] = @TobaccoChewing, [TobaccoChewingStartDate] = @TobaccoChewingStartDate, " +
                        "[TobaccoChewingEndDate] = @TobaccoChewingEndDate, " +
                        "[DeclinedClinicalSummary] = @DeclinedClinicalSummary, [Instructions] = @Instructions, " +
                        "[IsIcd10] = @IsIcd10 " +
                        "WHERE VisitNumber = @VisitNumber;";

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
