using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class FacilitiesMappingPoco
    {
        public int UniqueTableId { get; set; }
        public Guid FacilitiesID { get; set; }
        public string ExternalId { get; set; }
        public string SourceId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public FacilitiesMappingDomain MapToDomainModel()
        {
            FacilitiesMappingDomain domain = new FacilitiesMappingDomain
            {
                UniqueTableId = UniqueTableId,
                FacilitiesID = FacilitiesID,
                ExternalId = ExternalId,
                SourceId = SourceId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public FacilitiesMappingPoco() { }

        public FacilitiesMappingPoco(FacilitiesMappingDomain domain)
        {
            UniqueTableId = domain.UniqueTableId;
            FacilitiesID = domain.FacilitiesID;
            ExternalId = domain.ExternalId;
            SourceId = domain.SourceId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
