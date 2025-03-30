using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ClinicalAssessmentTrackingPoco
    {
        public int Id { get; set; }
        public int? ProviderId { get; set; }
        public string ProviderName { get; set; }
        public int? ClinicalAssessmentId { get; set; }
        public string ClinicalAssessmentName { get; set; }
        public DateTime? DateAccessed { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ClinicalAssessmentTrackingDomain MapToDomainModel()
        {
            ClinicalAssessmentTrackingDomain domain = new ClinicalAssessmentTrackingDomain
            {
                Id = Id,
                ProviderId = ProviderId,
                ProviderName = ProviderName,
                ClinicalAssessmentId = ClinicalAssessmentId,
                ClinicalAssessmentName = ClinicalAssessmentName,
                DateAccessed = DateAccessed,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ClinicalAssessmentTrackingPoco() { }

        public ClinicalAssessmentTrackingPoco(ClinicalAssessmentTrackingDomain domain)
        {
            Id = domain.Id;
            ProviderId = domain.ProviderId;
            ProviderName = domain.ProviderName;
            ClinicalAssessmentId = domain.ClinicalAssessmentId;
            ClinicalAssessmentName = domain.ClinicalAssessmentName;
            DateAccessed = domain.DateAccessed;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}