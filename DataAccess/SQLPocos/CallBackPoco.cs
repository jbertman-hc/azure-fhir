using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class CallBackPoco
    {
        public int PrimaryKeyID { get; set; }
        public int PatientID { get; set; }
        public DateTime? CallBackDate { get; set; }
        public string CallBackComment { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public CallBackDomain MapToDomainModel()
        {
            CallBackDomain domain = new CallBackDomain
            {
                PrimaryKeyID = PrimaryKeyID,
                PatientID = PatientID,
                CallBackDate = CallBackDate,
                CallBackComment = CallBackComment,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public CallBackPoco() { }

        public CallBackPoco(CallBackDomain domain)
        {
            PrimaryKeyID = domain.PrimaryKeyID;
            PatientID = domain.PatientID;
            CallBackDate = domain.CallBackDate;
            CallBackComment = domain.CallBackComment;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
