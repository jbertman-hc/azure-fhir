using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SuperbillPoco
    {
        public int BillingID { get; set; }
        public int PatientID { get; set; }
        public DateTime DoS { get; set; }
        public string PoS { get; set; }
        public string Provider { get; set; }
        public string Complexity { get; set; }
        public string CC { get; set; }
        public string ICD0 { get; set; }
        public string ICD1 { get; set; }
        public string ICD2 { get; set; }
        public string ICD3 { get; set; }
        public string ICD4 { get; set; }
        public string ICD5 { get; set; }
        public string ICD6 { get; set; }
        public string ICD7 { get; set; }
        public string CPT0 { get; set; }
        public string CPT1 { get; set; }
        public string CPT2 { get; set; }
        public string CPT3 { get; set; }
        public string CPT4 { get; set; }
        public string CPT5 { get; set; }
        public string CPT6 { get; set; }
        public string CPT7 { get; set; }
        public decimal? Charge0 { get; set; }
        public decimal? Charge1 { get; set; }
        public decimal? Charge2 { get; set; }
        public decimal? Charge3 { get; set; }
        public decimal? Charge4 { get; set; }
        public decimal? Charge5 { get; set; }
        public decimal? Charge6 { get; set; }
        public decimal? Charge7 { get; set; }
        public string sbBillingComments { get; set; }
        public int? ProviderID { get; set; }
        public string Miscellaneous1 { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string LocFac { get; set; }

        public SuperbillDomain MapToDomainModel()
        {
            SuperbillDomain domain = new SuperbillDomain
            {
                BillingID = BillingID,
                PatientID = PatientID,
                DoS = DoS,
                PoS = PoS,
                Provider = Provider,
                Complexity = Complexity,
                CC = CC,
                ICD0 = ICD0,
                ICD1 = ICD1,
                ICD2 = ICD2,
                ICD3 = ICD3,
                ICD4 = ICD4,
                ICD5 = ICD5,
                ICD6 = ICD6,
                ICD7 = ICD7,
                CPT0 = CPT0,
                CPT1 = CPT1,
                CPT2 = CPT2,
                CPT3 = CPT3,
                CPT4 = CPT4,
                CPT5 = CPT5,
                CPT6 = CPT6,
                CPT7 = CPT7,
                Charge0 = Charge0,
                Charge1 = Charge1,
                Charge2 = Charge2,
                Charge3 = Charge3,
                Charge4 = Charge4,
                Charge5 = Charge5,
                Charge6 = Charge6,
                Charge7 = Charge7,
                sbBillingComments = sbBillingComments,
                ProviderID = ProviderID,
                Miscellaneous1 = Miscellaneous1,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                LocFac = LocFac
            };

            return domain;
        }

        public SuperbillPoco() { }

        public SuperbillPoco(SuperbillDomain domain)
        {
            BillingID = domain.BillingID;
            PatientID = domain.PatientID;
            DoS = domain.DoS;
            PoS = domain.PoS;
            Provider = domain.Provider;
            Complexity = domain.Complexity;
            CC = domain.CC;
            ICD0 = domain.ICD0;
            ICD1 = domain.ICD1;
            ICD2 = domain.ICD2;
            ICD3 = domain.ICD3;
            ICD4 = domain.ICD4;
            ICD5 = domain.ICD5;
            ICD6 = domain.ICD6;
            ICD7 = domain.ICD7;
            CPT0 = domain.CPT0;
            CPT1 = domain.CPT1;
            CPT2 = domain.CPT2;
            CPT3 = domain.CPT3;
            CPT4 = domain.CPT4;
            CPT5 = domain.CPT5;
            CPT6 = domain.CPT6;
            CPT7 = domain.CPT7;
            Charge0 = domain.Charge0;
            Charge1 = domain.Charge1;
            Charge2 = domain.Charge2;
            Charge3 = domain.Charge3;
            Charge4 = domain.Charge4;
            Charge5 = domain.Charge5;
            Charge6 = domain.Charge6;
            Charge7 = domain.Charge7;
            sbBillingComments = domain.sbBillingComments;
            ProviderID = domain.ProviderID;
            Miscellaneous1 = domain.Miscellaneous1;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            LocFac = domain.LocFac;
        }
    }
}
