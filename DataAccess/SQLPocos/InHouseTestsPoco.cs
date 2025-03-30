using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class InHouseTestsPoco
    {
        public int ID { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? Migrated { get; set; }

        public InHouseTestsDomain MapToDomainModel()
        {
            InHouseTestsDomain domain = new InHouseTestsDomain
            {
                ID = ID,
                TestName = TestName,
                TestCode = TestCode,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Migrated = Migrated
            };

            return domain;
        }

        public InHouseTestsPoco() { }

        public InHouseTestsPoco(InHouseTestsDomain domain)
        {
            ID = domain.ID;
            TestName = domain.TestName;
            TestCode = domain.TestCode;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Migrated = domain.Migrated;
        }
    }
}
