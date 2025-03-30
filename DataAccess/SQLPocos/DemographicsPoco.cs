using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class DemographicsPoco
    {
        public int PatientID { get; set; }
        public string ChartID { get; set; }
        public string Salutation { get; set; }
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
        public string Suffix { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string SS { get; set; }
        public string PatientAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string WorkPhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string EmployerName { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string SpouseName { get; set; }
        public string InsuranceType { get; set; }
        public string PatientRel { get; set; }
        public string InsuredPlanName { get; set; }
        public string InsuredIDNo { get; set; }
        public string InsuredName { get; set; }
        public string InsuredGroupNo { get; set; }
        public string Copay { get; set; }
        public string InsuraceNotes { get; set; }
        public string InsuredPlanName2 { get; set; }
        public string InsuredIDNo2 { get; set; }
        public string InsuredName2 { get; set; }
        public string InsuredGroupNo2 { get; set; }
        public string Copay2 { get; set; }
        public string Comments { get; set; }
        public string RecordsReleased { get; set; }
        public string Referredby { get; set; }
        public string ReferredbyMore { get; set; }
        public bool Inactive { get; set; }
        public string ReasonInactive { get; set; }
        public string PreferredPhysician { get; set; }
        public string PreferredPharmacy { get; set; }
        public string UserLog { get; set; }
        public DateTime? DateAdded { get; set; }
        public string Photo { get; set; }
        public byte[] Picture { get; set; }
        public string ReferringDoc { get; set; }
        public string ReferringNumber { get; set; }
        public string InsuredsDOB { get; set; }
        public string InsAddL1 { get; set; }
        public string InsAddL2 { get; set; }
        public string InsAddCity { get; set; }
        public string InsAddState { get; set; }
        public string InsAddZip { get; set; }
        public string InsAddPhone { get; set; }
        public string Insureds2DOB { get; set; }
        public string Ins2AddL1 { get; set; }
        public string Ins2AddL2 { get; set; }
        public string Ins2AddCity { get; set; }
        public string Ins2AddState { get; set; }
        public string Ins2AddZip { get; set; }
        public string Ins2AddPhone { get; set; }
        public string Miscellaneous1 { get; set; }
        public string Miscellaneous2 { get; set; }
        public string Miscellaneous3 { get; set; }
        public string Miscellaneous4 { get; set; }
        public string MaritalStatus { get; set; }
        public string AllergiesDemo { get; set; }
        public string ImageName { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool ExemptFromReporting { get; set; }
        public bool? TakesNoMeds { get; set; }
        public string PatientRace { get; set; }
        public string ExemptFromReportingReason { get; set; }
        public string InsuranceNotes2 { get; set; }
        public string PatientRel2 { get; set; }
        public string PatientAddress2 { get; set; }
        public Guid PatientGUID { get; set; }
        public string LanguagePreference { get; set; }
        public string BarriersToCommunication { get; set; }
        public string MiddleName { get; set; }
        public string ContactPreference { get; set; }
        public int? EthnicityID { get; set; }
        public bool? HasNoActiveDiagnoses { get; set; }
        public DateTime? VFCInitialScreen { get; set; }
        public DateTime? VFCLastScreen { get; set; }
        public int? VFCReasonID { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public int? StatementDeliveryMethod { get; set; }
        public bool IsPayorConverted { get; set; }
        public bool IsPayorConverted2 { get; set; }
        public string MothersMaidenName { get; set; }
        public int? BirthOrder { get; set; }
        public DateTime? DateTimePatientInactivated { get; set; }
        public string PublicityCode { get; set; }
        public DateTime? PublicityCodeEffectiveDate { get; set; }
        public string MothersFirstName { get; set; }
        public string AcPmAccountId { get; set; }

        public DemographicsDomain MapToDomainModel()
        {
            DemographicsDomain domain = new DemographicsDomain
            {
                PatientID = PatientID,
                ChartID = ChartID,
                Salutation = Salutation,
                First = First,
                Middle = Middle,
                Last = Last,
                Suffix = Suffix,
                Gender = Gender,
                BirthDate = BirthDate,
                SS = SS,
                PatientAddress = PatientAddress,
                City = City,
                State = State,
                Zip = Zip,
                Phone = Phone,
                WorkPhone = WorkPhone,
                Fax = Fax,
                Email = Email,
                EmployerName = EmployerName,
                EmergencyContactName = EmergencyContactName,
                EmergencyContactPhone = EmergencyContactPhone,
                SpouseName = SpouseName,
                InsuranceType = InsuranceType,
                PatientRel = PatientRel,
                InsuredPlanName = InsuredPlanName,
                InsuredIDNo = InsuredIDNo,
                InsuredName = InsuredName,
                InsuredGroupNo = InsuredGroupNo,
                Copay = Copay,
                InsuraceNotes = InsuraceNotes,
                InsuredPlanName2 = InsuredPlanName2,
                InsuredIDNo2 = InsuredIDNo2,
                InsuredName2 = InsuredName2,
                InsuredGroupNo2 = InsuredGroupNo2,
                Copay2 = Copay2,
                Comments = Comments,
                RecordsReleased = RecordsReleased,
                Referredby = Referredby,
                ReferredbyMore = ReferredbyMore,
                Inactive = Inactive,
                ReasonInactive = ReasonInactive,
                PreferredPhysician = PreferredPhysician,
                PreferredPharmacy = PreferredPharmacy,
                UserLog = UserLog,
                DateAdded = DateAdded,
                Photo = Photo,
                Picture = Picture,
                ReferringDoc = ReferringDoc,
                ReferringNumber = ReferringNumber,
                InsuredsDOB = InsuredsDOB,
                InsAddL1 = InsAddL1,
                InsAddL2 = InsAddL2,
                InsAddCity = InsAddCity,
                InsAddState = InsAddState,
                InsAddZip = InsAddZip,
                InsAddPhone = InsAddPhone,
                Insureds2DOB = Insureds2DOB,
                Ins2AddL1 = Ins2AddL1,
                Ins2AddL2 = Ins2AddL2,
                Ins2AddCity = Ins2AddCity,
                Ins2AddState = Ins2AddState,
                Ins2AddZip = Ins2AddZip,
                Ins2AddPhone = Ins2AddPhone,
                Miscellaneous1 = Miscellaneous1,
                Miscellaneous2 = Miscellaneous2,
                Miscellaneous3 = Miscellaneous3,
                Miscellaneous4 = Miscellaneous4,
                MaritalStatus = MaritalStatus,
                AllergiesDemo = AllergiesDemo,
                ImageName = ImageName,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                ExemptFromReporting = ExemptFromReporting,
                TakesNoMeds = TakesNoMeds,
                PatientRace = PatientRace,
                ExemptFromReportingReason = ExemptFromReportingReason,
                InsuranceNotes2 = InsuranceNotes2,
                PatientRel2 = PatientRel2,
                PatientAddress2 = PatientAddress2,
                PatientGUID = PatientGUID,
                LanguagePreference = LanguagePreference,
                BarriersToCommunication = BarriersToCommunication,
                MiddleName = MiddleName,
                ContactPreference = ContactPreference,
                EthnicityID = EthnicityID,
                HasNoActiveDiagnoses = HasNoActiveDiagnoses,
                VFCInitialScreen = VFCInitialScreen,
                VFCLastScreen = VFCLastScreen,
                VFCReasonID = VFCReasonID,
                DateOfDeath = DateOfDeath,
                StatementDeliveryMethod = StatementDeliveryMethod,
                IsPayorConverted = IsPayorConverted,
                IsPayorConverted2 = IsPayorConverted2,
                MothersMaidenName = MothersMaidenName,
                BirthOrder = BirthOrder,
                DateTimePatientInactivated = DateTimePatientInactivated,
                PublicityCode = PublicityCode,
                PublicityCodeEffectiveDate = PublicityCodeEffectiveDate,
                MothersFirstName = MothersFirstName,
                AcPmAccountId = AcPmAccountId
            };

            return domain;
        }

        public DemographicsPoco() { }

        public DemographicsPoco(DemographicsDomain domain)
        {
            PatientID = domain.PatientID;
            ChartID = domain.ChartID;
            Salutation = domain.Salutation;
            First = domain.First;
            Middle = domain.Middle;
            Last = domain.Last;
            Suffix = domain.Suffix;
            Gender = domain.Gender;
            BirthDate = domain.BirthDate;
            SS = domain.SS;
            PatientAddress = domain.PatientAddress;
            City = domain.City;
            State = domain.State;
            Zip = domain.Zip;
            Phone = domain.Phone;
            WorkPhone = domain.WorkPhone;
            Fax = domain.Fax;
            Email = domain.Email;
            EmployerName = domain.EmployerName;
            EmergencyContactName = domain.EmergencyContactName;
            EmergencyContactPhone = domain.EmergencyContactPhone;
            SpouseName = domain.SpouseName;
            InsuranceType = domain.InsuranceType;
            PatientRel = domain.PatientRel;
            InsuredPlanName = domain.InsuredPlanName;
            InsuredIDNo = domain.InsuredIDNo;
            InsuredName = domain.InsuredName;
            InsuredGroupNo = domain.InsuredGroupNo;
            Copay = domain.Copay;
            InsuraceNotes = domain.InsuraceNotes;
            InsuredPlanName2 = domain.InsuredPlanName2;
            InsuredIDNo2 = domain.InsuredIDNo2;
            InsuredName2 = domain.InsuredName2;
            InsuredGroupNo2 = domain.InsuredGroupNo2;
            Copay2 = domain.Copay2;
            Comments = domain.Comments;
            RecordsReleased = domain.RecordsReleased;
            Referredby = domain.Referredby;
            ReferredbyMore = domain.ReferredbyMore;
            Inactive = domain.Inactive;
            ReasonInactive = domain.ReasonInactive;
            PreferredPhysician = domain.PreferredPhysician;
            PreferredPharmacy = domain.PreferredPharmacy;
            UserLog = domain.UserLog;
            DateAdded = domain.DateAdded;
            Photo = domain.Photo;
            Picture = domain.Picture;
            ReferringDoc = domain.ReferringDoc;
            ReferringNumber = domain.ReferringNumber;
            InsuredsDOB = domain.InsuredsDOB;
            InsAddL1 = domain.InsAddL1;
            InsAddL2 = domain.InsAddL2;
            InsAddCity = domain.InsAddCity;
            InsAddState = domain.InsAddState;
            InsAddZip = domain.InsAddZip;
            InsAddPhone = domain.InsAddPhone;
            Insureds2DOB = domain.Insureds2DOB;
            Ins2AddL1 = domain.Ins2AddL1;
            Ins2AddL2 = domain.Ins2AddL2;
            Ins2AddCity = domain.Ins2AddCity;
            Ins2AddState = domain.Ins2AddState;
            Ins2AddZip = domain.Ins2AddZip;
            Ins2AddPhone = domain.Ins2AddPhone;
            Miscellaneous1 = domain.Miscellaneous1;
            Miscellaneous2 = domain.Miscellaneous2;
            Miscellaneous3 = domain.Miscellaneous3;
            Miscellaneous4 = domain.Miscellaneous4;
            MaritalStatus = domain.MaritalStatus;
            AllergiesDemo = domain.AllergiesDemo;
            ImageName = domain.ImageName;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            ExemptFromReporting = domain.ExemptFromReporting;
            TakesNoMeds = domain.TakesNoMeds;
            PatientRace = domain.PatientRace;
            ExemptFromReportingReason = domain.ExemptFromReportingReason;
            InsuranceNotes2 = domain.InsuranceNotes2;
            PatientRel2 = domain.PatientRel2;
            PatientAddress2 = domain.PatientAddress2;
            PatientGUID = domain.PatientGUID;
            LanguagePreference = domain.LanguagePreference;
            BarriersToCommunication = domain.BarriersToCommunication;
            MiddleName = domain.MiddleName;
            ContactPreference = domain.ContactPreference;
            EthnicityID = domain.EthnicityID;
            HasNoActiveDiagnoses = domain.HasNoActiveDiagnoses;
            VFCInitialScreen = domain.VFCInitialScreen;
            VFCLastScreen = domain.VFCLastScreen;
            VFCReasonID = domain.VFCReasonID;
            DateOfDeath = domain.DateOfDeath;
            StatementDeliveryMethod = domain.StatementDeliveryMethod;
            IsPayorConverted = domain.IsPayorConverted;
            IsPayorConverted2 = domain.IsPayorConverted2;
            MothersMaidenName = domain.MothersMaidenName;
            BirthOrder = domain.BirthOrder;
            DateTimePatientInactivated = domain.DateTimePatientInactivated;
            PublicityCode = domain.PublicityCode;
            PublicityCodeEffectiveDate = domain.PublicityCodeEffectiveDate;
            MothersFirstName = domain.MothersFirstName;
            AcPmAccountId = domain.AcPmAccountId;
        }
    }
}