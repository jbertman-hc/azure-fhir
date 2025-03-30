using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class HIEfieldsPoco
    {
        public int RowID { get; set; }
        public int HieID { get; set; }
        public string FieldName { get; set; }
        public int? FieldIndex { get; set; }
        public string FieldValue { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string ProviderCode { get; set; }

        public HIEfieldsDomain MapToDomainModel()
        {
            HIEfieldsDomain domain = new HIEfieldsDomain
            {
                RowID = RowID,
                HieID = HieID,
                FieldName = FieldName,
                FieldIndex = FieldIndex,
                FieldValue = FieldValue,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                ProviderCode = ProviderCode
            };

            return domain;
        }

        public HIEfieldsPoco() { }

        public HIEfieldsPoco(HIEfieldsDomain domain)
        {
            RowID = domain.RowID;
            HieID = domain.HieID;
            FieldName = domain.FieldName;
            FieldIndex = domain.FieldIndex;
            FieldValue = domain.FieldValue;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            ProviderCode = domain.ProviderCode;
        }
    }
}
