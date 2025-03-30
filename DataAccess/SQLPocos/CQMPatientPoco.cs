using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class CQMPatientPoco
    {
        public int CQMPatientId { get; set; }
        public int CQMId { get; set; }
        public string CQMSection { get; set; }
        public int PatientId { get; set; }
        public DateTime? EncounterDate { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public CQMPatientDomain MapToDomainModel()
        {
            CQMPatientDomain domain = new CQMPatientDomain
            {
                CQMPatientId = CQMPatientId,
                CQMId = CQMId,
                CQMSection = CQMSection,
                PatientId = PatientId,
                EncounterDate = EncounterDate,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public CQMPatientPoco() { }

        public CQMPatientPoco(CQMPatientDomain domain)
        {
            CQMPatientId = domain.CQMPatientId;
            CQMId = domain.CQMId;
            CQMSection = domain.CQMSection;
            PatientId = domain.PatientId;
            EncounterDate = domain.EncounterDate;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
