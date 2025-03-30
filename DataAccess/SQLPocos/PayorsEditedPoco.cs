using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PayorsEditedPoco
    {
        public Guid PayorsID { get; set; }
        public string PayorName { get; set; }
        public string PlanName { get; set; }
        public string PlanCode { get; set; }
        public int? PayorType { get; set; }
        public string PhoneCountry { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneExt { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateOrRegion { get; set; }
        public string StateOrRegionText { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string ClaimID { get; set; }
        public string PayorsNotes { get; set; }
        public int? PayorsSubmissionType { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string WebSite { get; set; }
        public string EligibilityID { get; set; }
        public bool? Active { get; set; }
        public string ClaimFilingIndicatorCode { get; set; }
        public int? BillingProcessingOptionID { get; set; }

        public PayorsEditedDomain MapToDomainModel()
        {
            PayorsEditedDomain domain = new PayorsEditedDomain
            {
                PayorsID = PayorsID,
                PayorName = PayorName,
                PlanName = PlanName,
                PlanCode = PlanCode,
                PayorType = PayorType,
                PhoneCountry = PhoneCountry,
                PhoneNumber = PhoneNumber,
                PhoneExt = PhoneExt,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                StateOrRegion = StateOrRegion,
                StateOrRegionText = StateOrRegionText,
                Country = Country,
                PostalCode = PostalCode,
                ClaimID = ClaimID,
                PayorsNotes = PayorsNotes,
                PayorsSubmissionType = PayorsSubmissionType,
                Deleted = Deleted,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                WebSite = WebSite,
                EligibilityID = EligibilityID,
                Active = Active,
                ClaimFilingIndicatorCode = ClaimFilingIndicatorCode,
                BillingProcessingOptionID = BillingProcessingOptionID
            };

            return domain;
        }

        public PayorsEditedPoco() { }

        public PayorsEditedPoco(PayorsEditedDomain domain)
        {
            PayorsID = domain.PayorsID;
            PayorName = domain.PayorName;
            PlanName = domain.PlanName;
            PlanCode = domain.PlanCode;
            PayorType = domain.PayorType;
            PhoneCountry = domain.PhoneCountry;
            PhoneNumber = domain.PhoneNumber;
            PhoneExt = domain.PhoneExt;
            Address1 = domain.Address1;
            Address2 = domain.Address2;
            City = domain.City;
            StateOrRegion = domain.StateOrRegion;
            StateOrRegionText = domain.StateOrRegionText;
            Country = domain.Country;
            PostalCode = domain.PostalCode;
            ClaimID = domain.ClaimID;
            PayorsNotes = domain.PayorsNotes;
            PayorsSubmissionType = domain.PayorsSubmissionType;
            Deleted = domain.Deleted;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            WebSite = domain.WebSite;
            EligibilityID = domain.EligibilityID;
            Active = domain.Active;
            ClaimFilingIndicatorCode = domain.ClaimFilingIndicatorCode;
            BillingProcessingOptionID = domain.BillingProcessingOptionID;
        }
    }
}
