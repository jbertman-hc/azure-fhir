using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListRiskFactorsPoco
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int RiskFactorID { get; set; }
        public string RiskFactorName { get; set; }
        public string Provider { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListRiskFactorsDomain MapToDomainModel()
        {
            ListRiskFactorsDomain domain = new ListRiskFactorsDomain
            {
                ID = ID,
                PatientID = PatientID,
                RiskFactorID = RiskFactorID,
                RiskFactorName = RiskFactorName,
                Provider = Provider,
                LastTouchedBy = LastTouchedBy,
                DateLastTouched = DateLastTouched,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListRiskFactorsPoco() { }

        public ListRiskFactorsPoco(ListRiskFactorsDomain domain)
        {
            ID = domain.ID;
            PatientID = domain.PatientID;
            RiskFactorID = domain.RiskFactorID;
            RiskFactorName = domain.RiskFactorName;
            Provider = domain.Provider;
            LastTouchedBy = domain.LastTouchedBy;
            DateLastTouched = domain.DateLastTouched;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
