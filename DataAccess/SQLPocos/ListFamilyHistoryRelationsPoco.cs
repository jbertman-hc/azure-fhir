using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListFamilyHistoryRelationsPoco
    {
        public int FamilyHistoryRelationId { get; set; }
        public int FamilyHistoryID { get; set; }
        public string RelationCode { get; set; }
        public string RelationName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string DateOfDeath { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool HasNoSignificantHealthHistory { get; set; }
        public bool HasUnknownHealthHistory { get; set; }
        public string Notes { get; set; }

        public ListFamilyHistoryRelationsDomain MapToDomainModel()
        {
            ListFamilyHistoryRelationsDomain domain = new ListFamilyHistoryRelationsDomain
            {
                FamilyHistoryRelationId = FamilyHistoryRelationId,
                FamilyHistoryID = FamilyHistoryID,
                RelationCode = RelationCode,
                RelationName = RelationName,
                Gender = Gender,
                BirthDate = BirthDate,
                DateOfDeath = DateOfDeath,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                HasNoSignificantHealthHistory = HasNoSignificantHealthHistory,
                HasUnknownHealthHistory = HasUnknownHealthHistory,
                Notes = Notes
            };

            return domain;
        }

        public ListFamilyHistoryRelationsPoco() { }

        public ListFamilyHistoryRelationsPoco(ListFamilyHistoryRelationsDomain domain)
        {
            FamilyHistoryRelationId = domain.FamilyHistoryRelationId;
            FamilyHistoryID = domain.FamilyHistoryID;
            RelationCode = domain.RelationCode;
            RelationName = domain.RelationName;
            Gender = domain.Gender;
            BirthDate = domain.BirthDate;
            DateOfDeath = domain.DateOfDeath;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            HasNoSignificantHealthHistory = domain.HasNoSignificantHealthHistory;
            HasUnknownHealthHistory = domain.HasUnknownHealthHistory;
            Notes = domain.Notes;
        }
    }
}

