// Placeholder class generated from swagger.json definition (~line 29226)
// TODO: Refine property types (e.g., DateTimeOffset?, int?, bool?) and add validation/attributes as needed.
namespace ClaudeAcDirectSql.Models
{
    public class DemographicsDomain
    {
        public int? PatientID { get; set; }
        public string? ChartID { get; set; }
        public string? Salutation { get; set; }
        public string? First { get; set; }
        public string? Middle { get; set; } // See also MiddleName
        public string? Last { get; set; }
        public string? Suffix { get; set; }
        public string? Gender { get; set; }
        public string? BirthDate { get; set; } // Consider DateTimeOffset
        public string? Ss { get; set; }
        public string? PatientAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Phone { get; set; }
        public string? WorkPhone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? EmployerName { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? SpouseName { get; set; }
        public string? InsuranceType { get; set; } // Belongs to Coverage
        public string? PatientRel { get; set; } // Belongs to Coverage
        public string? InsuredPlanName { get; set; } // Belongs to Coverage
        public string? InsuredIDNo { get; set; } // Belongs to Coverage
        public string? InsuredName { get; set; } // Belongs to Coverage
        public string? InsuredGroupNo { get; set; } // Belongs to Coverage
        public string? Copay { get; set; } // Belongs to Coverage
        public string? InsuraceNotes { get; set; } // Belongs to Coverage
        public string? InsuredPlanName2 { get; set; } // Belongs to Coverage
        public string? InsuredIDNo2 { get; set; } // Belongs to Coverage
        public string? InsuredName2 { get; set; } // Belongs to Coverage
        public string? InsuredGroupNo2 { get; set; } // Belongs to Coverage
        public string? Copay2 { get; set; } // Belongs to Coverage
        public string? Comments { get; set; }
        public string? RecordsReleased { get; set; } // Meaning?
        public string? Referredby { get; set; } // Referral?
        public string? ReferredbyMore { get; set; } // Referral?
        public bool? Inactive { get; set; }
        public string? ReasonInactive { get; set; }
        public string? PreferredPhysician { get; set; }
        public string? PreferredPharmacy { get; set; }
        public string? UserLog { get; set; } // Audit
        public string? DateAdded { get; set; } // Consider DateTimeOffset, Audit/Meta
        public string? Photo { get; set; } // Legacy? See Picture
        public string? Picture { get; set; } // base64 byte array
        public string? ReferringDoc { get; set; } // Referral?
        public string? ReferringNumber { get; set; } // Referral?
        public string? InsuredsDOB { get; set; } // Belongs to Coverage
        public string? InsAddL1 { get; set; } // Belongs to Coverage
        public string? InsAddL2 { get; set; } // Belongs to Coverage
        public string? InsAddCity { get; set; } // Belongs to Coverage
        public string? InsAddState { get; set; } // Belongs to Coverage
        public string? InsAddZip { get; set; } // Belongs to Coverage
        public string? InsAddPhone { get; set; } // Belongs to Coverage
        public string? Insureds2DOB { get; set; } // Belongs to Coverage
        public string? Ins2AddL1 { get; set; } // Belongs to Coverage
        public string? Ins2AddL2 { get; set; } // Belongs to Coverage
        public string? Ins2AddCity { get; set; } // Belongs to Coverage
        public string? Ins2AddState { get; set; } // Belongs to Coverage
        public string? Ins2AddZip { get; set; } // Belongs to Coverage
        public string? Ins2AddPhone { get; set; } // Belongs to Coverage
        public string? Miscellaneous1 { get; set; }
        public string? Miscellaneous2 { get; set; }
        public string? Miscellaneous3 { get; set; }
        public string? Miscellaneous4 { get; set; }
        public string? MaritalStatus { get; set; }
        public string? AllergiesDemo { get; set; } // Related to AllergyIntolerance?
        public string? ImageName { get; set; }
        public string? DateLastTouched { get; set; } // Consider DateTimeOffset, Audit/Meta
        public string? LastTouchedBy { get; set; } // Audit
        public string? DateRowAdded { get; set; } // Consider DateTimeOffset, Audit/Meta
        public bool? ExemptFromReporting { get; set; }
        public bool? TakesNoMeds { get; set; }
        public string? PatientRace { get; set; }
        public string? ExemptFromReportingReason { get; set; }
        public string? InsuranceNotes2 { get; set; } // Belongs to Coverage
        public string? PatientRel2 { get; set; } // Belongs to Coverage
        public string? PatientAddress2 { get; set; }
        public string? PatientGUID { get; set; } // Consider Guid type
        public string? LanguagePreference { get; set; }
        public string? BarriersToCommunication { get; set; }
        public string? MiddleName { get; set; } // See also Middle
        public string? ContactPreference { get; set; }
        public int? EthnicityID { get; set; }
        public bool? HasNoActiveDiagnoses { get; set; }
        public string? VfcInitialScreen { get; set; } // Consider DateTimeOffset
        public string? VfcLastScreen { get; set; } // Consider DateTimeOffset
        public int? VfcReasonID { get; set; }
        public string? DateOfDeath { get; set; } // Consider DateTimeOffset
        public int? StatementDeliveryMethod { get; set; } // Meaning?
        public bool? IsPayorConverted { get; set; } // Belongs to Coverage?
        public bool? IsPayorConverted2 { get; set; } // Belongs to Coverage?
        public string? MothersMaidenName { get; set; }
        public int? BirthOrder { get; set; }
        public string? DateTimePatientInactivated { get; set; } // Consider DateTimeOffset
        public string? PublicityCode { get; set; }
        public string? PublicityCodeEffectiveDate { get; set; } // Consider DateTimeOffset
        public string? MothersFirstName { get; set; }
        public string? AcPmAccountId { get; set; }
    }
}
