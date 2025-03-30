using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class OrdersPoco
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Item_Name { get; set; }
        public string Item_CPT { get; set; }
        public string Item_ICD { get; set; }
        public string LOINC { get; set; }
        public string Location { get; set; }
        public bool InHouseTest { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string URL { get; set; }
        public string Comments { get; set; }
        public int? HmRuleID { get; set; }
        public string GUID { get; set; }
        public bool Inactive { get; set; }
        public bool? IsImm { get; set; }
        public bool? IsChild { get; set; }
        public bool AutoAddToBill { get; set; }
        public bool IsNumericalResult { get; set; }

        public OrdersDomain MapToDomainModel()
        {
            OrdersDomain domain = new OrdersDomain
            {
                ID = ID,
                Type = Type,
                Item_Name = Item_Name,
                Item_CPT = Item_CPT,
                Item_ICD = Item_ICD,
                LOINC = LOINC,
                Location = Location,
                InHouseTest = InHouseTest,
                LastTouchedBy = LastTouchedBy,
                DateLastTouched = DateLastTouched,
                DateRowAdded = DateRowAdded,
                URL = URL,
                Comments = Comments,
                HmRuleID = HmRuleID,
                GUID = GUID,
                Inactive = Inactive,
                IsImm = IsImm,
                IsChild = IsChild,
                AutoAddToBill = AutoAddToBill,
                IsNumericalResult = IsNumericalResult
            };

            return domain;
        }

        public OrdersPoco() { }

        public OrdersPoco(OrdersDomain domain)
        {
            ID = domain.ID;
            Type = domain.Type;
            Item_Name = domain.Item_Name;
            Item_CPT = domain.Item_CPT;
            Item_ICD = domain.Item_ICD;
            LOINC = domain.LOINC;
            Location = domain.Location;
            InHouseTest = domain.InHouseTest;
            LastTouchedBy = domain.LastTouchedBy;
            DateLastTouched = domain.DateLastTouched;
            DateRowAdded = domain.DateRowAdded;
            URL = domain.URL;
            Comments = domain.Comments;
            HmRuleID = domain.HmRuleID;
            GUID = domain.GUID;
            Inactive = domain.Inactive;
            IsImm = domain.IsImm;
            IsChild = domain.IsChild;
            AutoAddToBill = domain.AutoAddToBill;
            IsNumericalResult = domain.IsNumericalResult;
        }
    }
}
