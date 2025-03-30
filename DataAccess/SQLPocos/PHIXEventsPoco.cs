using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PHIXEventsPoco
    {
        public Guid PHIXEventsId { get; set; }
        public string ProviderCode { get; set; }
        public int PatientID { get; set; }
        public DateTime EventCreated { get; set; }
        public int EventType { get; set; }
        public string Demographics { get; set; }
        public string CCD { get; set; }
        public string CCR { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public PHIXEventsDomain MapToDomainModel()
        {
            PHIXEventsDomain domain = new PHIXEventsDomain
            {
                PHIXEventsId = PHIXEventsId,
                ProviderCode = ProviderCode,
                PatientID = PatientID,
                EventCreated = EventCreated,
                EventType = EventType,
                Demographics = Demographics,
                CCD = CCD,
                CCR = CCR,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public PHIXEventsPoco() { }

        public PHIXEventsPoco(PHIXEventsDomain domain)
        {
            PHIXEventsId = domain.PHIXEventsId;
            ProviderCode = domain.ProviderCode;
            PatientID = domain.PatientID;
            EventCreated = domain.EventCreated;
            EventType = domain.EventType;
            Demographics = domain.Demographics;
            CCD = domain.CCD;
            CCR = domain.CCR;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
