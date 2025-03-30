using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ImportLocationPoco
    {
        public int id { get; set; }
        public string Location { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ImportLocationDomain MapToDomainModel()
        {
            ImportLocationDomain domain = new ImportLocationDomain
            {
                id = id,
                Location = Location,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ImportLocationPoco() { }

        public ImportLocationPoco(ImportLocationDomain domain)
        {
            id = domain.id;
            Location = domain.Location;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}