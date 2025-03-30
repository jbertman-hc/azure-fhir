using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ClaimStatusMessagePoco
    {
        public Guid ClaimStatusMessageID { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        public string Entity { get; set; }
        public string TextMessage { get; set; }
        public Guid? ClaimStatusID { get; set; }
        public Guid? ClaimStatusServLineID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ClaimStatusMessageDomain MapToDomainModel()
        {
            ClaimStatusMessageDomain domain = new ClaimStatusMessageDomain
            {
                ClaimStatusMessageID = ClaimStatusMessageID,
                Category = Category,
                Code = Code,
                Entity = Entity,
                TextMessage = TextMessage,
                ClaimStatusID = ClaimStatusID,
                ClaimStatusServLineID = ClaimStatusServLineID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ClaimStatusMessagePoco() { }

        public ClaimStatusMessagePoco(ClaimStatusMessageDomain domain)
        {
            ClaimStatusMessageID = domain.ClaimStatusMessageID;
            Category = domain.Category;
            Code = domain.Code;
            Entity = domain.Entity;
            TextMessage = domain.TextMessage;
            ClaimStatusID = domain.ClaimStatusID;
            ClaimStatusServLineID = domain.ClaimStatusServLineID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
