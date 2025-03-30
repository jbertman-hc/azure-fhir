using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListMEDSPoco
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string PrescribingProvider { get; set; }
        public string MedName { get; set; }
        public string MedSig { get; set; }
        public string MedNo { get; set; }
        public string MedRefill { get; set; }
        public string MedDNS { get; set; }
        public DateTime? DateInitiated { get; set; }
        public DateTime? DateLastRefilled { get; set; }
        public string MedComments { get; set; }
        public int SciptID { get; set; }
        public string PriorRefills { get; set; }
        public bool Refillable { get; set; }
        public bool Inactive { get; set; }
        public int? DrugID { get; set; }
        public string MedSource { get; set; }
        public string ExternalID { get; set; }
        public string PharmacyID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
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
        public string Source { get; set; }
        public bool AdministeredDuringVisit { get; set; }
        public int? Course { get; set; }

        public ListMEDSDomain MapToDomainModel()
        {
            ListMEDSDomain domain = new ListMEDSDomain
            {
                PatientID = PatientID,
                PatientName = PatientName,
                PrescribingProvider = PrescribingProvider,
                MedName = MedName,
                MedSig = MedSig,
                MedNo = MedNo,
                MedRefill = MedRefill,
                MedDNS = MedDNS,
                DateInitiated = DateInitiated,
                DateLastRefilled = DateLastRefilled,
                MedComments = MedComments,
                SciptID = SciptID,
                PriorRefills = PriorRefills,
                Refillable = Refillable,
                Inactive = Inactive,
                DrugID = DrugID,
                MedSource = MedSource,
                ExternalID = ExternalID,
                PharmacyID = PharmacyID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
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
                Source = Source,
                AdministeredDuringVisit = AdministeredDuringVisit,
                Course = Course
            };

            return domain;
        }

        public ListMEDSPoco() { }

        public ListMEDSPoco(ListMEDSDomain domain)
        {
            PatientID = domain.PatientID;
            PatientName = domain.PatientName;
            PrescribingProvider = domain.PrescribingProvider;
            MedName = domain.MedName;
            MedSig = domain.MedSig;
            MedNo = domain.MedNo;
            MedRefill = domain.MedRefill;
            MedDNS = domain.MedDNS;
            DateInitiated = domain.DateInitiated;
            DateLastRefilled = domain.DateLastRefilled;
            MedComments = domain.MedComments;
            SciptID = domain.SciptID;
            PriorRefills = domain.PriorRefills;
            Refillable = domain.Refillable;
            Inactive = domain.Inactive;
            DrugID = domain.DrugID;
            MedSource = domain.MedSource;
            ExternalID = domain.ExternalID;
            PharmacyID = domain.PharmacyID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
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
            Source = domain.Source;
            AdministeredDuringVisit = domain.AdministeredDuringVisit;
            Course = domain.Course;
        }
    }
}
