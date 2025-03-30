using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PracticeInfoERxPoco
    {
        public int ID { get; set; }
        public string PracticeName { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string UniquePracticeID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string NewCropSiteID { get; set; }

        public PracticeInfoERxDomain MapToDomainModel()
        {
            PracticeInfoERxDomain domain = new PracticeInfoERxDomain
            {
                ID = ID,
                PracticeName = PracticeName,
                StreetAddress1 = StreetAddress1,
                StreetAddress2 = StreetAddress2,
                City = City,
                State = State,
                Zip = Zip,
                UniquePracticeID = UniquePracticeID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                NewCropSiteID = NewCropSiteID
            };

            return domain;
        }

        public PracticeInfoERxPoco() { }

        public PracticeInfoERxPoco(PracticeInfoERxDomain domain)
        {
            ID = domain.ID;
            PracticeName = domain.PracticeName;
            StreetAddress1 = domain.StreetAddress1;
            StreetAddress2 = domain.StreetAddress2;
            City = domain.City;
            State = domain.State;
            Zip = domain.Zip;
            UniquePracticeID = domain.UniquePracticeID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            NewCropSiteID = domain.NewCropSiteID;
        }
    }
}
