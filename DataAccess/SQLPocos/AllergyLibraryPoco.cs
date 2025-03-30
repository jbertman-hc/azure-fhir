using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class AllergyLibraryPoco
    {
        public decimal? CompositeAllergyID { get; set; }
        public string Description { get; set; }

        public AllergyLibraryDomain MapToDomainModel()
        {
            AllergyLibraryDomain domain = new AllergyLibraryDomain
            {
                CompositeAllergyID = CompositeAllergyID,
                Description = Description
            };

            return domain;
        }

        public AllergyLibraryPoco() { }

        public AllergyLibraryPoco(AllergyLibraryDomain domain)
        {
            CompositeAllergyID = domain.CompositeAllergyID;
            Description = domain.Description;
        }
    }
}