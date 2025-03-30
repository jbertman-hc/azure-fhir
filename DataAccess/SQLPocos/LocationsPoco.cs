using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LocationsPoco
    {
        public Guid LocationsID { get; set; }
        public string Locations { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateOrRegion { get; set; }
        public string StateOrRegionText { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool IsDefault { get; set; }

        public LocationsDomain MapToDomainModel()
        {
            LocationsDomain domain = new LocationsDomain
            {
                LocationsID = LocationsID,
                Locations = Locations,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                StateOrRegion = StateOrRegion,
                StateOrRegionText = StateOrRegionText,
                PostalCode = PostalCode,
                Country = Country,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                IsDefault = IsDefault
            };

            return domain;
        }

        public LocationsPoco() { }

        public LocationsPoco(LocationsDomain domain)
        {
            LocationsID = domain.LocationsID;
            Locations = domain.Locations;
            Address1 = domain.Address1;
            Address2 = domain.Address2;
            City = domain.City;
            StateOrRegion = domain.StateOrRegion;
            StateOrRegionText = domain.StateOrRegionText;
            PostalCode = domain.PostalCode;
            Country = domain.Country;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            IsDefault = domain.IsDefault;
        }
    }
}
