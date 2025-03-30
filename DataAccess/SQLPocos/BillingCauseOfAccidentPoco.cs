using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class BillingCauseOfAccidentPoco
    {
        public Guid BillingCauseOfAccidentID { get; set; }
        public Guid BillingOtherInformationID { get; set; }
        public int CauseOfAccidentID { get; set; }
        public string AccidentState { get; set; }
        public string AccidentCountry { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public BillingCauseOfAccidentDomain MapToDomainModel()
        {
            BillingCauseOfAccidentDomain domain = new BillingCauseOfAccidentDomain
            {
                BillingCauseOfAccidentID = BillingCauseOfAccidentID,
                BillingOtherInformationID = BillingOtherInformationID,
                CauseOfAccidentID = CauseOfAccidentID,
                AccidentState = AccidentState,
                AccidentCountry = AccidentCountry,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public BillingCauseOfAccidentPoco() { }

        public BillingCauseOfAccidentPoco(BillingCauseOfAccidentDomain domain)
        {
            BillingCauseOfAccidentID = domain.BillingCauseOfAccidentID;
            BillingOtherInformationID = domain.BillingOtherInformationID;
            CauseOfAccidentID = domain.CauseOfAccidentID;
            AccidentState = domain.AccidentState;
            AccidentCountry = domain.AccidentCountry;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}

