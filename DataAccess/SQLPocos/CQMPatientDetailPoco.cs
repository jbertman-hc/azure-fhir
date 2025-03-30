using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class CQMPatientDetailPoco
    {
        public int CQMPatientDetailId { get; set; }
        public int CQMPatientId { get; set; }
        public string ValueSetName { get; set; }
        public int? ValueSetKey { get; set; }
        public DateTime? ValueSetDate { get; set; }
        public string ValueSetPrefix { get; set; }
        public string Value { get; set; }
        public string UOM { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public CQMPatientDetailDomain MapToDomainModel()
        {
            CQMPatientDetailDomain domain = new CQMPatientDetailDomain
            {
                CQMPatientDetailId = CQMPatientDetailId,
                CQMPatientId = CQMPatientId,
                ValueSetName = ValueSetName,
                ValueSetKey = ValueSetKey,
                ValueSetDate = ValueSetDate,
                ValueSetPrefix = ValueSetPrefix,
                Value = Value,
                UOM = UOM,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
            };

            return domain;
        }

        public CQMPatientDetailPoco() { }

        public CQMPatientDetailPoco(CQMPatientDetailDomain domain)
        {
            CQMPatientDetailId = domain.CQMPatientDetailId;
            CQMPatientId = domain.CQMPatientId;
            ValueSetName = domain.ValueSetName;
            ValueSetKey = domain.ValueSetKey;
            ValueSetDate = domain.ValueSetDate;
            ValueSetPrefix = domain.ValueSetPrefix;
            Value = domain.Value;
            UOM = domain.UOM;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
