using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class BillingOtherInformationPoco
    {
        public Guid BillingOtherInformationID { get; set; }
        public Guid BillingID { get; set; }
        public int PatientID { get; set; }
        public string PriorAuthorizationNumber { get; set; }
        public int? ReferProviderID { get; set; }
        public string ServiceAuthExceptionCode { get; set; }
        public DateTime? AccidentDate { get; set; }
        public DateTime? DisabilityBeginDate { get; set; }
        public DateTime? DisabilityEndDate { get; set; }
        public DateTime? LastWorkedDate { get; set; }
        public DateTime? AuthorizedReturnToWorkDate { get; set; }
        public DateTime? InitialTreatmentDate { get; set; }
        public DateTime? AcuteManifestationDate { get; set; }
        public DateTime? LastXrayDate { get; set; }
        public int? SpinalManipulationConditionID { get; set; }
        public bool? IsPatientHomebound { get; set; }
        public int? TotalVisitsRendered { get; set; }
        public int? ProjectedVisitCount { get; set; }
        public int? VisitQuantity { get; set; }
        public int? FrequencyCount { get; set; }
        public string FrequencyPeriod { get; set; }
        public int? NumberOfPeriods { get; set; }
        public string PeriodUnit { get; set; }
        public DateTime? HospitalAdmissionDate { get; set; }
        public DateTime? HospitalDischargeDate { get; set; }
        public string CasualtyClaimNumber { get; set; }
        public DateTime? FirstSymptomDate { get; set; }
        public bool IsEmergencyVisit { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string ReferralNumber { get; set; }

        public BillingOtherInformationDomain MapToDomainModel()
        {
            BillingOtherInformationDomain domain = new BillingOtherInformationDomain
            {
                BillingOtherInformationID = BillingOtherInformationID,
                BillingID = BillingID,
                PatientID = PatientID,
                PriorAuthorizationNumber = PriorAuthorizationNumber,
                ReferProviderID = ReferProviderID,
                ServiceAuthExceptionCode = ServiceAuthExceptionCode,
                AccidentDate = AccidentDate,
                DisabilityBeginDate = DisabilityBeginDate,
                DisabilityEndDate = DisabilityEndDate,
                LastWorkedDate = LastWorkedDate,
                AuthorizedReturnToWorkDate = AuthorizedReturnToWorkDate,
                InitialTreatmentDate = InitialTreatmentDate,
                AcuteManifestationDate = AcuteManifestationDate,
                LastXrayDate = LastXrayDate,
                SpinalManipulationConditionID = SpinalManipulationConditionID,
                IsPatientHomebound = IsPatientHomebound,
                TotalVisitsRendered = TotalVisitsRendered,
                ProjectedVisitCount = ProjectedVisitCount,
                VisitQuantity = VisitQuantity,
                FrequencyCount = FrequencyCount,
                FrequencyPeriod = FrequencyPeriod,
                NumberOfPeriods = NumberOfPeriods,
                PeriodUnit = PeriodUnit,
                HospitalAdmissionDate = HospitalAdmissionDate,
                HospitalDischargeDate = HospitalDischargeDate,
                CasualtyClaimNumber = CasualtyClaimNumber,
                FirstSymptomDate = FirstSymptomDate,
                IsEmergencyVisit = IsEmergencyVisit,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                ReferralNumber = ReferralNumber
            };

            return domain;
        }

        public BillingOtherInformationPoco() { }

        public BillingOtherInformationPoco(BillingOtherInformationDomain domain)
        {
            BillingOtherInformationID = domain.BillingOtherInformationID;
            BillingID = domain.BillingID;
            PatientID = domain.PatientID;
            PriorAuthorizationNumber = domain.PriorAuthorizationNumber;
            ReferProviderID = domain.ReferProviderID;
            ServiceAuthExceptionCode = domain.ServiceAuthExceptionCode;
            AccidentDate = domain.AccidentDate;
            DisabilityBeginDate = domain.DisabilityBeginDate;
            DisabilityEndDate = domain.DisabilityEndDate;
            LastWorkedDate = domain.LastWorkedDate;
            AuthorizedReturnToWorkDate = domain.AuthorizedReturnToWorkDate;
            InitialTreatmentDate = domain.InitialTreatmentDate;
            AcuteManifestationDate = domain.AcuteManifestationDate;
            LastXrayDate = domain.LastXrayDate;
            SpinalManipulationConditionID = domain.SpinalManipulationConditionID;
            IsPatientHomebound = domain.IsPatientHomebound;
            TotalVisitsRendered = domain.TotalVisitsRendered;
            ProjectedVisitCount = domain.ProjectedVisitCount;
            VisitQuantity = domain.VisitQuantity;
            FrequencyCount = domain.FrequencyCount;
            FrequencyPeriod = domain.FrequencyPeriod;
            NumberOfPeriods = domain.NumberOfPeriods;
            PeriodUnit = domain.PeriodUnit;
            HospitalAdmissionDate = domain.HospitalAdmissionDate;
            HospitalDischargeDate = domain.HospitalDischargeDate;
            CasualtyClaimNumber = domain.CasualtyClaimNumber;
            FirstSymptomDate = domain.FirstSymptomDate;
            IsEmergencyVisit = domain.IsEmergencyVisit;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            ReferralNumber = domain.ReferralNumber;
        }
    }
}

