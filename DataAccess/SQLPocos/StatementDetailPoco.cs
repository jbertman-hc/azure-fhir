using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class StatementDetailPoco
    {
        public Guid StatementID { get; set; }
        public Guid BillingID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public int? Sequence { get; set; }
        public Guid? PatientChargesID { get; set; }
        public DateTime? DOS { get; set; }
        public string CPTCode { get; set; }
        public string CPTDescription { get; set; }
        public string ChargeType { get; set; }
        public decimal? PatientCharges { get; set; }
        public decimal? PatientPayment { get; set; }
        public decimal? PatientBalance { get; set; }

        public StatementDetailDomain MapToDomainModel()
        {
            StatementDetailDomain domain = new StatementDetailDomain
            {
                StatementID = StatementID,
                BillingID = BillingID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Sequence = Sequence,
                PatientChargesID = PatientChargesID,
                DOS = DOS,
                CPTCode = CPTCode,
                CPTDescription = CPTDescription,
                ChargeType = ChargeType,
                PatientCharges = PatientCharges,
                PatientPayment = PatientPayment,
                PatientBalance = PatientBalance
            };

            return domain;
        }

        public StatementDetailPoco() { }

        public StatementDetailPoco(StatementDetailDomain domain)
        {
            StatementID = domain.StatementID;
            BillingID = domain.BillingID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Sequence = domain.Sequence;
            PatientChargesID = domain.PatientChargesID;
            DOS = domain.DOS;
            CPTCode = domain.CPTCode;
            CPTDescription = domain.CPTDescription;
            ChargeType = domain.ChargeType;
            PatientCharges = domain.PatientCharges;
            PatientPayment = domain.PatientPayment;
            PatientBalance = domain.PatientBalance;
        }
    }
}
