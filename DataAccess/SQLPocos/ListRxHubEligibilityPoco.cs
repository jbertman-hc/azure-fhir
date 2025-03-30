using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListRxHubEligibilityPoco
    {
        public Guid RxHubEligibilityID { get; set; }
        public int PatientID { get; set; }
        public Guid EligibilityGUID { get; set; }
        public DateTime? EligibilityDate { get; set; }
        public string PharmacyBenefit { get; set; }
        public string MailOrderBenefit { get; set; }
        public string PlanID { get; set; }
        public string PlanName { get; set; }
        public string EligibilityIndex { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string DemographicsChanged { get; set; }
        public DateTime? EligibilityExpireTimestamp { get; set; }
        public string SpecialtyPharmacyBenefit { get; set; }
        public string LTCBenefit { get; set; }

        public ListRxHubEligibilityDomain MapToDomainModel()
        {
            ListRxHubEligibilityDomain domain = new ListRxHubEligibilityDomain
            {
                RxHubEligibilityID = RxHubEligibilityID,
                PatientID = PatientID,
                EligibilityGUID = EligibilityGUID,
                EligibilityDate = EligibilityDate,
                PharmacyBenefit = PharmacyBenefit,
                MailOrderBenefit = MailOrderBenefit,
                PlanID = PlanID,
                PlanName = PlanName,
                EligibilityIndex = EligibilityIndex,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                LastName = LastName,
                FirstName = FirstName,
                MiddleName = MiddleName,
                Suffix = Suffix,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                State = State,
                Zip = Zip,
                DOB = DOB,
                Gender = Gender,
                DemographicsChanged = DemographicsChanged,
                EligibilityExpireTimestamp = EligibilityExpireTimestamp,
                SpecialtyPharmacyBenefit = SpecialtyPharmacyBenefit,
                LTCBenefit = LTCBenefit
            };

            return domain;
        }

        public ListRxHubEligibilityPoco() { }

        public ListRxHubEligibilityPoco(ListRxHubEligibilityDomain domain)
        {
            RxHubEligibilityID = domain.RxHubEligibilityID;
            PatientID = domain.PatientID;
            EligibilityGUID = domain.EligibilityGUID;
            EligibilityDate = domain.EligibilityDate;
            PharmacyBenefit = domain.PharmacyBenefit;
            MailOrderBenefit = domain.MailOrderBenefit;
            PlanID = domain.PlanID;
            PlanName = domain.PlanName;
            EligibilityIndex = domain.EligibilityIndex;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            LastName = domain.LastName;
            FirstName = domain.FirstName;
            MiddleName = domain.MiddleName;
            Suffix = domain.Suffix;
            Address1 = domain.Address1;
            Address2 = domain.Address2;
            City = domain.City;
            State = domain.State;
            Zip = domain.Zip;
            DOB = domain.DOB;
            Gender = domain.Gender;
            DemographicsChanged = domain.DemographicsChanged;
            EligibilityExpireTimestamp = domain.EligibilityExpireTimestamp;
            SpecialtyPharmacyBenefit = domain.SpecialtyPharmacyBenefit;
            LTCBenefit = domain.LTCBenefit;
        }
    }
}
