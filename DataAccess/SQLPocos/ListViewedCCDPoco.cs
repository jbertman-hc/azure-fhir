using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListViewedCCDPoco
    {
        public int PatientId { get; set; }
        public int ListViewedCCDId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListViewedCCDDomain MapToDomainModel()
        {
            ListViewedCCDDomain domain = new ListViewedCCDDomain
            {
                PatientId = PatientId,
                ListViewedCCDId = ListViewedCCDId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListViewedCCDPoco() { }

        public ListViewedCCDPoco(ListViewedCCDDomain domain)
        {
            PatientId = domain.PatientId;
            ListViewedCCDId = domain.ListViewedCCDId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
