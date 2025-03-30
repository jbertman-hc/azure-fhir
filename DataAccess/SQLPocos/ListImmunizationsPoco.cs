using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListImmunizationsPoco
    {
        public int ListImmID { get; set; }
        public int PatientID { get; set; }
        public string Immunization { get; set; }
        public string LotNo { get; set; }
        public DateTime? DateGiven { get; set; }
        public string RecordedBy { get; set; }
        public string Volume { get; set; }
        public string Route { get; set; }
        public string Site { get; set; }
        public string Manufacturer { get; set; }
        public DateTime? Expiration { get; set; }
        public string Comment { get; set; }
        public string Sequence { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListImmunizationsDomain MapToDomainModel()
        {
            ListImmunizationsDomain domain = new ListImmunizationsDomain
            {
                ListImmID = ListImmID,
                PatientID = PatientID,
                Immunization = Immunization,
                LotNo = LotNo,
                DateGiven = DateGiven,
                RecordedBy = RecordedBy,
                Volume = Volume,
                Route = Route,
                Site = Site,
                Manufacturer = Manufacturer,
                Expiration = Expiration,
                Comment = Comment,
                Sequence = Sequence,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListImmunizationsPoco() { }

        public ListImmunizationsPoco(ListImmunizationsDomain domain)
        {
            ListImmID = domain.ListImmID;
            PatientID = domain.PatientID;
            Immunization = domain.Immunization;
            LotNo = domain.LotNo;
            DateGiven = domain.DateGiven;
            RecordedBy = domain.RecordedBy;
            Volume = domain.Volume;
            Route = domain.Route;
            Site = domain.Site;
            Manufacturer = domain.Manufacturer;
            Expiration = domain.Expiration;
            Comment = domain.Comment;
            Sequence = domain.Sequence;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
