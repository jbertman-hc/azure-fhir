using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListProblemPoco
    {
        public int ListProblemID { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string ProblemName { get; set; }
        public string ProblemICD { get; set; }
        public DateTime? DateActive { get; set; }
        public DateTime? DateInactive { get; set; }
        public string AddingProvider { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string Chronicity { get; set; }
        public DateTime? DateLastActivated { get; set; }
        public DateTime? DateSentToRegistry { get; set; }
        public DateTime? DateResolved { get; set; }
        public string Source { get; set; }
        public string COSTAR { get; set; }
        public string SNOMED { get; set; }
        public bool Historical { get; set; }
        public byte IcdType { get; set; }

        public ListProblemDomain MapToDomainModel()
        {
            ListProblemDomain domain = new ListProblemDomain
            {
                ListProblemID = ListProblemID,
                PatientID = PatientID,
                PatientName = PatientName,
                ProblemName = ProblemName,
                ProblemICD = ProblemICD,
                DateActive = DateActive,
                DateInactive = DateInactive,
                AddingProvider = AddingProvider,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Chronicity = Chronicity,
                DateLastActivated = DateLastActivated,
                DateSentToRegistry = DateSentToRegistry,
                DateResolved = DateResolved,
                Source = Source,
                COSTAR = COSTAR,
                SNOMED = SNOMED,
                Historical = Historical,
                IcdType = IcdType
            };

            return domain;
        }

        public ListProblemPoco() { }

        public ListProblemPoco(ListProblemDomain domain)
        {
            ListProblemID = domain.ListProblemID;
            PatientID = domain.PatientID;
            PatientName = domain.PatientName;
            ProblemName = domain.ProblemName;
            ProblemICD = domain.ProblemICD;
            DateActive = domain.DateActive;
            DateInactive = domain.DateInactive;
            AddingProvider = domain.AddingProvider;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Chronicity = domain.Chronicity;
            DateLastActivated = domain.DateLastActivated;
            DateSentToRegistry = domain.DateSentToRegistry;
            DateResolved = domain.DateResolved;
            Source = domain.Source;
            COSTAR = domain.COSTAR;
            SNOMED = domain.SNOMED;
            Historical = domain.Historical;
            IcdType = domain.IcdType;
        }

    }
}
