using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class BrandPatientEducationTrackingPoco
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public int? PatientId { get; set; }
        public int? DrugId { get; set; }
        public string PharmaSource { get; set; }
        public DateTime? DateAccessed { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public BrandPatientEducationTrackingDomain MapToDomainModel()
        {
            BrandPatientEducationTrackingDomain domain = new BrandPatientEducationTrackingDomain
            {
                Id = Id,
                ProviderName = ProviderName,
                PatientId = PatientId,
                DrugId = DrugId,
                PharmaSource = PharmaSource,
                DateAccessed = DateAccessed,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public BrandPatientEducationTrackingPoco() { }

        public BrandPatientEducationTrackingPoco(BrandPatientEducationTrackingDomain domain)
        {
            Id = domain.Id;
            ProviderName = domain.ProviderName;
            PatientId = domain.PatientId;
            DrugId = domain.DrugId;
            PharmaSource = domain.PharmaSource;
            DateAccessed = domain.DateAccessed;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}







