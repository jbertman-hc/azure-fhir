using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class AutoEligibilityPoco
    {
        public Guid AutoEligibilityID { get; set; }
        public bool AutoEligibilityCheck { get; set; }
        public bool RunSameDay { get; set; }
        public DateTime TimeToRun { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public AutoEligibilityDomain MapToDomainModel()
        {
            AutoEligibilityDomain domain = new AutoEligibilityDomain
            {
                AutoEligibilityID = AutoEligibilityID,
                AutoEligibilityCheck = AutoEligibilityCheck,
                RunSameDay = RunSameDay,
                TimeToRun = TimeToRun,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public AutoEligibilityPoco() { }

        public AutoEligibilityPoco(AutoEligibilityDomain domain)
        {
            AutoEligibilityID = domain.AutoEligibilityID;
            AutoEligibilityCheck = domain.AutoEligibilityCheck;
            RunSameDay = domain.RunSameDay;
            TimeToRun = domain.TimeToRun;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}

