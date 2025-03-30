using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ClinicalAssessmentResultsPoco
    {
        public int ClinicalAssessmentResultsID { get; set; }
        public Guid? UniqueSurveyId { get; set; }
        public int ProviderId { get; set; }
        public int PatientId { get; set; }
        public DateTime EncounterDate { get; set; }
        public int HMRuleID { get; set; }
        public string Score { get; set; }
        public string Comment { get; set; }
        public DateTime ResultDate { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ClinicalAssessmentResultsDomain MapToDomainModel()
        {
            ClinicalAssessmentResultsDomain domain = new ClinicalAssessmentResultsDomain
            {
                ClinicalAssessmentResultsID = ClinicalAssessmentResultsID,
                UniqueSurveyId = UniqueSurveyId,
                ProviderId = ProviderId,
                PatientId = PatientId,
                EncounterDate = EncounterDate,
                HMRuleID = HMRuleID,
                Score = Score,
                Comment = Comment,
                ResultDate = ResultDate,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ClinicalAssessmentResultsPoco() { }

        public ClinicalAssessmentResultsPoco(ClinicalAssessmentResultsDomain domain)
        {
            ClinicalAssessmentResultsID = domain.ClinicalAssessmentResultsID;
            UniqueSurveyId = domain.UniqueSurveyId;
            ProviderId = domain.ProviderId;
            PatientId = domain.PatientId;
            EncounterDate = domain.EncounterDate;
            HMRuleID = domain.HMRuleID;
            Score = domain.Score;
            Comment = domain.Comment;
            ResultDate = domain.ResultDate;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
