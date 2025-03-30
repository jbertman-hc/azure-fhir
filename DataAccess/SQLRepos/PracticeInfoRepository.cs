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
    public class PracticeInfoRepository : IPracticeInfoRepository
    {
        private readonly string _connectionString;

        public PracticeInfoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PracticeInfoDomain GetPracticeInfo(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PracticeInfo WHERE id = @id";

                    var PracticeInfoPoco = cn.QueryFirstOrDefault<PracticeInfoPoco>(query, new { id = id }) ?? new PracticeInfoPoco();

                    return PracticeInfoPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PracticeInfoDomain> GetPracticeInfos(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PracticeInfo WHERE @criteria";
                    List<PracticeInfoPoco> pocos = cn.Query<PracticeInfoPoco>(sql).ToList();
                    List<PracticeInfoDomain> domains = new List<PracticeInfoDomain>();

                    foreach (PracticeInfoPoco poco in pocos)
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

        public int DeletePracticeInfo(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PracticeInfo WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPracticeInfo(PracticeInfoDomain domain)
        {
            int insertedId = 0;

            try
            {
                PracticeInfoPoco poco = new PracticeInfoPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PracticeInfo] " +
                        "([ID], [PracticeName], [StreetAddress1], [StreetAddress2], [City], [State], [Zip], " +
                        "[Phone1], [fax], [logo], [Registration], [ResellerCode], [UtilitiesRun], [OSBU], " +
                        "[OSBUDate], [OSBUcode], [OSBURun], [ACU], [ACUDate], [GAF], [GAE], [SupportCodePrefix], " +
                        "[SupportCode], [SupportDate], [ACBS], [SPbiller], [SPbillerDate], [SPbillerExportPath], " +
                        "[SPlab], [SPlabDate], [SPmisc], [SPmiscDate], [EIN], [DefaultProvID], [NPI], [CLIA], " +
                        "[email], [UserID], [RegWithoutTag], [LicensedUsers], [announce_sentence1], [announce_sentence2], " +
                        "[announce_sentence3], [announce_url], [announce_zip], [announce_IsAlert], " +
                        "[announce_IsOnLogIn], [ACEP], [ACEPcode], [IsMedVerifyCode], [verify_GUID], [OSBU_ON], " +
                        "[BackupLocation1], [BackupLocation2], [BackupLocation3], [AutoBackupTime], [AutoBackupOn], " +
                        "[NoBackupImportedItems], [NoBackupImages], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded], [BackupErrorCheck], [PassSecurityOn], [PassMinLength], [PassAlphaNumeric], " +
                        "[PassLockOutCount], [PassCaseSensitive], [SessionInactivity], [PassReuseCount], " +
                        "[PassResetDuration], [UniquePracticeID], [LastDateAppSynched], [DefaultErxProvider], " +
                        "[LoginMessage], [ScriptSerialNumber], [ClearingHouseID], [ClearingHouseName], " +
                        "[ClearingHousePassword], [TTSD], [LastMessageId], [Icd10DateOverride], [UniqueInstallID]) " +
                        "VALUES " +
                        "(@ID, @PracticeName, @StreetAddress1, @StreetAddress2, @City, @State, @Zip, @Phone1, @fax, " +
                        "@logo, @Registration, @ResellerCode, @UtilitiesRun, @OSBU, @OSBUDate, @OSBUcode, @OSBURun, " +
                        "@ACU, @ACUDate, @GAF, @GAE, @SupportCodePrefix, @SupportCode, @SupportDate, @ACBS, @SPbiller, " +
                        "@SPbillerDate, @SPbillerExportPath, @SPlab, @SPlabDate, @SPmisc, @SPmiscDate, @EIN, " +
                        "@DefaultProvID, @NPI, @CLIA, @email, @UserID, @RegWithoutTag, @LicensedUsers, " +
                        "@announce_sentence1, @announce_sentence2, @announce_sentence3, @announce_url, @announce_zip, " +
                        "@announce_IsAlert, @announce_IsOnLogIn, @ACEP, @ACEPcode, @IsMedVerifyCode, @verify_GUID, " +
                        "@OSBU_ON, @BackupLocation1, @BackupLocation2, @BackupLocation3, @AutoBackupTime, " +
                        "@AutoBackupOn, @NoBackupImportedItems, @NoBackupImages, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @BackupErrorCheck, @PassSecurityOn, @PassMinLength, @PassAlphaNumeric, " +
                        "@PassLockOutCount, @PassCaseSensitive, @SessionInactivity, @PassReuseCount, @PassResetDuration, " +
                        "@UniquePracticeID, @LastDateAppSynched, @DefaultErxProvider, @LoginMessage, @ScriptSerialNumber, " +
                        "@ClearingHouseID, @ClearingHouseName, @ClearingHousePassword, @TTSD, @LastMessageId, " +
                        "@Icd10DateOverride, @UniqueInstallID); " +
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

        public int UpdatePracticeInfo(PracticeInfoDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PracticeInfoPoco poco = new PracticeInfoPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PracticeInfo " +
                        "SET [PracticeName] = @PracticeName, [StreetAddress1] = @StreetAddress1, " +
                        "[StreetAddress2] = @StreetAddress2, [City] = @City, [State] = @State, [Zip] = @Zip, " +
                        "[Phone1] = @Phone1, [fax] = @fax, [logo] = @logo, [Registration] = @Registration, " +
                        "[ResellerCode] = @ResellerCode, [UtilitiesRun] = @UtilitiesRun, [OSBU] = @OSBU, " +
                        "[OSBUDate] = @OSBUDate, [OSBUcode] = @OSBUcode, [OSBURun] = @OSBURun, [ACU] = @ACU, " +
                        "[ACUDate] = @ACUDate, [GAF] = @GAF, [GAE] = @GAE, [SupportCodePrefix] = @SupportCodePrefix, " +
                        "[SupportCode] = @SupportCode, [SupportDate] = @SupportDate, [ACBS] = @ACBS, " +
                        "[SPbiller] = @SPbiller, [SPbillerDate] = @SPbillerDate, " +
                        "[SPbillerExportPath] = @SPbillerExportPath, [SPlab] = @SPlab, [SPlabDate] = @SPlabDate, " +
                        "[SPmisc] = @SPmisc, [SPmiscDate] = @SPmiscDate, [EIN] = @EIN, " +
                        "[DefaultProvID] = @DefaultProvID, [NPI] = @NPI, [CLIA] = @CLIA, [email] = @email, " +
                        "[UserID] = @UserID, [RegWithoutTag] = @RegWithoutTag, [LicensedUsers] = @LicensedUsers, " +
                        "[announce_sentence1] = @announce_sentence1, [announce_sentence2] = @announce_sentence2, " +
                        "[announce_sentence3] = @announce_sentence3, [announce_url] = @announce_url, " +
                        "[announce_zip] = @announce_zip, [announce_IsAlert] = @announce_IsAlert, " +
                        "[announce_IsOnLogIn] = @announce_IsOnLogIn, [ACEP] = @ACEP, [ACEPcode] = @ACEPcode, " +
                        "[IsMedVerifyCode] = @IsMedVerifyCode, [verify_GUID] = @verify_GUID, [OSBU_ON] = @OSBU_ON, " +
                        "[BackupLocation1] = @BackupLocation1, [BackupLocation2] = @BackupLocation2, " +
                        "[BackupLocation3] = @BackupLocation3, [AutoBackupTime] = @AutoBackupTime, " +
                        "[AutoBackupOn] = @AutoBackupOn, [NoBackupImportedItems] = @NoBackupImportedItems, " +
                        "[NoBackupImages] = @NoBackupImages, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[BackupErrorCheck] = @BackupErrorCheck, [PassSecurityOn] = @PassSecurityOn, " +
                        "[PassMinLength] = @PassMinLength, [PassAlphaNumeric] = @PassAlphaNumeric, " +
                        "[PassLockOutCount] = @PassLockOutCount, [PassCaseSensitive] = @PassCaseSensitive, " +
                        "[SessionInactivity] = @SessionInactivity, [PassReuseCount] = @PassReuseCount, " +
                        "[PassResetDuration] = @PassResetDuration, [UniquePracticeID] = @UniquePracticeID, " +
                        "[LastDateAppSynched] = @LastDateAppSynched, [DefaultErxProvider] = @DefaultErxProvider, " +
                        "[LoginMessage] = @LoginMessage, [ScriptSerialNumber] = @ScriptSerialNumber, " +
                        "[ClearingHouseID] = @ClearingHouseID, [ClearingHouseName] = @ClearingHouseName, " +
                        "[ClearingHousePassword] = @ClearingHousePassword, [TTSD] = @TTSD, " +
                        "[LastMessageId] = @LastMessageId, [Icd10DateOverride] = @Icd10DateOverride, " +
                        "[UniqueInstallID] = @UniqueInstallID " +
                        "WHERE ID = @ID;";

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
