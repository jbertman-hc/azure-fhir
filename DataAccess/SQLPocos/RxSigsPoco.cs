using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class RxSigsPoco
    {
        public int RowID { get; set; }
        public int ListMedID { get; set; }
        public int ActionID { get; set; }
        public int FormID { get; set; }
        public int RouteID { get; set; }
        public int FrequencyID { get; set; }
        public int AmountID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public RxSigsDomain MapToDomainModel()
        {
            RxSigsDomain domain = new RxSigsDomain
            {
                RowID = RowID,
                ListMedID = ListMedID,
                ActionID = ActionID,
                FormID = FormID,
                RouteID = RouteID,
                FrequencyID = FrequencyID,
                AmountID = AmountID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public RxSigsPoco() { }

        public RxSigsPoco(RxSigsDomain domain)
        {
            RowID = domain.RowID;
            ListMedID = domain.ListMedID;
            ActionID = domain.ActionID;
            FormID = domain.FormID;
            RouteID = domain.RouteID;
            FrequencyID = domain.FrequencyID;
            AmountID = domain.AmountID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
