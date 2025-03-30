using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ReportingSavedQueriesPoco
    {
        public int SavedQueryID { get; set; }
        public string QueryName { get; set; }
        public string QueryType { get; set; }
        public string QuerySQL { get; set; }
        public string QueryFilter { get; set; }
        public int SavedByProviderID { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ReportingSavedQueriesDomain MapToDomainModel()
        {
            ReportingSavedQueriesDomain domain = new ReportingSavedQueriesDomain
            {
                SavedQueryID = SavedQueryID,
                QueryName = QueryName,
                QueryType = QueryType,
                QuerySQL = QuerySQL,
                QueryFilter = QueryFilter,
                SavedByProviderID = SavedByProviderID,
                LastTouchedBy = LastTouchedBy,
                DateLastTouched = DateLastTouched,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ReportingSavedQueriesPoco() { }

        public ReportingSavedQueriesPoco(ReportingSavedQueriesDomain domain)
        {
            SavedQueryID = domain.SavedQueryID;
            QueryName = domain.QueryName;
            QueryType = domain.QueryType;
            QuerySQL = domain.QuerySQL;
            QueryFilter = domain.QueryFilter;
            SavedByProviderID = domain.SavedByProviderID;
            LastTouchedBy = domain.LastTouchedBy;
            DateLastTouched = domain.DateLastTouched;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
