using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class AssociatedPartiesPoco
    {
        public Guid AssociatedPartiesID { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string SAGAddress1 { get; set; }
        public string SAGAddress2 { get; set; }
        public string City { get; set; }
        public string StateOrRegion { get; set; }
        public string StateOrRegionText { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneCountry { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneExt { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string Notes { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? IsNonPersonEntity { get; set; }
        public string Gender { get; set; }
        public string Employer { get; set; }

        public AssociatedPartiesDomain MapToDomainModel()
        {
            AssociatedPartiesDomain domain = new AssociatedPartiesDomain
            {
                AssociatedPartiesID = AssociatedPartiesID,
                FirstName = FirstName,
                MiddleInitial = MiddleInitial,
                LastName = LastName,
                SSN = SSN,
                SAGAddress1 = SAGAddress1,
                SAGAddress2 = SAGAddress2,
                City = City,
                StateOrRegion = StateOrRegion,
                StateOrRegionText = StateOrRegionText,
                PostalCode = PostalCode,
                Country = Country,
                PhoneCountry = PhoneCountry,
                PhoneNumber = PhoneNumber,
                PhoneExt = PhoneExt,
                Email = Email,
                DOB = DOB,
                Notes = Notes,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                IsNonPersonEntity = IsNonPersonEntity,
                Gender = Gender,
                Employer = Employer
            };

            return domain;
        }

        public AssociatedPartiesPoco() { }

        public AssociatedPartiesPoco(AssociatedPartiesDomain domain)
        {
            AssociatedPartiesID = domain.AssociatedPartiesID;
            FirstName = domain.FirstName;
            MiddleInitial = domain.MiddleInitial;
            LastName = domain.LastName;
            SSN = domain.SSN;
            SAGAddress1 = domain.SAGAddress1;
            SAGAddress2 = domain.SAGAddress2;
            City = domain.City;
            StateOrRegion = domain.StateOrRegion;
            StateOrRegionText = domain.StateOrRegionText;
            PostalCode = domain.PostalCode;
            Country = domain.Country;
            PhoneCountry = domain.PhoneCountry;
            PhoneNumber = domain.PhoneNumber;
            PhoneExt = domain.PhoneExt;
            Email = domain.Email;
            DOB = domain.DOB;
            Notes = domain.Notes;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            IsNonPersonEntity = domain.IsNonPersonEntity;
            Gender = domain.Gender;
            Employer = domain.Employer;
        }
    }
}
