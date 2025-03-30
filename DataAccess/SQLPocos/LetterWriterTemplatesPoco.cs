using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LetterWriterTemplatesPoco
    {
        public int LetterWriterTemplateId { get; set; }
        public string Name { get; set; }
        public byte[] Document { get; set; }
        public int TypeOfTemplate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Owner { get; set; }
        public bool IsPracticeWide { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public LetterWriterTemplatesDomain MapToDomainModel()
        {
            LetterWriterTemplatesDomain domain = new LetterWriterTemplatesDomain
            {
                LetterWriterTemplateId = LetterWriterTemplateId,
                Name = Name,
                Document = Document,
                TypeOfTemplate = TypeOfTemplate,
                CreatedBy = CreatedBy,
                CreatedDate = CreatedDate,
                LastModifiedBy = LastModifiedBy,
                LastModifiedDate = LastModifiedDate,
                Owner = Owner,
                IsPracticeWide = IsPracticeWide,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public LetterWriterTemplatesPoco() { }

        public LetterWriterTemplatesPoco(LetterWriterTemplatesDomain domain)
        {
            LetterWriterTemplateId = domain.LetterWriterTemplateId;
            Name = domain.Name;
            Document = domain.Document;
            TypeOfTemplate = domain.TypeOfTemplate;
            CreatedBy = domain.CreatedBy;
            CreatedDate = domain.CreatedDate;
            LastModifiedBy = domain.LastModifiedBy;
            LastModifiedDate = domain.LastModifiedDate;
            Owner = domain.Owner;
            IsPracticeWide = domain.IsPracticeWide;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
