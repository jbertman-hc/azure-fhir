using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class BillingICDsPoco
    {
        public Guid BillingICDsID { get; set; }
        public Guid? BillingCPTsID { get; set; }
        public string Code { get; set; }
        public string Comments { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public int? SortOrder { get; set; }

        public BillingICDsDomain MapToDomainModel()
        {
            BillingICDsDomain domain = new BillingICDsDomain
            {
                BillingICDsID = BillingICDsID,
                BillingCPTsID = BillingCPTsID,
                Code = Code,
                Comments = Comments,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                SortOrder = SortOrder
            };

            return domain;
        }

        public BillingICDsPoco() { }

        public BillingICDsPoco(BillingICDsDomain domain)
        {
            BillingICDsID = domain.BillingICDsID;
            BillingCPTsID = domain.BillingCPTsID;
            Code = domain.Code;
            Comments = domain.Comments;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            SortOrder = domain.SortOrder;
        }
    }
}

