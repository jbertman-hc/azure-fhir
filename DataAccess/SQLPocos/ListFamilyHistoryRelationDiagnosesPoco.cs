using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListFamilyHistoryRelationDiagnosesPoco
    {
        public int DiagnosisId { get; set; }
        public int FamilyHistoryRelationId { get; set; }
        public string DiagnosisCode { get; set; }
        public string Diagnosis { get; set; }
        public string DiagnosisDate { get; set; }
        public int? AgeAtDiagnosis { get; set; }
        public string AgeUnitAtDiagnosis { get; set; }
        public bool? WasDiagnosisCauseOfDeath { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string DiagnosisNotes { get; set; }
        public string SnomedCode { get; set; }

        public ListFamilyHistoryRelationDiagnosesDomain MapToDomainModel()
        {
            ListFamilyHistoryRelationDiagnosesDomain domain = new ListFamilyHistoryRelationDiagnosesDomain
            {
                DiagnosisId = DiagnosisId,
                FamilyHistoryRelationId = FamilyHistoryRelationId,
                DiagnosisCode = DiagnosisCode,
                Diagnosis = Diagnosis,
                DiagnosisDate = DiagnosisDate,
                AgeAtDiagnosis = AgeAtDiagnosis,
                AgeUnitAtDiagnosis = AgeUnitAtDiagnosis,
                WasDiagnosisCauseOfDeath = WasDiagnosisCauseOfDeath,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                DiagnosisNotes = DiagnosisNotes,
                SnomedCode = SnomedCode
            };

            return domain;
        }

        public ListFamilyHistoryRelationDiagnosesPoco() { }

        public ListFamilyHistoryRelationDiagnosesPoco(ListFamilyHistoryRelationDiagnosesDomain domain)
        {
            DiagnosisId = domain.DiagnosisId;
            FamilyHistoryRelationId = domain.FamilyHistoryRelationId;
            DiagnosisCode = domain.DiagnosisCode;
            Diagnosis = domain.Diagnosis;
            DiagnosisDate = domain.DiagnosisDate;
            AgeAtDiagnosis = domain.AgeAtDiagnosis;
            AgeUnitAtDiagnosis = domain.AgeUnitAtDiagnosis;
            WasDiagnosisCauseOfDeath = domain.WasDiagnosisCauseOfDeath;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            DiagnosisNotes = domain.DiagnosisNotes;
            SnomedCode = domain.SnomedCode;
        }
    }
}
