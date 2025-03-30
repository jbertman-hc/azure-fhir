using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class BillingCPTsPoco
    {
        public Guid BillingCPTsID { get; set; }
        public Guid? BillingID { get; set; }
        public string CPTCode { get; set; }
        public int? Units { get; set; }
        public decimal? Price { get; set; }
        public string Comments { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public DateTime? DatePerformed { get; set; }
        public bool? Resolved { get; set; }
        public int? Sequence { get; set; }
        public bool? AppealFlag { get; set; }
        public string NDCCode { get; set; }
        public string NDCUnits { get; set; }

        public BillingCPTsDomain MapToDomainModel()
        {
            BillingCPTsDomain domain = new BillingCPTsDomain
            {
                BillingCPTsID = BillingCPTsID,
                BillingID = BillingID,
                CPTCode = CPTCode,
                Units = Units,
                Price = Price,
                Comments = Comments,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                DatePerformed = DatePerformed,
                Resolved = Resolved,
                Sequence = Sequence,
                AppealFlag = AppealFlag,
                NDCCode = NDCCode,
                NDCUnits = NDCUnits
            };

            return domain;
        }

        public BillingCPTsPoco() { }

        public BillingCPTsPoco(BillingCPTsDomain domain)
        {
            BillingCPTsID = domain.BillingCPTsID;
            BillingID = domain.BillingID;
            CPTCode = domain.CPTCode;
            Units = domain.Units;
            Price = domain.Price;
            Comments = domain.Comments;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            DatePerformed = domain.DatePerformed;
            Resolved = domain.Resolved;
            Sequence = domain.Sequence;
            AppealFlag = domain.AppealFlag;
            NDCCode = domain.NDCCode;
            NDCUnits = domain.NDCUnits;
        }
    }
}

