using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListProceduresPoco
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CodeType { get; set; }
        public string TypeName { get; set; }
        public DateTime? ProcedureDate { get; set; }
        public string Site { get; set; }
        public string Narrative { get; set; }
        public string Findings { get; set; }
        public int PatientId { get; set; }
        public int? PatientProcedurePerformerId { get; set; }
        public int? PendingFlag { get; set; }
        public DateTime? ImportedDate { get; set; }
        public string MoodCode { get; set; }
        public string StatusCode { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListProceduresDomain MapToDomainModel()
        {
            ListProceduresDomain domain = new ListProceduresDomain
            {
                Id = Id,
                Code = Code,
                CodeType = CodeType,
                TypeName = TypeName,
                ProcedureDate = ProcedureDate,
                Site = Site,
                Narrative = Narrative,
                Findings = Findings,
                PatientId = PatientId,
                PatientProcedurePerformerId = PatientProcedurePerformerId,
                PendingFlag = PendingFlag,
                ImportedDate = ImportedDate,
                MoodCode = MoodCode,
                StatusCode = StatusCode,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListProceduresPoco() { }

        public ListProceduresPoco(ListProceduresDomain domain)
        {
            Id = domain.Id;
            Code = domain.Code;
            CodeType = domain.CodeType;
            TypeName = domain.TypeName;
            ProcedureDate = domain.ProcedureDate;
            Site = domain.Site;
            Narrative = domain.Narrative;
            Findings = domain.Findings;
            PatientId = domain.PatientId;
            PatientProcedurePerformerId = domain.PatientProcedurePerformerId;
            PendingFlag = domain.PendingFlag;
            ImportedDate = domain.ImportedDate;
            MoodCode = domain.MoodCode;
            StatusCode = domain.StatusCode;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
