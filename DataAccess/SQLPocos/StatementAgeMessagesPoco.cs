using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class StatementAgeMessagesPoco
    {
        public Guid StatementAgeMessagesID { get; set; }
        public Guid? StatementSettingsID { get; set; }
        public string Message { get; set; }
        public string Age { get; set; }
        public int? SortOrder { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public int? MinDays { get; set; }
        public int? MaxDays { get; set; }

        public StatementAgeMessagesDomain MapToDomainModel()
        {
            StatementAgeMessagesDomain domain = new StatementAgeMessagesDomain
            {
                StatementAgeMessagesID = StatementAgeMessagesID,
                StatementSettingsID = StatementSettingsID,
                Message = Message,
                Age = Age,
                SortOrder = SortOrder,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                MinDays = MinDays,
                MaxDays = MaxDays
            };

            return domain;
        }

        public StatementAgeMessagesPoco() { }

        public StatementAgeMessagesPoco(StatementAgeMessagesDomain domain)
        {
            StatementAgeMessagesID = domain.StatementAgeMessagesID;
            StatementSettingsID = domain.StatementSettingsID;
            Message = domain.Message;
            Age = domain.Age;
            SortOrder = domain.SortOrder;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            MinDays = domain.MinDays;
            MaxDays = domain.MaxDays;
        }
    }
}
