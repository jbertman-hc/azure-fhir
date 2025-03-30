using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LabTestsPoco
    {
        public int LabTestID { get; set; }
        public int PatientID { get; set; }
        public DateTime CreatedDt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdDt { get; set; }
        public int? LastUpdBy { get; set; }
        public DateTime? LabOrderSentDt { get; set; }
        public DateTime? LabResultMessageDt { get; set; }
        public DateTime? LabResultReceivedDt { get; set; }
        public string LabLocationCode { get; set; }
        public string LabCompany { get; set; }
        public string SpecimenNbr { get; set; }
        public string BillType { get; set; }
        public string SpecimenStatus { get; set; }
        public bool? Fasting { get; set; }
        public string LabTestStatus { get; set; }
        public int? SignOffID { get; set; }
        public DateTime? SignOffDt { get; set; }
        public string Comments { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string LabPatientFamilyName { get; set; }
        public string LabPatientGivenName { get; set; }
        public string LabPatientMiddleName { get; set; }
        public string LabPatientSuffix { get; set; }
        public string LabPatientRace { get; set; }
        public string LabPatientRaceAlternate { get; set; }
        public DateTime? LabPatientDOB { get; set; }
        public string LabPatientSex { get; set; }
        public string LabPatientIdNumber { get; set; }
        public string LabPatientAANamespaceId { get; set; }
        public string LabPatientIdTypeCode { get; set; }

        public LabTestsDomain MapToDomainModel()
        {
            LabTestsDomain domain = new LabTestsDomain
            {
                LabTestID = LabTestID,
                PatientID = PatientID,
                CreatedDt = CreatedDt,
                CreatedBy = CreatedBy,
                LastUpdDt = LastUpdDt,
                LastUpdBy = LastUpdBy,
                LabOrderSentDt = LabOrderSentDt,
                LabResultMessageDt = LabResultMessageDt,
                LabResultReceivedDt = LabResultReceivedDt,
                LabLocationCode = LabLocationCode,
                LabCompany = LabCompany,
                SpecimenNbr = SpecimenNbr,
                BillType = BillType,
                SpecimenStatus = SpecimenStatus,
                Fasting = Fasting,
                LabTestStatus = LabTestStatus,
                SignOffID = SignOffID,
                SignOffDt = SignOffDt,
                Comments = Comments,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                LabPatientFamilyName = LabPatientFamilyName,
                LabPatientGivenName = LabPatientGivenName,
                LabPatientMiddleName = LabPatientMiddleName,
                LabPatientSuffix = LabPatientSuffix,
                LabPatientRace = LabPatientRace,
                LabPatientRaceAlternate = LabPatientRaceAlternate,
                LabPatientDOB = LabPatientDOB,
                LabPatientSex = LabPatientSex,
                LabPatientIdNumber = LabPatientIdNumber,
                LabPatientAANamespaceId = LabPatientAANamespaceId,
                LabPatientIdTypeCode = LabPatientIdTypeCode
            };

            return domain;
        }

        public LabTestsPoco() { }

        public LabTestsPoco(LabTestsDomain domain)
        {
            LabTestID = domain.LabTestID;
            PatientID = domain.PatientID;
            CreatedDt = domain.CreatedDt;
            CreatedBy = domain.CreatedBy;
            LastUpdDt = domain.LastUpdDt;
            LastUpdBy = domain.LastUpdBy;
            LabOrderSentDt = domain.LabOrderSentDt;
            LabResultMessageDt = domain.LabResultMessageDt;
            LabResultReceivedDt = domain.LabResultReceivedDt;
            LabLocationCode = domain.LabLocationCode;
            LabCompany = domain.LabCompany;
            SpecimenNbr = domain.SpecimenNbr;
            BillType = domain.BillType;
            SpecimenStatus = domain.SpecimenStatus;
            Fasting = domain.Fasting;
            LabTestStatus = domain.LabTestStatus;
            SignOffID = domain.SignOffID;
            SignOffDt = domain.SignOffDt;
            Comments = domain.Comments;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            LabPatientFamilyName = domain.LabPatientFamilyName;
            LabPatientGivenName = domain.LabPatientGivenName;
            LabPatientMiddleName = domain.LabPatientMiddleName;
            LabPatientSuffix = domain.LabPatientSuffix;
            LabPatientRace = domain.LabPatientRace;
            LabPatientRaceAlternate = domain.LabPatientRaceAlternate;
            LabPatientDOB = domain.LabPatientDOB;
            LabPatientSex = domain.LabPatientSex;
            LabPatientIdNumber = domain.LabPatientIdNumber;
            LabPatientAANamespaceId = domain.LabPatientAANamespaceId;
            LabPatientIdTypeCode = domain.LabPatientIdTypeCode;
        }
    }
}
