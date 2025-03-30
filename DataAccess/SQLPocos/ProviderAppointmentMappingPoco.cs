using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ProviderAppointmentMappingPoco
    {
        public int UniqueTableId { get; set; }
        public int ProviderId { get; set; }
        public string ExternalId { get; set; }
        public string SourceId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ProviderAppointmentMappingDomain MapToDomainModel()
        {
            ProviderAppointmentMappingDomain domain = new ProviderAppointmentMappingDomain
            {
                UniqueTableId = UniqueTableId,
                ProviderId = ProviderId,
                ExternalId = ExternalId,
                SourceId = SourceId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ProviderAppointmentMappingPoco() { }

        public ProviderAppointmentMappingPoco(ProviderAppointmentMappingDomain domain)
        {
            UniqueTableId = domain.UniqueTableId;
            ProviderId = domain.ProviderId;
            ExternalId = domain.ExternalId;
            SourceId = domain.SourceId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
