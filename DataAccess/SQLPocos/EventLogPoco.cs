using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class EventLogPoco
    {
        public int ID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Comments { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public EventLogDomain MapToDomainModel()
        {
            EventLogDomain domain = new EventLogDomain
            {
                ID = ID,
                EventName = EventName,
                EventDate = EventDate,
                Comments = Comments,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public EventLogPoco() { }

        public EventLogPoco(EventLogDomain domain)
        {
            ID = domain.ID;
            EventName = domain.EventName;
            EventDate = domain.EventDate;
            Comments = domain.Comments;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
