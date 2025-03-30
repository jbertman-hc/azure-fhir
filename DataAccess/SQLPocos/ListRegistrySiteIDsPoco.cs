using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListRegistrySiteIDsPoco
    {
        public Guid ListRegistrySiteIDsId { get; set; }
        public int RegistryInterfaceId { get; set; }
        public Guid LocationsId { get; set; }
        public string SiteID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListRegistrySiteIDsDomain MapToDomainModel()
        {
            ListRegistrySiteIDsDomain domain = new ListRegistrySiteIDsDomain
            {
                ListRegistrySiteIDsId = ListRegistrySiteIDsId,
                RegistryInterfaceId = RegistryInterfaceId,
                LocationsId = LocationsId,
                SiteID = SiteID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListRegistrySiteIDsPoco() { }

        public ListRegistrySiteIDsPoco(ListRegistrySiteIDsDomain domain)
        {
            ListRegistrySiteIDsId = domain.ListRegistrySiteIDsId;
            RegistryInterfaceId = domain.RegistryInterfaceId;
            LocationsId = domain.LocationsId;
            SiteID = domain.SiteID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
