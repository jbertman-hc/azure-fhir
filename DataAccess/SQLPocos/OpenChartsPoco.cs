using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class OpenChartsPoco
    {
        public string ProviderCode { get; set; }
        public DateTime? TimeOpen { get; set; }
        public int? PatientID { get; set; }

        public OpenChartsDomain MapToDomainModel()
        {
            OpenChartsDomain domain = new OpenChartsDomain
            {
                ProviderCode = ProviderCode,
                TimeOpen = TimeOpen,
                PatientID = PatientID
            };

            return domain;
        }

        public OpenChartsPoco() { }

        public OpenChartsPoco(OpenChartsDomain domain)
        {
            ProviderCode = domain.ProviderCode;
            TimeOpen = domain.TimeOpen;
            PatientID = domain.PatientID;
        }
    }
}
