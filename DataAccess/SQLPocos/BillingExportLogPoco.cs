using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class BillingExportLogPoco
    {
        public int BillingExportLogID { get; set; }
        public Guid BillingID { get; set; }
        public string ExportFormatCode { get; set; }
        public string InterfaceName { get; set; }
        public DateTime ExportDate { get; set; }
        public Guid? PayorsID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public BillingExportLogDomain MapToDomainModel()
        {
            BillingExportLogDomain domain = new BillingExportLogDomain
            {
                BillingExportLogID = BillingExportLogID,
                BillingID = BillingID,
                ExportFormatCode = ExportFormatCode,
                InterfaceName = InterfaceName,
                ExportDate = ExportDate,
                PayorsID = PayorsID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public BillingExportLogPoco() { }

        public BillingExportLogPoco(BillingExportLogDomain domain)
        {
            BillingExportLogID = domain.BillingExportLogID;
            BillingID = domain.BillingID;
            ExportFormatCode = domain.ExportFormatCode;
            InterfaceName = domain.InterfaceName;
            ExportDate = domain.ExportDate;
            PayorsID = domain.PayorsID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}

