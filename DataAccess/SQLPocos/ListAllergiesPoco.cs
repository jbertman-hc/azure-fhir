using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListAllergiesPoco
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public string AllergyID { get; set; }
        public string AllergyDescription { get; set; }
        public string AllergySource { get; set; }
        public string Reaction { get; set; }
        public string Comments { get; set; }
        public string AddedBy { get; set; }
        public DateTime? DateAdded { get; set; }
        public string EditedBy { get; set; }
        public DateTime? DateEdited { get; set; }
        public bool? Inactive { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? Migrated { get; set; }
        public string LastConfirmedBy { get; set; }
        public DateTime? LastConfirmedDate { get; set; }
        public string Severity { get; set; }
        public string Source { get; set; }

        public ListAllergiesDomain MapToDomainModel()
        {
            ListAllergiesDomain domain = new ListAllergiesDomain
            {
                ID = ID,
                PatientID = PatientID,
                AllergyID = AllergyID,
                AllergyDescription = AllergyDescription,
                AllergySource = AllergySource,
                Reaction = Reaction,
                Comments = Comments,
                AddedBy = AddedBy,
                DateAdded = DateAdded,
                EditedBy = EditedBy,
                DateEdited = DateEdited,
                Inactive = Inactive,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Migrated = Migrated,
                LastConfirmedBy = LastConfirmedBy,
                LastConfirmedDate = LastConfirmedDate,
                Severity = Severity,
                Source = Source
            };

            return domain;
        }

        public ListAllergiesPoco() { }

        public ListAllergiesPoco(ListAllergiesDomain domain)
        {
            ID = domain.ID;
            PatientID = domain.PatientID;
            AllergyID = domain.AllergyID;
            AllergyDescription = domain.AllergyDescription;
            AllergySource = domain.AllergySource;
            Reaction = domain.Reaction;
            Comments = domain.Comments;
            AddedBy = domain.AddedBy;
            DateAdded = domain.DateAdded;
            EditedBy = domain.EditedBy;
            DateEdited = domain.DateEdited;
            Inactive = domain.Inactive;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Migrated = domain.Migrated;
            LastConfirmedBy = domain.LastConfirmedBy;
            LastConfirmedDate = domain.LastConfirmedDate;
            Severity = domain.Severity;
            Source = domain.Source;
        }
    }
}
