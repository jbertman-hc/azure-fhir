using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SearchItemPoco
    {
        public int SearchItemID { get; set; }
        public string GroupName { get; set; }
        public string GroupGUID { get; set; }
        public string Category { get; set; }
        public int? CategoryID { get; set; }
        public string Fields { get; set; }
        public int? FieldsID { get; set; }
        public string ComparisonOp { get; set; }
        public int? ComparisonOpID { get; set; }
        public string Value { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string ValueID { get; set; }
        public bool? IsOr { get; set; }
        public int? GroupCategoryID { get; set; }

        public SearchItemDomain MapToDomainModel()
        {
            SearchItemDomain domain = new SearchItemDomain
            {
                SearchItemID = SearchItemID,
                GroupName = GroupName,
                GroupGUID = GroupGUID,
                Category = Category,
                CategoryID = CategoryID,
                Fields = Fields,
                FieldsID = FieldsID,
                ComparisonOp = ComparisonOp,
                ComparisonOpID = ComparisonOpID,
                Value = Value,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                ValueID = ValueID,
                IsOr = IsOr,
                GroupCategoryID = GroupCategoryID
            };

            return domain;
        }

        public SearchItemPoco() { }

        public SearchItemPoco(SearchItemDomain domain)
        {
            SearchItemID = domain.SearchItemID;
            GroupName = domain.GroupName;
            GroupGUID = domain.GroupGUID;
            Category = domain.Category;
            CategoryID = domain.CategoryID;
            Fields = domain.Fields;
            FieldsID = domain.FieldsID;
            ComparisonOp = domain.ComparisonOp;
            ComparisonOpID = domain.ComparisonOpID;
            Value = domain.Value;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            ValueID = domain.ValueID;
            IsOr = domain.IsOr;
            GroupCategoryID = domain.GroupCategoryID;
        }
    }
}
