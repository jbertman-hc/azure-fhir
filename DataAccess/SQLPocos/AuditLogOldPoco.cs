using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class AuditLogOldPoco
    {
        public string TableName { get; set; }
        public string RowID { get; set; }
        public int? PatientID { get; set; }
        public string UserName { get; set; }
        public string Operation { get; set; }
        public DateTime? DateTime { get; set; }
        public string MiscInfo { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public Guid AuditID { get; set; }
        public string Location { get; set; }
        public string EventOutcome { get; set; }

        public AuditLogOldDomain MapToDomainModel()
        {
            AuditLogOldDomain domain = new AuditLogOldDomain
            {
                TableName = TableName,
                RowID = RowID,
                PatientID = PatientID,
                UserName = UserName,
                Operation = Operation,
                DateTime = DateTime,
                MiscInfo = MiscInfo,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                AuditID = AuditID,
                Location = Location,
                EventOutcome = EventOutcome
            };

            return domain;
        }

        public AuditLogOldPoco() { }

        public AuditLogOldPoco(AuditLogOldDomain domain)
        {
            TableName = domain.TableName;
            RowID = domain.RowID;
            PatientID = domain.PatientID;
            UserName = domain.UserName;
            Operation = domain.Operation;
            DateTime = domain.DateTime;
            MiscInfo = domain.MiscInfo;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            AuditID = domain.AuditID;
            Location = domain.Location;
            EventOutcome = domain.EventOutcome;
        }
    }
}

