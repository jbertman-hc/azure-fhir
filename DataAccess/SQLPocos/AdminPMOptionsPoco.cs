using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class AdminPMOptionsPoco
    {
        public Guid AdminPMOptionsID { get; set; }
        public bool IsIncomingPaymentToPractice { get; set; }
        public Guid? PaymentLocationID { get; set; }
        public DateTime? PMStartDate { get; set; }
        public bool WarnIfPMDataInvalid { get; set; }
        public bool MigratedSuperbillAccount { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool PMToggledOff { get; set; }

        public AdminPMOptionsDomain MapToDomainModel()
        {
            AdminPMOptionsDomain domain = new AdminPMOptionsDomain
            {
                AdminPMOptionsID = AdminPMOptionsID,
                IsIncomingPaymentToPractice = IsIncomingPaymentToPractice,
                PaymentLocationID = PaymentLocationID,
                PMStartDate = PMStartDate,
                WarnIfPMDataInvalid = WarnIfPMDataInvalid,
                MigratedSuperbillAccount = MigratedSuperbillAccount,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                PMToggledOff = PMToggledOff
            };

            return domain;
        }

        public AdminPMOptionsPoco() { }

        public AdminPMOptionsPoco(AdminPMOptionsDomain domain)
        {
            AdminPMOptionsID = domain.AdminPMOptionsID;
            IsIncomingPaymentToPractice = domain.IsIncomingPaymentToPractice;
            PaymentLocationID = domain.PaymentLocationID;
            PMStartDate = domain.PMStartDate;
            WarnIfPMDataInvalid = domain.WarnIfPMDataInvalid;
            MigratedSuperbillAccount = domain.MigratedSuperbillAccount;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            PMToggledOff = domain.PMToggledOff;
        }
    }
}