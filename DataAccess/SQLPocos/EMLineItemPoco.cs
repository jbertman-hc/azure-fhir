using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class EMLineItemPoco
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int BillingID { get; set; }
        public string CPT { get; set; }
        public string ICDprimary { get; set; }
        public string ICDsecondary { get; set; }
        public string ICDtertiary { get; set; }
        public string ICDquaternary { get; set; }
        public string modifier { get; set; }
        public decimal? Price { get; set; }
        public string BillingComments { get; set; }
        public string SavedBy { get; set; }
        public DateTime SavedDate { get; set; }
        public int? units { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public EMLineItemDomain MapToDomainModel()
        {
            EMLineItemDomain domain = new EMLineItemDomain
            {
                ID = ID,
                PatientID = PatientID,
                BillingID = BillingID,
                CPT = CPT,
                ICDprimary = ICDprimary,
                ICDsecondary = ICDsecondary,
                ICDtertiary = ICDtertiary,
                ICDquaternary = ICDquaternary,
                modifier = modifier,
                Price = Price,
                BillingComments = BillingComments,
                SavedBy = SavedBy,
                SavedDate = SavedDate,
                units = units,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public EMLineItemPoco() { }

        public EMLineItemPoco(EMLineItemDomain domain)
        {
            ID = domain.ID;
            PatientID = domain.PatientID;
            BillingID = domain.BillingID;
            CPT = domain.CPT;
            ICDprimary = domain.ICDprimary;
            ICDsecondary = domain.ICDsecondary;
            ICDtertiary = domain.ICDtertiary;
            ICDquaternary = domain.ICDquaternary;
            modifier = domain.modifier;
            Price = domain.Price;
            BillingComments = domain.BillingComments;
            SavedBy = domain.SavedBy;
            SavedDate = domain.SavedDate;
            units = domain.units;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
