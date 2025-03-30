using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListOrderDiagnosesPoco
    {
        public int ListOrderDiagnosisId { get; set; }
        public int ListOrderId { get; set; }
        public string IcdCode { get; set; }
        public string CostarCode { get; set; }
        public string ProblemDescription { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListOrderDiagnosesDomain MapToDomainModel()
        {
            ListOrderDiagnosesDomain domain = new ListOrderDiagnosesDomain
            {
                ListOrderDiagnosisId = ListOrderDiagnosisId,
                ListOrderId = ListOrderId,
                IcdCode = IcdCode,
                CostarCode = CostarCode,
                ProblemDescription = ProblemDescription,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListOrderDiagnosesPoco() { }

        public ListOrderDiagnosesPoco(ListOrderDiagnosesDomain domain)
        {
            ListOrderDiagnosisId = domain.ListOrderDiagnosisId;
            ListOrderId = domain.ListOrderId;
            IcdCode = domain.IcdCode;
            CostarCode = domain.CostarCode;
            ProblemDescription = domain.ProblemDescription;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
