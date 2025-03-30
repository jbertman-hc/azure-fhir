using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListPatientLanguagesPoco
    {
        public Guid ListPatientLanguagesId { get; set; }
        public int PatientID { get; set; }
        public int? LanguageID { get; set; }
        public string FreeText { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListPatientLanguagesDomain MapToDomainModel()
        {
            ListPatientLanguagesDomain domain = new ListPatientLanguagesDomain
            {
                ListPatientLanguagesId = ListPatientLanguagesId,
                PatientID = PatientID,
                LanguageID = LanguageID,
                FreeText = FreeText,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListPatientLanguagesPoco() { }

        public ListPatientLanguagesPoco(ListPatientLanguagesDomain domain)
        {
            ListPatientLanguagesId = domain.ListPatientLanguagesId;
            PatientID = domain.PatientID;
            LanguageID = domain.LanguageID;
            FreeText = domain.FreeText;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
