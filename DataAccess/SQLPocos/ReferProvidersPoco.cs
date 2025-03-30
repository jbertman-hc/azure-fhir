using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ReferProvidersPoco
    {
        public int ReferProviderID { get; set; }
        public string ReferringNumber { get; set; }
        public string RefConOtherAll { get; set; }
        public string Specialty { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string suffix { get; set; }
        public string prefix { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string comments { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string NPI { get; set; }

        public ReferProvidersDomain MapToDomainModel()
        {
            ReferProvidersDomain domain = new ReferProvidersDomain
            {
                ReferProviderID = ReferProviderID,
                ReferringNumber = ReferringNumber,
                RefConOtherAll = RefConOtherAll,
                Specialty = Specialty,
                Lastname = Lastname,
                Firstname = Firstname,
                suffix = suffix,
                prefix = prefix,
                address1 = address1,
                address2 = address2,
                city = city,
                state = state,
                zip = zip,
                phone = phone,
                fax = fax,
                email = email,
                comments = comments,
                other1 = other1,
                other2 = other2,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                NPI = NPI
            };

            return domain;
        }

        public ReferProvidersPoco() { }

        public ReferProvidersPoco(ReferProvidersDomain domain)
        {
            ReferProviderID = domain.ReferProviderID;
            ReferringNumber = domain.ReferringNumber;
            RefConOtherAll = domain.RefConOtherAll;
            Specialty = domain.Specialty;
            Lastname = domain.Lastname;
            Firstname = domain.Firstname;
            suffix = domain.suffix;
            prefix = domain.prefix;
            address1 = domain.address1;
            address2 = domain.address2;
            city = domain.city;
            state = domain.state;
            zip = domain.zip;
            phone = domain.phone;
            fax = domain.fax;
            email = domain.email;
            comments = domain.comments;
            other1 = domain.other1;
            other2 = domain.other2;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            NPI = domain.NPI;
        }
    }
}
