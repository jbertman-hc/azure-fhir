using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PracticeDocumentsPoco
    {
        public int ID { get; set; }
        public string FileAlias { get; set; }
        public string FileName { get; set; }
        public DateTime? DateImported { get; set; }
        public int? ParentID { get; set; }
        public int? Type { get; set; }
        public bool Permanent { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string Description { get; set; }
        public bool ShowAtCheckIn { get; set; }

        public PracticeDocumentsDomain MapToDomainModel()
        {
            PracticeDocumentsDomain domain = new PracticeDocumentsDomain
            {
                ID = ID,
                FileAlias = FileAlias,
                FileName = FileName,
                DateImported = DateImported,
                ParentID = ParentID,
                Type = Type,
                Permanent = Permanent,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Description = Description,
                ShowAtCheckIn = ShowAtCheckIn
            };

            return domain;
        }

        public PracticeDocumentsPoco() { }

        public PracticeDocumentsPoco(PracticeDocumentsDomain domain)
        {
            ID = domain.ID;
            FileAlias = domain.FileAlias;
            FileName = domain.FileName;
            DateImported = domain.DateImported;
            ParentID = domain.ParentID;
            Type = domain.Type;
            Permanent = domain.Permanent;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Description = domain.Description;
            ShowAtCheckIn = domain.ShowAtCheckIn;
        }
    }
}
