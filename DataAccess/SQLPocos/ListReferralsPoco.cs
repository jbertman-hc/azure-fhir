using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListReferralsPoco
    {
        public int ID { get; set; }
        public int? PatientID { get; set; }
        public string ReferralName { get; set; }
        public int? ReferProviderID { get; set; }
        public DateTime? DateStarts { get; set; }
        public DateTime? DateEnds { get; set; }
        public int? NumberOfVisits { get; set; }
        public string Comment { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? Outgoing { get; set; }
        public int? OnBehalfOfProvider { get; set; }

        public ListReferralsDomain MapToDomainModel()
        {
            ListReferralsDomain domain = new ListReferralsDomain
            {
                ID = ID,
                PatientID = PatientID,
                ReferralName = ReferralName,
                ReferProviderID = ReferProviderID,
                DateStarts = DateStarts,
                DateEnds = DateEnds,
                NumberOfVisits = NumberOfVisits,
                Comment = Comment,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Outgoing = Outgoing,
                OnBehalfOfProvider = OnBehalfOfProvider
            };

            return domain;
        }

        public ListReferralsPoco() { }

        public ListReferralsPoco(ListReferralsDomain domain)
        {
            ID = domain.ID;
            PatientID = domain.PatientID;
            ReferralName = domain.ReferralName;
            ReferProviderID = domain.ReferProviderID;
            DateStarts = domain.DateStarts;
            DateEnds = domain.DateEnds;
            NumberOfVisits = domain.NumberOfVisits;
            Comment = domain.Comment;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Outgoing = domain.Outgoing;
            OnBehalfOfProvider = domain.OnBehalfOfProvider;
        }
    }
}
