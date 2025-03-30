using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ClaimStatusPoco
    {
        public Guid ClaimStatusID { get; set; }
        public Guid? BillingDatesID { get; set; }
        public string PayorICN { get; set; }
        public DateTime? PaidOrDeniedDate { get; set; }
        public string PaymentType { get; set; }
        public string CheckNum { get; set; }
        public DateTime? CheckIssueDate { get; set; }
        public Guid? PayorID { get; set; }
        public decimal? ChargeAMT { get; set; }
        public decimal? PaidAMT { get; set; }
        public DateTime? DOS { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public bool? IsRejection { get; set; }
        public bool? Unread { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ClaimStatusDomain MapToDomainModel()
        {
            ClaimStatusDomain domain = new ClaimStatusDomain
            {
                ClaimStatusID = ClaimStatusID,
                BillingDatesID = BillingDatesID,
                PayorICN = PayorICN,
                PaidOrDeniedDate = PaidOrDeniedDate,
                PaymentType = PaymentType,
                CheckNum = CheckNum,
                CheckIssueDate = CheckIssueDate,
                PayorID = PayorID,
                ChargeAMT = ChargeAMT,
                PaidAMT = PaidAMT,
                DOS = DOS,
                EffectiveDate = EffectiveDate,
                IsRejection = IsRejection,
                Unread = Unread,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ClaimStatusPoco() { }

        public ClaimStatusPoco(ClaimStatusDomain domain)
        {
            ClaimStatusID = domain.ClaimStatusID;
            BillingDatesID = domain.BillingDatesID;
            PayorICN = domain.PayorICN;
            PaidOrDeniedDate = domain.PaidOrDeniedDate;
            PaymentType = domain.PaymentType;
            CheckNum = domain.CheckNum;
            CheckIssueDate = domain.CheckIssueDate;
            PayorID = domain.PayorID;
            ChargeAMT = domain.ChargeAMT;
            PaidAMT = domain.PaidAMT;
            DOS = domain.DOS;
            EffectiveDate = domain.EffectiveDate;
            IsRejection = domain.IsRejection;
            Unread = domain.Unread;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}