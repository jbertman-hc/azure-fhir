using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class RenewalsPoco
    {
        public int RenewalID { get; set; }
        public string RenewRequestID { get; set; }
        public int? ResponseID { get; set; }
        public string SentToProviderCode { get; set; }
        public string DenyReasonCode { get; set; }
        public string DenyReason { get; set; }
        public string Comments { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? DateResponded { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string ListMedID { get; set; }

        public RenewalsDomain MapToDomainModel()
        {
            RenewalsDomain domain = new RenewalsDomain
            {
                RenewalID = RenewalID,
                RenewRequestID = RenewRequestID,
                ResponseID = ResponseID,
                SentToProviderCode = SentToProviderCode,
                DenyReasonCode = DenyReasonCode,
                DenyReason = DenyReason,
                Comments = Comments,
                DateReceived = DateReceived,
                DateResponded = DateResponded,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                ListMedID = ListMedID
            };

            return domain;
        }

        public RenewalsPoco() { }

        public RenewalsPoco(RenewalsDomain domain)
        {
            RenewalID = domain.RenewalID;
            RenewRequestID = domain.RenewRequestID;
            ResponseID = domain.ResponseID;
            SentToProviderCode = domain.SentToProviderCode;
            DenyReasonCode = domain.DenyReasonCode;
            DenyReason = domain.DenyReason;
            Comments = domain.Comments;
            DateReceived = domain.DateReceived;
            DateResponded = domain.DateResponded;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            ListMedID = domain.ListMedID;
        }
    }
}
