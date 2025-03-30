using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class BillingDatesPoco
    {
        public Guid BillingDatesID { get; set; }
        public Guid? BillingID { get; set; }
        public int? EventTypeID { get; set; }
        public DateTime? EventDate { get; set; }
        public Guid? PayorID { get; set; }
        public int? BillingActionID { get; set; }
        public Guid? RemitProviderAdjustmentsHeaderID { get; set; }
        public int? SlushAcctID { get; set; }
        public DateTime? InsuranceSnapshotDate { get; set; }
        public string MessageDetail { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public BillingDatesDomain MapToDomainModel()
        {
            BillingDatesDomain domain = new BillingDatesDomain
            {
                BillingDatesID = BillingDatesID,
                BillingID = BillingID,
                EventTypeID = EventTypeID,
                EventDate = EventDate,
                PayorID = PayorID,
                BillingActionID = BillingActionID,
                RemitProviderAdjustmentsHeaderID = RemitProviderAdjustmentsHeaderID,
                SlushAcctID = SlushAcctID,
                InsuranceSnapshotDate = InsuranceSnapshotDate,
                MessageDetail = MessageDetail,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public BillingDatesPoco() { }

        public BillingDatesPoco(BillingDatesDomain domain)
        {
            BillingDatesID = domain.BillingDatesID;
            BillingID = domain.BillingID;
            EventTypeID = domain.EventTypeID;
            EventDate = domain.EventDate;
            PayorID = domain.PayorID;
            BillingActionID = domain.BillingActionID;
            RemitProviderAdjustmentsHeaderID = domain.RemitProviderAdjustmentsHeaderID;
            SlushAcctID = domain.SlushAcctID;
            InsuranceSnapshotDate = domain.InsuranceSnapshotDate;
            MessageDetail = domain.MessageDetail;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}

