using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListFamilyHistoryPoco
    {
        public int FamilyHistoryID { get; set; }
        public int PatientId { get; set; }
        public string FamilyHistory { get; set; }
        public int? PendingFlag { get; set; }
        public DateTime? ImportedDate { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool NoSignificantFamilyHealthHistory { get; set; }
        public bool UnknownFamilyHealthHistory { get; set; }

        public ListFamilyHistoryDomain MapToDomainModel()
        {
            ListFamilyHistoryDomain domain = new ListFamilyHistoryDomain
            {
                FamilyHistoryID = FamilyHistoryID,
                PatientId = PatientId,
                FamilyHistory = FamilyHistory,
                PendingFlag = PendingFlag,
                ImportedDate = ImportedDate,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                NoSignificantFamilyHealthHistory = NoSignificantFamilyHealthHistory,
                UnknownFamilyHealthHistory = UnknownFamilyHealthHistory
            };

            return domain;
        }

        public ListFamilyHistoryPoco() { }

        public ListFamilyHistoryPoco(ListFamilyHistoryDomain domain)
        {
            FamilyHistoryID = domain.FamilyHistoryID;
            PatientId = domain.PatientId;
            FamilyHistory = domain.FamilyHistory;
            PendingFlag = domain.PendingFlag;
            ImportedDate = domain.ImportedDate;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            NoSignificantFamilyHealthHistory = domain.NoSignificantFamilyHealthHistory;
            UnknownFamilyHealthHistory = domain.UnknownFamilyHealthHistory;
        }
    }
}
