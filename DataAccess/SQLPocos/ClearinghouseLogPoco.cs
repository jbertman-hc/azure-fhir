using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ClearinghouseLogPoco
    {
        public int ID { get; set; }
        public DateTime? LogTime { get; set; }
        public int TracePoint { get; set; }
        public string Clearinghouse { get; set; }
        public bool Success { get; set; }
        public string StackTrace { get; set; }
        public string Comments { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ClearinghouseLogDomain MapToDomainModel()
        {
            ClearinghouseLogDomain domain = new ClearinghouseLogDomain
            {
                ID = ID,
                LogTime = LogTime,
                TracePoint = TracePoint,
                Clearinghouse = Clearinghouse,
                Success = Success,
                StackTrace = StackTrace,
                Comments = Comments,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ClearinghouseLogPoco() { }

        public ClearinghouseLogPoco(ClearinghouseLogDomain domain)
        {
            ID = domain.ID;
            LogTime = domain.LogTime;
            TracePoint = domain.TracePoint;
            Clearinghouse = domain.Clearinghouse;
            Success = domain.Success;
            StackTrace = domain.StackTrace;
            Comments = domain.Comments;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
