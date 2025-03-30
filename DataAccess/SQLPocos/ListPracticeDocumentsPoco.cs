using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListPracticeDocumentsPoco
    {
        public int ID { get; set; }
        public int? PatientID { get; set; }
        public int? PracticeDocID { get; set; }
        public string PracticeDocAlias { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsSigned { get; set; }

        public ListPracticeDocumentsDomain MapToDomainModel()
        {
            ListPracticeDocumentsDomain domain = new ListPracticeDocumentsDomain
            {
                ID = ID,
                PatientID = PatientID,
                PracticeDocID = PracticeDocID,
                PracticeDocAlias = PracticeDocAlias,
                LastTouchedBy = LastTouchedBy,
                DateLastTouched = DateLastTouched,
                DateRowAdded = DateRowAdded,
                DateModified = DateModified,
                IsSigned = IsSigned
            };

            return domain;
        }

        public ListPracticeDocumentsPoco() { }

        public ListPracticeDocumentsPoco(ListPracticeDocumentsDomain domain)
        {
            ID = domain.ID;
            PatientID = domain.PatientID;
            PracticeDocID = domain.PracticeDocID;
            PracticeDocAlias = domain.PracticeDocAlias;
            LastTouchedBy = domain.LastTouchedBy;
            DateLastTouched = domain.DateLastTouched;
            DateRowAdded = domain.DateRowAdded;
            DateModified = domain.DateModified;
            IsSigned = domain.IsSigned;
        }
    }
}
