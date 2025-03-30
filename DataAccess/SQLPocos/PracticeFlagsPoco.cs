using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PracticeFlagsPoco
    {
        public int RowID { get; set; }
        public string FlagName { get; set; }
        public bool? Inactive { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public PracticeFlagsDomain MapToDomainModel()
        {
            PracticeFlagsDomain domain = new PracticeFlagsDomain
            {
                RowID = RowID,
                FlagName = FlagName,
                Inactive = Inactive,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public PracticeFlagsPoco() { }

        public PracticeFlagsPoco(PracticeFlagsDomain domain)
        {
            RowID = domain.RowID;
            FlagName = domain.FlagName;
            Inactive = domain.Inactive;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
