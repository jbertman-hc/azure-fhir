using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class BillingPoco
    {
        public Guid BillingID { get; set; }
        public int? PatientID { get; set; }
        public string PlaceOfService { get; set; }
        public Guid? LocationID { get; set; }
        public Guid? FacilityID { get; set; }
        public string ProviderCode { get; set; }
        public string Complexity { get; set; }
        public string CC { get; set; }
        public string Comments { get; set; }
        public bool? SignedOff { get; set; }
        public int? BillingState { get; set; }
        public int? TargetInsurType { get; set; }
        public int? OldBillingID { get; set; }
        public bool? UseCurrentInsurInfo { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public DateTime? DateOfService { get; set; }
        public bool? Historical { get; set; }
        public bool IsOpen { get; set; }
        public int? MsgID { get; set; }
        public string PmAccountId { get; set; }
        public byte IcdType { get; set; }

        public BillingDomain MapToDomainModel()
        {
            BillingDomain domain = new BillingDomain
            {
                BillingID = BillingID,
                PatientID = PatientID,
                PlaceOfService = PlaceOfService,
                LocationID = LocationID,
                FacilityID = FacilityID,
                ProviderCode = ProviderCode,
                Complexity = Complexity,
                CC = CC,
                Comments = Comments,
                SignedOff = SignedOff,
                BillingState = BillingState,
                TargetInsurType = TargetInsurType,
                OldBillingID = OldBillingID,
                UseCurrentInsurInfo = UseCurrentInsurInfo,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                DateOfService = DateOfService,
                Historical = Historical,
                IsOpen = IsOpen,
                MsgID = MsgID,
                PmAccountId = PmAccountId,
                IcdType = IcdType
            };

            return domain;
        }

        public BillingPoco() { }

        public BillingPoco(BillingDomain domain)
        {
            BillingID = domain.BillingID;
            PatientID = domain.PatientID;
            PlaceOfService = domain.PlaceOfService;
            LocationID = domain.LocationID;
            FacilityID = domain.FacilityID;
            ProviderCode = domain.ProviderCode;
            Complexity = domain.Complexity;
            CC = domain.CC;
            Comments = domain.Comments;
            SignedOff = domain.SignedOff;
            BillingState = domain.BillingState;
            TargetInsurType = domain.TargetInsurType;
            OldBillingID = domain.OldBillingID;
            UseCurrentInsurInfo = domain.UseCurrentInsurInfo;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            DateOfService = domain.DateOfService;
            Historical = domain.Historical;
            IsOpen = domain.IsOpen;
            MsgID = domain.MsgID;
            PmAccountId = domain.PmAccountId;
            IcdType = domain.IcdType;
        }
    }
}

