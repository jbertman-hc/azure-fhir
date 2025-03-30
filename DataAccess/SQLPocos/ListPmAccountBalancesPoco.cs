using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListPmAccountBalancesPoco
    {
        public int PmAccountBalancesId { get; set; }
        public int PmAccountId { get; set; }
        public decimal? InsuranceBalance { get; set; }
        public decimal? PatientBalance { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListPmAccountBalancesDomain MapToDomainModel()
        {
            ListPmAccountBalancesDomain domain = new ListPmAccountBalancesDomain
            {
                PmAccountBalancesId = PmAccountBalancesId,
                PmAccountId = PmAccountId,
                InsuranceBalance = InsuranceBalance,
                PatientBalance = PatientBalance,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListPmAccountBalancesPoco() { }

        public ListPmAccountBalancesPoco(ListPmAccountBalancesDomain domain)
        {
            PmAccountBalancesId = domain.PmAccountBalancesId;
            PmAccountId = domain.PmAccountId;
            InsuranceBalance = domain.InsuranceBalance;
            PatientBalance = domain.PatientBalance;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
