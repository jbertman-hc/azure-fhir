using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LabTestFilesPoco
    {
        public int LabTestID { get; set; }
        public string FileData { get; set; }
        public DateTime? Uploaded { get; set; }
        public DateTime? Sent { get; set; }
        public bool? RequiresSignOff { get; set; }
        public string ExportFileName { get; set; }
        public string LogFilePath { get; set; }
        public int interfaceID { get; set; }

        public LabTestFilesDomain MapToDomainModel()
        {
            LabTestFilesDomain domain = new LabTestFilesDomain
            {
                LabTestID = LabTestID,
                FileData = FileData,
                Uploaded = Uploaded,
                Sent = Sent,
                RequiresSignOff = RequiresSignOff,
                ExportFileName = ExportFileName,
                LogFilePath = LogFilePath,
                interfaceID = interfaceID
            };

            return domain;
        }

        public LabTestFilesPoco() { }

        public LabTestFilesPoco(LabTestFilesDomain domain)
        {
            LabTestID = domain.LabTestID;
            FileData = domain.FileData;
            Uploaded = domain.Uploaded;
            Sent = domain.Sent;
            RequiresSignOff = domain.RequiresSignOff;
            ExportFileName = domain.ExportFileName;
            LogFilePath = domain.LogFilePath;
            interfaceID = domain.interfaceID;
        }
    }
}
