using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListOrderResultsPoco
    {
        public int RowId { get; set; }
        public int OrderID { get; set; }
        public int ResultItemID { get; set; }
        public int ResultType { get; set; }
        public DateTime DateAssigned { get; set; }
        public string ResultText { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? IncludesRadiologyImage { get; set; }

        public ListOrderResultsDomain MapToDomainModel()
        {
            ListOrderResultsDomain domain = new ListOrderResultsDomain
            {
                RowId = RowId,
                OrderID = OrderID,
                ResultItemID = ResultItemID,
                ResultType = ResultType,
                DateAssigned = DateAssigned,
                ResultText = ResultText,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                IncludesRadiologyImage = IncludesRadiologyImage
            };

            return domain;
        }

        public ListOrderResultsPoco() { }

        public ListOrderResultsPoco(ListOrderResultsDomain domain)
        {
            RowId = domain.RowId;
            OrderID = domain.OrderID;
            ResultItemID = domain.ResultItemID;
            ResultType = domain.ResultType;
            DateAssigned = domain.DateAssigned;
            ResultText = domain.ResultText;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            IncludesRadiologyImage = domain.IncludesRadiologyImage;
        }
    }
}
