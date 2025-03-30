using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListPatientPreferredPharmacyPoco
    {
        public Guid ListPatientPreferredPharmacyId { get; set; }
        public int PatientId { get; set; }
        public string PharmacyId { get; set; }
        public string FreeText { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListPatientPreferredPharmacyDomain MapToDomainModel()
        {
            ListPatientPreferredPharmacyDomain domain = new ListPatientPreferredPharmacyDomain
            {
                ListPatientPreferredPharmacyId = ListPatientPreferredPharmacyId,
                PatientId = PatientId,
                PharmacyId = PharmacyId,
                FreeText = FreeText,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListPatientPreferredPharmacyPoco() { }

        public ListPatientPreferredPharmacyPoco(ListPatientPreferredPharmacyDomain domain)
        {
            ListPatientPreferredPharmacyId = domain.ListPatientPreferredPharmacyId;
            PatientId = domain.PatientId;
            PharmacyId = domain.PharmacyId;
            FreeText = domain.FreeText;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
