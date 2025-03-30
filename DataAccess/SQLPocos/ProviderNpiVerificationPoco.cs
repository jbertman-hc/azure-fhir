using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ProviderNpiVerificationPoco
    {
        public Guid ProviderNpiVerificationId { get; set; }
        public string ProviderCode { get; set; }
        public string Status { get; set; }
        public DateTime? LastCheckedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? UserNotifiedDate { get; set; }
        public string Message { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ProviderNpiVerificationDomain MapToDomainModel()
        {
            ProviderNpiVerificationDomain domain = new ProviderNpiVerificationDomain
            {
                ProviderNpiVerificationId = ProviderNpiVerificationId,
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

        public ProviderNpiVerificationPoco() { }

        public ProviderNpiVerificationPoco(ProviderNpiVerificationDomain domain)
        {
            ProviderNpiVerificationId = domain.ProviderNpiVerificationId;
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
