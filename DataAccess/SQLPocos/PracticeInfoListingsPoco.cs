using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PracticeInfoListingsPoco
    {
        public int ID { get; set; }
        public string DataHeadings { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public PracticeInfoListingsDomain MapToDomainModel()
        {
            PracticeInfoListingsDomain domain = new PracticeInfoListingsDomain
            {
                ID = ID,
                DataHeadings = DataHeadings,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public PracticeInfoListingsPoco() { }

        public PracticeInfoListingsPoco(PracticeInfoListingsDomain domain)
        {
            ID = domain.ID;
            DataHeadings = domain.DataHeadings;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
