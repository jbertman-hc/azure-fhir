using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LocationsMappingPoco
    {
        public int UniqueTableId { get; set; }
        public Guid LocationsID { get; set; }
        public string ExternalId { get; set; }
        public string SourceId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public LocationsMappingDomain MapToDomainModel()
        {
            LocationsMappingDomain domain = new LocationsMappingDomain
            {
                UniqueTableId = UniqueTableId,
                LocationsID = LocationsID,
                ExternalId = ExternalId,
                SourceId = SourceId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public LocationsMappingPoco() { }

        public LocationsMappingPoco(LocationsMappingDomain domain)
        {
            UniqueTableId = domain.UniqueTableId;
            LocationsID = domain.LocationsID;
            ExternalId = domain.ExternalId;
            SourceId = domain.SourceId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
