using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class StatementPoco
    {
        public Guid StatementID { get; set; }
        public string PersonalizedMessage { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public DateTime? DateSent { get; set; }
        public int? PatientID { get; set; }
        public string BillingPeriod { get; set; }
        public bool? Printed { get; set; }
        public bool? Emailed { get; set; }
        public bool? Closed { get; set; }
        public string Subscriber { get; set; }

        public StatementDomain MapToDomainModel()
        {
            StatementDomain domain = new StatementDomain
            {
                StatementID = StatementID,
                PersonalizedMessage = PersonalizedMessage,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                DateSent = DateSent,
                PatientID = PatientID,
                BillingPeriod = BillingPeriod,
                Printed = Printed,
                Emailed = Emailed,
                Closed = Closed,
                Subscriber = Subscriber
            };

            return domain;
        }

        public StatementPoco() { }

        public StatementPoco(StatementDomain domain)
        {
            StatementID = domain.StatementID;
            PersonalizedMessage = domain.PersonalizedMessage;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            DateSent = domain.DateSent;
            PatientID = domain.PatientID;
            BillingPeriod = domain.BillingPeriod;
            Printed = domain.Printed;
            Emailed = domain.Emailed;
            Closed = domain.Closed;
            Subscriber = domain.Subscriber;
        }
    }
}
