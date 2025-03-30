using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class EligibilityVerificationPoco
    {
        public Guid VerificationID { get; set; }
        public int PatientID { get; set; }
        public Guid ListPayorID { get; set; }
        public DateTime VerifiedDate { get; set; }
        public bool Result { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public EligibilityVerificationDomain MapToDomainModel()
        {
            EligibilityVerificationDomain domain = new EligibilityVerificationDomain
            {
                VerificationID = VerificationID,
                PatientID = PatientID,
                ListPayorID = ListPayorID,
                VerifiedDate = VerifiedDate,
                Result = Result,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public EligibilityVerificationPoco() { }

        public EligibilityVerificationPoco(EligibilityVerificationDomain domain)
        {
            VerificationID = domain.VerificationID;
            PatientID = domain.PatientID;
            ListPayorID = domain.ListPayorID;
            VerifiedDate = domain.VerifiedDate;
            Result = domain.Result;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}