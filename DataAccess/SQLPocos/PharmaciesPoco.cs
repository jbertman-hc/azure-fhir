using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PharmaciesPoco
    {
        public int PharmacyID { get; set; }
        public string PharmacyName { get; set; }
        public string PharmacyPhone { get; set; }
        public string PharmacyFax { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public PharmaciesDomain MapToDomainModel()
        {
            PharmaciesDomain domain = new PharmaciesDomain
            {
                PharmacyID = PharmacyID,
                PharmacyName = PharmacyName,
                PharmacyPhone = PharmacyPhone,
                PharmacyFax = PharmacyFax,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public PharmaciesPoco() { }

        public PharmaciesPoco(PharmaciesDomain domain)
        {
            PharmacyID = domain.PharmacyID;
            PharmacyName = domain.PharmacyName;
            PharmacyPhone = domain.PharmacyPhone;
            PharmacyFax = domain.PharmacyFax;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
