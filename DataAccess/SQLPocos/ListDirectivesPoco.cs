using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListDirectivesPoco
    {
        public int id { get; set; }
        public int PatientID { get; set; }
        public int ProviderID { get; set; }
        public string SavedBy { get; set; }
        public DateTime DateSaved { get; set; }
        public string DirectiveName { get; set; }
        public string DirectiveText { get; set; }
        public DateTime? DateActive { get; set; }
        public DateTime? DateInactive { get; set; }
        public string DirectiveCode { get; set; }
        public string DirectiveLevel { get; set; }
        public bool IsActive { get; set; }
        public bool IsValidDirective { get; set; }
        public string Comments { get; set; }
        public string PathToDirective { get; set; }
        public string History { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListDirectivesDomain MapToDomainModel()
        {
            ListDirectivesDomain domain = new ListDirectivesDomain
            {
                id = id,
                PatientID = PatientID,
                ProviderID = ProviderID,
                SavedBy = SavedBy,
                DateSaved = DateSaved,
                DirectiveName = DirectiveName,
                DirectiveText = DirectiveText,
                DateActive = DateActive,
                DateInactive = DateInactive,
                DirectiveCode = DirectiveCode,
                DirectiveLevel = DirectiveLevel,
                IsActive = IsActive,
                IsValidDirective = IsValidDirective,
                Comments = Comments,
                PathToDirective = PathToDirective,
                History = History,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListDirectivesPoco() { }

        public ListDirectivesPoco(ListDirectivesDomain domain)
        {
            id = domain.id;
            PatientID = domain.PatientID;
            ProviderID = domain.ProviderID;
            SavedBy = domain.SavedBy;
            DateSaved = domain.DateSaved;
            DirectiveName = domain.DirectiveName;
            DirectiveText = domain.DirectiveText;
            DateActive = domain.DateActive;
            DateInactive = domain.DateInactive;
            DirectiveCode = domain.DirectiveCode;
            DirectiveLevel = domain.DirectiveLevel;
            IsActive = domain.IsActive;
            IsValidDirective = domain.IsValidDirective;
            Comments = domain.Comments;
            PathToDirective = domain.PathToDirective;
            History = domain.History;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
