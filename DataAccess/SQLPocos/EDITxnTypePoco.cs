using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class EDITxnTypePoco
    {
        public string EDITxnTypeCode { get; set; }
        public string EDITxnTypeDesc { get; set; }
        public DateTime CreatedDt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdDt { get; set; }
        public int? LastUpdBy { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public EDITxnTypeDomain MapToDomainModel()
        {
            EDITxnTypeDomain domain = new EDITxnTypeDomain
            {
                EDITxnTypeCode = EDITxnTypeCode,
                EDITxnTypeDesc = EDITxnTypeDesc,
                CreatedDt = CreatedDt,
                CreatedBy = CreatedBy,
                LastUpdDt = LastUpdDt,
                LastUpdBy = LastUpdBy,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public EDITxnTypePoco() { }

        public EDITxnTypePoco(EDITxnTypeDomain domain)
        {
            EDITxnTypeCode = domain.EDITxnTypeCode;
            EDITxnTypeDesc = domain.EDITxnTypeDesc;
            CreatedDt = domain.CreatedDt;
            CreatedBy = domain.CreatedBy;
            LastUpdDt = domain.LastUpdDt;
            LastUpdBy = domain.LastUpdBy;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
