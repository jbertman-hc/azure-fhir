using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class IgnoredInteractionsPoco
    {
        public int IgnoredInteractionsID { get; set; }
        public int PatientID { get; set; }
        public int? AllergyID { get; set; }
        public int? DrugID1 { get; set; }
        public int? DrugID2 { get; set; }
        public int? DiseaseID { get; set; }
        public bool? Pregnancy { get; set; }
        public bool? Exceedweight { get; set; }
        public int? OverrideReasonID { get; set; }
        public string OverrideReason { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string OverrideComments { get; set; }
        public string TypeString { get; set; }
        public string InteractionName { get; set; }

        public IgnoredInteractionsDomain MapToDomainModel()
        {
            IgnoredInteractionsDomain domain = new IgnoredInteractionsDomain
            {
                IgnoredInteractionsID = IgnoredInteractionsID,
                PatientID = PatientID,
                AllergyID = AllergyID,
                DrugID1 = DrugID1,
                DrugID2 = DrugID2,
                DiseaseID = DiseaseID,
                Pregnancy = Pregnancy,
                Exceedweight = Exceedweight,
                OverrideReasonID = OverrideReasonID,
                OverrideReason = OverrideReason,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                OverrideComments = OverrideComments,
                TypeString = TypeString,
                InteractionName = InteractionName
            };

            return domain;
        }

        public IgnoredInteractionsPoco() { }

        public IgnoredInteractionsPoco(IgnoredInteractionsDomain domain)
        {
            IgnoredInteractionsID = domain.IgnoredInteractionsID;
            PatientID = domain.PatientID;
            AllergyID = domain.AllergyID;
            DrugID1 = domain.DrugID1;
            DrugID2 = domain.DrugID2;
            DiseaseID = domain.DiseaseID;
            Pregnancy = domain.Pregnancy;
            Exceedweight = domain.Exceedweight;
            OverrideReasonID = domain.OverrideReasonID;
            OverrideReason = domain.OverrideReason;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            OverrideComments = domain.OverrideComments;
            TypeString = domain.TypeString;
            InteractionName = domain.InteractionName;
        }
    }
}
