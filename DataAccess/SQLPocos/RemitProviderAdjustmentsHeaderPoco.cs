using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class RemitProviderAdjustmentsHeaderPoco
    {
        public Guid ID { get; set; }
        public Guid? PayorPaymentID { get; set; }
        public string ProviderNumber { get; set; }
        public DateTime? LastDayFiscalYear { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public RemitProviderAdjustmentsHeaderDomain MapToDomainModel()
        {
            RemitProviderAdjustmentsHeaderDomain domain = new RemitProviderAdjustmentsHeaderDomain
            {
                ID = ID,
                PayorPaymentID = PayorPaymentID,
                ProviderNumber = ProviderNumber,
                LastDayFiscalYear = LastDayFiscalYear,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public RemitProviderAdjustmentsHeaderPoco() { }

        public RemitProviderAdjustmentsHeaderPoco(RemitProviderAdjustmentsHeaderDomain domain)
        {
            ID = domain.ID;
            PayorPaymentID = domain.PayorPaymentID;
            ProviderNumber = domain.ProviderNumber;
            LastDayFiscalYear = domain.LastDayFiscalYear;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
