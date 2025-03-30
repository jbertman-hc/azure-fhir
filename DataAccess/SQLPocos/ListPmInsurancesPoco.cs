using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListPmInsurancesPoco
    {
        public int PmInsuranceId { get; set; }
        public int PatientID { get; set; }
        public string InsuranceSeqNo { get; set; }
        public string PlanName { get; set; }
        public string PolicyNo { get; set; }
        public string GroupNo { get; set; }
        public string GroupName { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string PatientRelToSubscriber { get; set; }
        public string SubscriberSalutation { get; set; }
        public string SubscriberFirstName { get; set; }
        public string SubscriberMiddleName { get; set; }
        public string SubscriberLastName { get; set; }
        public string SubscriberSuffix { get; set; }
        public DateTime? SubscriberDob { get; set; }
        public decimal? CopayAmount { get; set; }
        public DateTime? InsuranceLastUpdated { get; set; }
        public string PlanPhone { get; set; }
        public string PlanAddressLine1 { get; set; }
        public string PlanAddressLine2 { get; set; }
        public string PlanCity { get; set; }
        public string PlanState { get; set; }
        public string PlanZip { get; set; }
        public int? PmAccountId { get; set; }
        public string PayerId { get; set; }
        public string InsuranceId { get; set; }
        public string AccountId { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListPmInsurancesDomain MapToDomainModel()
        {
            ListPmInsurancesDomain domain = new ListPmInsurancesDomain
            {
                PmInsuranceId = PmInsuranceId,
                PatientID = PatientID,
                InsuranceSeqNo = InsuranceSeqNo,
                PlanName = PlanName,
                PolicyNo = PolicyNo,
                GroupNo = GroupNo,
                GroupName = GroupName,
                EffectiveDate = EffectiveDate,
                ExpirationDate = ExpirationDate,
                PatientRelToSubscriber = PatientRelToSubscriber,
                SubscriberSalutation = SubscriberSalutation,
                SubscriberFirstName = SubscriberFirstName,
                SubscriberMiddleName = SubscriberMiddleName,
                SubscriberLastName = SubscriberLastName,
                SubscriberSuffix = SubscriberSuffix,
                SubscriberDob = SubscriberDob,
                CopayAmount = CopayAmount,
                InsuranceLastUpdated = InsuranceLastUpdated,
                PlanPhone = PlanPhone,
                PlanAddressLine1 = PlanAddressLine1,
                PlanAddressLine2 = PlanAddressLine2,
                PlanCity = PlanCity,
                PlanState = PlanState,
                PlanZip = PlanZip,
                PmAccountId = PmAccountId,
                PayerId = PayerId,
                InsuranceId = InsuranceId,
                AccountId = AccountId,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListPmInsurancesPoco() { }

        public ListPmInsurancesPoco(ListPmInsurancesDomain domain)
        {
            PmInsuranceId = domain.PmInsuranceId;
            PatientID = domain.PatientID;
            InsuranceSeqNo = domain.InsuranceSeqNo;
            PlanName = domain.PlanName;
            PolicyNo = domain.PolicyNo;
            GroupNo = domain.GroupNo;
            GroupName = domain.GroupName;
            EffectiveDate = domain.EffectiveDate;
            ExpirationDate = domain.ExpirationDate;
            PatientRelToSubscriber = domain.PatientRelToSubscriber;
            SubscriberSalutation = domain.SubscriberSalutation;
            SubscriberFirstName = domain.SubscriberFirstName;
            SubscriberMiddleName = domain.SubscriberMiddleName;
            SubscriberLastName = domain.SubscriberLastName;
            SubscriberSuffix = domain.SubscriberSuffix;
            SubscriberDob = domain.SubscriberDob;
            CopayAmount = domain.CopayAmount;
            InsuranceLastUpdated = domain.InsuranceLastUpdated;
            PlanPhone = domain.PlanPhone;
            PlanAddressLine1 = domain.PlanAddressLine1;
            PlanAddressLine2 = domain.PlanAddressLine2;
            PlanCity = domain.PlanCity;
            PlanState = domain.PlanState;
            PlanZip = domain.PlanZip;
            PmAccountId = domain.PmAccountId;
            PayerId = domain.PayerId;
            InsuranceId = domain.InsuranceId;
            AccountId = domain.AccountId;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
