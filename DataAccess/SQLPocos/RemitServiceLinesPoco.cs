using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class RemitServiceLinesPoco
    {
        public Guid RemitServiceLinesID { get; set; }
        public Guid? RemitClaimsID { get; set; }
        public string CPTCode { get; set; }
        public int? Units { get; set; }
        public decimal? Charge { get; set; }
        public decimal? Payment { get; set; }
        public decimal? DeniedAmount { get; set; }
        public string DeniedCodes { get; set; }
        public decimal? AllowedAmt { get; set; }
        public string RemarkCode { get; set; }
        public Guid? BillingCPTsID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public int? Bundled { get; set; }
        public Guid? PatientChargesID { get; set; }

        public RemitServiceLinesDomain MapToDomainModel()
        {
            RemitServiceLinesDomain domain = new RemitServiceLinesDomain
            {
                RemitServiceLinesID = RemitServiceLinesID,
                RemitClaimsID = RemitClaimsID,
                CPTCode = CPTCode,
                Units = Units,
                Charge = Charge,
                Payment = Payment,
                DeniedAmount = DeniedAmount,
                DeniedCodes = DeniedCodes,
                AllowedAmt = AllowedAmt,
                RemarkCode = RemarkCode,
                BillingCPTsID = BillingCPTsID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Bundled = Bundled,
                PatientChargesID = PatientChargesID
            };

            return domain;
        }

        public RemitServiceLinesPoco() { }

        public RemitServiceLinesPoco(RemitServiceLinesDomain domain)
        {
            RemitServiceLinesID = domain.RemitServiceLinesID;
            RemitClaimsID = domain.RemitClaimsID;
            CPTCode = domain.CPTCode;
            Units = domain.Units;
            Charge = domain.Charge;
            Payment = domain.Payment;
            DeniedAmount = domain.DeniedAmount;
            DeniedCodes = domain.DeniedCodes;
            AllowedAmt = domain.AllowedAmt;
            RemarkCode = domain.RemarkCode;
            BillingCPTsID = domain.BillingCPTsID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Bundled = domain.Bundled;
            PatientChargesID = domain.PatientChargesID;
        }
    }
}
