using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ProviderSecurityPoco
    {
        public int ProviderID { get; set; }
        public string ProviderName { get; set; }
        public string ProviderPassword { get; set; }
        public DateTime DatePassLastChanged { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Degree { get; set; }
        public string DEA { get; set; }
        public string StateLicenseNumber { get; set; }
        public string State { get; set; }
        public string ProviderLevel { get; set; }
        public bool CoSignReq { get; set; }
        public string Supervisor { get; set; }
        public bool NotifySupervisor { get; set; }
        public bool Inactive { get; set; }
        public string UPIN { get; set; }
        public string EINoverride { get; set; }
        public string Specialty { get; set; }
        public string XLinkProviderID { get; set; }
        public string NPI { get; set; }
        public string ProviderSig { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? LockedOut { get; set; }
        public bool? ResetPasswordRequired { get; set; }
        public bool? AllowStaffToTransmit { get; set; }
        public string PrescribeFor { get; set; }
        public int? AllowOverrideInteraction { get; set; }
        public bool? AllowEmergencyOverride { get; set; }
        public string TaxonomyCode1 { get; set; }
        public string TaxonomyCode2 { get; set; }
        public bool? AcceptsMedicareAssignment { get; set; }
        public bool? AllowBillingAccess { get; set; }
        public bool? AllowAccessToPatientHealthInfo { get; set; }

        public ProviderSecurityDomain MapToDomainModel()
        {
            ProviderSecurityDomain domain = new ProviderSecurityDomain
            {
                ProviderID = ProviderID,
                ProviderName = ProviderName,
                ProviderPassword = ProviderPassword,
                DatePassLastChanged = DatePassLastChanged,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                Degree = Degree,
                DEA = DEA,
                StateLicenseNumber = StateLicenseNumber,
                State = State,
                ProviderLevel = ProviderLevel,
                CoSignReq = CoSignReq,
                Supervisor = Supervisor,
                NotifySupervisor = NotifySupervisor,
                Inactive = Inactive,
                UPIN = UPIN,
                EINoverride = EINoverride,
                Specialty = Specialty,
                XLinkProviderID = XLinkProviderID,
                NPI = NPI,
                ProviderSig = ProviderSig,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                LockedOut = LockedOut,
                ResetPasswordRequired = ResetPasswordRequired,
                AllowStaffToTransmit = AllowStaffToTransmit,
                PrescribeFor = PrescribeFor,
                AllowOverrideInteraction = AllowOverrideInteraction,
                AllowEmergencyOverride = AllowEmergencyOverride,
                TaxonomyCode1 = TaxonomyCode1,
                TaxonomyCode2 = TaxonomyCode2,
                AcceptsMedicareAssignment = AcceptsMedicareAssignment,
                AllowBillingAccess = AllowBillingAccess,
                AllowAccessToPatientHealthInfo = AllowAccessToPatientHealthInfo
            };

            return domain;
        }

        public ProviderSecurityPoco() { }

        public ProviderSecurityPoco(ProviderSecurityDomain domain)
        {
            ProviderID = domain.ProviderID;
            ProviderName = domain.ProviderName;
            ProviderPassword = domain.ProviderPassword;
            DatePassLastChanged = domain.DatePassLastChanged;
            FirstName = domain.FirstName;
            MiddleName = domain.MiddleName;
            LastName = domain.LastName;
            Degree = domain.Degree;
            DEA = domain.DEA;
            StateLicenseNumber = domain.StateLicenseNumber;
            State = domain.State;
            ProviderLevel = domain.ProviderLevel;
            CoSignReq = domain.CoSignReq;
            Supervisor = domain.Supervisor;
            NotifySupervisor = domain.NotifySupervisor;
            Inactive = domain.Inactive;
            UPIN = domain.UPIN;
            EINoverride = domain.EINoverride;
            Specialty = domain.Specialty;
            XLinkProviderID = domain.XLinkProviderID;
            NPI = domain.NPI;
            ProviderSig = domain.ProviderSig;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            LockedOut = domain.LockedOut;
            ResetPasswordRequired = domain.ResetPasswordRequired;
            AllowStaffToTransmit = domain.AllowStaffToTransmit;
            PrescribeFor = domain.PrescribeFor;
            AllowOverrideInteraction = domain.AllowOverrideInteraction;
            AllowEmergencyOverride = domain.AllowEmergencyOverride;
            TaxonomyCode1 = domain.TaxonomyCode1;
            TaxonomyCode2 = domain.TaxonomyCode2;
            AcceptsMedicareAssignment = domain.AcceptsMedicareAssignment;
            AllowBillingAccess = domain.AllowBillingAccess;
            AllowAccessToPatientHealthInfo = domain.AllowAccessToPatientHealthInfo;
        }
    }
}

