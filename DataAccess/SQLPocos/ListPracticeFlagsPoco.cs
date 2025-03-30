using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListPracticeFlagsPoco
    {
        public int RowID { get; set; }
        public int PatientID { get; set; }
        public int PracticeFlagID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListPracticeFlagsDomain MapToDomainModel()
        {
            ListPracticeFlagsDomain domain = new ListPracticeFlagsDomain
            {
                RowID = RowID,
                PatientID = PatientID,
                PracticeFlagID = PracticeFlagID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListPracticeFlagsPoco() { }

        public ListPracticeFlagsPoco(ListPracticeFlagsDomain domain)
        {
            RowID = domain.RowID;
            PatientID = domain.PatientID;
            PracticeFlagID = domain.PracticeFlagID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
