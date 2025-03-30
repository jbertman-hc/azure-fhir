using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LabTestDirectoryPoco
    {
        public string TestCode { get; set; }
        public string LabCompany { get; set; }
        public string TestName { get; set; }
        public DateTime CreatedDt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdDt { get; set; }
        public int? LastUpdBy { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public LabTestDirectoryDomain MapToDomainModel()
        {
            LabTestDirectoryDomain domain = new LabTestDirectoryDomain
            {
                TestCode = TestCode,
                LabCompany = LabCompany,
                TestName = TestName,
                CreatedDt = CreatedDt,
                CreatedBy = CreatedBy,
                LastUpdDt = LastUpdDt,
                LastUpdBy = LastUpdBy,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public LabTestDirectoryPoco() { }

        public LabTestDirectoryPoco(LabTestDirectoryDomain domain)
        {
            TestCode = domain.TestCode;
            LabCompany = domain.LabCompany;
            TestName = domain.TestName;
            CreatedDt = domain.CreatedDt;
            CreatedBy = domain.CreatedBy;
            LastUpdDt = domain.LastUpdDt;
            LastUpdBy = domain.LastUpdBy;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
