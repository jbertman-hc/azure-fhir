using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListCustomFieldsPoco
    {
        public int ID { get; set; }
        public int? PatientID { get; set; }
        public int? CustomFieldID { get; set; }
        public string Value { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListCustomFieldsDomain MapToDomainModel()
        {
            ListCustomFieldsDomain domain = new ListCustomFieldsDomain
            {
                ID = ID,
                PatientID = PatientID,
                CustomFieldID = CustomFieldID,
                Value = Value,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListCustomFieldsPoco() { }

        public ListCustomFieldsPoco(ListCustomFieldsDomain domain)
        {
            ID = domain.ID;
            PatientID = domain.PatientID;
            CustomFieldID = domain.CustomFieldID;
            Value = domain.Value;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
