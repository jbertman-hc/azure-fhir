using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PatientOptionsPoco
    {
        public int PatientID { get; set; }
        public bool? DrugHistoryGranted { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public PatientOptionsDomain MapToDomainModel()
        {
            PatientOptionsDomain domain = new PatientOptionsDomain
            {
                PatientID = PatientID,
                DrugHistoryGranted = DrugHistoryGranted,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public PatientOptionsPoco() { }

        public PatientOptionsPoco(PatientOptionsDomain domain)
        {
            PatientID = domain.PatientID;
            DrugHistoryGranted = domain.DrugHistoryGranted;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
