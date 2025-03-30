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
    public class EmailRepository : IEmailRepository
    {
        private readonly string _connectionString;

        public EmailRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public EmailDomain GetEmail(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Email WHERE msgID = @id";

                    var EmailPoco = cn.QueryFirstOrDefault<EmailPoco>(query, new { id = id }) ?? new EmailPoco();

                    return EmailPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<EmailDomain> GetEmails(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Email WHERE @criteria";
                    List<EmailPoco> pocos = cn.Query<EmailPoco>(sql).ToList();
                    List<EmailDomain> domains = new List<EmailDomain>();

                    foreach (EmailPoco poco in pocos)
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

        public int DeleteEmail(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Email WHERE msgID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertEmail(EmailDomain domain)
        {
            int insertedId = 0;

            try
            {
                EmailPoco poco = new EmailPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Email] " +
                        "([msgID], [To], [From], [Date], [Re], [CC], [PatientName], [Body], [PatientID], " +
                        "[Chart], [Chief], [HPI], [ROS], [PMH], [Meds], [Allergies], [SH], [FH], [PE], " +
                        "[Ass], [Plan], [SBP], [DBP], [Temp], [RR], [Pulse], [Weight], [Height], [HC], " +
                        "[VitalComments], [Test1], [TestResult1], [TestDate1], [Test2], [TestResult2], " +
                        "[TestDate2], [Test3], [TestResult3], [TestDate3], [Test4], [TestResult4], " +
                        "[TestDate4], [Test5], [TestResult5], [TestDate5], [Test6], [TestResult6], " +
                        "[TestDate6], [Test7], [TestResult7], [TestDate7], [Test8], [TestResult8], " +
                        "[TestDate8], [Test9], [TestResult9], [TestDate9], [Test10], [TestResult10], " +
                        "[TestDate10], [Test11], [TestResult11], [TestDate11], [Test12], [TestResult12], " +
                        "[TestDate12], [Test13], [TestResult13], [TestDate13], [Test14], [TestResult14], " +
                        "[TestDate14], [CallBackComment], [Image1Location], [Image2Location], " +
                        "[Image1LocationOld], [Image2LocationOld], [Image1Desc], [Image2Desc], " +
                        "[PicForwarded1], [PicForwarded2], [NoteTypeCoSign], [NoteTypeRefill], " +
                        "[EncounterDate], [Illustration1Loc], [Illustration2Loc], [Illustration1Desc], " +
                        "[Illustration2Desc], [LabTestID], [LabSignOffID], [LabSignOffDt], [ImportItemID], " +
                        "[DeleteItem], [MsgHighlightColor], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded], [Sat], [Pain], [PF], [OtherVS], [ConfidentialInformation], " +
                        "[ActiveTemplates], [TaskComplete], [MetaData], [SatAirSource], [SatSuppO2Amount], " +
                        "[PFPost], [SatSuppO2Type], [PacksPerDay], [YearsSmoked], [YearsQuit], " +
                        "[IsResourceProvided], [TobaccoCDCCode], [IsMedReconciled], [IsPatientTransfer], " +
                        "[LastMenstrualPeriod], [EstimatedDeliveryDate], [PregnancyComments], [VisionOS], " +
                        "[VisionOD], [Hearing], [HearingComments], [SBPSupine], [DBPSupine], [Link], " +
                        "[TobaccoStatusStartDate], [TobaccoStatusEndDate], [TobaccoPipeSmoker], " +
                        "[TobaccoPipeStartDate], [TobaccoPipeEndDate], [TobaccoCigarSmoker], " +
                        "[TobaccoCigarStartDate], [TobaccoCigarEndDate], [TobaccoChewing], " +
                        "[TobaccoChewingStartDate], [TobaccoChewingEndDate], [IsFromCICO], " +
                        "[DeclinedClinicalSummary], [Instructions], [IsIcd10], [LbsOzMode]) " +
                        "VALUES " +
                        "(@msgID, @To, @From, @Date, @Re, @CC, @PatientName, @Body, @PatientID, @Chart, " +
                        "@Chief, @HPI, @ROS, @PMH, @Meds, @Allergies, @SH, @FH, @PE, @Ass, @Plan, @SBP, " +
                        "@DBP, @Temp, @RR, @Pulse, @Weight, @Height, @HC, @VitalComments, @Test1, @TestResult1, " +
                        "@TestDate1, @Test2, @TestResult2, @TestDate2, @Test3, @TestResult3, @TestDate3, " +
                        "@Test4, @TestResult4, @TestDate4, @Test5, @TestResult5, @TestDate5, @Test6, " +
                        "@TestResult6, @TestDate6, @Test7, @TestResult7, @TestDate7, @Test8, @TestResult8, " +
                        "@TestDate8, @Test9, @TestResult9, @TestDate9, @Test10, @TestResult10, @TestDate10, " +
                        "@Test11, @TestResult11, @TestDate11, @Test12, @TestResult12, @TestDate12, @Test13, " +
                        "@TestResult13, @TestDate13, @Test14, @TestResult14, @TestDate14, @CallBackComment, " +
                        "@Image1Location, @Image2Location, @Image1LocationOld, @Image2LocationOld, @Image1Desc, " +
                        "@Image2Desc, @PicForwarded1, @PicForwarded2, @NoteTypeCoSign, @NoteTypeRefill, " +
                        "@EncounterDate, @Illustration1Loc, @Illustration2Loc, @Illustration1Desc, " +
                        "@Illustration2Desc, @LabTestID, @LabSignOffID, @LabSignOffDt, @ImportItemID, " +
                        "@DeleteItem, @MsgHighlightColor, @DateLastTouched, @LastTouchedBy, @DateRowAdded, " +
                        "@Sat, @Pain, @PF, @OtherVS, @ConfidentialInformation, @ActiveTemplates, @TaskComplete, " +
                        "@MetaData, @SatAirSource, @SatSuppO2Amount, @PFPost, @SatSuppO2Type, @PacksPerDay, " +
                        "@YearsSmoked, @YearsQuit, @IsResourceProvided, @TobaccoCDCCode, @IsMedReconciled, " +
                        "@IsPatientTransfer, @LastMenstrualPeriod, @EstimatedDeliveryDate, @PregnancyComments, " +
                        "@VisionOS, @VisionOD, @Hearing, @HearingComments, @SBPSupine, @DBPSupine, @Link, " +
                        "@TobaccoStatusStartDate, @TobaccoStatusEndDate, @TobaccoPipeSmoker, " +
                        "@TobaccoPipeStartDate, @TobaccoPipeEndDate, @TobaccoCigarSmoker, @TobaccoCigarStartDate, " +
                        "@TobaccoCigarEndDate, @TobaccoChewing, @TobaccoChewingStartDate, @TobaccoChewingEndDate, " +
                        "@IsFromCICO, @DeclinedClinicalSummary, @Instructions, @IsIcd10, @LbsOzMode); " +
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

        public int UpdateEmail(EmailDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                EmailPoco poco = new EmailPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Email " +
                        "SET [To] = @To, [From] = @From, [Date] = @Date, [Re] = @Re, [CC] = @CC, " +
                        "[PatientName] = @PatientName, [Body] = @Body, [PatientID] = @PatientID, " +
                        "[Chart] = @Chart, [Chief] = @Chief, [HPI] = @HPI, [ROS] = @ROS, [PMH] = @PMH, " +
                        "[Meds] = @Meds, [Allergies] = @Allergies, [SH] = @SH, [FH] = @FH, [PE] = @PE, " +
                        "[Ass] = @Ass, [Plan] = @Plan, [SBP] = @SBP, [DBP] = @DBP, [Temp] = @Temp, " +
                        "[RR] = @RR, [Pulse] = @Pulse, [Weight] = @Weight, [Height] = @Height, [HC] = @HC, " +
                        "[VitalComments] = @VitalComments, [Test1] = @Test1, [TestResult1] = @TestResult1, " +
                        "[TestDate1] = @TestDate1, [Test2] = @Test2, [TestResult2] = @TestResult2, " +
                        "[TestDate2] = @TestDate2, [Test3] = @Test3, [TestResult3] = @TestResult3, " +
                        "[TestDate3] = @TestDate3, [Test4] = @Test4, [TestResult4] = @TestResult4, " +
                        "[TestDate4] = @TestDate4, [Test5] = @Test5, [TestResult5] = @TestResult5, " +
                        "[TestDate5] = @TestDate5, [Test6] = @Test6, [TestResult6] = @TestResult6, " +
                        "[TestDate6] = @TestDate6, [Test7] = @Test7, [TestResult7] = @TestResult7, " +
                        "[TestDate7] = @TestDate7, [Test8] = @Test8, [TestResult8] = @TestResult8, " +
                        "[TestDate8] = @TestDate8, [Test9] = @Test9, [TestResult9] = @TestResult9, " +
                        "[TestDate9] = @TestDate9, [Test10] = @Test10, [TestResult10] = @TestResult10, " +
                        "[TestDate10] = @TestDate10, [Test11] = @Test11, [TestResult11] = @TestResult11, " +
                        "[TestDate11] = @TestDate11, [Test12] = @Test12, [TestResult12] = @TestResult12, " +
                        "[TestDate12] = @TestDate12, [Test13] = @Test13, [TestResult13] = @TestResult13, " +
                        "[TestDate13] = @TestDate13, [Test14] = @Test14, [TestResult14] = @TestResult14, " +
                        "[TestDate14] = @TestDate14, [CallBackComment] = @CallBackComment, " +
                        "[Image1Location] = @Image1Location, [Image2Location] = @Image2Location, " +
                        "[Image1LocationOld] = @Image1LocationOld, [Image2LocationOld] = @Image2LocationOld, " +
                        "[Image1Desc] = @Image1Desc, [Image2Desc] = @Image2Desc, [PicForwarded1] = @PicForwarded1, " +
                        "[PicForwarded2] = @PicForwarded2, [NoteTypeCoSign] = @NoteTypeCoSign, " +
                        "[NoteTypeRefill] = @NoteTypeRefill, [EncounterDate] = @EncounterDate, " +
                        "[Illustration1Loc] = @Illustration1Loc, [Illustration2Loc] = @Illustration2Loc, " +
                        "[Illustration1Desc] = @Illustration1Desc, [Illustration2Desc] = @Illustration2Desc, " +
                        "[LabTestID] = @LabTestID, [LabSignOffID] = @LabSignOffID, [LabSignOffDt] = @LabSignOffDt, " +
                        "[ImportItemID] = @ImportItemID, [DeleteItem] = @DeleteItem, " +
                        "[MsgHighlightColor] = @MsgHighlightColor, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [Sat] = @Sat, " +
                        "[Pain] = @Pain, [PF] = @PF, [OtherVS] = @OtherVS, " +
                        "[ConfidentialInformation] = @ConfidentialInformation, " +
                        "[ActiveTemplates] = @ActiveTemplates, [TaskComplete] = @TaskComplete, " +
                        "[MetaData] = @MetaData, [SatAirSource] = @SatAirSource, " +
                        "[SatSuppO2Amount] = @SatSuppO2Amount, [PFPost] = @PFPost, " +
                        "[SatSuppO2Type] = @SatSuppO2Type, [PacksPerDay] = @PacksPerDay, " +
                        "[YearsSmoked] = @YearsSmoked, [YearsQuit] = @YearsQuit, " +
                        "[IsResourceProvided] = @IsResourceProvided, [TobaccoCDCCode] = @TobaccoCDCCode, " +
                        "[IsMedReconciled] = @IsMedReconciled, [IsPatientTransfer] = @IsPatientTransfer, " +
                        "[LastMenstrualPeriod] = @LastMenstrualPeriod, " +
                        "[EstimatedDeliveryDate] = @EstimatedDeliveryDate, " +
                        "[PregnancyComments] = @PregnancyComments, [VisionOS] = @VisionOS, " +
                        "[VisionOD] = @VisionOD, [Hearing] = @Hearing, [HearingComments] = @HearingComments, " +
                        "[SBPSupine] = @SBPSupine, [DBPSupine] = @DBPSupine, [Link] = @Link, " +
                        "[TobaccoStatusStartDate] = @TobaccoStatusStartDate, " +
                        "[TobaccoStatusEndDate] = @TobaccoStatusEndDate, [TobaccoPipeSmoker] = @TobaccoPipeSmoker, " +
                        "[TobaccoPipeStartDate] = @TobaccoPipeStartDate, [TobaccoPipeEndDate] = @TobaccoPipeEndDate, " +
                        "[TobaccoCigarSmoker] = @TobaccoCigarSmoker, [TobaccoCigarStartDate] = @TobaccoCigarStartDate, " +
                        "[TobaccoCigarEndDate] = @TobaccoCigarEndDate, [TobaccoChewing] = @TobaccoChewing, " +
                        "[TobaccoChewingStartDate] = @TobaccoChewingStartDate, " +
                        "[TobaccoChewingEndDate] = @TobaccoChewingEndDate, [IsFromCICO] = @IsFromCICO, " +
                        "[DeclinedClinicalSummary] = @DeclinedClinicalSummary, [Instructions] = @Instructions, " +
                        "[IsIcd10] = @IsIcd10, [LbsOzMode] = @LbsOzMode " +
                        "WHERE msgID = @msgID;";

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
