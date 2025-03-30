using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class DemographicsVerificationPoco
    {
        public Guid VerificationID { get; set; }
        public int? PatientID { get; set; }
        public bool? Result { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public DateTime? EncounterDate { get; set; }

        public DemographicsVerificationDomain MapToDomainModel()
        {
            DemographicsVerificationDomain domain = new DemographicsVerificationDomain
            {
                VerificationID = VerificationID,
                PatientID = PatientID,
                Result = Result,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                EncounterDate = EncounterDate
            };

            return domain;
        }

        public DemographicsVerificationPoco() { }

        public DemographicsVerificationPoco(DemographicsVerificationDomain domain)
        {
            VerificationID = domain.VerificationID;
            PatientID = domain.PatientID;
            Result = domain.Result;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            EncounterDate = domain.EncounterDate;
        }
    }
}