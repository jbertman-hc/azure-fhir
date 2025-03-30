using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListDataTrackingPoco
    {
        public int ListDataTrackID { get; set; }
        public int PatientID { get; set; }
        public string Item { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public string Comments { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListDataTrackingDomain MapToDomainModel()
        {
            ListDataTrackingDomain domain = new ListDataTrackingDomain
            {
                ListDataTrackID = ListDataTrackID,
                PatientID = PatientID,
                Item = Item,
                Date = Date,
                Value = Value,
                Comments = Comments,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListDataTrackingPoco() { }

        public ListDataTrackingPoco(ListDataTrackingDomain domain)
        {
            ListDataTrackID = domain.ListDataTrackID;
            PatientID = domain.PatientID;
            Item = domain.Item;
            Date = domain.Date;
            Value = domain.Value;
            Comments = domain.Comments;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
