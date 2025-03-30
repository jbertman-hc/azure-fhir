using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class TrackedTemplatePoco
    {
        public int id { get; set; }
        public string Item { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public TrackedTemplateDomain MapToDomainModel()
        {
            TrackedTemplateDomain domain = new TrackedTemplateDomain
            {
                id = id,
                Item = Item,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public TrackedTemplatePoco() { }

        public TrackedTemplatePoco(TrackedTemplateDomain domain)
        {
            id = domain.id;
            Item = domain.Item;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
