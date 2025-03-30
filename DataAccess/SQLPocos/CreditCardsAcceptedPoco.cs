using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class CreditCardsAcceptedPoco
    {
        public Guid CreditCardID { get; set; }
        public Guid? StatementSettingsID { get; set; }
        public int CreditCardTypeID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public CreditCardsAcceptedDomain MapToDomainModel()
        {
            CreditCardsAcceptedDomain domain = new CreditCardsAcceptedDomain
            {
                CreditCardID = CreditCardID,
                StatementSettingsID = StatementSettingsID,
                CreditCardTypeID = CreditCardTypeID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public CreditCardsAcceptedPoco() { }

        public CreditCardsAcceptedPoco(CreditCardsAcceptedDomain domain)
        {
            CreditCardID = domain.CreditCardID;
            StatementSettingsID = domain.StatementSettingsID;
            CreditCardTypeID = domain.CreditCardTypeID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
