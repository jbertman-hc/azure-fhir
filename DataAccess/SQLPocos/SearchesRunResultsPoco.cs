using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SearchesRunResultsPoco
    {
        public Guid SearchesRunResultID { get; set; }
        public Guid SearchRunID { get; set; }
        public int PatientID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public SearchesRunResultsDomain MapToDomainModel()
        {
            SearchesRunResultsDomain domain = new SearchesRunResultsDomain
            {
                SearchesRunResultID = SearchesRunResultID,
                SearchRunID = SearchRunID,
                PatientID = PatientID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public SearchesRunResultsPoco() { }

        public SearchesRunResultsPoco(SearchesRunResultsDomain domain)
        {
            SearchesRunResultID = domain.SearchesRunResultID;
            SearchRunID = domain.SearchRunID;
            PatientID = domain.PatientID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
