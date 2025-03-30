using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ServiceLogPoco
    {
        public long ServiceLogID { get; set; }
        public long ScheduleID { get; set; }
        public DateTime StartDt { get; set; }
        public DateTime EndDt { get; set; }
        public int Status { get; set; }
        public DateTime LastUpdatedDt { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ServiceLogDomain MapToDomainModel()
        {
            ServiceLogDomain domain = new ServiceLogDomain
            {
                ServiceLogID = ServiceLogID,
                ScheduleID = ScheduleID,
                StartDt = StartDt,
                EndDt = EndDt,
                Status = Status,
                LastUpdatedDt = LastUpdatedDt,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ServiceLogPoco() { }

        public ServiceLogPoco(ServiceLogDomain domain)
        {
            ServiceLogID = domain.ServiceLogID;
            ScheduleID = domain.ScheduleID;
            StartDt = domain.StartDt;
            EndDt = domain.EndDt;
            Status = domain.Status;
            LastUpdatedDt = domain.LastUpdatedDt;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
