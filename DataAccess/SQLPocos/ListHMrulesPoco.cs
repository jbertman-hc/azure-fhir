using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListHMrulesPoco
    {
        public int RowID { get; set; }
        public int? PatientID { get; set; }
        public Guid HMRuleGUID { get; set; }
        public string RuleName { get; set; }
        public string RecommendedAge { get; set; }
        public string MinAge { get; set; }
        public string MaxAge { get; set; }
        public string RecommendedInterval { get; set; }
        public string MinInterval { get; set; }
        public string MaxInterval { get; set; }
        public string RuleText { get; set; }
        public string FrequencyOfService { get; set; }
        public string RuleRationale { get; set; }
        public string Footnote { get; set; }
        public string Source { get; set; }
        public string Type { get; set; }
        public string Grade { get; set; }
        public int? DoseNumber { get; set; }
        public string ApplicableGender { get; set; }
        public string ApplicableICDs { get; set; }
        public string RestrictedICDs { get; set; }
        public bool? LiveVaccine { get; set; }
        public bool? EggComponent { get; set; }
        public bool? GelatinComponent { get; set; }
        public string RiskCategory { get; set; }
        public string RiskFactors { get; set; }
        public string ApplicableAgeGroup { get; set; }
        public string CPT { get; set; }
        public int? VaccineID { get; set; }
        public string Comment { get; set; }
        public bool Inactive { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public int HmRuleID { get; set; }
        public bool AgeSpecific { get; set; }
        public string FundingSource { get; set; }
        public string ReleaseRevisionDate { get; set; }
        public string BibliographicCitation { get; set; }
        public string RuleType { get; set; }
        public string RuleSql { get; set; }
        public string RuleSqlDescription { get; set; }

        public ListHMrulesDomain MapToDomainModel()
        {
            ListHMrulesDomain domain = new ListHMrulesDomain
            {
                RowID = RowID,
                PatientID = PatientID,
                HMRuleGUID = HMRuleGUID,
                RuleName = RuleName,
                RecommendedAge = RecommendedAge,
                MinAge = MinAge,
                MaxAge = MaxAge,
                RecommendedInterval = RecommendedInterval,
                MinInterval = MinInterval,
                MaxInterval = MaxInterval,
                RuleText = RuleText,
                FrequencyOfService = FrequencyOfService,
                RuleRationale = RuleRationale,
                Footnote = Footnote,
                Source = Source,
                Type = Type,
                Grade = Grade,
                DoseNumber = DoseNumber,
                ApplicableGender = ApplicableGender,
                ApplicableICDs = ApplicableICDs,
                RestrictedICDs = RestrictedICDs,
                LiveVaccine = LiveVaccine,
                EggComponent = EggComponent,
                GelatinComponent = GelatinComponent,
                RiskCategory = RiskCategory,
                RiskFactors = RiskFactors,
                ApplicableAgeGroup = ApplicableAgeGroup,
                CPT = CPT,
                VaccineID = VaccineID,
                Comment = Comment,
                Inactive = Inactive,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                HmRuleID = HmRuleID,
                AgeSpecific = AgeSpecific,
                FundingSource = FundingSource,
                ReleaseRevisionDate = ReleaseRevisionDate,
                BibliographicCitation = BibliographicCitation,
                RuleType = RuleType,
                RuleSql = RuleSql,
                RuleSqlDescription = RuleSqlDescription
            };

            return domain;
        }

        public ListHMrulesPoco() { }

        public ListHMrulesPoco(ListHMrulesDomain domain)
        {
            RowID = domain.RowID;
            PatientID = domain.PatientID;
            HMRuleGUID = domain.HMRuleGUID;
            RuleName = domain.RuleName;
            RecommendedAge = domain.RecommendedAge;
            MinAge = domain.MinAge;
            MaxAge = domain.MaxAge;
            RecommendedInterval = domain.RecommendedInterval;
            MinInterval = domain.MinInterval;
            MaxInterval = domain.MaxInterval;
            RuleText = domain.RuleText;
            FrequencyOfService = domain.FrequencyOfService;
            RuleRationale = domain.RuleRationale;
            Footnote = domain.Footnote;
            Source = domain.Source;
            Type = domain.Type;
            Grade = domain.Grade;
            DoseNumber = domain.DoseNumber;
            ApplicableGender = domain.ApplicableGender;
            ApplicableICDs = domain.ApplicableICDs;
            RestrictedICDs = domain.RestrictedICDs;
            LiveVaccine = domain.LiveVaccine;
            EggComponent = domain.EggComponent;
            GelatinComponent = domain.GelatinComponent;
            RiskCategory = domain.RiskCategory;
            RiskFactors = domain.RiskFactors;
            ApplicableAgeGroup = domain.ApplicableAgeGroup;
            CPT = domain.CPT;
            VaccineID = domain.VaccineID;
            Comment = domain.Comment;
            Inactive = domain.Inactive;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            HmRuleID = domain.HmRuleID;
            AgeSpecific = domain.AgeSpecific;
            FundingSource = domain.FundingSource;
            ReleaseRevisionDate = domain.ReleaseRevisionDate;
            BibliographicCitation = domain.BibliographicCitation;
            RuleType = domain.RuleType;
            RuleSql = domain.RuleSql;
            RuleSqlDescription = domain.RuleSqlDescription;
        }
    }
}
