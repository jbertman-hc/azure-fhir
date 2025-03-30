using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LetterWriterTemplateDefaultsPoco
    {
        public int LetterWriterTemplateDefaultId { get; set; }
        public string ProviderCode { get; set; }
        public int TemplateType { get; set; }
        public int LetterWriterTemplateId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool PracticeWide { get; set; }

        public LetterWriterTemplateDefaultsDomain MapToDomainModel()
        {
            LetterWriterTemplateDefaultsDomain domain = new LetterWriterTemplateDefaultsDomain
            {
                LetterWriterTemplateDefaultId = LetterWriterTemplateDefaultId,
                ProviderCode = ProviderCode,
                TemplateType = TemplateType,
                LetterWriterTemplateId = LetterWriterTemplateId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                PracticeWide = PracticeWide
            };

            return domain;
        }

        public LetterWriterTemplateDefaultsPoco() { }

        public LetterWriterTemplateDefaultsPoco(LetterWriterTemplateDefaultsDomain domain)
        {
            LetterWriterTemplateDefaultId = domain.LetterWriterTemplateDefaultId;
            ProviderCode = domain.ProviderCode;
            TemplateType = domain.TemplateType;
            LetterWriterTemplateId = domain.LetterWriterTemplateId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            PracticeWide = domain.PracticeWide;
        }
    }
}
