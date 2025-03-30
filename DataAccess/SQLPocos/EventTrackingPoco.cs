using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class EventTrackingPoco
    {
        public Guid EventTrackingGUID { get; set; }
        public int? PatientID { get; set; }
        public int? EventDefinitionID { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public EventTrackingDomain MapToDomainModel()
        {
            EventTrackingDomain domain = new EventTrackingDomain
            {
                EventTrackingGUID = EventTrackingGUID,
                PatientID = PatientID,
                EventDefinitionID = EventDefinitionID,
                LastTouchedBy = LastTouchedBy,
                DateLastTouched = DateLastTouched,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public EventTrackingPoco() { }

        public EventTrackingPoco(EventTrackingDomain domain)
        {
            EventTrackingGUID = domain.EventTrackingGUID;
            PatientID = domain.PatientID;
            EventDefinitionID = domain.EventDefinitionID;
            LastTouchedBy = domain.LastTouchedBy;
            DateLastTouched = domain.DateLastTouched;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
