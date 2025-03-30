using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class EDIExceptionPoco
    {
        public int EDIExceptionID { get; set; }
        public string InterfaceName { get; set; }
        public string InterfaceType { get; set; }
        public string InterfaceDirection { get; set; }
        public string PatientChartID { get; set; }
        public string PatientFirst { get; set; }
        public string PatientMiddle { get; set; }
        public string PatientLast { get; set; }
        public DateTime? PatientDOB { get; set; }
        public string PatientGender { get; set; }
        public bool Resolved { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public string ResolvedBy { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public bool Processed { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public string ProcessedBy { get; set; }
        public int? PatientIDMatch { get; set; }
        public string EDITxn { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public EDIExceptionDomain MapToDomainModel()
        {
            EDIExceptionDomain domain = new EDIExceptionDomain
            {
                EDIExceptionID = EDIExceptionID,
                InterfaceName = InterfaceName,
                InterfaceType = InterfaceType,
                InterfaceDirection = InterfaceDirection,
                PatientChartID = PatientChartID,
                PatientFirst = PatientFirst,
                PatientMiddle = PatientMiddle,
                PatientLast = PatientLast,
                PatientDOB = PatientDOB,
                PatientGender = PatientGender,
                Resolved = Resolved,
                ResolvedDate = ResolvedDate,
                ResolvedBy = ResolvedBy,
                Deleted = Deleted,
                DeletedDate = DeletedDate,
                DeletedBy = DeletedBy,
                Processed = Processed,
                ProcessedDate = ProcessedDate,
                ProcessedBy = ProcessedBy,
                PatientIDMatch = PatientIDMatch,
                EDITxn = EDITxn,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public EDIExceptionPoco() { }

        public EDIExceptionPoco(EDIExceptionDomain domain)
        {
            EDIExceptionID = domain.EDIExceptionID;
            InterfaceName = domain.InterfaceName;
            InterfaceType = domain.InterfaceType;
            InterfaceDirection = domain.InterfaceDirection;
            PatientChartID = domain.PatientChartID;
            PatientFirst = domain.PatientFirst;
            PatientMiddle = domain.PatientMiddle;
            PatientLast = domain.PatientLast;
            PatientDOB = domain.PatientDOB;
            PatientGender = domain.PatientGender;
            Resolved = domain.Resolved;
            ResolvedDate = domain.ResolvedDate;
            ResolvedBy = domain.ResolvedBy;
            Deleted = domain.Deleted;
            DeletedDate = domain.DeletedDate;
            DeletedBy = domain.DeletedBy;
            Processed = domain.Processed;
            ProcessedDate = domain.ProcessedDate;
            ProcessedBy = domain.ProcessedBy;
            PatientIDMatch = domain.PatientIDMatch;
            EDITxn = domain.EDITxn;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
