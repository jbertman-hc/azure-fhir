using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListMedsPendingPoco
    {
        public int ScriptID { get; set; }
        public int PatientID { get; set; }
        public string PrescribingProvider { get; set; }
        public string MedName { get; set; }
        public string MedSig { get; set; }
        public string MedNo { get; set; }
        public string MedRefill { get; set; }
        public string MedDNS { get; set; }
        public DateTime? DateInitiated { get; set; }
        public DateTime? DateLastRefilled { get; set; }
        public string MedComments { get; set; }
        public string PriorRefills { get; set; }
        public bool Refillable { get; set; }
        public bool Inactive { get; set; }
        public int? DrugID { get; set; }
        public string MedSource { get; set; }
        public string ExternalID { get; set; }
        public string PharmacyID { get; set; }
        public string PharmacyTransactionID { get; set; }
        public string QuickAddWhoPrescribed { get; set; }
        public string QuickAddReasonPrescribed { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateInactivated { get; set; }
        public Guid RxGUID { get; set; }
        public DateTime? DateStarted { get; set; }
        public string DispenseQualifier { get; set; }
        public string ERXstatus { get; set; }
        public bool? DAW { get; set; }
        public bool? IsFormularyChecked { get; set; }
        public bool? SentBySureScripts { get; set; }
        public bool? PharmacyTransmitFailed { get; set; }
        public string InactivateReason { get; set; }
        public bool? ScriptPrinted { get; set; }
        public bool? ScriptFaxed { get; set; }
        public int? PendingFlag { get; set; }
        public DateTime? ImportedDate { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string Source { get; set; }

        public ListMedsPendingDomain MapToDomainModel()
        {
            ListMedsPendingDomain domain = new ListMedsPendingDomain
            {
                ScriptID = ScriptID,
                PatientID = PatientID,
                PrescribingProvider = PrescribingProvider,
                MedName = MedName,
                MedSig = MedSig,
                MedNo = MedNo,
                MedRefill = MedRefill,
                MedDNS = MedDNS,
                DateInitiated = DateInitiated,
                DateLastRefilled = DateLastRefilled,
                MedComments = MedComments,
                PriorRefills = PriorRefills,
                Refillable = Refillable,
                Inactive = Inactive,
                DrugID = DrugID,
                MedSource = MedSource,
                ExternalID = ExternalID,
                PharmacyID = PharmacyID,
                PharmacyTransactionID = PharmacyTransactionID,
                QuickAddWhoPrescribed = QuickAddWhoPrescribed,
                QuickAddReasonPrescribed = QuickAddReasonPrescribed,
                Deleted = Deleted,
                DateInactivated = DateInactivated,
                RxGUID = RxGUID,
                DateStarted = DateStarted,
                DispenseQualifier = DispenseQualifier,
                ERXstatus = ERXstatus,
                DAW = DAW,
                IsFormularyChecked = IsFormularyChecked,
                SentBySureScripts = SentBySureScripts,
                PharmacyTransmitFailed = PharmacyTransmitFailed,
                InactivateReason = InactivateReason,
                ScriptPrinted = ScriptPrinted,
                ScriptFaxed = ScriptFaxed,
                PendingFlag = PendingFlag,
                ImportedDate = ImportedDate,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Source = Source
            };

            return domain;
        }

        public ListMedsPendingPoco() { }

        public ListMedsPendingPoco(ListMedsPendingDomain domain)
        {
            ScriptID = domain.ScriptID;
            PatientID = domain.PatientID;
            PrescribingProvider = domain.PrescribingProvider;
            MedName = domain.MedName;
            MedSig = domain.MedSig;
            MedNo = domain.MedNo;
            MedRefill = domain.MedRefill;
            MedDNS = domain.MedDNS;
            DateInitiated = domain.DateInitiated;
            DateLastRefilled = domain.DateLastRefilled;
            MedComments = domain.MedComments;
            PriorRefills = domain.PriorRefills;
            Refillable = domain.Refillable;
            Inactive = domain.Inactive;
            DrugID = domain.DrugID;
            MedSource = domain.MedSource;
            ExternalID = domain.ExternalID;
            PharmacyID = domain.PharmacyID;
            PharmacyTransactionID = domain.PharmacyTransactionID;
            QuickAddWhoPrescribed = domain.QuickAddWhoPrescribed;
            QuickAddReasonPrescribed = domain.QuickAddReasonPrescribed;
            Deleted = domain.Deleted;
            DateInactivated = domain.DateInactivated;
            RxGUID = domain.RxGUID;
            DateStarted = domain.DateStarted;
            DispenseQualifier = domain.DispenseQualifier;
            ERXstatus = domain.ERXstatus;
            DAW = domain.DAW;
            IsFormularyChecked = domain.IsFormularyChecked;
            SentBySureScripts = domain.SentBySureScripts;
            PharmacyTransmitFailed = domain.PharmacyTransmitFailed;
            InactivateReason = domain.InactivateReason;
            ScriptPrinted = domain.ScriptPrinted;
            ScriptFaxed = domain.ScriptFaxed;
            PendingFlag = domain.PendingFlag;
            ImportedDate = domain.ImportedDate;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Source = domain.Source;
        }
    }
}
