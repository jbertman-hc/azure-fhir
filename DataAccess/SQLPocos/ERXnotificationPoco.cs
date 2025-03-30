using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ERXnotificationPoco
    {
        public int NotificationID { get; set; }
        public string IncomingProviderCode { get; set; }
        public string DestinationProviderCode { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ERXnotificationDomain MapToDomainModel()
        {
            ERXnotificationDomain domain = new ERXnotificationDomain
            {
                NotificationID = NotificationID,
                IncomingProviderCode = IncomingProviderCode,
                DestinationProviderCode = DestinationProviderCode,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ERXnotificationPoco() { }

        public ERXnotificationPoco(ERXnotificationDomain domain)
        {
            NotificationID = domain.NotificationID;
            IncomingProviderCode = domain.IncomingProviderCode;
            DestinationProviderCode = domain.DestinationProviderCode;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
