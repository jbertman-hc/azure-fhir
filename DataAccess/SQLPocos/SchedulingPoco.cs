using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SchedulingPoco
    {
        public int VisitID { get; set; }
        public DateTime Date { get; set; }
        public int? PatientID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string VisitType { get; set; }
        public string Comments { get; set; }
        public string Booker { get; set; }
        public DateTime? DateBooked { get; set; }
        public int? ProviderID { get; set; }
        public int? Duration { get; set; }
        public string XLinkProviderID { get; set; }
        public string VisitIdExternal { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? IsEditable { get; set; }
        public string SourceSystemId { get; set; }

        public SchedulingDomain MapToDomainModel()
        {
            SchedulingDomain domain = new SchedulingDomain
            {
                VisitID = VisitID,
                Date = Date,
                PatientID = PatientID,
                Name = Name,
                Phone = Phone,
                VisitType = VisitType,
                Comments = Comments,
                Booker = Booker,
                DateBooked = DateBooked,
                ProviderID = ProviderID,
                Duration = Duration,
                XLinkProviderID = XLinkProviderID,
                VisitIdExternal = VisitIdExternal,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                IsEditable = IsEditable,
                SourceSystemId = SourceSystemId
            };

            return domain;
        }

        public SchedulingPoco() { }

        public SchedulingPoco(SchedulingDomain domain)
        {
            VisitID = domain.VisitID;
            Date = domain.Date;
            PatientID = domain.PatientID;
            Name = domain.Name;
            Phone = domain.Phone;
            VisitType = domain.VisitType;
            Comments = domain.Comments;
            Booker = domain.Booker;
            DateBooked = domain.DateBooked;
            ProviderID = domain.ProviderID;
            Duration = domain.Duration;
            XLinkProviderID = domain.XLinkProviderID;
            VisitIdExternal = domain.VisitIdExternal;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            IsEditable = domain.IsEditable;
            SourceSystemId = domain.SourceSystemId;
        }
    }
}
