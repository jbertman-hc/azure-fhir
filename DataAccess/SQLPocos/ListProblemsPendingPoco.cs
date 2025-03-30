using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListProblemsPendingPoco
    {
        public int ListProblemID { get; set; }
        public int PatientID { get; set; }
        public string ProblemICD { get; set; }
        public string ProblemName { get; set; }
        public DateTime? DateActive { get; set; }
        public DateTime? DateInactive { get; set; }
        public string AddingProvider { get; set; }
        public string Chronicity { get; set; }
        public DateTime? DateLastActivated { get; set; }
        public DateTime? DateSentToRegistry { get; set; }
        public DateTime? DateResolved { get; set; }
        public int? PendingFlag { get; set; }
        public DateTime? ImportedDate { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string Source { get; set; }
        public string COSTAR { get; set; }
        public string SNOMED { get; set; }
        public byte IcdType { get; set; }

        public ListProblemsPendingDomain MapToDomainModel()
        {
            ListProblemsPendingDomain domain = new ListProblemsPendingDomain
            {
                ListProblemID = ListProblemID,
                PatientID = PatientID,
                ProblemICD = ProblemICD,
                ProblemName = ProblemName,
                DateActive = DateActive,
                DateInactive = DateInactive,
                AddingProvider = AddingProvider,
                Chronicity = Chronicity,
                DateLastActivated = DateLastActivated,
                DateSentToRegistry = DateSentToRegistry,
                DateResolved = DateResolved,
                PendingFlag = PendingFlag,
                ImportedDate = ImportedDate,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Source = Source,
                COSTAR = COSTAR,
                SNOMED = SNOMED,
                IcdType = IcdType
            };

            return domain;
        }

        public ListProblemsPendingPoco() { }

        public ListProblemsPendingPoco(ListProblemsPendingDomain domain)
        {
            ListProblemID = domain.ListProblemID;
            PatientID = domain.PatientID;
            ProblemICD = domain.ProblemICD;
            ProblemName = domain.ProblemName;
            DateActive = domain.DateActive;
            DateInactive = domain.DateInactive;
            AddingProvider = domain.AddingProvider;
            Chronicity = domain.Chronicity;
            DateLastActivated = domain.DateLastActivated;
            DateSentToRegistry = domain.DateSentToRegistry;
            DateResolved = domain.DateResolved;
            PendingFlag = domain.PendingFlag;
            ImportedDate = domain.ImportedDate;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Source = domain.Source;
            COSTAR = domain.COSTAR;
            SNOMED = domain.SNOMED;
            IcdType = domain.IcdType;
        }
    }
}
