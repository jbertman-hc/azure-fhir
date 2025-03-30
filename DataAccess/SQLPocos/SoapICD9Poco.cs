using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SoapICD9Poco
    {
        public int SoapICD9ID { get; set; }
        public int SoapID { get; set; }
        public string ICD9 { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public SoapICD9Domain MapToDomainModel()
        {
            SoapICD9Domain domain = new SoapICD9Domain
            {
                SoapICD9ID = SoapICD9ID,
                SoapID = SoapID,
                ICD9 = ICD9,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public SoapICD9Poco() { }

        public SoapICD9Poco(SoapICD9Domain domain)
        {
            SoapICD9ID = domain.SoapICD9ID;
            SoapID = domain.SoapID;
            ICD9 = domain.ICD9;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
