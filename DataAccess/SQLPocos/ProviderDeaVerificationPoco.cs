using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ProviderDeaVerificationPoco
    {
        public Guid ProviderDeaVerificationId { get; set; }
        public string ProviderCode { get; set; }
        public string Status { get; set; }
        public DateTime? LastCheckedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? UserNotifiedDate { get; set; }
        public string Message { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ProviderDeaVerificationDomain MapToDomainModel()
        {
            ProviderDeaVerificationDomain domain = new ProviderDeaVerificationDomain
            {
                ProviderDeaVerificationId = ProviderDeaVerificationId,
                ProviderCode = ProviderCode,
                Status = Status,
                LastCheckedDate = LastCheckedDate,
                ExpirationDate = ExpirationDate,
                UserNotifiedDate = UserNotifiedDate,
                Message = Message,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ProviderDeaVerificationPoco() { }

        public ProviderDeaVerificationPoco(ProviderDeaVerificationDomain domain)
        {
            ProviderDeaVerificationId = domain.ProviderDeaVerificationId;
            ProviderCode = domain.ProviderCode;
            Status = domain.Status;
            LastCheckedDate = domain.LastCheckedDate;
            ExpirationDate = domain.ExpirationDate;
            UserNotifiedDate = domain.UserNotifiedDate;
            Message = domain.Message;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
