using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class HmConditionsForRulesEditedPoco
    {
        public Guid HmConditionForRuleID { get; set; }
        public int HmRuleID { get; set; }
        public Guid HmConditionID { get; set; }
        public string Comments { get; set; }
        public string Value { get; set; }
        public string ComparisonOp { get; set; }
        public Guid? GroupID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string CriteriaType { get; set; }
        public bool Deleted { get; set; }

        public HmConditionsForRulesEditedDomain MapToDomainModel()
        {
            HmConditionsForRulesEditedDomain domain = new HmConditionsForRulesEditedDomain
            {
                HmConditionForRuleID = HmConditionForRuleID,
                HmRuleID = HmRuleID,
                HmConditionID = HmConditionID,
                Comments = Comments,
                Value = Value,
                ComparisonOp = ComparisonOp,
                GroupID = GroupID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                CriteriaType = CriteriaType,
                Deleted = Deleted
            };

            return domain;
        }

        public HmConditionsForRulesEditedPoco() { }

        public HmConditionsForRulesEditedPoco(HmConditionsForRulesEditedDomain domain)
        {
            HmConditionForRuleID = domain.HmConditionForRuleID;
            HmRuleID = domain.HmRuleID;
            HmConditionID = domain.HmConditionID;
            Comments = domain.Comments;
            Value = domain.Value;
            ComparisonOp = domain.ComparisonOp;
            GroupID = domain.GroupID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            CriteriaType = domain.CriteriaType;
            Deleted = domain.Deleted;
        }
    }
}
