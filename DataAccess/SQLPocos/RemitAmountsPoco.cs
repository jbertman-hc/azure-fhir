using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class RemitAmountsPoco
    {
        public Guid ID { get; set; }
        public Guid OwnerID { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public RemitAmountsDomain MapToDomainModel()
        {
            RemitAmountsDomain domain = new RemitAmountsDomain
            {
                ID = ID,
                OwnerID = OwnerID,
                Code = Code,
                Amount = Amount,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public RemitAmountsPoco() { }

        public RemitAmountsPoco(RemitAmountsDomain domain)
        {
            ID = domain.ID;
            OwnerID = domain.OwnerID;
            Code = domain.Code;
            Amount = domain.Amount;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
