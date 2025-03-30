using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class VisitTypesPoco
    {
        public int ID { get; set; }
        public string VisitType { get; set; }
        public short? TimeForVisit { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public VisitTypesDomain MapToDomainModel()
        {
            VisitTypesDomain domain = new VisitTypesDomain
            {
                ID = ID,
                VisitType = VisitType,
                TimeForVisit = TimeForVisit,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public VisitTypesPoco() { }

        public VisitTypesPoco(VisitTypesDomain domain)
        {
            ID = domain.ID;
            VisitType = domain.VisitType;
            TimeForVisit = domain.TimeForVisit;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
