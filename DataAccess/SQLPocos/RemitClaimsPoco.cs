using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class RemitClaimsPoco
    {
        public Guid RemitClaimsID { get; set; }
        public Guid PayorPaymentID { get; set; }
        public Guid BillingID { get; set; }
        public decimal? TotalCharge { get; set; }
        public decimal? TotalPayment { get; set; }
        public DateTime? DateRecieved { get; set; }
        public int? ClaimStatusCode { get; set; }
        public bool? FinalPayor { get; set; }
        public bool? DeniedLines { get; set; }
        public string Comments { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string PayerInternalControlNum { get; set; }

        public RemitClaimsDomain MapToDomainModel()
        {
            RemitClaimsDomain domain = new RemitClaimsDomain
            {
                RemitClaimsID = RemitClaimsID,
                PayorPaymentID = PayorPaymentID,
                BillingID = BillingID,
                TotalCharge = TotalCharge,
                TotalPayment = TotalPayment,
                DateRecieved = DateRecieved,
                ClaimStatusCode = ClaimStatusCode,
                FinalPayor = FinalPayor,
                DeniedLines = DeniedLines,
                Comments = Comments,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                PayerInternalControlNum = PayerInternalControlNum
            };

            return domain;
        }

        public RemitClaimsPoco() { }

        public RemitClaimsPoco(RemitClaimsDomain domain)
        {
            RemitClaimsID = domain.RemitClaimsID;
            PayorPaymentID = domain.PayorPaymentID;
            BillingID = domain.BillingID;
            TotalCharge = domain.TotalCharge;
            TotalPayment = domain.TotalPayment;
            DateRecieved = domain.DateRecieved;
            ClaimStatusCode = domain.ClaimStatusCode;
            FinalPayor = domain.FinalPayor;
            DeniedLines = domain.DeniedLines;
            Comments = domain.Comments;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            PayerInternalControlNum = domain.PayerInternalControlNum;
        }
    }
}
