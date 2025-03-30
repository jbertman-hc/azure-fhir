using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class InterfacePartnersEditedPoco
    {
        public Guid InterfacePartnerId { get; set; }
        public string InterfaceName { get; set; }
        public string InterfaceType { get; set; }
        public string CompanyName { get; set; }
        public string CompanyURL { get; set; }
        public string CompanyDesc { get; set; }
        public bool RequiresCredentials { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public InterfacePartnersEditedDomain MapToDomainModel()
        {
            InterfacePartnersEditedDomain domain = new InterfacePartnersEditedDomain
            {
                InterfacePartnerId = InterfacePartnerId,
                InterfaceName = InterfaceName,
                InterfaceType = InterfaceType,
                CompanyName = CompanyName,
                CompanyURL = CompanyURL,
                CompanyDesc = CompanyDesc,
                RequiresCredentials = RequiresCredentials,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                IsActive = IsActive,
                IsDeleted = IsDeleted
            };

            return domain;
        }

        public InterfacePartnersEditedPoco() { }

        public InterfacePartnersEditedPoco(InterfacePartnersEditedDomain domain)
        {
            InterfacePartnerId = domain.InterfacePartnerId;
            InterfaceName = domain.InterfaceName;
            InterfaceType = domain.InterfaceType;
            CompanyName = domain.CompanyName;
            CompanyURL = domain.CompanyURL;
            CompanyDesc = domain.CompanyDesc;
            RequiresCredentials = domain.RequiresCredentials;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            IsActive = domain.IsActive;
            IsDeleted = domain.IsDeleted;
        }
    }
}
