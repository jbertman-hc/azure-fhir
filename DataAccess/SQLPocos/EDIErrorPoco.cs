using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class EDIErrorPoco
    {
        public int EDIErrorID { get; set; }
        public DateTime CreatedDt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdDt { get; set; }
        public int? LastUpdBy { get; set; }
        public string EDITxnTypeCode { get; set; }
        public string LabCompany { get; set; }
        public int? PatientID { get; set; }
        public int? LabTestID { get; set; }
        public DateTime? EDIMsgDt { get; set; }
        public string ErrorMsg { get; set; }
        public string EDITxn { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool Reviewed { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public int? ReviewedBy { get; set; }
        public string ReviewedComment { get; set; }

        public EDIErrorDomain MapToDomainModel()
        {
            EDIErrorDomain domain = new EDIErrorDomain
            {
                EDIErrorID = EDIErrorID,
                CreatedDt = CreatedDt,
                CreatedBy = CreatedBy,
                LastUpdDt = LastUpdDt,
                LastUpdBy = LastUpdBy,
                EDITxnTypeCode = EDITxnTypeCode,
                LabCompany = LabCompany,
                PatientID = PatientID,
                LabTestID = LabTestID,
                EDIMsgDt = EDIMsgDt,
                ErrorMsg = ErrorMsg,
                EDITxn = EDITxn,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Reviewed = Reviewed,
                ReviewedDate = ReviewedDate,
                ReviewedBy = ReviewedBy,
                ReviewedComment = ReviewedComment
            };

            return domain;
        }

        public EDIErrorPoco() { }

        public EDIErrorPoco(EDIErrorDomain domain)
        {
            EDIErrorID = domain.EDIErrorID;
            CreatedDt = domain.CreatedDt;
            CreatedBy = domain.CreatedBy;
            LastUpdDt = domain.LastUpdDt;
            LastUpdBy = domain.LastUpdBy;
            EDITxnTypeCode = domain.EDITxnTypeCode;
            LabCompany = domain.LabCompany;
            PatientID = domain.PatientID;
            LabTestID = domain.LabTestID;
            EDIMsgDt = domain.EDIMsgDt;
            ErrorMsg = domain.ErrorMsg;
            EDITxn = domain.EDITxn;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Reviewed = domain.Reviewed;
            ReviewedDate = domain.ReviewedDate;
            ReviewedBy = domain.ReviewedBy;
            ReviewedComment = domain.ReviewedComment;
        }
    }
}
