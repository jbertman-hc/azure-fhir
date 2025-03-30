using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PatientProcedurePerformersPoco
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public PatientProcedurePerformersDomain MapToDomainModel()
        {
            PatientProcedurePerformersDomain domain = new PatientProcedurePerformersDomain
            {
                Id = Id,
                Name = Name,
                Address = Address,
                PhoneNumber = PhoneNumber,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public PatientProcedurePerformersPoco() { }

        public PatientProcedurePerformersPoco(PatientProcedurePerformersDomain domain)
        {
            Id = domain.Id;
            Name = domain.Name;
            Address = domain.Address;
            PhoneNumber = domain.PhoneNumber;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
