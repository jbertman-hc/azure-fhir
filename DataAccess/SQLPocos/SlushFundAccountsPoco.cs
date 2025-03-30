using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SlushFundAccountsPoco
    {
        public int SlushAccountID { get; set; }
        public int? OldSlushAccountID { get; set; }
        public Guid SlushAccountGuid { get; set; }
        public string ProviderCode { get; set; }
        public Guid PayorID { get; set; }
        public string OldPayorID { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }

        public SlushFundAccountsDomain MapToDomainModel()
        {
            SlushFundAccountsDomain domain = new SlushFundAccountsDomain
            {
                SlushAccountID = SlushAccountID,
                OldSlushAccountID = OldSlushAccountID,
                SlushAccountGuid = SlushAccountGuid,
                ProviderCode = ProviderCode,
                PayorID = PayorID,
                OldPayorID = OldPayorID,
                DateRowAdded = DateRowAdded,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy
            };

            return domain;
        }

        public SlushFundAccountsPoco() { }

        public SlushFundAccountsPoco(SlushFundAccountsDomain domain)
        {
            SlushAccountID = domain.SlushAccountID;
            OldSlushAccountID = domain.OldSlushAccountID;
            SlushAccountGuid = domain.SlushAccountGuid;
            ProviderCode = domain.ProviderCode;
            PayorID = domain.PayorID;
            OldPayorID = domain.OldPayorID;
            DateRowAdded = domain.DateRowAdded;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
        }
    }
}
