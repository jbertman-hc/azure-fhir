using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LabResultsPoco
    {
        public int LabResultID { get; set; }
        public int LabTestID { get; set; }
        public string AccessionNbrAC { get; set; }
        public DateTime CreatedDt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdDt { get; set; }
        public int? LastUpdBy { get; set; }
        public int? OrderingProviderID { get; set; }
        public DateTime? ElectronicOrderCreationDt { get; set; }
        public string SpecimenNbr { get; set; }
        public string LabTestCode { get; set; }
        public double? SpecimenVolume { get; set; }
        public DateTime? SpecimenCollectedDt { get; set; }
        public string ActionCode { get; set; }
        public string ClinicalInfo { get; set; }
        public DateTime? SpecimenReceiptDt { get; set; }
        public string SpecimenSource { get; set; }
        public string AlternateID1 { get; set; }
        public string AlternateID2 { get; set; }
        public DateTime? ResultsSentDt { get; set; }
        public string FacilityPerformingTest { get; set; }
        public string LabTestStatus { get; set; }
        public string ParentForReflexOBX { get; set; }
        public string ParentForReflexOBR { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string OrderingProviderNotInAC { get; set; }
        public string SpecimenCondition { get; set; }
        public string SpecimenUniversalId { get; set; }
        public string SpecimenUniversalIdType { get; set; }
        public DateTime? SpecimenCollectedEndDate { get; set; }
        public DateTime? TimingStartDate { get; set; }
        public DateTime? TimingEndDate { get; set; }
        public string SpecimenType { get; set; }
        public int? CourtesyCopyToProviderId { get; set; }
        public string SpecimenRejectReason { get; set; }
        public string AccessionNbrNamespaceId { get; set; }
        public string SpecimenNbrNamespaceId { get; set; }
        public string PlacerGroupId { get; set; }
        public string PlacerGroupNamespaceId { get; set; }
        public short? ParentForReflexObxSubId { get; set; }
        public string LoincTestCode { get; set; }
        public string LabOrderingProvIDNumber { get; set; }
        public string LabOrderingProvNameTypeCode { get; set; }
        public string LabOrderingProvIDTypeCode { get; set; }
        public int? ResultSeqNo { get; set; }

        public LabResultsDomain MapToDomainModel()
        {
            LabResultsDomain domain = new LabResultsDomain
            {
                LabResultID = LabResultID,
                LabTestID = LabTestID,
                AccessionNbrAC = AccessionNbrAC,
                CreatedDt = CreatedDt,
                CreatedBy = CreatedBy,
                LastUpdDt = LastUpdDt,
                LastUpdBy = LastUpdBy,
                OrderingProviderID = OrderingProviderID,
                ElectronicOrderCreationDt = ElectronicOrderCreationDt,
                SpecimenNbr = SpecimenNbr,
                LabTestCode = LabTestCode,
                SpecimenVolume = SpecimenVolume,
                SpecimenCollectedDt = SpecimenCollectedDt,
                ActionCode = ActionCode,
                ClinicalInfo = ClinicalInfo,
                SpecimenReceiptDt = SpecimenReceiptDt,
                SpecimenSource = SpecimenSource,
                AlternateID1 = AlternateID1,
                AlternateID2 = AlternateID2,
                ResultsSentDt = ResultsSentDt,
                FacilityPerformingTest = FacilityPerformingTest,
                LabTestStatus = LabTestStatus,
                ParentForReflexOBX = ParentForReflexOBX,
                ParentForReflexOBR = ParentForReflexOBR,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                OrderingProviderNotInAC = OrderingProviderNotInAC,
                SpecimenCondition = SpecimenCondition,
                SpecimenUniversalId = SpecimenUniversalId,
                SpecimenUniversalIdType = SpecimenUniversalIdType,
                SpecimenCollectedEndDate = SpecimenCollectedEndDate,
                TimingStartDate = TimingStartDate,
                TimingEndDate = TimingEndDate,
                SpecimenType = SpecimenType,
                CourtesyCopyToProviderId = CourtesyCopyToProviderId,
                SpecimenRejectReason = SpecimenRejectReason,
                AccessionNbrNamespaceId = AccessionNbrNamespaceId,
                SpecimenNbrNamespaceId = SpecimenNbrNamespaceId,
                PlacerGroupId = PlacerGroupId,
                PlacerGroupNamespaceId = PlacerGroupNamespaceId,
                ParentForReflexObxSubId = ParentForReflexObxSubId,
                LoincTestCode = LoincTestCode,
                LabOrderingProvIDNumber = LabOrderingProvIDNumber,
                LabOrderingProvNameTypeCode = LabOrderingProvNameTypeCode,
                LabOrderingProvIDTypeCode = LabOrderingProvIDTypeCode,
                ResultSeqNo = ResultSeqNo
            };

            return domain;
        }

        public LabResultsPoco() { }

        public LabResultsPoco(LabResultsDomain domain)
        {
            LabResultID = domain.LabResultID;
            LabTestID = domain.LabTestID;
            AccessionNbrAC = domain.AccessionNbrAC;
            CreatedDt = domain.CreatedDt;
            CreatedBy = domain.CreatedBy;
            LastUpdDt = domain.LastUpdDt;
            LastUpdBy = domain.LastUpdBy;
            OrderingProviderID = domain.OrderingProviderID;
            ElectronicOrderCreationDt = domain.ElectronicOrderCreationDt;
            SpecimenNbr = domain.SpecimenNbr;
            LabTestCode = domain.LabTestCode;
            SpecimenVolume = domain.SpecimenVolume;
            SpecimenCollectedDt = domain.SpecimenCollectedDt;
            ActionCode = domain.ActionCode;
            ClinicalInfo = domain.ClinicalInfo;
            SpecimenReceiptDt = domain.SpecimenReceiptDt;
            SpecimenSource = domain.SpecimenSource;
            AlternateID1 = domain.AlternateID1;
            AlternateID2 = domain.AlternateID2;
            ResultsSentDt = domain.ResultsSentDt;
            FacilityPerformingTest = domain.FacilityPerformingTest;
            LabTestStatus = domain.LabTestStatus;
            ParentForReflexOBX = domain.ParentForReflexOBX;
            ParentForReflexOBR = domain.ParentForReflexOBR;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            OrderingProviderNotInAC = domain.OrderingProviderNotInAC;
            SpecimenCondition = domain.SpecimenCondition;
            SpecimenUniversalId = domain.SpecimenUniversalId;
            SpecimenUniversalIdType = domain.SpecimenUniversalIdType;
            SpecimenCollectedEndDate = domain.SpecimenCollectedEndDate;
            TimingStartDate = domain.TimingStartDate;
            TimingEndDate = domain.TimingEndDate;
            SpecimenType = domain.SpecimenType;
            CourtesyCopyToProviderId = domain.CourtesyCopyToProviderId;
            SpecimenRejectReason = domain.SpecimenRejectReason;
            AccessionNbrNamespaceId = domain.AccessionNbrNamespaceId;
            SpecimenNbrNamespaceId = domain.SpecimenNbrNamespaceId;
            PlacerGroupId = domain.PlacerGroupId;
            PlacerGroupNamespaceId = domain.PlacerGroupNamespaceId;
            ParentForReflexObxSubId = domain.ParentForReflexObxSubId;
            LoincTestCode = domain.LoincTestCode;
            LabOrderingProvIDNumber = domain.LabOrderingProvIDNumber;
            LabOrderingProvNameTypeCode = domain.LabOrderingProvNameTypeCode;
            LabOrderingProvIDTypeCode = domain.LabOrderingProvIDTypeCode;
            ResultSeqNo = domain.ResultSeqNo;
        }
    }
}

