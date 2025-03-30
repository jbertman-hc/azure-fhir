using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PHIXEventsComponentsPoco
    {
        public Guid PHIXEventsComponentsId { get; set; }
        public Guid PHIXEventsId { get; set; }
        public string ViewPHIXId { get; set; }
        public int Status { get; set; }
        public int Attempts { get; set; }
        public DateTime? LastFailureTime { get; set; }
        public string LastFailureMessage { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public PHIXEventsComponentsDomain MapToDomainModel()
        {
            PHIXEventsComponentsDomain domain = new PHIXEventsComponentsDomain
            {
                PHIXEventsComponentsId = PHIXEventsComponentsId,
                PHIXEventsId = PHIXEventsId,
                ViewPHIXId = ViewPHIXId,
                Status = Status,
                Attempts = Attempts,
                LastFailureTime = LastFailureTime,
                LastFailureMessage = LastFailureMessage,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public PHIXEventsComponentsPoco() { }

        public PHIXEventsComponentsPoco(PHIXEventsComponentsDomain domain)
        {
            PHIXEventsComponentsId = domain.PHIXEventsComponentsId;
            PHIXEventsId = domain.PHIXEventsId;
            ViewPHIXId = domain.ViewPHIXId;
            Status = domain.Status;
            Attempts = domain.Attempts;
            LastFailureTime = domain.LastFailureTime;
            LastFailureMessage = domain.LastFailureMessage;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
