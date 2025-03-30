using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class InterfacesPoco
    {
        public int UniqueTableID { get; set; }
        public int InterfaceID { get; set; }
        public string InterfaceName { get; set; }
        public DateTime DateOn { get; set; }
        public DateTime? DateOff { get; set; }
        public string PracticeCode { get; set; }
        public string ActivationCode { get; set; }
        public bool IsInterfaceActive { get; set; }
        public string Comments { get; set; }
        public string PathToSend { get; set; }
        public string PathToReceive { get; set; }
        public bool OnlineOverride { get; set; }
        public DateTime OnlineOverrideDate { get; set; }
        public DateTime LastOnlineCheckDate { get; set; }
        public string InterfaceType { get; set; }
        public string FileFormat { get; set; }
        public string PatientInfoDir { get; set; }
        public bool? PatientInfoAuto { get; set; }
        public string BillingInfoDir { get; set; }
        public bool? BillingInfoAuto { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? IncludeAllDXinFT { get; set; }
        public bool? HidePreviousLabs { get; set; }
        public string ComponentData { get; set; }
        public bool? ACEnabled { get; set; }
        public string ACMessage { get; set; }
        public string InterfaceLicenseStatusId { get; set; }
        public DateTime? LastCallToAC { get; set; }
        public bool HideXlinkGUI { get; set; }
        public DateTime? DateTimeLastSync { get; set; }
        public bool BillingExportMidLevels { get; set; }

        public InterfacesDomain MapToDomainModel()
        {
            InterfacesDomain domain = new InterfacesDomain
            {
                UniqueTableID = UniqueTableID,
                InterfaceID = InterfaceID,
                InterfaceName = InterfaceName,
                DateOn = DateOn,
                DateOff = DateOff,
                PracticeCode = PracticeCode,
                ActivationCode = ActivationCode,
                IsInterfaceActive = IsInterfaceActive,
                Comments = Comments,
                PathToSend = PathToSend,
                PathToReceive = PathToReceive,
                OnlineOverride = OnlineOverride,
                OnlineOverrideDate = OnlineOverrideDate,
                LastOnlineCheckDate = LastOnlineCheckDate,
                InterfaceType = InterfaceType,
                FileFormat = FileFormat,
                PatientInfoDir = PatientInfoDir,
                PatientInfoAuto = PatientInfoAuto,
                BillingInfoDir = BillingInfoDir,
                BillingInfoAuto = BillingInfoAuto,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                IncludeAllDXinFT = IncludeAllDXinFT,
                HidePreviousLabs = HidePreviousLabs,
                ComponentData = ComponentData,
                ACEnabled = ACEnabled,
                ACMessage = ACMessage,
                InterfaceLicenseStatusId = InterfaceLicenseStatusId,
                LastCallToAC = LastCallToAC,
                HideXlinkGUI = HideXlinkGUI,
                DateTimeLastSync = DateTimeLastSync,
                BillingExportMidLevels = BillingExportMidLevels
            };

            return domain;
        }

        public InterfacesPoco() { }

        public InterfacesPoco(InterfacesDomain domain)
        {
            UniqueTableID = domain.UniqueTableID;
            InterfaceID = domain.InterfaceID;
            InterfaceName = domain.InterfaceName;
            DateOn = domain.DateOn;
            DateOff = domain.DateOff;
            PracticeCode = domain.PracticeCode;
            ActivationCode = domain.ActivationCode;
            IsInterfaceActive = domain.IsInterfaceActive;
            Comments = domain.Comments;
            PathToSend = domain.PathToSend;
            PathToReceive = domain.PathToReceive;
            OnlineOverride = domain.OnlineOverride;
            OnlineOverrideDate = domain.OnlineOverrideDate;
            LastOnlineCheckDate = domain.LastOnlineCheckDate;
            InterfaceType = domain.InterfaceType;
            FileFormat = domain.FileFormat;
            PatientInfoDir = domain.PatientInfoDir;
            PatientInfoAuto = domain.PatientInfoAuto;
            BillingInfoDir = domain.BillingInfoDir;
            BillingInfoAuto = domain.BillingInfoAuto;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            IncludeAllDXinFT = domain.IncludeAllDXinFT;
            HidePreviousLabs = domain.HidePreviousLabs;
            ComponentData = domain.ComponentData;
            ACEnabled = domain.ACEnabled;
            ACMessage = domain.ACMessage;
            InterfaceLicenseStatusId = domain.InterfaceLicenseStatusId;
            LastCallToAC = domain.LastCallToAC;
            HideXlinkGUI = domain.HideXlinkGUI;
            DateTimeLastSync = domain.DateTimeLastSync;
            BillingExportMidLevels = domain.BillingExportMidLevels;
        }
    }
}
