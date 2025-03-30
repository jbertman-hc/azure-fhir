using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ArchiveSchedulePoco
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
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ArchiveScheduleDomain MapToDomainModel()
        {
            ArchiveScheduleDomain domain = new ArchiveScheduleDomain
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
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ArchiveSchedulePoco() { }

        public ArchiveSchedulePoco(ArchiveScheduleDomain domain)
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
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}

