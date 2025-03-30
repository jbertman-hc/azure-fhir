using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class BillingVerificationPoco
    {
        public Guid VerificationID { get; set; }
        public int PatientID { get; set; }
        public Guid ListPayorID { get; set; }
        public DateTime DateVerified { get; set; }
        public bool Result { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public BillingVerificationDomain MapToDomainModel()
        {
            BillingVerificationDomain domain = new BillingVerificationDomain
            {
                VerificationID = VerificationID,
                PatientID = PatientID,
                ListPayorID = ListPayorID,
                DateVerified = DateVerified,
                Result = Result,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public BillingVerificationPoco() { }

        public BillingVerificationPoco(BillingVerificationDomain domain)
        {
            VerificationID = domain.VerificationID;
            PatientID = domain.PatientID;
            ListPayorID = domain.ListPayorID;
            DateVerified = domain.DateVerified;
            Result = domain.Result;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}

