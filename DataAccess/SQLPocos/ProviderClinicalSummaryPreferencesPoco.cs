using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ProviderClinicalSummaryPreferencesPoco
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public bool IsPatientDemographics { get; set; }
        public bool IsProviderOfficeInformation { get; set; }
        public bool IsDateAndVisitLocation { get; set; }
        public bool IsChiefComplaint { get; set; }
        public bool IsProblems { get; set; }
        public bool IsProblemsIncludeInactive { get; set; }
        public bool IsMedications { get; set; }
        public bool IsMedicationsIncludeInactive { get; set; }
        public bool IsAllergies { get; set; }
        public bool IsAllergiesIncludeInactive { get; set; }
        public bool IsVitalSigns { get; set; }
        public bool IsSmokingStatus { get; set; }
        public bool IsProceduresDoneDuringVisit { get; set; }
        public bool IsClinicalInstructions { get; set; }
        public bool IsPlanOfCare { get; set; }
        public bool IsImmunizations { get; set; }
        public bool IsLabTestResults { get; set; }
        public string ExportLocation { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ProviderClinicalSummaryPreferencesDomain MapToDomainModel()
        {
            ProviderClinicalSummaryPreferencesDomain domain = new ProviderClinicalSummaryPreferencesDomain
            {
                Id = Id,
                ProviderId = ProviderId,
                IsPatientDemographics = IsPatientDemographics,
                IsProviderOfficeInformation = IsProviderOfficeInformation,
                IsDateAndVisitLocation = IsDateAndVisitLocation,
                IsChiefComplaint = IsChiefComplaint,
                IsProblems = IsProblems,
                IsProblemsIncludeInactive = IsProblemsIncludeInactive,
                IsMedications = IsMedications,
                IsMedicationsIncludeInactive = IsMedicationsIncludeInactive,
                IsAllergies = IsAllergies,
                IsAllergiesIncludeInactive = IsAllergiesIncludeInactive,
                IsVitalSigns = IsVitalSigns,
                IsSmokingStatus = IsSmokingStatus,
                IsProceduresDoneDuringVisit = IsProceduresDoneDuringVisit,
                IsClinicalInstructions = IsClinicalInstructions,
                IsPlanOfCare = IsPlanOfCare,
                IsImmunizations = IsImmunizations,
                IsLabTestResults = IsLabTestResults,
                ExportLocation = ExportLocation,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ProviderClinicalSummaryPreferencesPoco() { }

        public ProviderClinicalSummaryPreferencesPoco(ProviderClinicalSummaryPreferencesDomain domain)
        {
            Id = domain.Id;
            ProviderId = domain.ProviderId;
            IsPatientDemographics = domain.IsPatientDemographics;
            IsProviderOfficeInformation = domain.IsProviderOfficeInformation;
            IsDateAndVisitLocation = domain.IsDateAndVisitLocation;
            IsChiefComplaint = domain.IsChiefComplaint;
            IsProblems = domain.IsProblems;
            IsProblemsIncludeInactive = domain.IsProblemsIncludeInactive;
            IsMedications = domain.IsMedications;
            IsMedicationsIncludeInactive = domain.IsMedicationsIncludeInactive;
            IsAllergies = domain.IsAllergies;
            IsAllergiesIncludeInactive = domain.IsAllergiesIncludeInactive;
            IsVitalSigns = domain.IsVitalSigns;
            IsSmokingStatus = domain.IsSmokingStatus;
            IsProceduresDoneDuringVisit = domain.IsProceduresDoneDuringVisit;
            IsClinicalInstructions = domain.IsClinicalInstructions;
            IsPlanOfCare = domain.IsPlanOfCare;
            IsImmunizations = domain.IsImmunizations;
            IsLabTestResults = domain.IsLabTestResults;
            ExportLocation = domain.ExportLocation;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
