using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ICD9CodesEditedPoco
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool personalcode { get; set; }
        public bool common { get; set; }
        public string ShortDescription { get; set; }
        public string ShorterDescription { get; set; }
        public string IsComplete { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ICD9CodesEditedDomain MapToDomainModel()
        {
            ICD9CodesEditedDomain domain = new ICD9CodesEditedDomain
            {
                ID = ID,
                Code = Code,
                Description = Description,
                personalcode = personalcode,
                common = common,
                ShortDescription = ShortDescription,
                ShorterDescription = ShorterDescription,
                IsComplete = IsComplete,
                Deleted = Deleted,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ICD9CodesEditedPoco() { }

        public ICD9CodesEditedPoco(ICD9CodesEditedDomain domain)
        {
            ID = domain.ID;
            Code = domain.Code;
            Description = domain.Description;
            personalcode = domain.personalcode;
            common = domain.common;
            ShortDescription = domain.ShortDescription;
            ShorterDescription = domain.ShorterDescription;
            IsComplete = domain.IsComplete;
            Deleted = domain.Deleted;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
