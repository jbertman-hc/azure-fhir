using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SchedulingMappingPoco
    {
        public int UniqueTableId { get; set; }
        public int VisitId { get; set; }
        public string ExternalId { get; set; }
        public string SourceId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public SchedulingMappingDomain MapToDomainModel()
        {
            SchedulingMappingDomain domain = new SchedulingMappingDomain
            {
                UniqueTableId = UniqueTableId,
                VisitId = VisitId,
                ExternalId = ExternalId,
                SourceId = SourceId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public SchedulingMappingPoco() { }

        public SchedulingMappingPoco(SchedulingMappingDomain domain)
        {
            UniqueTableId = domain.UniqueTableId;
            VisitId = domain.VisitId;
            ExternalId = domain.ExternalId;
            SourceId = domain.SourceId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
