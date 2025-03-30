using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListInjectionsPoco
    {
        public int InjectionID { get; set; }
        public int PatientID { get; set; }
        public string InjectionName { get; set; }
        public string LotNo { get; set; }
        public DateTime? DateGiven { get; set; }
        public string RecordedBy { get; set; }
        public string Volume { get; set; }
        public string Route { get; set; }
        public string Site { get; set; }
        public string Manufacturer { get; set; }
        public DateTime? Expiration { get; set; }
        public string Comment { get; set; }
        public string CPT { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? IsGivenElsewhere { get; set; }
        public bool? Deleted { get; set; }
        public string NDCCode { get; set; }

        public ListInjectionsDomain MapToDomainModel()
        {
            ListInjectionsDomain domain = new ListInjectionsDomain
            {
                InjectionID = InjectionID,
                PatientID = PatientID,
                InjectionName = InjectionName,
                LotNo = LotNo,
                DateGiven = DateGiven,
                RecordedBy = RecordedBy,
                Volume = Volume,
                Route = Route,
                Site = Site,
                Manufacturer = Manufacturer,
                Expiration = Expiration,
                Comment = Comment,
                CPT = CPT,
                LastTouchedBy = LastTouchedBy,
                DateLastTouched = DateLastTouched,
                DateRowAdded = DateRowAdded,
                IsGivenElsewhere = IsGivenElsewhere,
                Deleted = Deleted,
                NDCCode = NDCCode
            };

            return domain;
        }

        public ListInjectionsPoco() { }

        public ListInjectionsPoco(ListInjectionsDomain domain)
        {
            InjectionID = domain.InjectionID;
            PatientID = domain.PatientID;
            InjectionName = domain.InjectionName;
            LotNo = domain.LotNo;
            DateGiven = domain.DateGiven;
            RecordedBy = domain.RecordedBy;
            Volume = domain.Volume;
            Route = domain.Route;
            Site = domain.Site;
            Manufacturer = domain.Manufacturer;
            Expiration = domain.Expiration;
            Comment = domain.Comment;
            CPT = domain.CPT;
            LastTouchedBy = domain.LastTouchedBy;
            DateLastTouched = domain.DateLastTouched;
            DateRowAdded = domain.DateRowAdded;
            IsGivenElsewhere = domain.IsGivenElsewhere;
            Deleted = domain.Deleted;
            NDCCode = domain.NDCCode;
        }
    }
}
