using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ImportedItemsPoco
    {
        public int id { get; set; }
        public int? PatientID { get; set; }
        public string ImportedBy { get; set; }
        public DateTime? DateOfItem { get; set; }
        public DateTime? DateImported { get; set; }
        public string TypeOfItem { get; set; }
        public string ItemRE { get; set; }
        public string ItemFrom { get; set; }
        public string ItemComments { get; set; }
        public string ItemOriginalPath { get; set; }
        public string ItemCurrentPath { get; set; }
        public int? ToBeSignedByID { get; set; }
        public int? SignOffID { get; set; }
        public DateTime? SignOffDt { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool IsLetter { get; set; }
        public bool IsLabEmbeddedRpt { get; set; }

        public ImportedItemsDomain MapToDomainModel()
        {
            ImportedItemsDomain domain = new ImportedItemsDomain
            {
                id = id,
                PatientID = PatientID,
                ImportedBy = ImportedBy,
                DateOfItem = DateOfItem,
                DateImported = DateImported,
                TypeOfItem = TypeOfItem,
                ItemRE = ItemRE,
                ItemFrom = ItemFrom,
                ItemComments = ItemComments,
                ItemOriginalPath = ItemOriginalPath,
                ItemCurrentPath = ItemCurrentPath,
                ToBeSignedByID = ToBeSignedByID,
                SignOffID = SignOffID,
                SignOffDt = SignOffDt,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                IsLetter = IsLetter,
                IsLabEmbeddedRpt = IsLabEmbeddedRpt
            };

            return domain;
        }

        public ImportedItemsPoco() { }

        public ImportedItemsPoco(ImportedItemsDomain domain)
        {
            id = domain.id;
            PatientID = domain.PatientID;
            ImportedBy = domain.ImportedBy;
            DateOfItem = domain.DateOfItem;
            DateImported = domain.DateImported;
            TypeOfItem = domain.TypeOfItem;
            ItemRE = domain.ItemRE;
            ItemFrom = domain.ItemFrom;
            ItemComments = domain.ItemComments;
            ItemOriginalPath = domain.ItemOriginalPath;
            ItemCurrentPath = domain.ItemCurrentPath;
            ToBeSignedByID = domain.ToBeSignedByID;
            SignOffID = domain.SignOffID;
            SignOffDt = domain.SignOffDt;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            IsLetter = domain.IsLetter;
            IsLabEmbeddedRpt = domain.IsLabEmbeddedRpt;
        }
    }
}