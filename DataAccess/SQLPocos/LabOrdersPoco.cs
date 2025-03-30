using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LabOrdersPoco
    {
        public int AccessionNbrAC { get; set; }
        public int LabTestID { get; set; }
        public DateTime CreatedDt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdDt { get; set; }
        public int? LastUpdBy { get; set; }
        public int OrderingProviderID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public LabOrdersDomain MapToDomainModel()
        {
            LabOrdersDomain domain = new LabOrdersDomain
            {
                AccessionNbrAC = AccessionNbrAC,
                LabTestID = LabTestID,
                CreatedDt = CreatedDt,
                CreatedBy = CreatedBy,
                LastUpdDt = LastUpdDt,
                LastUpdBy = LastUpdBy,
                OrderingProviderID = OrderingProviderID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public LabOrdersPoco() { }

        public LabOrdersPoco(LabOrdersDomain domain)
        {
            AccessionNbrAC = domain.AccessionNbrAC;
            LabTestID = domain.LabTestID;
            CreatedDt = domain.CreatedDt;
            CreatedBy = domain.CreatedBy;
            LastUpdDt = domain.LastUpdDt;
            LastUpdBy = domain.LastUpdBy;
            OrderingProviderID = domain.OrderingProviderID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
