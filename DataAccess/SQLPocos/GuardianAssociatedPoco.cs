using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class GuardianAssociatedPoco
    {
        public Guid GuardianAssociatedID { get; set; }
        public int PatientID { get; set; }
        public Guid AssociatedPartiesID { get; set; }
        public int? RelationID { get; set; }
        public bool IsPrimaryGuardian { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string Comments { get; set; }

        public GuardianAssociatedDomain MapToDomainModel()
        {
            GuardianAssociatedDomain domain = new GuardianAssociatedDomain
            {
                GuardianAssociatedID = GuardianAssociatedID,
                PatientID = PatientID,
                AssociatedPartiesID = AssociatedPartiesID,
                RelationID = RelationID,
                IsPrimaryGuardian = IsPrimaryGuardian,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Comments = Comments
            };

            return domain;
        }

        public GuardianAssociatedPoco() { }

        public GuardianAssociatedPoco(GuardianAssociatedDomain domain)
        {
            GuardianAssociatedID = domain.GuardianAssociatedID;
            PatientID = domain.PatientID;
            AssociatedPartiesID = domain.AssociatedPartiesID;
            RelationID = domain.RelationID;
            IsPrimaryGuardian = domain.IsPrimaryGuardian;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Comments = domain.Comments;
        }
    }
}
