using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PatientIndexPoco
    {
        public int PatientIndexId { get; set; }
        public int AcPatientId { get; set; }
        public string ExternalPatientId { get; set; }
        public string Source { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string ExternalData { get; set; }

        public PatientIndexDomain MapToDomainModel()
        {
            PatientIndexDomain domain = new PatientIndexDomain
            {
                PatientIndexId = PatientIndexId,
                AcPatientId = AcPatientId,
                ExternalPatientId = ExternalPatientId,
                Source = Source,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                ExternalData = ExternalData
            };

            return domain;
        }

        public PatientIndexPoco() { }

        public PatientIndexPoco(PatientIndexDomain domain)
        {
            PatientIndexId = domain.PatientIndexId;
            AcPatientId = domain.AcPatientId;
            ExternalPatientId = domain.ExternalPatientId;
            Source = domain.Source;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            ExternalData = domain.ExternalData;
        }
    }
}
