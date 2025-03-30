using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class CCDSettingsPoco
    {
        public Guid CCDSettingID { get; set; }
        public string SettingType { get; set; }
        public string ProviderName { get; set; }
        public bool? IncludeDemographics { get; set; }
        public bool? IncludePurpose { get; set; }
        public bool? IncludePayors { get; set; }
        public bool? IncludeAllergies { get; set; }
        public bool? IncludeProblems { get; set; }
        public bool? IncludeMeds { get; set; }
        public bool? IncludeDirectives { get; set; }
        public bool? IncludeImmunizations { get; set; }
        public bool? IncludeResults { get; set; }
        public bool? IncludePlanOfCare { get; set; }
        public bool? IncludeEncounters { get; set; }
        public string AllergyCodesToUse { get; set; }
        public string MedsCodesToUse { get; set; }
        public bool? IncludeInactiveMeds { get; set; }
        public bool? IncludeInactiveProblems { get; set; }
        public bool? IncludeResolvedProblems { get; set; }
        public bool? IncludeOnlyHighPriorityDirectives { get; set; }
        public bool? IncludeRefusedImmunizations { get; set; }
        public string ResultsDateRange { get; set; }
        public string EncountersDateRange { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public CCDSettingsDomain MapToDomainModel()
        {
            CCDSettingsDomain domain = new CCDSettingsDomain
            {
                CCDSettingID = CCDSettingID,
                SettingType = SettingType,
                ProviderName = ProviderName,
                IncludeDemographics = IncludeDemographics,
                IncludePurpose = IncludePurpose,
                IncludePayors = IncludePayors,
                IncludeAllergies = IncludeAllergies,
                IncludeProblems = IncludeProblems,
                IncludeMeds = IncludeMeds,
                IncludeDirectives = IncludeDirectives,
                IncludeImmunizations = IncludeImmunizations,
                IncludeResults = IncludeResults,
                IncludePlanOfCare = IncludePlanOfCare,
                IncludeEncounters = IncludeEncounters,
                AllergyCodesToUse = AllergyCodesToUse,
                MedsCodesToUse = MedsCodesToUse,
                IncludeInactiveMeds = IncludeInactiveMeds,
                IncludeInactiveProblems = IncludeInactiveProblems,
                IncludeResolvedProblems = IncludeResolvedProblems,
                IncludeOnlyHighPriorityDirectives = IncludeOnlyHighPriorityDirectives,
                IncludeRefusedImmunizations = IncludeRefusedImmunizations,
                ResultsDateRange = ResultsDateRange,
                EncountersDateRange = EncountersDateRange,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public CCDSettingsPoco() { }

        public CCDSettingsPoco(CCDSettingsDomain domain)
        {
            CCDSettingID = domain.CCDSettingID;
            SettingType = domain.SettingType;
            ProviderName = domain.ProviderName;
            IncludeDemographics = domain.IncludeDemographics;
            IncludePurpose = domain.IncludePurpose;
            IncludePayors = domain.IncludePayors;
            IncludeAllergies = domain.IncludeAllergies;
            IncludeProblems = domain.IncludeProblems;
            IncludeMeds = domain.IncludeMeds;
            IncludeDirectives = domain.IncludeDirectives;
            IncludeImmunizations = domain.IncludeImmunizations;
            IncludeResults = domain.IncludeResults;
            IncludePlanOfCare = domain.IncludePlanOfCare;
            IncludeEncounters = domain.IncludeEncounters;
            AllergyCodesToUse = domain.AllergyCodesToUse;
            MedsCodesToUse = domain.MedsCodesToUse;
            IncludeInactiveMeds = domain.IncludeInactiveMeds;
            IncludeInactiveProblems = domain.IncludeInactiveProblems;
            IncludeResolvedProblems = domain.IncludeResolvedProblems;
            IncludeOnlyHighPriorityDirectives = domain.IncludeOnlyHighPriorityDirectives;
            IncludeRefusedImmunizations = domain.IncludeRefusedImmunizations;
            ResultsDateRange = domain.ResultsDateRange;
            EncountersDateRange = domain.EncountersDateRange;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
