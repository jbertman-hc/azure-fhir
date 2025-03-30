using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class TemplatesPoco
    {
        public int ID { get; set; }
        public string ProviderName { get; set; }
        public string TemplateName { get; set; }
        public string TemplateText { get; set; }
        public string TemplateLocation { get; set; }
        public bool Default { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public int? TemplateWeighting { get; set; }

        public TemplatesDomain MapToDomainModel()
        {
            TemplatesDomain domain = new TemplatesDomain
            {
                ID = ID,
                ProviderName = ProviderName,
                TemplateName = TemplateName,
                TemplateText = TemplateText,
                TemplateLocation = TemplateLocation,
                Default = Default,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                TemplateWeighting = TemplateWeighting
            };

            return domain;
        }

        public TemplatesPoco() { }

        public TemplatesPoco(TemplatesDomain domain)
        {
            ID = domain.ID;
            ProviderName = domain.ProviderName;
            TemplateName = domain.TemplateName;
            TemplateText = domain.TemplateText;
            TemplateLocation = domain.TemplateLocation;
            Default = domain.Default;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            TemplateWeighting = domain.TemplateWeighting;
        }
    }
}
