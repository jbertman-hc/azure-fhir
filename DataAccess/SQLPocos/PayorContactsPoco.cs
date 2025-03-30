using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PayorContactsPoco
    {
        public Guid PayorContactID { get; set; }
        public Guid PayorsID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneExt { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string JobTitle { get; set; }
        public bool? IsPrimaryContact { get; set; }

        public PayorContactsDomain MapToDomainModel()
        {
            PayorContactsDomain domain = new PayorContactsDomain
            {
                PayorContactID = PayorContactID,
                PayorsID = PayorsID,
                FirstName = FirstName,
                LastName = LastName,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                State = State,
                PostalCode = PostalCode,
                PhoneNumber = PhoneNumber,
                PhoneExt = PhoneExt,
                Fax = Fax,
                Email = Email,
                Notes = Notes,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                JobTitle = JobTitle,
                IsPrimaryContact = IsPrimaryContact
            };

            return domain;
        }

        public PayorContactsPoco() { }

        public PayorContactsPoco(PayorContactsDomain domain)
        {
            PayorContactID = domain.PayorContactID;
            PayorsID = domain.PayorsID;
            FirstName = domain.FirstName;
            LastName = domain.LastName;
            Address1 = domain.Address1;
            Address2 = domain.Address2;
            City = domain.City;
            State = domain.State;
            PostalCode = domain.PostalCode;
            PhoneNumber = domain.PhoneNumber;
            PhoneExt = domain.PhoneExt;
            Fax = domain.Fax;
            Email = domain.Email;
            Notes = domain.Notes;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            JobTitle = domain.JobTitle;
            IsPrimaryContact = domain.IsPrimaryContact;
        }
    }
}
