using Hl7.Fhir.Model;
using Hl7.Fhir.R4;
using ClaudeAcDirectSql.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class PatientMapper
    {
        // Maps the DemographicsDomain object (from AC Direct SQL API)
        // to a FHIR R4 Patient resource, compliant with US Core 3.1.1 or higher.
        public static Patient ToFhirPatient(DemographicsDomain source)
        {
            if (source == null)
            {
                return null;
            }

            var patient = new Patient();

            // --- Set Profile ---
            patient.Meta = new Meta();
            // <<< FLAG: Confirm target US Core version (using 6.1.0 here)
            patient.Meta.Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-patient|6.1.0" };

            // --- Identifiers (Mandatory) ---
            // TODO: Map MedicalRecordNumber and potentially other identifiers.
            // US Core requires at least one identifier.
            if (!string.IsNullOrWhiteSpace(source.MedicalRecordNumber))
            {
                patient.Identifier.Add(new Identifier
                {
                    // TODO: Determine the correct system URI for the MRN.
                    // System = "urn:oid:YOUR_ORG_OID.mrn", // Example OID based system
                    System = "http://hospital.smarthealthit.org", // Example system URI <<< FLAG
                    Value = source.MedicalRecordNumber,
                    // TODO: Assign appropriate type if known (e.g., MR for Medical Record)
                    Type = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v2-0203", "MR", "Medical Record Number")
                });
            }
            // TODO: Map SocialSecurityNumber if available and appropriate policy allows.
            if (!string.IsNullOrWhiteSpace(source.SocialSecurityNumber))
            {
                 patient.Identifier.Add(new Identifier
                 {
                     System = "http://hl7.org/fhir/sid/us-ssn",
                     Value = source.SocialSecurityNumber,
                     Type = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v2-0203", "SS", "Social Security Number")
                 });
            }
            // <<< Add mapping for other source identifiers if present >>>
            if (patient.Identifier.Count == 0)
            {
                 // TODO: Handle cases with NO identifiers (US Core Validation Error)
                 // Consider creating a temporary or system-generated ID if absolutely necessary.
                 // patient.Identifier.Add(new Identifier { System = "urn:ietf:rfc:3986", Value = $"urn:uuid:{Guid.NewGuid()}" }); // Example
                 // <<< FLAG: Identifier strategy needed for patients without MRN/SSN
            }

            // Set Resource ID (Optional, but recommended)
            // TODO: Define ID strategy (e.g., use MRN, GUID, or source primary key)
            // patient.Id = source.MedicalRecordNumber; // Example using MRN - ensure uniqueness!
            patient.Id = Guid.NewGuid().ToString(); // <<< FLAG (Defaulting to GUID, needs strategy)

            // --- Active Status (Optional) ---
            // TODO: Determine if patient record is active (e.g., based on deceased status or other flags)
            // patient.Active = source.IsActive; // Assuming a boolean field
            patient.Active = (source.DeceasedDate == null); // Example: active if not deceased

            // --- Name (Mandatory) ---
            // TODO: Map first name, last name, middle name, suffix.
            // US Core requires >= 1 name with family and >= 1 given name.
            var name = new HumanName();
            if (!string.IsNullOrWhiteSpace(source.LastName))
            {
                name.Family = source.LastName;
            }
            if (!string.IsNullOrWhiteSpace(source.FirstName))
            {
                name.Given = new List<string> { source.FirstName };
            }
            if (!string.IsNullOrWhiteSpace(source.MiddleName))
            {
                // FHIR separates multiple given names
                if (name.Given == null) name.Given = new List<string>();
                name.GivenElement.Add(new FhirString(source.MiddleName)); // Middle name often goes here
                 // Or, treat middle initial/name as part of the given name list:
                 // name.Given.Add(source.MiddleName);
            }
            if (!string.IsNullOrWhiteSpace(source.Suffix))
            {
                 name.Suffix = new List<string> { source.Suffix };
            }
            // TODO: Determine Name Use (official, usual, maiden, etc.)
            name.Use = HumanName.NameUse.Official; // <<< FLAG (Defaulting to official)
            // name.Use = MapNameUse(source.NameType);

            if (!string.IsNullOrWhiteSpace(name.Family) && name.Given.Any(g => !string.IsNullOrWhiteSpace(g)))
            {
                patient.Name.Add(name);
            }
            else
            {
                 // TODO: Handle missing required name components (US Core Validation Error)
                 // <<< FLAG: Need valid Name strategy
            }
            // TODO: Add other names if available (e.g., Maiden Name -> use: maiden)
            // var maidenName = new HumanName { Family = source.MaidenName, Use = HumanName.NameUse.Maiden };
            // if (!string.IsNullOrWhiteSpace(maidenName.Family)) patient.Name.Add(maidenName);

            // --- Telecom (Phone, Email - Mandatory if known) ---
            // TODO: Map HomePhone, WorkPhone, MobilePhone, Email.
            // US Core requires telecom if known.
            if (!string.IsNullOrWhiteSpace(source.HomePhone))
            {
                patient.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Phone, Value = source.HomePhone, Use = ContactPoint.ContactPointUse.Home });
            }
            if (!string.IsNullOrWhiteSpace(source.WorkPhone))
            {
                patient.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Phone, Value = source.WorkPhone, Use = ContactPoint.ContactPointUse.Work });
            }
            if (!string.IsNullOrWhiteSpace(source.MobilePhone))
            {
                 // <<< FLAG: Determine if MobilePhone should be 'mobile' or just 'home'/ 'work' based on source meaning
                patient.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Phone, Value = source.MobilePhone, Use = ContactPoint.ContactPointUse.Mobile });
            }
            if (!string.IsNullOrWhiteSpace(source.Email))
            {
                patient.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Email, Value = source.Email, Use = ContactPoint.ContactPointUse.Home }); // Assuming home email
            }
            if (patient.Telecom.Count == 0)
            {
                 // TODO: Add dataAbsentReason extension if telecom is truly unknown but required
                 // <<< FLAG: US Core requirement if telecom is missing
                 // Example: patient.Telecom.Add(new ContactPoint().SetDataAbsentReason(DataAbsentReason.Unknown));
            }

            // --- Gender (Mandatory) ---
            // TODO: Map source.Gender to FHIR AdministrativeGender code (male, female, other, unknown).
            // This is administrative gender, NOT necessarily sex assigned at birth.
            switch (source.Gender?.ToLowerInvariant())
            {
                case "m":
                case "male":
                    patient.Gender = AdministrativeGender.Male; break;
                case "f":
                case "female":
                    patient.Gender = AdministrativeGender.Female; break;
                case "o":
                case "other":
                    patient.Gender = AdministrativeGender.Other; break;
                default:
                    patient.Gender = AdministrativeGender.Unknown; break; // <<< FLAG (Handle unmapped/null gender)
            }

            // --- Birth Date (Mandatory) ---
            // TODO: Map source.DateOfBirth.
            if (!string.IsNullOrWhiteSpace(source.DateOfBirth))
            {
                // FHIR Date format is YYYY, YYYY-MM, or YYYY-MM-DD
                if (DateTime.TryParse(source.DateOfBirth, out DateTime dobDt))
                {
                    patient.BirthDate = dobDt.ToString("yyyy-MM-dd");
                }
                else
                {
                     // TODO: Handle invalid date format
                     // patient.BirthDate = source.DateOfBirth; // Assign as string if necessary? Not ideal.
                     // <<< FLAG: Invalid DOB format
                }
            }
            else
            {
                 // TODO: Handle missing required DOB (US Core Validation Error)
                 // patient.BirthDateElement.SetDataAbsentReason(DataAbsentReason.Unknown);
                 // <<< FLAG: US Core requirement if DOB is missing
            }

            // --- Deceased ---
            // TODO: Map DeceasedDate or a Deceased flag.
            if (!string.IsNullOrWhiteSpace(source.DeceasedDate))
            {
                if (DateTime.TryParse(source.DeceasedDate, out DateTime deceasedDt))
                {
                     patient.Deceased = new FhirDateTime(new DateTimeOffset(deceasedDt));
                }
                else
                {
                    // Handle boolean case if source is like IsDeceased
                     // patient.Deceased = new FhirBoolean(true); // If only flag is available
                     // <<< FLAG: Handle non-date deceased info
                }
            }
             // else { patient.Deceased = new FhirBoolean(false); } // Optional: Explicitly set to false if not deceased

            // --- Address (Mandatory if known) ---
            // TODO: Map Address1, Address2, City, State, ZipCode, Country.
            // US Core requires address if known.
            var address = new Address();
            if (!string.IsNullOrWhiteSpace(source.Address1) || !string.IsNullOrWhiteSpace(source.Address2))
            {
                address.Line = new List<string>();
                if (!string.IsNullOrWhiteSpace(source.Address1)) address.Line.Add(source.Address1);
                if (!string.IsNullOrWhiteSpace(source.Address2)) address.Line.Add(source.Address2);
            }
            address.City = source.City;
            // TODO: Map state - needs to be the 2-letter code for US addresses usually.
            address.State = source.State; // <<< FLAG (Ensure state is mapped correctly, e.g., full name to abbreviation)
            address.PostalCode = source.ZipCode;
            address.Country = source.Country ?? "USA"; // <<< FLAG (Defaulting country to USA)
            // TODO: Determine Address Use (home, work, temp, old)
            address.Use = Address.AddressUse.Home; // <<< FLAG (Defaulting to home)
            // TODO: Determine Address Type (postal, physical, both)
            address.Type = Address.AddressType.Physical; // <<< FLAG (Assuming physical)

            if (address.Line.Any() || !string.IsNullOrWhiteSpace(address.City) || !string.IsNullOrWhiteSpace(address.State) || !string.IsNullOrWhiteSpace(address.PostalCode))
            {
                patient.Address.Add(address);
            }
            else
            {
                // TODO: Add dataAbsentReason extension if address is truly unknown but required
                // <<< FLAG: US Core requirement if address is missing
                 // Example: patient.Address.Add(new Address().SetDataAbsentReason(DataAbsentReason.Unknown));
            }

            // --- Marital Status ---
            // TODO: Map source.MaritalStatus to FHIR MaritalStatus code system.
             if (!string.IsNullOrWhiteSpace(source.MaritalStatus))
             {
                 // See http://hl7.org/fhir/R4/valueset-marital-status.html
                 // Example mapping - refine based on source values
                 CodeableConcept maritalStatusCode = null;
                 string msLower = source.MaritalStatus.ToLowerInvariant();
                 if (msLower.Contains("married")) maritalStatusCode = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus", "M", "Married");
                 else if (msLower.Contains("single") || msLower.Contains("never married")) maritalStatusCode = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus", "S", "Never Married");
                 else if (msLower.Contains("divorced")) maritalStatusCode = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus", "D", "Divorced");
                 else if (msLower.Contains("widowed")) maritalStatusCode = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus", "W", "Widowed");
                 // ... add other mappings (Annulled 'A', Legally Separated 'L', Partnered 'P'?) ...
                 else maritalStatusCode = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-NullFlavor", "UNK", "unknown"); // <<< FLAG (Handle unmapped status)

                 patient.MaritalStatus = maritalStatusCode;
             }

            // --- Multiple Birth ---
            // TODO: Map if known (e.g., source.IsMultipleBirth as boolean or source.BirthOrder as integer)
            // if (source.IsMultipleBirth.HasValue) patient.MultipleBirth = new FhirBoolean(source.IsMultipleBirth.Value);
            // if (source.BirthOrder.HasValue) patient.MultipleBirth = new Integer(source.BirthOrder.Value);

            // --- Photo ---
            // TODO: Map patient photo if available (usually requires separate retrieval)
            // var photo = new Attachment { ContentType = "image/jpeg", Data = ... };
            // patient.Photo.Add(photo);

            // --- Contact (Next of Kin, Emergency Contact) ---
            // TODO: Map source.EmergencyContactName, source.EmergencyContactPhone, source.EmergencyContactRelationship
            if (!string.IsNullOrWhiteSpace(source.EmergencyContactName) || !string.IsNullOrWhiteSpace(source.EmergencyContactPhone))
            {
                var contact = new Patient.ContactComponent();
                if (!string.IsNullOrWhiteSpace(source.EmergencyContactName))
                {
                    // Parsing name might be needed if it's not structured
                     contact.Name = new HumanName { Text = source.EmergencyContactName }; // Basic name mapping
                     // TODO: Parse EmergencyContactName into Family/Given if possible <<< FLAG
                }
                if (!string.IsNullOrWhiteSpace(source.EmergencyContactPhone))
                {
                    contact.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Phone, Value = source.EmergencyContactPhone });
                }
                if (!string.IsNullOrWhiteSpace(source.EmergencyContactRelationship))
                {
                    // TODO: Map relationship string to Patient Relationship codes
                    // See http://hl7.org/fhir/R4/valueset-patient-contactrelationship.html
                    contact.Relationship.Add(new CodeableConcept { Text = source.EmergencyContactRelationship }); // <<< FLAG (Needs mapping to standard codeset)
                    // Example: if (rel == "Spouse") code = new CodeableConcept(system, "SPO", "Spouse")
                }
                // contact.Gender = ... // If known
                // contact.Address = ... // If known
                // contact.Organization = ... // If contact is org
                patient.Contact.Add(contact);
            }

            // --- Communication (Language) ---
            // TODO: Map source.PreferredLanguage.
            if (!string.IsNullOrWhiteSpace(source.PreferredLanguage))
            {
                var communication = new Patient.CommunicationComponent();
                // TODO: Map language string to BCP-47 code (e.g., "English" -> "en", "Spanish" -> "es")
                // See https://www.iana.org/assignments/language-subtag-registry/language-subtag-registry
                communication.Language = new CodeableConcept { Text = source.PreferredLanguage }; // <<< FLAG (Needs mapping to standard codeset, e.g., urn:ietf:bcp:47)
                // communication.Language = new CodeableConcept("urn:ietf:bcp:47", MapLanguageToBCP47(source.PreferredLanguage));

                // TODO: Determine if this is the preferred language
                 communication.Preferred = true; // <<< FLAG (Assuming source is preferred)
                patient.Communication.Add(communication);
            }

            // --- General Practitioner ---
            // TODO: Map primary care provider (PCP) if available (Reference to Practitioner or Organization)
            if (!string.IsNullOrWhiteSpace(source.PreferredPhysician))
            {
                // <<< FLAG: Need strategy to resolve PreferredPhysician name into a Practitioner Reference
                // Example: patient.GeneralPractitioner.Add(ResolvePractitionerReference(source.PreferredPhysician));
                patient.GeneralPractitioner.Add(new ResourceReference()
                {
                    Display = source.PreferredPhysician // Set display name, but need reference ID
                });
            }

            // --- Managing Organization ---
            // TODO: Map the organization managing the patient record (Reference to Organization)
            // <<< FLAG: Source for Managing Organization is unclear from DemographicsDomain
            // if (source.ManagingOrgId != null)
            // {
            //    patient.ManagingOrganization = new ResourceReference($"Organization/{source.ManagingOrgId}") { Display = source.ManagingOrgName }; // <<< FLAG (Confirm reference strategy)
            // }

            // --- Link ---
            // TODO: Link to other patient records if this record replaces or refers to others.

            // --- US Core Extensions (Race, Ethnicity, Birth Sex) ---
            // These are REQUIRED by US Core if known.

            // ** Race **
            // TODO: Map source.Race to the OMB Race categories (extension required).
            // See: http://hl7.org/fhir/us/core/StructureDefinition/us-core-race.html
            var raceExt = new Extension
            {
                Url = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-race"
            };
            // Race requires nested extensions for category and optional detailed codes/text
            // Example for "White":
            // raceExt.Extension.Add(new Extension("ombCategory", new Coding("urn:oid:2.16.840.1.113883.6.238", "2106-3", "White")));
            // Example for "Other Race":
            // raceExt.Extension.Add(new Extension("ombCategory", new Coding("urn:oid:2.16.840.1.113883.6.238", "2131-1", "Other Race")));
            // raceExt.Extension.Add(new Extension("text", new FhirString("Patient specified race description")));
            // <<< FLAG: Need mapping logic from source.Race to these OMB codes/text
            // if (mappedRaceCode != null) raceExt.Extension.Add(new Extension("ombCategory", mappedRaceCode));
            // if (hasDetailedRace) raceExt.Extension.Add(new Extension("detailed", ...));
            // if (!string.IsNullOrWhiteSpace(source.RaceText)) raceExt.Extension.Add(new Extension("text", new FhirString(source.RaceText)));
            // if (raceExt.Extension.Any()) patient.Extension.Add(raceExt);
            // else { /* TODO: Add dataAbsentReason if race is unknown? */ }

            // ** Ethnicity **
            // TODO: Map source.Ethnicity to the OMB Ethnicity categories (extension required).
            // See: http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity.html
             var ethnicityExt = new Extension
             {
                 Url = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity"
             };
            // Ethnicity requires nested extensions for category and optional detailed codes/text
            // Example for "Not Hispanic or Latino":
            // ethnicityExt.Extension.Add(new Extension("ombCategory", new Coding("urn:oid:2.16.840.1.113883.6.238", "2186-5", "Not Hispanic or Latino")));
            // Example for "Hispanic or Latino":
            // ethnicityExt.Extension.Add(new Extension("ombCategory", new Coding("urn:oid:2.16.840.1.113883.6.238", "2135-2", "Hispanic or Latino")));
            // <<< FLAG: Need mapping logic from source.Ethnicity to these OMB codes/text
            // if (mappedEthnicityCode != null) ethnicityExt.Extension.Add(new Extension("ombCategory", mappedEthnicityCode));
            // if (hasDetailedEthnicity) ethnicityExt.Extension.Add(new Extension("detailed", ...));
            // if (!string.IsNullOrWhiteSpace(source.EthnicityText)) ethnicityExt.Extension.Add(new Extension("text", new FhirString(source.EthnicityText)));
            // if (ethnicityExt.Extension.Any()) patient.Extension.Add(ethnicityExt);
            // else { /* TODO: Add dataAbsentReason if ethnicity is unknown? */ }

            // ** Birth Sex **
            // TODO: Map source.BirthSex (or infer if possible) to US Core Birth Sex value set (extension required).
            // See: http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex.html
             var birthSexExt = new Extension
             {
                 Url = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex"
             };
            // Value is a simple code: M (Male), F (Female), O (Other), UNK (Unknown)
            // switch (source.BirthSex?.ToLowerInvariant())
            // {
            //     case "m": birthSexExt.Value = new Code("M"); break;
            //     case "f": birthSexExt.Value = new Code("F"); break;
            //     case "o": birthSexExt.Value = new Code("OTH"); break; // Note: US Core uses OTH here, not O
            //     default: birthSexExt.Value = new Code("UNK"); break;
            // }
             // <<< FLAG: Need mapping logic from source.BirthSex to M/F/OTH/UNK codes
             // if (birthSexExt.Value != null) patient.Extension.Add(birthSexExt);
             // else { /* TODO: Add dataAbsentReason if birth sex is unknown? */ }

            // TODO: Review all TODOs and FLAGs

            return patient;
        }
    }
}
