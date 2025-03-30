using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LabNotesPoco
    {
        public int LabNoteID { get; set; }
        public int? LabTestID { get; set; }
        public int? LabResultID { get; set; }
        public int? LabResultDetailID { get; set; }
        public int? LabOrderID { get; set; }
        public int? LabOrderDetailID { get; set; }
        public DateTime CreatedDt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdDt { get; set; }
        public int? LastUpdBy { get; set; }
        public string OwnerType { get; set; }
        public int? OwnerID { get; set; }
        public short? NoteSeqNbr { get; set; }
        public string NoteText { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? Replaced { get; set; }

        public LabNotesDomain MapToDomainModel()
        {
            LabNotesDomain domain = new LabNotesDomain
            {
                LabNoteID = LabNoteID,
                LabTestID = LabTestID,
                LabResultID = LabResultID,
                LabResultDetailID = LabResultDetailID,
                LabOrderID = LabOrderID,
                LabOrderDetailID = LabOrderDetailID,
                CreatedDt = CreatedDt,
                CreatedBy = CreatedBy,
                LastUpdDt = LastUpdDt,
                LastUpdBy = LastUpdBy,
                OwnerType = OwnerType,
                OwnerID = OwnerID,
                NoteSeqNbr = NoteSeqNbr,
                NoteText = NoteText,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Replaced = Replaced
            };

            return domain;
        }

        public LabNotesPoco() { }

        public LabNotesPoco(LabNotesDomain domain)
        {
            LabNoteID = domain.LabNoteID;
            LabTestID = domain.LabTestID;
            LabResultID = domain.LabResultID;
            LabResultDetailID = domain.LabResultDetailID;
            LabOrderID = domain.LabOrderID;
            LabOrderDetailID = domain.LabOrderDetailID;
            CreatedDt = domain.CreatedDt;
            CreatedBy = domain.CreatedBy;
            LastUpdDt = domain.LastUpdDt;
            LastUpdBy = domain.LastUpdBy;
            OwnerType = domain.OwnerType;
            OwnerID = domain.OwnerID;
            NoteSeqNbr = domain.NoteSeqNbr;
            NoteText = domain.NoteText;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Replaced = domain.Replaced;
        }
    }
}
