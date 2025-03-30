using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PatientMessagesPoco
    {
        public int msgID { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public DateTime Date { get; set; }
        public string Re { get; set; }
        public string CC { get; set; }
        public string PatientName { get; set; }
        public string Body { get; set; }
        public int PatientID { get; set; }
        public string ProviderSignature { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public PatientMessagesDomain MapToDomainModel()
        {
            PatientMessagesDomain domain = new PatientMessagesDomain
            {
                msgID = msgID,
                To = To,
                From = From,
                Date = Date,
                Re = Re,
                CC = CC,
                PatientName = PatientName,
                Body = Body,
                PatientID = PatientID,
                ProviderSignature = ProviderSignature,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public PatientMessagesPoco() { }

        public PatientMessagesPoco(PatientMessagesDomain domain)
        {
            msgID = domain.msgID;
            To = domain.To;
            From = domain.From;
            Date = domain.Date;
            Re = domain.Re;
            CC = domain.CC;
            PatientName = domain.PatientName;
            Body = domain.Body;
            PatientID = domain.PatientID;
            ProviderSignature = domain.ProviderSignature;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
