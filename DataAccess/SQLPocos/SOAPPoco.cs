using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SOAPPoco
    {
        public int VisitNumber { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public DateTime EncounterDate { get; set; }
        public string CC { get; set; }
        public string HPI { get; set; }
        public string ROS { get; set; }
        public string PMH { get; set; }
        public string Meds { get; set; }
        public string Allergies { get; set; }
        public string SH { get; set; }
        public string FH { get; set; }
        public string PE { get; set; }
        public string Ass { get; set; }
        public string Plan { get; set; }
        public string BP { get; set; }
        public string Temp { get; set; }
        public string RR { get; set; }
        public string Pulse { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string BMI { get; set; }
        public string HC { get; set; }
        public string VitalComments { get; set; }
        public string VisitCode { get; set; }
        public int? ProviderID { get; set; }
        public string ProviderSignature { get; set; }
        public string lblAgeGenderSentence { get; set; }
        public string CallBackComment { get; set; }
        public string CPTcode { get; set; }
        public string CPTcomments { get; set; }
        public string Tests { get; set; }
        public string Image1Location { get; set; }
        public string Image2Location { get; set; }
        public string Image1LocationOLD { get; set; }
        public string Image2LocationOLD { get; set; }
        public string Image1Desc { get; set; }
        public string Image2Desc { get; set; }
        public string Illustration1Loc { get; set; }
        public string Illustration2Loc { get; set; }
        public string Illustration1Desc { get; set; }
        public string Illustration2Desc { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public int? Sat { get; set; }
        public int? Pain { get; set; }
        public int? PF { get; set; }
        public string OtherVS { get; set; }
        public string ConfidentialInformation { get; set; }
        public int? SatAirSource { get; set; }
        public int? SatSuppO2Amount { get; set; }
        public int? PFPost { get; set; }
        public int? SatSuppO2Type { get; set; }
        public decimal? PacksPerDay { get; set; }
        public decimal? YearsSmoked { get; set; }
        public decimal? YearsQuit { get; set; }
        public bool? IsResourceProvided { get; set; }
        public string TobaccoCDCCode { get; set; }
        public bool? IsMedReconciled { get; set; }
        public bool? IsPatientTransfer { get; set; }
        public DateTime? LastMenstrualPeriod { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string PregnancyComments { get; set; }
        public string VisionOS { get; set; }
        public string VisionOD { get; set; }
        public string Hearing { get; set; }
        public string HearingComments { get; set; }
        public int? SBPSupine { get; set; }
        public int? DBPSupine { get; set; }
        public DateTime? TobaccoStatusStartDate { get; set; }
        public DateTime? TobaccoStatusEndDate { get; set; }
        public bool? TobaccoPipeSmoker { get; set; }
        public DateTime? TobaccoPipeStartDate { get; set; }
        public DateTime? TobaccoPipeEndDate { get; set; }
        public bool? TobaccoCigarSmoker { get; set; }
        public DateTime? TobaccoCigarStartDate { get; set; }
        public DateTime? TobaccoCigarEndDate { get; set; }
        public bool? TobaccoChewing { get; set; }
        public DateTime? TobaccoChewingStartDate { get; set; }
        public DateTime? TobaccoChewingEndDate { get; set; }
        public bool? DeclinedClinicalSummary { get; set; }
        public string Instructions { get; set; }
        public bool IsIcd10 { get; set; }

        public SOAPDomain MapToDomainModel()
        {
            SOAPDomain domain = new SOAPDomain
            {
                VisitNumber = VisitNumber,
                PatientID = PatientID,
                PatientName = PatientName,
                EncounterDate = EncounterDate,
                CC = CC,
                HPI = HPI,
                ROS = ROS,
                PMH = PMH,
                Meds = Meds,
                Allergies = Allergies,
                SH = SH,
                FH = FH,
                PE = PE,
                Ass = Ass,
                Plan = Plan,
                BP = BP,
                Temp = Temp,
                RR = RR,
                Pulse = Pulse,
                Weight = Weight,
                Height = Height,
                BMI = BMI,
                HC = HC,
                VitalComments = VitalComments,
                VisitCode = VisitCode,
                ProviderID = ProviderID,
                ProviderSignature = ProviderSignature,
                lblAgeGenderSentence = lblAgeGenderSentence,
                CallBackComment = CallBackComment,
                CPTcode = CPTcode,
                CPTcomments = CPTcomments,
                Tests = Tests,
                Image1Location = Image1Location,
                Image2Location = Image2Location,
                Image1LocationOLD = Image1LocationOLD,
                Image2LocationOLD = Image2LocationOLD,
                Image1Desc = Image1Desc,
                Image2Desc = Image2Desc,
                Illustration1Loc = Illustration1Loc,
                Illustration2Loc = Illustration2Loc,
                Illustration1Desc = Illustration1Desc,
                Illustration2Desc = Illustration2Desc,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Sat = Sat,
                Pain = Pain,
                PF = PF,
                OtherVS = OtherVS,
                ConfidentialInformation = ConfidentialInformation,
                SatAirSource = SatAirSource,
                SatSuppO2Amount = SatSuppO2Amount,
                PFPost = PFPost,
                SatSuppO2Type = SatSuppO2Type,
                PacksPerDay = PacksPerDay,
                YearsSmoked = YearsSmoked,
                YearsQuit = YearsQuit,
                IsResourceProvided = IsResourceProvided,
                TobaccoCDCCode = TobaccoCDCCode,
                IsMedReconciled = IsMedReconciled,
                IsPatientTransfer = IsPatientTransfer,
                LastMenstrualPeriod = LastMenstrualPeriod,
                EstimatedDeliveryDate = EstimatedDeliveryDate,
                PregnancyComments = PregnancyComments,
                VisionOS = VisionOS,
                VisionOD = VisionOD,
                Hearing = Hearing,
                HearingComments = HearingComments,
                SBPSupine = SBPSupine,
                DBPSupine = DBPSupine,
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
                DeclinedClinicalSummary = DeclinedClinicalSummary,
                Instructions = Instructions,
                IsIcd10 = IsIcd10
            };

            return domain;
        }

        public SOAPPoco() { }

        public SOAPPoco(SOAPDomain domain)
        {
            VisitNumber = domain.VisitNumber;
            PatientID = domain.PatientID;
            PatientName = domain.PatientName;
            EncounterDate = domain.EncounterDate;
            CC = domain.CC;
            HPI = domain.HPI;
            ROS = domain.ROS;
            PMH = domain.PMH;
            Meds = domain.Meds;
            Allergies = domain.Allergies;
            SH = domain.SH;
            FH = domain.FH;
            PE = domain.PE;
            Ass = domain.Ass;
            Plan = domain.Plan;
            BP = domain.BP;
            Temp = domain.Temp;
            RR = domain.RR;
            Pulse = domain.Pulse;
            Weight = domain.Weight;
            Height = domain.Height;
            BMI = domain.BMI;
            HC = domain.HC;
            VitalComments = domain.VitalComments;
            VisitCode = domain.VisitCode;
            ProviderID = domain.ProviderID;
            ProviderSignature = domain.ProviderSignature;
            lblAgeGenderSentence = domain.lblAgeGenderSentence;
            CallBackComment = domain.CallBackComment;
            CPTcode = domain.CPTcode;
            CPTcomments = domain.CPTcomments;
            Tests = domain.Tests;
            Image1Location = domain.Image1Location;
            Image2Location = domain.Image2Location;
            Image1LocationOLD = domain.Image1LocationOLD;
            Image2LocationOLD = domain.Image2LocationOLD;
            Image1Desc = domain.Image1Desc;
            Image2Desc = domain.Image2Desc;
            Illustration1Loc = domain.Illustration1Loc;
            Illustration2Loc = domain.Illustration2Loc;
            Illustration1Desc = domain.Illustration1Desc;
            Illustration2Desc = domain.Illustration2Desc;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Sat = domain.Sat;
            Pain = domain.Pain;
            PF = domain.PF;
            OtherVS = domain.OtherVS;
            ConfidentialInformation = domain.ConfidentialInformation;
            SatAirSource = domain.SatAirSource;
            SatSuppO2Amount = domain.SatSuppO2Amount;
            PFPost = domain.PFPost;
            SatSuppO2Type = domain.SatSuppO2Type;
            PacksPerDay = domain.PacksPerDay;
            YearsSmoked = domain.YearsSmoked;
            YearsQuit = domain.YearsQuit;
            IsResourceProvided = domain.IsResourceProvided;
            TobaccoCDCCode = domain.TobaccoCDCCode;
            IsMedReconciled = domain.IsMedReconciled;
            IsPatientTransfer = domain.IsPatientTransfer;
            LastMenstrualPeriod = domain.LastMenstrualPeriod;
            EstimatedDeliveryDate = domain.EstimatedDeliveryDate;
            PregnancyComments = domain.PregnancyComments;
            VisionOS = domain.VisionOS;
            VisionOD = domain.VisionOD;
            Hearing = domain.Hearing;
            HearingComments = domain.HearingComments;
            SBPSupine = domain.SBPSupine;
            DBPSupine = domain.DBPSupine;
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
            DeclinedClinicalSummary = domain.DeclinedClinicalSummary;
            Instructions = domain.Instructions;
            IsIcd10 = domain.IsIcd10;
        }
    }
}
