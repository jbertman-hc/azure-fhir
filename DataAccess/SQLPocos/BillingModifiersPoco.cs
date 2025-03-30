using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class BillingModifiersPoco
    {
        public Guid BillingModifiersID { get; set; }
        public Guid? BillingCPTsID { get; set; }
        public string Code { get; set; }
        public string Comments { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public int? Sequence { get; set; }

        public BillingModifiersDomain MapToDomainModel()
        {
            BillingModifiersDomain domain = new BillingModifiersDomain
            {
                BillingModifiersID = BillingModifiersID,
                BillingCPTsID = BillingCPTsID,
                Code = Code,
                Comments = Comments,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Sequence = Sequence
            };

            return domain;
        }

        public BillingModifiersPoco() { }

        public BillingModifiersPoco(BillingModifiersDomain domain)
        {
            BillingModifiersID = domain.BillingModifiersID;
            BillingCPTsID = domain.BillingCPTsID;
            Code = domain.Code;
            Comments = domain.Comments;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Sequence = domain.Sequence;
        }
    }
}
