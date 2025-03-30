using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ClaimStatusServLinePoco
    {
        public Guid ClaimStatusServLineID { get; set; }
        public Guid ClaimStatusID { get; set; }
        public string CPTandMods { get; set; }
        public decimal? ChargeAMT { get; set; }
        public decimal? PaidAMT { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ClaimStatusServLineDomain MapToDomainModel()
        {
            ClaimStatusServLineDomain domain = new ClaimStatusServLineDomain
            {
                ClaimStatusServLineID = ClaimStatusServLineID,
                ClaimStatusID = ClaimStatusID,
                CPTandMods = CPTandMods,
                ChargeAMT = ChargeAMT,
                PaidAMT = PaidAMT,
                EffectiveDate = EffectiveDate,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ClaimStatusServLinePoco() { }

        public ClaimStatusServLinePoco(ClaimStatusServLineDomain domain)
        {
            ClaimStatusServLineID = domain.ClaimStatusServLineID;
            ClaimStatusID = domain.ClaimStatusID;
            CPTandMods = domain.CPTandMods;
            ChargeAMT = domain.ChargeAMT;
            PaidAMT = domain.PaidAMT;
            EffectiveDate = domain.EffectiveDate;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
