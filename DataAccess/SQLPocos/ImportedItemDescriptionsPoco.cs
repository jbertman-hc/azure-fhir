using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ImportedItemDescriptionsPoco
    {
        public int Id { get; set; }
        public int? InitId { get; set; }
        public string Description { get; set; }
        public bool IsReadOnly { get; set; }
        public bool ShowOnAdd { get; set; }
        public bool ShowOnEdit { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ImportedItemDescriptionsDomain MapToDomainModel()
        {
            ImportedItemDescriptionsDomain domain = new ImportedItemDescriptionsDomain
            {
                Id = Id,
                InitId = InitId,
                Description = Description,
                IsReadOnly = IsReadOnly,
                ShowOnAdd = ShowOnAdd,
                ShowOnEdit = ShowOnEdit,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ImportedItemDescriptionsPoco() { }

        public ImportedItemDescriptionsPoco(ImportedItemDescriptionsDomain domain)
        {
            Id = domain.Id;
            InitId = domain.InitId;
            Description = domain.Description;
            IsReadOnly = domain.IsReadOnly;
            ShowOnAdd = domain.ShowOnAdd;
            ShowOnEdit = domain.ShowOnEdit;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}