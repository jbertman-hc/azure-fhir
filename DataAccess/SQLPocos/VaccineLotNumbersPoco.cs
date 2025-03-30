using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class VaccineLotNumbersPoco
    {
        public int ID { get; set; }
        public string LotNo { get; set; }
        public DateTime? Expiration { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public VaccineLotNumbersDomain MapToDomainModel()
        {
            VaccineLotNumbersDomain domain = new VaccineLotNumbersDomain
            {
                ID = ID,
                LotNo = LotNo,
                Expiration = Expiration,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public VaccineLotNumbersPoco() { }

        public VaccineLotNumbersPoco(VaccineLotNumbersDomain domain)
        {
            ID = domain.ID;
            LotNo = domain.LotNo;
            Expiration = domain.Expiration;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
