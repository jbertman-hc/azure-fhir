using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LabResultDetailsPoco
    {
        public int LabResultDetailID { get; set; }
        public int LabResultID { get; set; }
        public DateTime CreatedDt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdDt { get; set; }
        public int? LastUpdBy { get; set; }
        public bool InactiveFlag { get; set; }
        public int? CorrectsLabTestID { get; set; }
        public int? CorrectedByLabTestID { get; set; }
        public string LabTestStatus { get; set; }
        public string LabTestCode { get; set; }
        public string LoincTestCode { get; set; }
        public short? ObservationSubID { get; set; }
        public string ObservationValue { get; set; }
        public string UOM { get; set; }
        public string ReferenceRanges { get; set; }
        public string AbnormalFlag { get; set; }
        public string NormalAbnormalType { get; set; }
        public DateTime? ReferenceRangeChangeDt { get; set; }
        public string SecurityAccessChecks { get; set; }
        public DateTime? ObservationSentDt { get; set; }
        public string LabLocationCode { get; set; }
        public string LabCompany { get; set; }
        public string ValueType { get; set; }
        public string RptFileName { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string ResponsibleObserver { get; set; }
        public int? DetailSeqNo { get; set; }
        public bool IsLabReport { get; set; }

        public LabResultDetailsDomain MapToDomainModel()
        {
            LabResultDetailsDomain domain = new LabResultDetailsDomain
            {
                LabResultDetailID = LabResultDetailID,
                LabResultID = LabResultID,
                CreatedDt = CreatedDt,
                CreatedBy = CreatedBy,
                LastUpdDt = LastUpdDt,
                LastUpdBy = LastUpdBy,
                InactiveFlag = InactiveFlag,
                CorrectsLabTestID = CorrectsLabTestID,
                CorrectedByLabTestID = CorrectedByLabTestID,
                LabTestStatus = LabTestStatus,
                LabTestCode = LabTestCode,
                LoincTestCode = LoincTestCode,
                ObservationSubID = ObservationSubID,
                ObservationValue = ObservationValue,
                UOM = UOM,
                ReferenceRanges = ReferenceRanges,
                AbnormalFlag = AbnormalFlag,
                NormalAbnormalType = NormalAbnormalType,
                ReferenceRangeChangeDt = ReferenceRangeChangeDt,
                SecurityAccessChecks = SecurityAccessChecks,
                ObservationSentDt = ObservationSentDt,
                LabLocationCode = LabLocationCode,
                LabCompany = LabCompany,
                ValueType = ValueType,
                RptFileName = RptFileName,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                ResponsibleObserver = ResponsibleObserver,
                DetailSeqNo = DetailSeqNo,
                IsLabReport = IsLabReport
            };

            return domain;
        }

        public LabResultDetailsPoco() { }

        public LabResultDetailsPoco(LabResultDetailsDomain domain)
        {
            LabResultDetailID = domain.LabResultDetailID;
            LabResultID = domain.LabResultID;
            CreatedDt = domain.CreatedDt;
            CreatedBy = domain.CreatedBy;
            LastUpdDt = domain.LastUpdDt;
            LastUpdBy = domain.LastUpdBy;
            InactiveFlag = domain.InactiveFlag;
            CorrectsLabTestID = domain.CorrectsLabTestID;
            CorrectedByLabTestID = domain.CorrectedByLabTestID;
            LabTestStatus = domain.LabTestStatus;
            LabTestCode = domain.LabTestCode;
            LoincTestCode = domain.LoincTestCode;
            ObservationSubID = domain.ObservationSubID;
            ObservationValue = domain.ObservationValue;
            UOM = domain.UOM;
            ReferenceRanges = domain.ReferenceRanges;
            AbnormalFlag = domain.AbnormalFlag;
            NormalAbnormalType = domain.NormalAbnormalType;
            ReferenceRangeChangeDt = domain.ReferenceRangeChangeDt;
            SecurityAccessChecks = domain.SecurityAccessChecks;
            ObservationSentDt = domain.ObservationSentDt;
            LabLocationCode = domain.LabLocationCode;
            LabCompany = domain.LabCompany;
            ValueType = domain.ValueType;
            RptFileName = domain.RptFileName;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            ResponsibleObserver = domain.ResponsibleObserver;
            DetailSeqNo = domain.DetailSeqNo;
            IsLabReport = domain.IsLabReport;
        }
    }
}
