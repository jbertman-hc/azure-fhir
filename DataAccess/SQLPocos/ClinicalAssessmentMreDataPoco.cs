using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ClinicalAssessmentMreDataPoco
    {
        public int PatientId { get; set; }
        public int? O2Sat { get; set; }
        public int? O2SatSuppAmount { get; set; }
        public int? O2SatAirSource { get; set; }
        public int? O2SatSuppType { get; set; }
        public int? SystolicBp { get; set; }
        public int? DiastolicBp { get; set; }
        public string Temperature { get; set; }
        public int? RespRate { get; set; }
        public int? Pulse { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string HeadCircumference { get; set; }
        public DateTime? Edd { get; set; }
        public DateTime? Lmp { get; set; }
        public float? Bmi { get; set; }
        public DateTime? TobaccoStatusStartDate { get; set; }
        public DateTime? TobaccoStatusEndDate { get; set; }
        public int? TobaccoPipeSmoker { get; set; }
        public DateTime? TobaccoPipeStartDate { get; set; }
        public DateTime? TobaccoPipeEndDate { get; set; }
        public int? TobaccoCigarSmoker { get; set; }
        public DateTime? TobaccoCigarStartDate { get; set; }
        public DateTime? TobaccoCigarEndDate { get; set; }
        public int? TobaccoChewing { get; set; }
        public DateTime? TobaccoChewingStartDate { get; set; }
        public DateTime? TobaccoChewingEndDate { get; set; }
        public string TobaccoCdcCode { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ClinicalAssessmentMreDataDomain MapToDomainModel()
        {
            ClinicalAssessmentMreDataDomain domain = new ClinicalAssessmentMreDataDomain
            {
                PatientId = PatientId,
                O2Sat = O2Sat,
                O2SatSuppAmount = O2SatSuppAmount,
                O2SatAirSource = O2SatAirSource,
                O2SatSuppType = O2SatSuppType,
                SystolicBp = SystolicBp,
                DiastolicBp = DiastolicBp,
                Temperature = Temperature,
                RespRate = RespRate,
                Pulse = Pulse,
                Weight = Weight,
                Height = Height,
                HeadCircumference = HeadCircumference,
                Edd = Edd,
                Lmp = Lmp,
                Bmi = Bmi,
                TobaccoStatusStartDate = TobaccoStatusStartDate,
                TobaccoStatusEndDate = TobaccoStatusEndDate,
                TobaccoPipeSmoker = TobaccoPipeSmoker,
                TobaccoPipeStartDate = TobaccoPipeStartDate,
                TobaccoPipeEndDate = TobaccoPipeEndDate,
                TobaccoCigarSmoker = TobaccoCigarSmoker,
                TobaccoCigarStartDate = TobaccoCigarStartDate,
                TobaccoCigarEndDate = TobaccoCigarEndDate,
                TobaccoChewing = TobaccoChewing,
                TobaccoChewingStartDate = TobaccoChewingStartDate,
                TobaccoChewingEndDate = TobaccoChewingEndDate,
                TobaccoCdcCode = TobaccoCdcCode,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ClinicalAssessmentMreDataPoco() { }

        public ClinicalAssessmentMreDataPoco(ClinicalAssessmentMreDataDomain domain)
        {
            PatientId = domain.PatientId;
            O2Sat = domain.O2Sat;
            O2SatSuppAmount = domain.O2SatSuppAmount;
            O2SatAirSource = domain.O2SatAirSource;
            O2SatSuppType = domain.O2SatSuppType;
            SystolicBp = domain.SystolicBp;
            DiastolicBp = domain.DiastolicBp;
            Temperature = domain.Temperature;
            RespRate = domain.RespRate;
            Pulse = domain.Pulse;
            Weight = domain.Weight;
            Height = domain.Height;
            HeadCircumference = domain.HeadCircumference;
            Edd = domain.Edd;
            Lmp = domain.Lmp;
            Bmi = domain.Bmi;
            TobaccoStatusStartDate = domain.TobaccoStatusStartDate;
            TobaccoStatusEndDate = domain.TobaccoStatusEndDate;
            TobaccoPipeSmoker = domain.TobaccoPipeSmoker;
            TobaccoPipeStartDate = domain.TobaccoPipeStartDate;
            TobaccoPipeEndDate = domain.TobaccoPipeEndDate;
            TobaccoCigarSmoker = domain.TobaccoCigarSmoker;
            TobaccoCigarStartDate = domain.TobaccoCigarStartDate;
            TobaccoCigarEndDate = domain.TobaccoCigarEndDate;
            TobaccoChewing = domain.TobaccoChewing;
            TobaccoChewingStartDate = domain.TobaccoChewingStartDate;
            TobaccoChewingEndDate = domain.TobaccoChewingEndDate;
            TobaccoCdcCode = domain.TobaccoCdcCode;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
