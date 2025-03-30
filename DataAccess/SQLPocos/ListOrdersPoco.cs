using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListOrdersPoco
    {
        public int RowID { get; set; }
        public int PatientID { get; set; }
        public int? OrderID { get; set; }
        public int OrderType { get; set; }
        public string OrderText { get; set; }
        public string CPTs { get; set; }
        public string ICDs { get; set; }
        public string Comments { get; set; }
        public string SentBy { get; set; }
        public string SentTo { get; set; }
        public int? StatusID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public DateTime? DateSent { get; set; }
        public string OrderGUID { get; set; }
        public bool? IsPartialResult { get; set; }
        public bool IsSigned { get; set; }

        public ListOrdersDomain MapToDomainModel()
        {
            ListOrdersDomain domain = new ListOrdersDomain
            {
                RowID = RowID,
                PatientID = PatientID,
                OrderID = OrderID,
                OrderType = OrderType,
                OrderText = OrderText,
                CPTs = CPTs,
                ICDs = ICDs,
                Comments = Comments,
                SentBy = SentBy,
                SentTo = SentTo,
                StatusID = StatusID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                DateSent = DateSent,
                OrderGUID = OrderGUID,
                IsPartialResult = IsPartialResult,
                IsSigned = IsSigned
            };

            return domain;
        }

        public ListOrdersPoco() { }

        public ListOrdersPoco(ListOrdersDomain domain)
        {
            RowID = domain.RowID;
            PatientID = domain.PatientID;
            OrderID = domain.OrderID;
            OrderType = domain.OrderType;
            OrderText = domain.OrderText;
            CPTs = domain.CPTs;
            ICDs = domain.ICDs;
            Comments = domain.Comments;
            SentBy = domain.SentBy;
            SentTo = domain.SentTo;
            StatusID = domain.StatusID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            DateSent = domain.DateSent;
            OrderGUID = domain.OrderGUID;
            IsPartialResult = domain.IsPartialResult;
            IsSigned = domain.IsSigned;
        }
    }
}
