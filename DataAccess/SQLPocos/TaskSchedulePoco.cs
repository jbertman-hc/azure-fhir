using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class TaskSchedulePoco
    {
        public int TaskScheduleID { get; set; }
        public int TaskID { get; set; }
        public int FrequencyNumber { get; set; }
        public int FrequencyUnit { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime? DateTimeLastRun { get; set; }
        public DateTime? DateTimeToRun { get; set; }
        public bool TaskEnabled { get; set; }
        public int CurrentFailures { get; set; }
        public DateTime? LastFailureTime { get; set; }
        public string LastFailureMessage { get; set; }
        public DateTime? LastFailureNotificationTime { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public TaskScheduleDomain MapToDomainModel()
        {
            TaskScheduleDomain domain = new TaskScheduleDomain
            {
                TaskScheduleID = TaskScheduleID,
                TaskID = TaskID,
                FrequencyNumber = FrequencyNumber,
                FrequencyUnit = FrequencyUnit,
                IsRecurring = IsRecurring,
                DateTimeLastRun = DateTimeLastRun,
                DateTimeToRun = DateTimeToRun,
                TaskEnabled = TaskEnabled,
                CurrentFailures = CurrentFailures,
                LastFailureTime = LastFailureTime,
                LastFailureMessage = LastFailureMessage,
                LastFailureNotificationTime = LastFailureNotificationTime,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public TaskSchedulePoco() { }

        public TaskSchedulePoco(TaskScheduleDomain domain)
        {
            TaskScheduleID = domain.TaskScheduleID;
            TaskID = domain.TaskID;
            FrequencyNumber = domain.FrequencyNumber;
            FrequencyUnit = domain.FrequencyUnit;
            IsRecurring = domain.IsRecurring;
            DateTimeLastRun = domain.DateTimeLastRun;
            DateTimeToRun = domain.DateTimeToRun;
            TaskEnabled = domain.TaskEnabled;
            CurrentFailures = domain.CurrentFailures;
            LastFailureTime = domain.LastFailureTime;
            LastFailureMessage = domain.LastFailureMessage;
            LastFailureNotificationTime = domain.LastFailureNotificationTime;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
