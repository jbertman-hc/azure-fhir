using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SoapCostarPoco
    {
        public int SoapCostarId { get; set; }
        public int SoapId { get; set; }
        public string Costar { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public SoapCostarDomain MapToDomainModel()
        {
            SoapCostarDomain domain = new SoapCostarDomain
            {
                SoapCostarId = SoapCostarId,
                SoapId = SoapId,
                Costar = Costar,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public SoapCostarPoco() { }

        public SoapCostarPoco(SoapCostarDomain domain)
        {
            SoapCostarId = domain.SoapCostarId;
            SoapId = domain.SoapId;
            Costar = domain.Costar;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
