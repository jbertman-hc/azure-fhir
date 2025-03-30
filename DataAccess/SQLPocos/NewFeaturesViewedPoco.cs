using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class NewFeaturesViewedPoco
    {
        public long ID { get; set; }
        public long NewFeaturesDisplayId { get; set; }
        public bool HasBeenViewed { get; set; }
        public string UserId { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime DateLastTouched { get; set; }
        public DateTime DateRowAdded { get; set; }

        public NewFeaturesViewedDomain MapToDomainModel()
        {
            NewFeaturesViewedDomain domain = new NewFeaturesViewedDomain
            {
                ID = ID,
                NewFeaturesDisplayId = NewFeaturesDisplayId,
                HasBeenViewed = HasBeenViewed,
                UserId = UserId,
                LastTouchedBy = LastTouchedBy,
                DateLastTouched = DateLastTouched,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public NewFeaturesViewedPoco() { }

        public NewFeaturesViewedPoco(NewFeaturesViewedDomain domain)
        {
            ID = domain.ID;
            NewFeaturesDisplayId = domain.NewFeaturesDisplayId;
            HasBeenViewed = domain.HasBeenViewed;
            UserId = domain.UserId;
            LastTouchedBy = domain.LastTouchedBy;
            DateLastTouched = domain.DateLastTouched;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
