using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListHMrulesIgnoredPoco
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int? HmRuleID { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string Comments { get; set; }

        public ListHMrulesIgnoredDomain MapToDomainModel()
        {
            ListHMrulesIgnoredDomain domain = new ListHMrulesIgnoredDomain
            {
                ID = ID,
                PatientID = PatientID,
                HmRuleID = HmRuleID,
                LastTouchedBy = LastTouchedBy,
                DateLastTouched = DateLastTouched,
                DateRowAdded = DateRowAdded,
                Comments = Comments
            };

            return domain;
        }

        public ListHMrulesIgnoredPoco() { }

        public ListHMrulesIgnoredPoco(ListHMrulesIgnoredDomain domain)
        {
            ID = domain.ID;
            PatientID = domain.PatientID;
            HmRuleID = domain.HmRuleID;
            LastTouchedBy = domain.LastTouchedBy;
            DateLastTouched = domain.DateLastTouched;
            DateRowAdded = domain.DateRowAdded;
            Comments = domain.Comments;
        }
    }
}
