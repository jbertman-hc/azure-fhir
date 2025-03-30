using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SearchLastReminderPoco
    {
        public int SearchLastReminderID { get; set; }
        public int PatientID { get; set; }
        public Guid GroupGUID { get; set; }
        public bool IsPreferredMethod { get; set; }
        public DateTime LastReminder { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public SearchLastReminderDomain MapToDomainModel()
        {
            SearchLastReminderDomain domain = new SearchLastReminderDomain
            {
                SearchLastReminderID = SearchLastReminderID,
                PatientID = PatientID,
                GroupGUID = GroupGUID,
                IsPreferredMethod = IsPreferredMethod,
                LastReminder = LastReminder,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public SearchLastReminderPoco() { }

        public SearchLastReminderPoco(SearchLastReminderDomain domain)
        {
            SearchLastReminderID = domain.SearchLastReminderID;
            PatientID = domain.PatientID;
            GroupGUID = domain.GroupGUID;
            IsPreferredMethod = domain.IsPreferredMethod;
            LastReminder = domain.LastReminder;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
