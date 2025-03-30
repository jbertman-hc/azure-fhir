using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListOrdersRefIdsPoco
    {
        public int LORI_Id { get; set; }
        public string RefId { get; set; }
        public int LabOrderRowId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime DateRowAdded { get; set; }

        public ListOrdersRefIdsDomain MapToDomainModel()
        {
            ListOrdersRefIdsDomain domain = new ListOrdersRefIdsDomain
            {
                LORI_Id = LORI_Id,
                RefId = RefId,
                LabOrderRowId = LabOrderRowId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListOrdersRefIdsPoco() { }

        public ListOrdersRefIdsPoco(ListOrdersRefIdsDomain domain)
        {
            LORI_Id = domain.LORI_Id;
            RefId = domain.RefId;
            LabOrderRowId = domain.LabOrderRowId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
