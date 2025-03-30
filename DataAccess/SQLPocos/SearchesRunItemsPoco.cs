using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SearchesRunItemsPoco
    {
        public Guid SearchesRunItemID { get; set; }
        public Guid SearchRunID { get; set; }
        public string GroupGUID { get; set; }
        public int? GroupCategoryID { get; set; }
        public int? FieldCategoryID { get; set; }
        public int? FieldID { get; set; }
        public int? ComparisonOpID { get; set; }
        public string Value { get; set; }
        public string ValueID { get; set; }
        public bool? IsOr { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public SearchesRunItemsDomain MapToDomainModel()
        {
            SearchesRunItemsDomain domain = new SearchesRunItemsDomain
            {
                SearchesRunItemID = SearchesRunItemID,
                SearchRunID = SearchRunID,
                GroupGUID = GroupGUID,
                GroupCategoryID = GroupCategoryID,
                FieldCategoryID = FieldCategoryID,
                FieldID = FieldID,
                ComparisonOpID = ComparisonOpID,
                Value = Value,
                ValueID = ValueID,
                IsOr = IsOr,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public SearchesRunItemsPoco() { }

        public SearchesRunItemsPoco(SearchesRunItemsDomain domain)
        {
            SearchesRunItemID = domain.SearchesRunItemID;
            SearchRunID = domain.SearchRunID;
            GroupGUID = domain.GroupGUID;
            GroupCategoryID = domain.GroupCategoryID;
            FieldCategoryID = domain.FieldCategoryID;
            FieldID = domain.FieldID;
            ComparisonOpID = domain.ComparisonOpID;
            Value = domain.Value;
            ValueID = domain.ValueID;
            IsOr = domain.IsOr;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
