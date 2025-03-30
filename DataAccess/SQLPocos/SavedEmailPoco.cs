using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SavedEmailPoco
    {
        public int msgID { get; set; }
        public string SavedTo { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public DateTime Date { get; set; }
        public string Re { get; set; }
        public string CC { get; set; }
        public string PatientName { get; set; }
        public string Body { get; set; }
        public int? PatientID { get; set; }
        public string MsgHighlightColor { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public SavedEmailDomain MapToDomainModel()
        {
            SavedEmailDomain domain = new SavedEmailDomain
            {
                msgID = msgID,
                SavedTo = SavedTo,
                To = To,
                From = From,
                Date = Date,
                Re = Re,
                CC = CC,
                PatientName = PatientName,
                Body = Body,
                PatientID = PatientID,
                MsgHighlightColor = MsgHighlightColor,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public SavedEmailPoco() { }

        public SavedEmailPoco(SavedEmailDomain domain)
        {
            msgID = domain.msgID;
            SavedTo = domain.SavedTo;
            To = domain.To;
            From = domain.From;
            Date = domain.Date;
            Re = domain.Re;
            CC = domain.CC;
            PatientName = domain.PatientName;
            Body = domain.Body;
            PatientID = domain.PatientID;
            MsgHighlightColor = domain.MsgHighlightColor;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
