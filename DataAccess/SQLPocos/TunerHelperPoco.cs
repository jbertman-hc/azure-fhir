using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class TunerHelperPoco
    {
        public int? rowid { get; set; }
        public bool? ProblemDateLastActivatedRemoved { get; set; }
        public bool? MigratedRecordsReleased { get; set; }
        public bool? MigratedReferrals2 { get; set; }
        public bool? MigratedAllergies2 { get; set; }
        public bool? ListImmunizationsDepricated { get; set; }
        public bool? MigratedInHouseTests { get; set; }
        public bool? MedsMigrated { get; set; }
        public bool? RefillsMigrated { get; set; }
        public bool? MigratedAllergies { get; set; }
        public bool? PracticeErxFixed { get; set; }
        public bool? UpdatedSpouseAndEmergencyContact { get; set; }
        public bool? UpdatedTakesNoMeds { get; set; }
        public bool? UpdatedRaceMissingValues { get; set; }
        public bool? UpdatedSoapICD9Data { get; set; }
        public bool? UpdatedEthnicity { get; set; }
        public bool? PayorMigrationMessageShown { get; set; }
        public bool? ClaimStatusReset { get; set; }
        public bool? UpdatedVitalComments { get; set; }
        public bool? MigratedCPT1CodesEdited { get; set; }
        public bool? SetObGynPregControl { get; set; }
        public bool? UpdateProviderCodeInBilling { get; set; }
        public bool? SetPMStartDate { get; set; }
        public bool? RemitFinalPayor { get; set; }
        public bool? BillingConversion { get; set; }
        public bool? MigratedSuperbillAccount { get; set; }
        public bool? MigratedSuperbillAccountFix { get; set; }
        public bool? PMEmailSent { get; set; }
        public bool? UpdatedRegistryUploads { get; set; }
        public bool? MigratedPatientRace { get; set; }
        public bool? MigratedPatientLanguage { get; set; }
        public bool? RegistryPatientInfoCreation { get; set; }
        public bool? MigrateExistingSOAPIcd9s { get; set; }

        public TunerHelperDomain MapToDomainModel()
        {
            TunerHelperDomain domain = new TunerHelperDomain
            {
                rowid = rowid,
                ProblemDateLastActivatedRemoved = ProblemDateLastActivatedRemoved,
                MigratedRecordsReleased = MigratedRecordsReleased,
                MigratedReferrals2 = MigratedReferrals2,
                MigratedAllergies2 = MigratedAllergies2,
                ListImmunizationsDepricated = ListImmunizationsDepricated,
                MigratedInHouseTests = MigratedInHouseTests,
                MedsMigrated = MedsMigrated,
                RefillsMigrated = RefillsMigrated,
                MigratedAllergies = MigratedAllergies,
                PracticeErxFixed = PracticeErxFixed,
                UpdatedSpouseAndEmergencyContact = UpdatedSpouseAndEmergencyContact,
                UpdatedTakesNoMeds = UpdatedTakesNoMeds,
                UpdatedRaceMissingValues = UpdatedRaceMissingValues,
                UpdatedSoapICD9Data = UpdatedSoapICD9Data,
                UpdatedEthnicity = UpdatedEthnicity,
                PayorMigrationMessageShown = PayorMigrationMessageShown,
                ClaimStatusReset = ClaimStatusReset,
                UpdatedVitalComments = UpdatedVitalComments,
                MigratedCPT1CodesEdited = MigratedCPT1CodesEdited,
                SetObGynPregControl = SetObGynPregControl,
                UpdateProviderCodeInBilling = UpdateProviderCodeInBilling,
                SetPMStartDate = SetPMStartDate,
                RemitFinalPayor = RemitFinalPayor,
                BillingConversion = BillingConversion,
                MigratedSuperbillAccount = MigratedSuperbillAccount,
                MigratedSuperbillAccountFix = MigratedSuperbillAccountFix,
                PMEmailSent = PMEmailSent,
                UpdatedRegistryUploads = UpdatedRegistryUploads,
                MigratedPatientRace = MigratedPatientRace,
                MigratedPatientLanguage = MigratedPatientLanguage,
                RegistryPatientInfoCreation = RegistryPatientInfoCreation,
                MigrateExistingSOAPIcd9s = MigrateExistingSOAPIcd9s
            };

            return domain;
        }

        public TunerHelperPoco() { }

        public TunerHelperPoco(TunerHelperDomain domain)
        {
            rowid = domain.rowid;
            ProblemDateLastActivatedRemoved = domain.ProblemDateLastActivatedRemoved;
            MigratedRecordsReleased = domain.MigratedRecordsReleased;
            MigratedReferrals2 = domain.MigratedReferrals2;
            MigratedAllergies2 = domain.MigratedAllergies2;
            ListImmunizationsDepricated = domain.ListImmunizationsDepricated;
            MigratedInHouseTests = domain.MigratedInHouseTests;
            MedsMigrated = domain.MedsMigrated;
            RefillsMigrated = domain.RefillsMigrated;
            MigratedAllergies = domain.MigratedAllergies;
            PracticeErxFixed = domain.PracticeErxFixed;
            UpdatedSpouseAndEmergencyContact = domain.UpdatedSpouseAndEmergencyContact;
            UpdatedTakesNoMeds = domain.UpdatedTakesNoMeds;
            UpdatedRaceMissingValues = domain.UpdatedRaceMissingValues;
            UpdatedSoapICD9Data = domain.UpdatedSoapICD9Data;
            UpdatedEthnicity = domain.UpdatedEthnicity;
            PayorMigrationMessageShown = domain.PayorMigrationMessageShown;
            ClaimStatusReset = domain.ClaimStatusReset;
            UpdatedVitalComments = domain.UpdatedVitalComments;
            MigratedCPT1CodesEdited = domain.MigratedCPT1CodesEdited;
            SetObGynPregControl = domain.SetObGynPregControl;
            UpdateProviderCodeInBilling = domain.UpdateProviderCodeInBilling;
            SetPMStartDate = domain.SetPMStartDate;
            RemitFinalPayor = domain.RemitFinalPayor;
            BillingConversion = domain.BillingConversion;
            MigratedSuperbillAccount = domain.MigratedSuperbillAccount;
            MigratedSuperbillAccountFix = domain.MigratedSuperbillAccountFix;
            PMEmailSent = domain.PMEmailSent;
            UpdatedRegistryUploads = domain.UpdatedRegistryUploads;
            MigratedPatientRace = domain.MigratedPatientRace;
            MigratedPatientLanguage = domain.MigratedPatientLanguage;
            RegistryPatientInfoCreation = domain.RegistryPatientInfoCreation;
            MigrateExistingSOAPIcd9s = domain.MigrateExistingSOAPIcd9s;
        }
    }
}
