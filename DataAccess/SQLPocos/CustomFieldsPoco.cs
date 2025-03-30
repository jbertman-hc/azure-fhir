using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class CustomFieldsPoco
    {
        public int ID { get; set; }
        public string FieldName { get; set; }
        public string DemoFieldName { get; set; }
        public string Type { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public CustomFieldsDomain MapToDomainModel()
        {
            CustomFieldsDomain domain = new CustomFieldsDomain
            {
                ID = ID,
                FieldName = FieldName,
                DemoFieldName = DemoFieldName,
                Type = Type,
                DateModified = DateModified,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public CustomFieldsPoco() { }

        public CustomFieldsPoco(CustomFieldsDomain domain)
        {
            ID = domain.ID;
            FieldName = domain.FieldName;
            DemoFieldName = domain.DemoFieldName;
            Type = domain.Type;
            DateModified = domain.DateModified;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
