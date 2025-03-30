using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListProblemsForEncounterPoco
    {
        public int ListProblemForEncounterID { get; set; }
        public int PatientID { get; set; }
        public int ProblemID { get; set; }
        public DateTime EncounterDate { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListProblemsForEncounterDomain MapToDomainModel()
        {
            ListProblemsForEncounterDomain domain = new ListProblemsForEncounterDomain
            {
                ListProblemForEncounterID = ListProblemForEncounterID,
                PatientID = PatientID,
                ProblemID = ProblemID,
                EncounterDate = EncounterDate,
                Deleted = Deleted,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListProblemsForEncounterPoco() { }

        public ListProblemsForEncounterPoco(ListProblemsForEncounterDomain domain)
        {
            ListProblemForEncounterID = domain.ListProblemForEncounterID;
            PatientID = domain.PatientID;
            ProblemID = domain.ProblemID;
            EncounterDate = domain.EncounterDate;
            Deleted = domain.Deleted;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
