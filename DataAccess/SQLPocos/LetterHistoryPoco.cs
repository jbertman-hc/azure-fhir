using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LetterHistoryPoco
    {
        public int LetterHistoryId { get; set; }
        public string Name { get; set; }
        public int? PatientId { get; set; }
        public byte[] Document { get; set; }
        public DateTime DateSaved { get; set; }
        public string SavedBy { get; set; }
        public int? RecipientId { get; set; }
        public int? LetterType { get; set; }
        public int? EncounterId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public LetterHistoryDomain MapToDomainModel()
        {
            LetterHistoryDomain domain = new LetterHistoryDomain
            {
                LetterHistoryId = LetterHistoryId,
                Name = Name,
                PatientId = PatientId,
                Document = Document,
                DateSaved = DateSaved,
                SavedBy = SavedBy,
                RecipientId = RecipientId,
                LetterType = LetterType,
                EncounterId = EncounterId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public LetterHistoryPoco() { }

        public LetterHistoryPoco(LetterHistoryDomain domain)
        {
            LetterHistoryId = domain.LetterHistoryId;
            Name = domain.Name;
            PatientId = domain.PatientId;
            Document = domain.Document;
            DateSaved = domain.DateSaved;
            SavedBy = domain.SavedBy;
            RecipientId = domain.RecipientId;
            LetterType = domain.LetterType;
            EncounterId = domain.EncounterId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
