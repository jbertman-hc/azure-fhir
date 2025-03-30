using Hl7.Fhir.Model;
using ClaudeAcDirectSql.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class OrganizationMapper
    {
        // <<< FLAG: Dependency Injection needed for repository access >>>
        // private static IPracticeInfoRepository _practiceInfoRepository;
        // public static void Initialize(IPracticeInfoRepository practiceInfoRepository)
        // {
        //     _practiceInfoRepository = practiceInfoRepository;
        // }

        /// <summary>
        /// Maps PracticeInfo data to a FHIR Organization resource.
        /// Retrieves full data using IPracticeInfoRepository if an ID is provided.
        /// <<< FLAG: Verify mapping logic, identifiers, address format, telecom use codes with team >>>
        /// </summary>
        /// <param name="practiceId">The ID of the practice to map (used for lookup).</param>
        /// <returns>A FHIR Organization resource.</returns>
        public static Organization GetFhirOrganization(int practiceId)
        {
            // <<< FLAG: Implement actual data retrieval using injected repository >>>
            // PracticeInfoDomain practiceData = _practiceInfoRepository?.GetPracticeInfo(practiceId);
            PracticeInfoDomain practiceData = null; // Placeholder for retrieved data

            // Simulate retrieval for placeholder mapping
            if (practiceData == null) {
                 practiceData = new PracticeInfoDomain { /* Populate with dummy data based on ID? */ };
                 // <<< FLAG: Handle case where practice info is not found for the given ID >>>
            }

            if (practiceData == null) return null;

            var organization = new Organization();

            // --- ID ---
            // <<< FLAG: Define final strategy for Organization ID. Using PracticeInfo.ID? >>>
            organization.Id = $"org-{practiceId}"; // Example ID strategy using the input practiceId

            // --- Identifier ---
            // <<< FLAG: Add relevant identifiers (NPI, EIN, CLIA, etc.) confirmed from PracticeInfo table >>>
            // <<< FLAG: Confirm identifier system URIs and use codes (official, usual) >>>
            if (!string.IsNullOrWhiteSpace(practiceData.NPI))
            {
                organization.Identifier.Add(new Identifier
                {
                    System = "http://hl7.org/fhir/sid/us-npi", // <<< FLAG: Confirm system URI is correct
                    Value = practiceData.NPI,
                    Use = Identifier.IdentifierUse.Official // <<< FLAG: Confirm Use (Official? Usual?)
                });
            }
            if (!string.IsNullOrWhiteSpace(practiceData.EIN)) // Employer Identification Number (Tax ID?)
            {
                 organization.Identifier.Add(new Identifier
                 {
                     System = "urn:oid:2.16.840.1.113883.4.2", // TIN OID <<< FLAG: Confirm system URI is correct for EIN
                     Value = practiceData.EIN,
                     Use = Identifier.IdentifierUse.Official // <<< FLAG: Confirm Use
                 });
            }
            // TODO: Add other identifiers like CLIA, UniquePracticeID if needed

            // --- Active Status ---
            // <<< FLAG: PracticeInfo table schema doesn't show an explicit 'Active' field. Assume active? Needs confirmation. >>>
            organization.Active = true; // Defaulting to true

            // --- Type ---
            // <<< FLAG: Assign appropriate Organization type code(s) (e.g., 'prov' for healthcare provider). Needs confirmation. >>>
            organization.Type.Add(new CodeableConcept("http://terminology.hl7.org/CodeSystem/organization-type", "prov", "Healthcare Provider")); // Example

            // --- Name ---
            organization.Name = practiceData.PracticeName;

            // --- Telecom ---
            // <<< FLAG: Map phone, fax, email using actual field names (Phone1, fax, email). Confirm ContactPointUse codes (work, mobile, etc.). >>>
            if (!string.IsNullOrWhiteSpace(practiceData.Phone1))
            {
                organization.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Phone, Value = practiceData.Phone1, Use = ContactPoint.ContactPointUse.Work });
            }
            if (!string.IsNullOrWhiteSpace(practiceData.fax))
            {
                organization.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Fax, Value = practiceData.fax, Use = ContactPoint.ContactPointUse.Work });
            }
            if (!string.IsNullOrWhiteSpace(practiceData.email))
            {
                organization.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Email, Value = practiceData.email, Use = ContactPoint.ContactPointUse.Work });
            }

            // --- Address ---
            // <<< FLAG: Map address fields using actual field names (StreetAddress1, StreetAddress2, City, State, Zip). Confirm AddressUse/Type codes. >>>
            if (!string.IsNullOrWhiteSpace(practiceData.StreetAddress1) || !string.IsNullOrWhiteSpace(practiceData.City))
            {
                var address = new Address
                {
                    Use = Address.AddressUse.Work, // <<< FLAG: Confirm use
                    Type = Address.AddressType.Physical, // <<< FLAG: Confirm type
                    Line = new List<string>(),
                    City = practiceData.City,
                    State = practiceData.State, // <<< FLAG: Requires state mapping if not abbreviation
                    PostalCode = practiceData.Zip,
                    Country = "USA" // <<< FLAG: Confirm default/source field if available
                };
                if (!string.IsNullOrWhiteSpace(practiceData.StreetAddress1)) address.Line.Add(practiceData.StreetAddress1);
                if (!string.IsNullOrWhiteSpace(practiceData.StreetAddress2)) address.Line.Add(practiceData.StreetAddress2);

                if (address.Line.Any() || !string.IsNullOrWhiteSpace(address.City)) // Only add if address has some data
                {
                    organization.Address.Add(address);
                }
            }

            // --- Contact ---
            // TODO: Add contact persons if available in PracticeInfo (not obvious from schema)

            // --- Endpoints ---
            // TODO: Add relevant endpoints if PracticeInfo contains URLs for services (e.g., FHIR endpoint, announce_url?)

            return organization;
        }

         // <<< FLAG: This internal domain model simulation is for placeholder purposes >>>
         // Replace with actual POC.Domain.DomainModels.PracticeInfoDomain when available
         internal class PracticeInfoDomain
         {
             public int ID { get; set; }
             public string PracticeName { get; set; }
             public string StreetAddress1 { get; set; }
             public string StreetAddress2 { get; set; }
             public string City { get; set; }
             public string State { get; set; }
             public string Zip { get; set; }
             public string Phone1 { get; set; }
             public string fax { get; set; }
             public string EIN { get; set; }
             public string NPI { get; set; }
             public string CLIA { get; set; }
             public string email { get; set; }
             public string UniquePracticeID { get; set; }
             // Add other fields used in mapping...
         }
    }
}
