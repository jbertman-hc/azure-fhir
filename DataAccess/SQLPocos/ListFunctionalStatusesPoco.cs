using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListFunctionalStatusesPoco
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; }
        public int EnteringProviderId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListFunctionalStatusesDomain MapToDomainModel()
        {
            ListFunctionalStatusesDomain domain = new ListFunctionalStatusesDomain
            {
                Id = Id,
                PatientId = PatientId,
                Description = Description,
                EnteringProviderId = EnteringProviderId,
                DateCreated = DateCreated,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListFunctionalStatusesPoco() { }

        public ListFunctionalStatusesPoco(ListFunctionalStatusesDomain domain)
        {
            Id = domain.Id;
            PatientId = domain.PatientId;
            Description = domain.Description;
            EnteringProviderId = domain.EnteringProviderId;
            DateCreated = domain.DateCreated;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
