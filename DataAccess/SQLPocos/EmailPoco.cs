using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class EmailPoco
    {
        public int msgID { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public DateTime Date { get; set; }
        public string Re { get; set; }
        public string CC { get; set; }
        public string PatientName { get; set; }
        public string Body { get; set; }
        public int? PatientID { get; set; }
        public string Chart { get; set; }
        public string Chief { get; set; }
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
        public string SBP { get; set; }
        public string DBP { get; set; }
        public string Temp { get; set; }
        public string RR { get; set; }
        public string Pulse { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string HC { get; set; }
        public string VitalComments { get; set; }
        public string Test1 { get; set; }
        public string TestResult1 { get; set; }
        public string TestDate1 { get; set; }
        public string Test2 { get; set; }
        public string TestResult2 { get; set; }
        public string TestDate2 { get; set; }
        public string Test3 { get; set; }
        public string TestResult3 { get; set; }
        public string TestDate3 { get; set; }
        public string Test4 { get; set; }
        public string TestResult4 { get; set; }
        public string TestDate4 { get; set; }
        public string Test5 { get; set; }
        public string TestResult5 { get; set; }
        public string TestDate5 { get; set; }
        public string Test6 { get; set; }
        public string TestResult6 { get; set; }
        public string TestDate6 { get; set; }
        public string Test7 { get; set; }
        public string TestResult7 { get; set; }
        public string TestDate7 { get; set; }
        public string Test8 { get; set; }
        public string TestResult8 { get; set; }
        public string TestDate8 { get; set; }
        public string Test9 { get; set; }
        public string TestResult9 { get; set; }
        public string TestDate9 { get; set; }
        public string Test10 { get; set; }
        public string TestResult10 { get; set; }
        public string TestDate10 { get; set; }
        public string Test11 { get; set; }
        public string TestResult11 { get; set; }
        public string TestDate11 { get; set; }
        public string Test12 { get; set; }
        public string TestResult12 { get; set; }
        public string TestDate12 { get; set; }
        public string Test13 { get; set; }
        public string TestResult13 { get; set; }
        public string TestDate13 { get; set; }
        public string Test14 { get; set; }
        public string TestResult14 { get; set; }
        public string TestDate14 { get; set; }
        public string CallBackComment { get; set; }
        public string Image1Location { get; set; }
        public string Image2Location { get; set; }
        public string Image1LocationOld { get; set; }
        public string Image2LocationOld { get; set; }
        public string Image1Desc { get; set; }
        public string Image2Desc { get; set; }
        public bool PicForwarded1 { get; set; }
        public bool PicForwarded2 { get; set; }
        public bool NoteTypeCoSign { get; set; }
        public bool NoteTypeRefill { get; set; }
        public DateTime? EncounterDate { get; set; }
        public string Illustration1Loc { get; set; }
        public string Illustration2Loc { get; set; }
        public string Illustration1Desc { get; set; }
        public string Illustration2Desc { get; set; }
        public int? LabTestID { get; set; }
        public int? LabSignOffID { get; set; }
        public DateTime? LabSignOffDt { get; set; }
        public int? ImportItemID { get; set; }
        public bool DeleteItem { get; set; }
        public string MsgHighlightColor { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public int? Sat { get; set; }
        public int? Pain { get; set; }
        public int? PF { get; set; }
        public string OtherVS { get; set; }
        public string ConfidentialInformation { get; set; }
        public string ActiveTemplates { get; set; }
        public bool? TaskComplete { get; set; }
        public string MetaData { get; set; }
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
        public string Link { get; set; }
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
        public bool? IsFromCICO { get; set; }
        public bool? DeclinedClinicalSummary { get; set; }
        public string Instructions { get; set; }
        public bool IsIcd10 { get; set; }
        public bool? LbsOzMode { get; set; }

        public EmailDomain MapToDomainModel()
        {
            EmailDomain domain = new EmailDomain
            {
                msgID = msgID,
                To = To,
                From = From,
                Date = Date,
                Re = Re,
                CC = CC,
                PatientName = PatientName,
                Body = Body,
                PatientID = PatientID,
                Chart = Chart,
                Chief = Chief,
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
                SBP = SBP,
                DBP = DBP,
                Temp = Temp,
                RR = RR,
                Pulse = Pulse,
                Weight = Weight,
                Height = Height,
                HC = HC,
                VitalComments = VitalComments,
                Test1 = Test1,
                TestResult1 = TestResult1,
                TestDate1 = TestDate1,
                Test2 = Test2,
                TestResult2 = TestResult2,
                TestDate2 = TestDate2,
                Test3 = Test3,
                TestResult3 = TestResult3,
                TestDate3 = TestDate3,
                Test4 = Test4,
                TestResult4 = TestResult4,
                TestDate4 = TestDate4,
                Test5 = Test5,
                TestResult5 = TestResult5,
                TestDate5 = TestDate5,
                Test6 = Test6,
                TestResult6 = TestResult6,
                TestDate6 = TestDate6,
                Test7 = Test7,
                TestResult7 = TestResult7,
                TestDate7 = TestDate7,
                Test8 = Test8,
                TestResult8 = TestResult8,
                TestDate8 = TestDate8,
                Test9 = Test9,
                TestResult9 = TestResult9,
                TestDate9 = TestDate9,
                Test10 = Test10,
                TestResult10 = TestResult10,
                TestDate10 = TestDate10,
                Test11 = Test11,
                TestResult11 = TestResult11,
                TestDate11 = TestDate11,
                Test12 = Test12,
                TestResult12 = TestResult12,
                TestDate12 = TestDate12,
                Test13 = Test13,
                TestResult13 = TestResult13,
                TestDate13 = TestDate13,
                Test14 = Test14,
                TestResult14 = TestResult14,
                TestDate14 = TestDate14,
                CallBackComment = CallBackComment,
                Image1Location = Image1Location,
                Image2Location = Image2Location,
                Image1LocationOld = Image1LocationOld,
                Image2LocationOld = Image2LocationOld,
                Image1Desc = Image1Desc,
                Image2Desc = Image2Desc,
                PicForwarded1 = PicForwarded1,
                PicForwarded2 = PicForwarded2,
                NoteTypeCoSign = NoteTypeCoSign,
                NoteTypeRefill = NoteTypeRefill,
                EncounterDate = EncounterDate,
                Illustration1Loc = Illustration1Loc,
                Illustration2Loc = Illustration2Loc,
                Illustration1Desc = Illustration1Desc,
                Illustration2Desc = Illustration2Desc,
                LabTestID = LabTestID,
                LabSignOffID = LabSignOffID,
                LabSignOffDt = LabSignOffDt,
                ImportItemID = ImportItemID,
                DeleteItem = DeleteItem,
                MsgHighlightColor = MsgHighlightColor,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Sat = Sat,
                Pain = Pain,
                PF = PF,
                OtherVS = OtherVS,
                ConfidentialInformation = ConfidentialInformation,
                ActiveTemplates = ActiveTemplates,
                TaskComplete = TaskComplete,
                MetaData = MetaData,
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
                Link = Link,
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
                IsFromCICO = IsFromCICO,
                DeclinedClinicalSummary = DeclinedClinicalSummary,
                Instructions = Instructions,
                IsIcd10 = IsIcd10,
                LbsOzMode = LbsOzMode
            };

            return domain;
        }

        public EmailPoco() { }

        public EmailPoco(EmailDomain domain)
        {
            msgID = domain.msgID;
            To = domain.To;
            From = domain.From;
            Date = domain.Date;
            Re = domain.Re;
            CC = domain.CC;
            PatientName = domain.PatientName;
            Body = domain.Body;
            PatientID = domain.PatientID;
            Chart = domain.Chart;
            Chief = domain.Chief;
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
            SBP = domain.SBP;
            DBP = domain.DBP;
            Temp = domain.Temp;
            RR = domain.RR;
            Pulse = domain.Pulse;
            Weight = domain.Weight;
            Height = domain.Height;
            HC = domain.HC;
            VitalComments = domain.VitalComments;
            Test1 = domain.Test1;
            TestResult1 = domain.TestResult1;
            TestDate1 = domain.TestDate1;
            Test2 = domain.Test2;
            TestResult2 = domain.TestResult2;
            TestDate2 = domain.TestDate2;
            Test3 = domain.Test3;
            TestResult3 = domain.TestResult3;
            TestDate3 = domain.TestDate3;
            Test4 = domain.Test4;
            TestResult4 = domain.TestResult4;
            TestDate4 = domain.TestDate4;
            Test5 = domain.Test5;
            TestResult5 = domain.TestResult5;
            TestDate5 = domain.TestDate5;
            Test6 = domain.Test6;
            TestResult6 = domain.TestResult6;
            TestDate6 = domain.TestDate6;
            Test7 = domain.Test7;
            TestResult7 = domain.TestResult7;
            TestDate7 = domain.TestDate7;
            Test8 = domain.Test8;
            TestResult8 = domain.TestResult8;
            TestDate8 = domain.TestDate8;
            Test9 = domain.Test9;
            TestResult9 = domain.TestResult9;
            TestDate9 = domain.TestDate9;
            Test10 = domain.Test10;
            TestResult10 = domain.TestResult10;
            TestDate10 = domain.TestDate10;
            Test11 = domain.Test11;
            TestResult11 = domain.TestResult11;
            TestDate11 = domain.TestDate11;
            Test12 = domain.Test12;
            TestResult12 = domain.TestResult12;
            TestDate12 = domain.TestDate12;
            Test13 = domain.Test13;
            TestResult13 = domain.TestResult13;
            TestDate13 = domain.TestDate13;
            Test14 = domain.Test14;
            TestResult14 = domain.TestResult14;
            TestDate14 = domain.TestDate14;
            CallBackComment = domain.CallBackComment;
            Image1Location = domain.Image1Location;
            Image2Location = domain.Image2Location;
            Image1LocationOld = domain.Image1LocationOld;
            Image2LocationOld = domain.Image2LocationOld;
            Image1Desc = domain.Image1Desc;
            Image2Desc = domain.Image2Desc;
            PicForwarded1 = domain.PicForwarded1;
            PicForwarded2 = domain.PicForwarded2;
            NoteTypeCoSign = domain.NoteTypeCoSign;
            NoteTypeRefill = domain.NoteTypeRefill;
            EncounterDate = domain.EncounterDate;
            Illustration1Loc = domain.Illustration1Loc;
            Illustration2Loc = domain.Illustration2Loc;
            Illustration1Desc = domain.Illustration1Desc;
            Illustration2Desc = domain.Illustration2Desc;
            LabTestID = domain.LabTestID;
            LabSignOffID = domain.LabSignOffID;
            LabSignOffDt = domain.LabSignOffDt;
            ImportItemID = domain.ImportItemID;
            DeleteItem = domain.DeleteItem;
            MsgHighlightColor = domain.MsgHighlightColor;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Sat = domain.Sat;
            Pain = domain.Pain;
            PF = domain.PF;
            OtherVS = domain.OtherVS;
            ConfidentialInformation = domain.ConfidentialInformation;
            ActiveTemplates = domain.ActiveTemplates;
            TaskComplete = domain.TaskComplete;
            MetaData = domain.MetaData;
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
            Link = domain.Link;
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
            IsFromCICO = domain.IsFromCICO;
            DeclinedClinicalSummary = domain.DeclinedClinicalSummary;
            Instructions = domain.Instructions;
            IsIcd10 = domain.IsIcd10;
            LbsOzMode = domain.LbsOzMode;
        }
    }
}
