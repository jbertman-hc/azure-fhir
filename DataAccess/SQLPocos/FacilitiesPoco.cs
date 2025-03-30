using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class FacilitiesPoco
    {
        public Guid FacilitiesID { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateOrRegion { get; set; }
        public string StateOrRegionText { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string NPI { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public int? FacilityTypeCode { get; set; }
        public string PracticeFacilityCode { get; set; }

        public FacilitiesDomain MapToDomainModel()
        {
            FacilitiesDomain domain = new FacilitiesDomain
            {
                FacilitiesID = FacilitiesID,
                Name = Name,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                StateOrRegion = StateOrRegion,
                StateOrRegionText = StateOrRegionText,
                PostalCode = PostalCode,
                Country = Country,
                NPI = NPI,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                FacilityTypeCode = FacilityTypeCode,
                PracticeFacilityCode = PracticeFacilityCode
            };

            return domain;
        }

        public FacilitiesPoco() { }

        public FacilitiesPoco(FacilitiesDomain domain)
        {
            FacilitiesID = domain.FacilitiesID;
            Name = domain.Name;
            Address1 = domain.Address1;
            Address2 = domain.Address2;
            City = domain.City;
            StateOrRegion = domain.StateOrRegion;
            StateOrRegionText = domain.StateOrRegionText;
            PostalCode = domain.PostalCode;
            Country = domain.Country;
            NPI = domain.NPI;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            FacilityTypeCode = domain.FacilityTypeCode;
            PracticeFacilityCode = domain.PracticeFacilityCode;
        }
    }
}
