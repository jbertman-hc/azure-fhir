using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SearchesRunPoco
    {
        public Guid SearchRunID { get; set; }
        public DateTime DateRan { get; set; }
        public int? CategoryID { get; set; }
        public string SQL { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public SearchesRunDomain MapToDomainModel()
        {
            SearchesRunDomain domain = new SearchesRunDomain
            {
                SearchRunID = SearchRunID,
                DateRan = DateRan,
                CategoryID = CategoryID,
                SQL = SQL,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public SearchesRunPoco() { }

        public SearchesRunPoco(SearchesRunDomain domain)
        {
            SearchRunID = domain.SearchRunID;
            DateRan = domain.DateRan;
            CategoryID = domain.CategoryID;
            SQL = domain.SQL;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
