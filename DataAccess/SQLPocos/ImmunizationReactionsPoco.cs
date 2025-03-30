using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ImmunizationReactionsPoco
    {
        public Guid ImmunizationReactionID { get; set; }
        public string Reaction { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ImmunizationReactionsDomain MapToDomainModel()
        {
            ImmunizationReactionsDomain domain = new ImmunizationReactionsDomain
            {
                ImmunizationReactionID = ImmunizationReactionID,
                Reaction = Reaction,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ImmunizationReactionsPoco() { }

        public ImmunizationReactionsPoco(ImmunizationReactionsDomain domain)
        {
            ImmunizationReactionID = domain.ImmunizationReactionID;
            Reaction = domain.Reaction;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
