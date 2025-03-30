using Hl7.Fhir.Model;
using ClaudeAcDirectSql.Models;
// using POC.Domain.RepositoryDefinitions; // <<< FLAG: Uncomment and ensure correct namespace for IReferProvidersRepository
// using POC.Domain.DomainModels;       // <<< FLAG: Uncomment and ensure correct namespace for ReferProvidersDomain
using System.Collections.Generic;
using System.Linq;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class PractitionerMapper
    {
        // <<< FLAG: Dependency Injection needed for repository access >>>
        // private static IReferProvidersRepository _referProvidersRepository;
        // public static void Initialize(IReferProvidersRepository referProvidersRepository)
        // {
        //     _referProvidersRepository = referProvidersRepository;
        // }

        /// <summary>
        /// Maps ReferProviders data to a FHIR Practitioner resource.
        /// Retrieves full data using IReferProvidersRepository if an ID is provided.
        /// <<< FLAG: Verify mapping logic, identifiers, name format, telecom use codes, address format with team >>>
        /// </summary>
        /// <param name="referProviderId">The ID of the referring provider to map (used for lookup).</param>
        /// <returns>A FHIR Practitioner resource.</returns>
        public static Practitioner GetFhirPractitioner(int referProviderId)
        {
            // <<< FLAG: Implement actual data retrieval using injected repository >>>
            // ReferProvidersDomain providerData = _referProvidersRepository?.GetReferProviders(referProviderId);
            ReferProvidersDomain providerData = null; // Placeholder for retrieved data

            // Simulate retrieval for placeholder mapping
            if (providerData == null) {
                providerData = new ReferProvidersDomain { /* Populate with dummy data based on ID? */ };
                // <<< FLAG: Handle case where provider info is not found for the given ID >>>
            }

            if (providerData == null) return null;

            var practitioner = new Practitioner();

            // --- ID ---
            // <<< FLAG: Define final strategy for Practitioner ID. Using ReferProviderID? >>>
            practitioner.Id = $"pract-{referProviderId}"; // Example ID strategy using the input referProviderId

            // --- Identifier ---
            // <<< FLAG: Add relevant identifiers (NPI, ReferringNumber) confirmed from ReferProviders table >>>
            // <<< FLAG: Confirm identifier systems and use codes (official, usual). Is ReferringNumber a specific type? >>>
            if (!string.IsNullOrWhiteSpace(providerData.NPI))
            {
                practitioner.Identifier.Add(new Identifier
                {
                    System = "http://hl7.org/fhir/sid/us-npi", // <<< FLAG: Confirm system URI
                    Value = providerData.NPI,
                    Use = Identifier.IdentifierUse.Official // <<< FLAG: Confirm Use
                });
            }
            if (!string.IsNullOrWhiteSpace(providerData.ReferringNumber))
            {
                 practitioner.Identifier.Add(new Identifier
                 {
                     // System = ??? // <<< FLAG: What system does ReferringNumber belong to?
                     Value = providerData.ReferringNumber,
                     Use = Identifier.IdentifierUse.Usual // <<< FLAG: Confirm Use (Usual? Official? Other?)
                 });
            }
            // TODO: Add License Number, DEA Number identifiers if source becomes available

            // --- Active Status ---
            // <<< FLAG: ReferProviders table schema doesn't show an explicit 'Active' field. Assume active? Needs confirmation. >>>
            practitioner.Active = true; // Defaulting to true

            // --- Name ---
            // <<< FLAG: Confirm name assembly logic (prefix, suffix). MiddleName not present in ReferProviders table. >>>
            var humanName = new HumanName()
            {
                Use = HumanName.NameUse.Official, // <<< FLAG: Confirm use (Official? Usual?)
                Family = providerData.Lastname,
                Given = new List<string> { providerData.Firstname }
                // No MiddleName field available in ReferProviders
            };
            if (!string.IsNullOrWhiteSpace(providerData.prefix)) humanName.Prefix = new List<string> { providerData.prefix };
            if (!string.IsNullOrWhiteSpace(providerData.suffix)) humanName.Suffix = new List<string> { providerData.suffix };

            practitioner.Name.Add(humanName);

            // --- Telecom ---
            // <<< FLAG: Map phone, fax, email using actual field names (phone, fax, email). Confirm ContactPointUse codes (work, mobile, etc.) >>>
            if (!string.IsNullOrWhiteSpace(providerData.phone))
            {
                practitioner.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Phone, Value = providerData.phone, Use = ContactPoint.ContactPointUse.Work });
            }
             if (!string.IsNullOrWhiteSpace(providerData.fax))
            {
                practitioner.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Fax, Value = providerData.fax, Use = ContactPoint.ContactPointUse.Work });
            }
            if (!string.IsNullOrWhiteSpace(providerData.email))
            {
                practitioner.Telecom.Add(new ContactPoint { System = ContactPoint.ContactPointSystem.Email, Value = providerData.email, Use = ContactPoint.ContactPointUse.Work });
            }

            // --- Address ---
            // <<< FLAG: Map address fields using actual field names (address1, address2, city, state, zip). Confirm AddressUse/Type codes. >>>
            if (!string.IsNullOrWhiteSpace(providerData.address1) || !string.IsNullOrWhiteSpace(providerData.city))
            {
                var address = new Address
                {
                    Use = Address.AddressUse.Work, // <<< FLAG: Confirm use
                    Type = Address.AddressType.Physical, // <<< FLAG: Confirm type
                    Line = new List<string>(),
                    City = providerData.city,
                    State = providerData.state, // <<< FLAG: Requires state mapping if not abbreviation
                    PostalCode = providerData.zip,
                    Country = "USA" // <<< FLAG: Confirm default/source field if available
                };
                if (!string.IsNullOrWhiteSpace(providerData.address1)) address.Line.Add(providerData.address1);
                if (!string.IsNullOrWhiteSpace(providerData.address2)) address.Line.Add(providerData.address2);

                if (address.Line.Any() || !string.IsNullOrWhiteSpace(address.City)) // Only add if address has some data
                {
                    practitioner.Address.Add(address);
                }
            }

            // --- Gender ---
            // <<< FLAG: Source for Practitioner gender? Not in current ReferProviders placeholder/table. >>>
            // practitioner.Gender = AdministrativeGender.Unknown;

            // --- BirthDate ---
            // <<< FLAG: Source for Practitioner birth date? Not in current ReferProviders placeholder/table. >>>
            // practitioner.BirthDate = "YYYY-MM-DD";

            // --- Qualification ---
            // <<< FLAG: Map Specialty field. Needs mapping to standard specialty codeset if possible. License info not in ReferProviders table. >>>
            if (!string.IsNullOrWhiteSpace(providerData.Specialty))
            {
                 var qualification = new Practitioner.QualificationComponent
                 {
                      Code = new CodeableConcept { Text = providerData.Specialty } // <<< FLAG: Use standard code system if possible (e.g., NUCC Health Care Provider Taxonomy)
                      // TODO: Add issuer, period if available/relevant
                 };
                 practitioner.Qualification.Add(qualification);
            }
            // TODO: Map License Number if source becomes available

            // --- Communication ---
            // <<< FLAG: Map language preferences if source becomes available >>>
            // TODO: Map language preferences if available

            return practitioner;
        }

        // <<< FLAG: This internal domain model simulation is for placeholder purposes >>>
        // Replace with actual POC.Domain.DomainModels.ReferProvidersDomain when available
        internal class ReferProvidersDomain
        {
            public int ReferProviderID { get; set; }
            public string ReferringNumber { get; set; }
            public string Specialty { get; set; }
            public string Lastname { get; set; }
            public string Firstname { get; set; }
            public string suffix { get; set; }
            public string prefix { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string zip { get; set; }
            public string phone { get; set; }
            public string fax { get; set; }
            public string email { get; set; }
            public string NPI { get; set; }
            // Add other fields used in mapping...
        }
    }
}
