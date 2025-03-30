using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class PracticeFeesPoco
    {
        public Guid PracticeFeesID { get; set; }
        public Guid StatementSettingsID { get; set; }
        public string ChargeName { get; set; }
        public string ChargeText { get; set; }
        public decimal? Charge { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public PracticeFeesDomain MapToDomainModel()
        {
            PracticeFeesDomain domain = new PracticeFeesDomain
            {
                PracticeFeesID = PracticeFeesID,
                StatementSettingsID = StatementSettingsID,
                ChargeName = ChargeName,
                ChargeText = ChargeText,
                Charge = Charge,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public PracticeFeesPoco() { }

        public PracticeFeesPoco(PracticeFeesDomain domain)
        {
            PracticeFeesID = domain.PracticeFeesID;
            StatementSettingsID = domain.StatementSettingsID;
            ChargeName = domain.ChargeName;
            ChargeText = domain.ChargeText;
            Charge = domain.Charge;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
