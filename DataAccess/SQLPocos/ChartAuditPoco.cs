using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ChartAuditPoco
    {
        public int RowID { get; set; }
        public int? SoapID { get; set; }
        public int PatientID { get; set; }
        public string ProviderCode { get; set; }
        public string Action { get; set; }
        public DateTime EventDateTime { get; set; }
        public string DestinationProvider { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ChartAuditDomain MapToDomainModel()
        {
            ChartAuditDomain domain = new ChartAuditDomain
            {
                RowID = RowID,
                SoapID = SoapID,
                PatientID = PatientID,
                ProviderCode = ProviderCode,
                Action = Action,
                EventDateTime = EventDateTime,
                DestinationProvider = DestinationProvider,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ChartAuditPoco() { }

        public ChartAuditPoco(ChartAuditDomain domain)
        {
            RowID = domain.RowID;
            SoapID = domain.SoapID;
            PatientID = domain.PatientID;
            ProviderCode = domain.ProviderCode;
            Action = domain.Action;
            EventDateTime = domain.EventDateTime;
            DestinationProvider = domain.DestinationProvider;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
