using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class BackupSettingsPoco
    {
        public int RowID { get; set; }
        public string BackupLocation1 { get; set; }
        public string BackupLocation2 { get; set; }
        public string BackupLocation3 { get; set; }
        public string AutoBackupTime { get; set; }
        public bool? AutoBackupOn { get; set; }
        public bool? NoBackupImportedItems { get; set; }
        public bool? NoBackupImages { get; set; }
        public bool? BackupErrorCheck { get; set; }
        public bool? CheckZip { get; set; }
        public bool? CheckEncryption { get; set; }
        public bool? OSBUisOn { get; set; }
        public bool? SendConfirmationEmail { get; set; }
        public string ConfirmationProvider { get; set; }
        public string BackupLocation1UNC { get; set; }
        public string BackupLocation2UNC { get; set; }
        public string BackupLocation3UNC { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public BackupSettingsDomain MapToDomainModel()
        {
            BackupSettingsDomain domain = new BackupSettingsDomain
            {
                RowID = RowID,
                BackupLocation1 = BackupLocation1,
                BackupLocation2 = BackupLocation2,
                BackupLocation3 = BackupLocation3,
                AutoBackupTime = AutoBackupTime,
                AutoBackupOn = AutoBackupOn,
                NoBackupImportedItems = NoBackupImportedItems,
                NoBackupImages = NoBackupImages,
                BackupErrorCheck = BackupErrorCheck,
                CheckZip = CheckZip,
                CheckEncryption = CheckEncryption,
                OSBUisOn = OSBUisOn,
                SendConfirmationEmail = SendConfirmationEmail,
                ConfirmationProvider = ConfirmationProvider,
                BackupLocation1UNC = BackupLocation1UNC,
                BackupLocation2UNC = BackupLocation2UNC,
                BackupLocation3UNC = BackupLocation3UNC,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public BackupSettingsPoco() { }

        public BackupSettingsPoco(BackupSettingsDomain domain)
        {
            RowID = domain.RowID;
            BackupLocation1 = domain.BackupLocation1;
            BackupLocation2 = domain.BackupLocation2;
            BackupLocation3 = domain.BackupLocation3;
            AutoBackupTime = domain.AutoBackupTime;
            AutoBackupOn = domain.AutoBackupOn;
            NoBackupImportedItems = domain.NoBackupImportedItems;
            NoBackupImages = domain.NoBackupImages;
            BackupErrorCheck = domain.BackupErrorCheck;
            CheckZip = domain.CheckZip;
            CheckEncryption = domain.CheckEncryption;
            OSBUisOn = domain.OSBUisOn;
            SendConfirmationEmail = domain.SendConfirmationEmail;
            ConfirmationProvider = domain.ConfirmationProvider;
            BackupLocation1UNC = domain.BackupLocation1UNC;
            BackupLocation2UNC = domain.BackupLocation2UNC;
            BackupLocation3UNC = domain.BackupLocation3UNC;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
