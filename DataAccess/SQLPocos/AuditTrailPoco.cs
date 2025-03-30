using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class AuditTrailPoco
    {
        public int AuditID { get; set; }
        public DateTime AuditDate { get; set; }
        public int ProviderID { get; set; }
        public string AuditTable { get; set; }
        public string ActionTaken { get; set; }
        public int PatientID { get; set; }
        public int RecNo { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public AuditTrailDomain MapToDomainModel()
        {
            AuditTrailDomain domain = new AuditTrailDomain
            {
                AuditID = AuditID,
                AuditDate = AuditDate,
                ProviderID = ProviderID,
                AuditTable = AuditTable,
                ActionTaken = ActionTaken,
                PatientID = PatientID,
                RecNo = RecNo,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public AuditTrailPoco() { }

        public AuditTrailPoco(AuditTrailDomain domain)
        {
            AuditID = domain.AuditID;
            AuditDate = domain.AuditDate;
            ProviderID = domain.ProviderID;
            AuditTable = domain.AuditTable;
            ActionTaken = domain.ActionTaken;
            PatientID = domain.PatientID;
            RecNo = domain.RecNo;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}

