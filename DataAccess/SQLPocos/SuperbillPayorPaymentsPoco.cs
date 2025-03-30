using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SuperbillPayorPaymentsPoco
    {
        public int SuperbillPayorPaymentID { get; set; }
        public int SuperbillPaymentID { get; set; }
        public Guid PayorsID { get; set; }
        public decimal? PayorAmount { get; set; }
        public string PayorComment { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public SuperbillPayorPaymentsDomain MapToDomainModel()
        {
            SuperbillPayorPaymentsDomain domain = new SuperbillPayorPaymentsDomain
            {
                SuperbillPayorPaymentID = SuperbillPayorPaymentID,
                SuperbillPaymentID = SuperbillPaymentID,
                PayorsID = PayorsID,
                PayorAmount = PayorAmount,
                PayorComment = PayorComment,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public SuperbillPayorPaymentsPoco() { }

        public SuperbillPayorPaymentsPoco(SuperbillPayorPaymentsDomain domain)
        {
            SuperbillPayorPaymentID = domain.SuperbillPayorPaymentID;
            SuperbillPaymentID = domain.SuperbillPaymentID;
            PayorsID = domain.PayorsID;
            PayorAmount = domain.PayorAmount;
            PayorComment = domain.PayorComment;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
