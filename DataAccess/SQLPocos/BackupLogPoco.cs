using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class BackupLogPoco
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEndBackup { get; set; }
        public DateTime? TimeEndZipEncrypt { get; set; }
        public DateTime? TimeEndUpload { get; set; }
        public string ElapsedTimeBackup { get; set; }
        public string ElapsedTimeUpload { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? ProcessSuccessful { get; set; }
        public string Messages { get; set; }
        public DateTime? TimeStartUpload { get; set; }

        public BackupLogDomain MapToDomainModel()
        {
            BackupLogDomain domain = new BackupLogDomain
            {
                ID = ID,
                Type = Type,
                TimeStart = TimeStart,
                TimeEndBackup = TimeEndBackup,
                TimeEndZipEncrypt = TimeEndZipEncrypt,
                TimeEndUpload = TimeEndUpload,
                ElapsedTimeBackup = ElapsedTimeBackup,
                ElapsedTimeUpload = ElapsedTimeUpload,
                FileName = FileName,
                FileSize = FileSize,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                ProcessSuccessful = ProcessSuccessful,
                Messages = Messages,
                TimeStartUpload = TimeStartUpload
            };

            return domain;
        }

        public BackupLogPoco() { }

        public BackupLogPoco(BackupLogDomain domain)
        {
            ID = domain.ID;
            Type = domain.Type;
            TimeStart = domain.TimeStart;
            TimeEndBackup = domain.TimeEndBackup;
            TimeEndZipEncrypt = domain.TimeEndZipEncrypt;
            TimeEndUpload = domain.TimeEndUpload;
            ElapsedTimeBackup = domain.ElapsedTimeBackup;
            ElapsedTimeUpload = domain.ElapsedTimeUpload;
            FileName = domain.FileName;
            FileSize = domain.FileSize;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            ProcessSuccessful = domain.ProcessSuccessful;
            Messages = domain.Messages;
            TimeStartUpload = domain.TimeStartUpload;
        }
    }
}

