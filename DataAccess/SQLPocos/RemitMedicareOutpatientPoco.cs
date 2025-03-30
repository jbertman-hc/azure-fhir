using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class RemitMedicareOutpatientPoco
    {
        public Guid ID { get; set; }
        public Guid RemitClaimsID { get; set; }
        public decimal? MOA01 { get; set; }
        public decimal? MOA02 { get; set; }
        public string MOA03 { get; set; }
        public string MOA04 { get; set; }
        public string MOA05 { get; set; }
        public string MOA06 { get; set; }
        public string MOA07 { get; set; }
        public decimal? MOA08 { get; set; }
        public decimal? MOA09 { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public RemitMedicareOutpatientDomain MapToDomainModel()
        {
            RemitMedicareOutpatientDomain domain = new RemitMedicareOutpatientDomain
            {
                ID = ID,
                RemitClaimsID = RemitClaimsID,
                MOA01 = MOA01,
                MOA02 = MOA02,
                MOA03 = MOA03,
                MOA04 = MOA04,
                MOA05 = MOA05,
                MOA06 = MOA06,
                MOA07 = MOA07,
                MOA08 = MOA08,
                MOA09 = MOA09,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public RemitMedicareOutpatientPoco() { }

        public RemitMedicareOutpatientPoco(RemitMedicareOutpatientDomain domain)
        {
            ID = domain.ID;
            RemitClaimsID = domain.RemitClaimsID;
            MOA01 = domain.MOA01;
            MOA02 = domain.MOA02;
            MOA03 = domain.MOA03;
            MOA04 = domain.MOA04;
            MOA05 = domain.MOA05;
            MOA06 = domain.MOA06;
            MOA07 = domain.MOA07;
            MOA08 = domain.MOA08;
            MOA09 = domain.MOA09;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
