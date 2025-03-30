using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SchedulingVerificationPoco
    {
        public Guid VerificationID { get; set; }
        public int? PatientID { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public string Result { get; set; }
        public string Details { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public SchedulingVerificationDomain MapToDomainModel()
        {
            SchedulingVerificationDomain domain = new SchedulingVerificationDomain
            {
                VerificationID = VerificationID,
                PatientID = PatientID,
                ScheduledDate = ScheduledDate,
                Result = Result,
                Details = Details,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public SchedulingVerificationPoco() { }

        public SchedulingVerificationPoco(SchedulingVerificationDomain domain)
        {
            VerificationID = domain.VerificationID;
            PatientID = domain.PatientID;
            ScheduledDate = domain.ScheduledDate;
            Result = domain.Result;
            Details = domain.Details;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
