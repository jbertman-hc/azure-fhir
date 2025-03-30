using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PracticeInfoPoco
    {
        public int ID { get; set; }
        public string PracticeName { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone1 { get; set; }
        public string fax { get; set; }
        public byte[] logo { get; set; }
        public string Registration { get; set; }
        public string ResellerCode { get; set; }
        public DateTime? UtilitiesRun { get; set; }
        public bool OSBU { get; set; }
        public DateTime? OSBUDate { get; set; }
        public string OSBUcode { get; set; }
        public DateTime? OSBURun { get; set; }
        public bool ACU { get; set; }
        public DateTime? ACUDate { get; set; }
        public bool GAF { get; set; }
        public bool GAE { get; set; }
        public string SupportCodePrefix { get; set; }
        public string SupportCode { get; set; }
        public DateTime? SupportDate { get; set; }
        public bool ACBS { get; set; }
        public string SPbiller { get; set; }
        public DateTime? SPbillerDate { get; set; }
        public string SPbillerExportPath { get; set; }
        public string SPlab { get; set; }
        public DateTime? SPlabDate { get; set; }
        public string SPmisc { get; set; }
        public DateTime? SPmiscDate { get; set; }
        public string EIN { get; set; }
        public int? DefaultProvID { get; set; }
        public string NPI { get; set; }
        public string CLIA { get; set; }
        public string email { get; set; }
        public string UserID { get; set; }
        public string RegWithoutTag { get; set; }
        public string LicensedUsers { get; set; }
        public string announce_sentence1 { get; set; }
        public string announce_sentence2 { get; set; }
        public string announce_sentence3 { get; set; }
        public string announce_url { get; set; }
        public string announce_zip { get; set; }
        public string announce_IsAlert { get; set; }
        public string announce_IsOnLogIn { get; set; }
        public string ACEP { get; set; }
        public string ACEPcode { get; set; }
        public string IsMedVerifyCode { get; set; }
        public string verify_GUID { get; set; }
        public bool? OSBU_ON { get; set; }
        public string BackupLocation1 { get; set; }
        public string BackupLocation2 { get; set; }
        public string BackupLocation3 { get; set; }
        public string AutoBackupTime { get; set; }
        public bool? AutoBackupOn { get; set; }
        public bool? NoBackupImportedItems { get; set; }
        public bool? NoBackupImages { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? BackupErrorCheck { get; set; }
        public bool? PassSecurityOn { get; set; }
        public int? PassMinLength { get; set; }
        public bool? PassAlphaNumeric { get; set; }
        public int? PassLockOutCount { get; set; }
        public bool? PassCaseSensitive { get; set; }
        public int? SessionInactivity { get; set; }
        public int? PassReuseCount { get; set; }
        public int? PassResetDuration { get; set; }
        public string UniquePracticeID { get; set; }
        public DateTime? LastDateAppSynched { get; set; }
        public string DefaultErxProvider { get; set; }
        public string LoginMessage { get; set; }
        public string ScriptSerialNumber { get; set; }
        public string ClearingHouseID { get; set; }
        public string ClearingHouseName { get; set; }
        public string ClearingHousePassword { get; set; }
        public DateTime? TTSD { get; set; }
        public long? LastMessageId { get; set; }
        public DateTime? Icd10DateOverride { get; set; }
        public string UniqueInstallID { get; set; }

        public PracticeInfoDomain MapToDomainModel()
        {
            PracticeInfoDomain domain = new PracticeInfoDomain
            {
                ID = ID,
                PracticeName = PracticeName,
                StreetAddress1 = StreetAddress1,
                StreetAddress2 = StreetAddress2,
                City = City,
                State = State,
                Zip = Zip,
                Phone1 = Phone1,
                fax = fax,
                logo = logo,
                Registration = Registration,
                ResellerCode = ResellerCode,
                UtilitiesRun = UtilitiesRun,
                OSBU = OSBU,
                OSBUDate = OSBUDate,
                OSBUcode = OSBUcode,
                OSBURun = OSBURun,
                ACU = ACU,
                ACUDate = ACUDate,
                GAF = GAF,
                GAE = GAE,
                SupportCodePrefix = SupportCodePrefix,
                SupportCode = SupportCode,
                SupportDate = SupportDate,
                ACBS = ACBS,
                SPbiller = SPbiller,
                SPbillerDate = SPbillerDate,
                SPbillerExportPath = SPbillerExportPath,
                SPlab = SPlab,
                SPlabDate = SPlabDate,
                SPmisc = SPmisc,
                SPmiscDate = SPmiscDate,
                EIN = EIN,
                DefaultProvID = DefaultProvID,
                NPI = NPI,
                CLIA = CLIA,
                email = email,
                UserID = UserID,
                RegWithoutTag = RegWithoutTag,
                LicensedUsers = LicensedUsers,
                announce_sentence1 = announce_sentence1,
                announce_sentence2 = announce_sentence2,
                announce_sentence3 = announce_sentence3,
                announce_url = announce_url,
                announce_zip = announce_zip,
                announce_IsAlert = announce_IsAlert,
                announce_IsOnLogIn = announce_IsOnLogIn,
                ACEP = ACEP,
                ACEPcode = ACEPcode,
                IsMedVerifyCode = IsMedVerifyCode,
                verify_GUID = verify_GUID,
                OSBU_ON = OSBU_ON,
                BackupLocation1 = BackupLocation1,
                BackupLocation2 = BackupLocation2,
                BackupLocation3 = BackupLocation3,
                AutoBackupTime = AutoBackupTime,
                AutoBackupOn = AutoBackupOn,
                NoBackupImportedItems = NoBackupImportedItems,
                NoBackupImages = NoBackupImages,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                BackupErrorCheck = BackupErrorCheck,
                PassSecurityOn = PassSecurityOn,
                PassMinLength = PassMinLength,
                PassAlphaNumeric = PassAlphaNumeric,
                PassLockOutCount = PassLockOutCount,
                PassCaseSensitive = PassCaseSensitive,
                SessionInactivity = SessionInactivity,
                PassReuseCount = PassReuseCount,
                PassResetDuration = PassResetDuration,
                UniquePracticeID = UniquePracticeID,
                LastDateAppSynched = LastDateAppSynched,
                DefaultErxProvider = DefaultErxProvider,
                LoginMessage = LoginMessage,
                ScriptSerialNumber = ScriptSerialNumber,
                ClearingHouseID = ClearingHouseID,
                ClearingHouseName = ClearingHouseName,
                ClearingHousePassword = ClearingHousePassword,
                TTSD = TTSD,
                LastMessageId = LastMessageId,
                Icd10DateOverride = Icd10DateOverride,
                UniqueInstallID = UniqueInstallID
            };

            return domain;
        }

        public PracticeInfoPoco() { }

        public PracticeInfoPoco(PracticeInfoDomain domain)
        {
            ID = domain.ID;
            PracticeName = domain.PracticeName;
            StreetAddress1 = domain.StreetAddress1;
            StreetAddress2 = domain.StreetAddress2;
            City = domain.City;
            State = domain.State;
            Zip = domain.Zip;
            Phone1 = domain.Phone1;
            fax = domain.fax;
            logo = domain.logo;
            Registration = domain.Registration;
            ResellerCode = domain.ResellerCode;
            UtilitiesRun = domain.UtilitiesRun;
            OSBU = domain.OSBU;
            OSBUDate = domain.OSBUDate;
            OSBUcode = domain.OSBUcode;
            OSBURun = domain.OSBURun;
            ACU = domain.ACU;
            ACUDate = domain.ACUDate;
            GAF = domain.GAF;
            GAE = domain.GAE;
            SupportCodePrefix = domain.SupportCodePrefix;
            SupportCode = domain.SupportCode;
            SupportDate = domain.SupportDate;
            ACBS = domain.ACBS;
            SPbiller = domain.SPbiller;
            SPbillerDate = domain.SPbillerDate;
            SPbillerExportPath = domain.SPbillerExportPath;
            SPlab = domain.SPlab;
            SPlabDate = domain.SPlabDate;
            SPmisc = domain.SPmisc;
            SPmiscDate = domain.SPmiscDate;
            EIN = domain.EIN;
            DefaultProvID = domain.DefaultProvID;
            NPI = domain.NPI;
            CLIA = domain.CLIA;
            email = domain.email;
            UserID = domain.UserID;
            RegWithoutTag = domain.RegWithoutTag;
            LicensedUsers = domain.LicensedUsers;
            announce_sentence1 = domain.announce_sentence1;
            announce_sentence2 = domain.announce_sentence2;
            announce_sentence3 = domain.announce_sentence3;
            announce_url = domain.announce_url;
            announce_zip = domain.announce_zip;
            announce_IsAlert = domain.announce_IsAlert;
            announce_IsOnLogIn = domain.announce_IsOnLogIn;
            ACEP = domain.ACEP;
            ACEPcode = domain.ACEPcode;
            IsMedVerifyCode = domain.IsMedVerifyCode;
            verify_GUID = domain.verify_GUID;
            OSBU_ON = domain.OSBU_ON;
            BackupLocation1 = domain.BackupLocation1;
            BackupLocation2 = domain.BackupLocation2;
            BackupLocation3 = domain.BackupLocation3;
            AutoBackupTime = domain.AutoBackupTime;
            AutoBackupOn = domain.AutoBackupOn;
            NoBackupImportedItems = domain.NoBackupImportedItems;
            NoBackupImages = domain.NoBackupImages;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            BackupErrorCheck = domain.BackupErrorCheck;
            PassSecurityOn = domain.PassSecurityOn;
            PassMinLength = domain.PassMinLength;
            PassAlphaNumeric = domain.PassAlphaNumeric;
            PassLockOutCount = domain.PassLockOutCount;
            PassCaseSensitive = domain.PassCaseSensitive;
            SessionInactivity = domain.SessionInactivity;
            PassReuseCount = domain.PassReuseCount;
            PassResetDuration = domain.PassResetDuration;
            UniquePracticeID = domain.UniquePracticeID;
            LastDateAppSynched = domain.LastDateAppSynched;
            DefaultErxProvider = domain.DefaultErxProvider;
            LoginMessage = domain.LoginMessage;
            ScriptSerialNumber = domain.ScriptSerialNumber;
            ClearingHouseID = domain.ClearingHouseID;
            ClearingHouseName = domain.ClearingHouseName;
            ClearingHousePassword = domain.ClearingHousePassword;
            TTSD = domain.TTSD;
            LastMessageId = domain.LastMessageId;
            Icd10DateOverride = domain.Icd10DateOverride;
            UniqueInstallID = domain.UniqueInstallID;
        }
    }
}
