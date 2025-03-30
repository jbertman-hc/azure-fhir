using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListPatientRegistryInfoPoco
    {
        public Guid ListPatientRegistryInfoId { get; set; }
        public int PatientId { get; set; }
        public int RegistryInterfaceId { get; set; }
        public int? VFCReasonId { get; set; }
        public DateTime? DateVFCInitialScreen { get; set; }
        public int? ProtectionIndicator { get; set; }
        public DateTime? DateProtectionIndicator { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListPatientRegistryInfoDomain MapToDomainModel()
        {
            ListPatientRegistryInfoDomain domain = new ListPatientRegistryInfoDomain
            {
                ListPatientRegistryInfoId = ListPatientRegistryInfoId,
                PatientId = PatientId,
                RegistryInterfaceId = RegistryInterfaceId,
                VFCReasonId = VFCReasonId,
                DateVFCInitialScreen = DateVFCInitialScreen,
                ProtectionIndicator = ProtectionIndicator,
                DateProtectionIndicator = DateProtectionIndicator,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListPatientRegistryInfoPoco() { }

        public ListPatientRegistryInfoPoco(ListPatientRegistryInfoDomain domain)
        {
            ListPatientRegistryInfoId = domain.ListPatientRegistryInfoId;
            PatientId = domain.PatientId;
            RegistryInterfaceId = domain.RegistryInterfaceId;
            VFCReasonId = domain.VFCReasonId;
            DateVFCInitialScreen = domain.DateVFCInitialScreen;
            ProtectionIndicator = domain.ProtectionIndicator;
            DateProtectionIndicator = domain.DateProtectionIndicator;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}

