using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PendingCcdaToGeneratePoco
    {
        public int PendingCcdaId { get; set; }
        public int EncounterId { get; set; }
        public int CcdaTypeId { get; set; }
        public string ComponentId { get; set; }
        public Guid? PhixEventsId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public PendingCcdaToGenerateDomain MapToDomainModel()
        {
            PendingCcdaToGenerateDomain domain = new PendingCcdaToGenerateDomain
            {
                PendingCcdaId = PendingCcdaId,
                EncounterId = EncounterId,
                CcdaTypeId = CcdaTypeId,
                ComponentId = ComponentId,
                PhixEventsId = PhixEventsId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public PendingCcdaToGeneratePoco() { }

        public PendingCcdaToGeneratePoco(PendingCcdaToGenerateDomain domain)
        {
            PendingCcdaId = domain.PendingCcdaId;
            EncounterId = domain.EncounterId;
            CcdaTypeId = domain.CcdaTypeId;
            ComponentId = domain.ComponentId;
            PhixEventsId = domain.PhixEventsId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
