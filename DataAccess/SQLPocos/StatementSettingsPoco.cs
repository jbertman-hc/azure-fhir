using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class StatementSettingsPoco
    {
        public Guid StatementSettingsID { get; set; }
        public decimal? MinimunDueAmount { get; set; }
        public int? HowManyBeforeCollections { get; set; }
        public string PracticeMessage { get; set; }
        public int? DaysLateFeeAssessed { get; set; }
        public string ChecksPayableTo { get; set; }
        public string BillByDate { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string RemitName { get; set; }
        public string RemitAttention { get; set; }
        public string RemitAddress1 { get; set; }
        public string RemitAddress2 { get; set; }
        public string RemitCity { get; set; }
        public string RemitState { get; set; }
        public string RemitZipCode { get; set; }
        public string BillingPhone { get; set; }
        public string DefaultEmailSubject { get; set; }
        public string DefaultEmailMessage { get; set; }
        public string PracticeEmailAddress { get; set; }
        public string DisplayName { get; set; }
        public string BillingPhoneExt { get; set; }
        public string PracticeLogo { get; set; }
        public decimal? LateFee { get; set; }
        public string OnlinePayments { get; set; }

        public StatementSettingsDomain MapToDomainModel()
        {
            StatementSettingsDomain domain = new StatementSettingsDomain
            {
                StatementSettingsID = StatementSettingsID,
                MinimunDueAmount = MinimunDueAmount,
                HowManyBeforeCollections = HowManyBeforeCollections,
                PracticeMessage = PracticeMessage,
                DaysLateFeeAssessed = DaysLateFeeAssessed,
                ChecksPayableTo = ChecksPayableTo,
                BillByDate = BillByDate,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                RemitName = RemitName,
                RemitAttention = RemitAttention,
                RemitAddress1 = RemitAddress1,
                RemitAddress2 = RemitAddress2,
                RemitCity = RemitCity,
                RemitState = RemitState,
                RemitZipCode = RemitZipCode,
                BillingPhone = BillingPhone,
                DefaultEmailSubject = DefaultEmailSubject,
                DefaultEmailMessage = DefaultEmailMessage,
                PracticeEmailAddress = PracticeEmailAddress,
                DisplayName = DisplayName,
                BillingPhoneExt = BillingPhoneExt,
                PracticeLogo = PracticeLogo,
                LateFee = LateFee,
                OnlinePayments = OnlinePayments
            };

            return domain;
        }

        public StatementSettingsPoco() { }

        public StatementSettingsPoco(StatementSettingsDomain domain)
        {
            StatementSettingsID = domain.StatementSettingsID;
            MinimunDueAmount = domain.MinimunDueAmount;
            HowManyBeforeCollections = domain.HowManyBeforeCollections;
            PracticeMessage = domain.PracticeMessage;
            DaysLateFeeAssessed = domain.DaysLateFeeAssessed;
            ChecksPayableTo = domain.ChecksPayableTo;
            BillByDate = domain.BillByDate;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            RemitName = domain.RemitName;
            RemitAttention = domain.RemitAttention;
            RemitAddress1 = domain.RemitAddress1;
            RemitAddress2 = domain.RemitAddress2;
            RemitCity = domain.RemitCity;
            RemitState = domain.RemitState;
            RemitZipCode = domain.RemitZipCode;
            BillingPhone = domain.BillingPhone;
            DefaultEmailSubject = domain.DefaultEmailSubject;
            DefaultEmailMessage = domain.DefaultEmailMessage;
            PracticeEmailAddress = domain.PracticeEmailAddress;
            DisplayName = domain.DisplayName;
            BillingPhoneExt = domain.BillingPhoneExt;
            PracticeLogo = domain.PracticeLogo;
            LateFee = domain.LateFee;
            OnlinePayments = domain.OnlinePayments;
        }
    }
}
