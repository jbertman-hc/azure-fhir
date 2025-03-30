using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListPHIXPoco
    {
        public Guid ListPhixId { get; set; }
        public string ViewPhixId { get; set; }
        public int PatientId { get; set; }
        public bool IsActive { get; set; }
        public int SubscriptionStatusId { get; set; }
        public DateTime? DateActivate { get; set; }
        public DateTime? DateInactive { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListPHIXDomain MapToDomainModel()
        {
            ListPHIXDomain domain = new ListPHIXDomain
            {
                ListPhixId = ListPhixId,
                ViewPhixId = ViewPhixId,
                PatientId = PatientId,
                IsActive = IsActive,
                SubscriptionStatusId = SubscriptionStatusId,
                DateActivate = DateActivate,
                DateInactive = DateInactive,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListPHIXPoco() { }

        public ListPHIXPoco(ListPHIXDomain domain)
        {
            ListPhixId = domain.ListPhixId;
            ViewPhixId = domain.ViewPhixId;
            PatientId = domain.PatientId;
            IsActive = domain.IsActive;
            SubscriptionStatusId = domain.SubscriptionStatusId;
            DateActivate = domain.DateActivate;
            DateInactive = domain.DateInactive;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
