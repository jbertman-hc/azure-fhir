using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListClearinghousesPoco
    {
        public Guid ListClearinghousesID { get; set; }
        public Guid? CHID { get; set; }
        public string CustomerID { get; set; }
        public string BillingID { get; set; }
        public string SubmitterID { get; set; }
        public string SubmitterName { get; set; }
        public string Login { get; set; }
        public string SUID { get; set; }
        public string PWD { get; set; }
        public int? ContactID { get; set; }
        public string ContactPhone { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public DateTime? DateIncomingLastOpened { get; set; }
        public bool? Active { get; set; }

        public ListClearinghousesDomain MapToDomainModel()
        {
            ListClearinghousesDomain domain = new ListClearinghousesDomain
            {
                ListClearinghousesID = ListClearinghousesID,
                CHID = CHID,
                CustomerID = CustomerID,
                BillingID = BillingID,
                SubmitterID = SubmitterID,
                SubmitterName = SubmitterName,
                Login = Login,
                SUID = SUID,
                PWD = PWD,
                ContactID = ContactID,
                ContactPhone = ContactPhone,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                DateIncomingLastOpened = DateIncomingLastOpened,
                Active = Active
            };

            return domain;
        }

        public ListClearinghousesPoco() { }

        public ListClearinghousesPoco(ListClearinghousesDomain domain)
        {
            ListClearinghousesID = domain.ListClearinghousesID;
            CHID = domain.CHID;
            CustomerID = domain.CustomerID;
            BillingID = domain.BillingID;
            SubmitterID = domain.SubmitterID;
            SubmitterName = domain.SubmitterName;
            Login = domain.Login;
            SUID = domain.SUID;
            PWD = domain.PWD;
            ContactID = domain.ContactID;
            ContactPhone = domain.ContactPhone;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            DateIncomingLastOpened = domain.DateIncomingLastOpened;
            Active = domain.Active;
        }
    }
}
