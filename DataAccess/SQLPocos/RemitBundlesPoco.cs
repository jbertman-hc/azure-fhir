using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class RemitBundlesPoco
    {
        public Guid BundleID { get; set; }
        public Guid RemitServiceLinesID { get; set; }
        public Guid BillingCPTsID { get; set; }
        public bool? WasFirst { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public RemitBundlesDomain MapToDomainModel()
        {
            RemitBundlesDomain domain = new RemitBundlesDomain
            {
                BundleID = BundleID,
                RemitServiceLinesID = RemitServiceLinesID,
                BillingCPTsID = BillingCPTsID,
                WasFirst = WasFirst,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public RemitBundlesPoco() { }

        public RemitBundlesPoco(RemitBundlesDomain domain)
        {
            BundleID = domain.BundleID;
            RemitServiceLinesID = domain.RemitServiceLinesID;
            BillingCPTsID = domain.BillingCPTsID;
            WasFirst = domain.WasFirst;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
