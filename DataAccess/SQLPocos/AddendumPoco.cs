using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class AddendumPoco
    {
        public int id { get; set; }
        public int PatID { get; set; }
        public string PatientName { get; set; }
        public DateTime Date { get; set; }
        public string NoteType { get; set; }
        public string NoteSubject { get; set; }
        public string NoteBody { get; set; }
        public string SavedBy { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public AddendumDomain MapToDomainModel()
        {
            AddendumDomain domain = new AddendumDomain
            {
                id = id,
                PatID = PatID,
                PatientName = PatientName,
                Date = Date,
                NoteType = NoteType,
                NoteSubject = NoteSubject,
                NoteBody = NoteBody,
                SavedBy = SavedBy,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public AddendumPoco() { }

        public AddendumPoco(AddendumDomain domain)
        {
            id = domain.id;
            PatID = domain.PatID;
            PatientName = domain.PatientName;
            Date = domain.Date;
            NoteType = domain.NoteType;
            NoteSubject = domain.NoteSubject;
            NoteBody = domain.NoteBody;
            SavedBy = domain.SavedBy;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}

