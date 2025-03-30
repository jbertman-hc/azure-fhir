using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListOrderTrackingPoco
    {
        public int RowID { get; set; }
        public int ListOrderID { get; set; }
        public int OrderStatus { get; set; }
        public DateTime DateDone { get; set; }
        public string Comments { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string DoneBy { get; set; }
        public string AssignedTo { get; set; }
        public string OriginalOrderText { get; set; }

        public ListOrderTrackingDomain MapToDomainModel()
        {
            ListOrderTrackingDomain domain = new ListOrderTrackingDomain
            {
                RowID = RowID,
                ListOrderID = ListOrderID,
                OrderStatus = OrderStatus,
                DateDone = DateDone,
                Comments = Comments,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                DoneBy = DoneBy,
                AssignedTo = AssignedTo,
                OriginalOrderText = OriginalOrderText
            };

            return domain;
        }

        public ListOrderTrackingPoco() { }

        public ListOrderTrackingPoco(ListOrderTrackingDomain domain)
        {
            RowID = domain.RowID;
            ListOrderID = domain.ListOrderID;
            OrderStatus = domain.OrderStatus;
            DateDone = domain.DateDone;
            Comments = domain.Comments;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            DoneBy = domain.DoneBy;
            AssignedTo = domain.AssignedTo;
            OriginalOrderText = domain.OriginalOrderText;
        }
    }
}
