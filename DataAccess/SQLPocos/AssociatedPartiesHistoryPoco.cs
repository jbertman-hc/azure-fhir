using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class AssociatedPartiesHistoryPoco
    {
        public int APHistoryID { get; set; }
        public Guid? AssociatedPartiesID { get; set; }
        public int? RelationPatientID { get; set; }
        public bool? IsPatient { get; set; }
        public int? FieldID { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime? DateEdited { get; set; }
        public string EditedBy { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string LastTouchedBy { get; set; }

        public AssociatedPartiesHistoryDomain MapToDomainModel()
        {
            AssociatedPartiesHistoryDomain domain = new AssociatedPartiesHistoryDomain
            {
                APHistoryID = APHistoryID,
                AssociatedPartiesID = AssociatedPartiesID,
                RelationPatientID = RelationPatientID,
                IsPatient = IsPatient,
                FieldID = FieldID,
                OldValue = OldValue,
                NewValue = NewValue,
                DateEdited = DateEdited,
                EditedBy = EditedBy,
                DateLastTouched = DateLastTouched,
                DateRowAdded = DateRowAdded,
                LastTouchedBy = LastTouchedBy
            };

            return domain;
        }

        public AssociatedPartiesHistoryPoco() { }

        public AssociatedPartiesHistoryPoco(AssociatedPartiesHistoryDomain domain)
        {
            APHistoryID = domain.APHistoryID;
            AssociatedPartiesID = domain.AssociatedPartiesID;
            RelationPatientID = domain.RelationPatientID;
            IsPatient = domain.IsPatient;
            FieldID = domain.FieldID;
            OldValue = domain.OldValue;
            NewValue = domain.NewValue;
            DateEdited = domain.DateEdited;
            EditedBy = domain.EditedBy;
            DateLastTouched = domain.DateLastTouched;
            DateRowAdded = domain.DateRowAdded;
            LastTouchedBy = domain.LastTouchedBy;
        }
    }
}

