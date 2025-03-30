using Hl7.Fhir.Model;
using Hl7.Fhir.R4;
using ClaudeAcDirectSql.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class DiagnosticReportMapper
    {
        // Maps a LabReportItem (representing a lab order/report) to a FHIR DiagnosticReport
        // Assumes individual results (Observations) are mapped separately and linked via `result`
        public static DiagnosticReport ToFhirDiagnosticReport(LabReportItem source, List<Observation> associatedResults)
        {
            if (source == null)
            {
                return null;
            }

            var report = new DiagnosticReport();

            // --- Set Resource ID ---
            // TODO: Define ID strategy (e.g., prefix + ReportId/OrderId)
            if (source.ReportId.HasValue)
            {
                report.Id = $"DiagnosticReport-{source.ReportId.Value}";
            }

            // --- Based On (Reference to ServiceRequest) ---
            // TODO: Link to the original lab order (ServiceRequest) if available.

            // --- Status (Mandatory) ---
            // TODO: Map source.Status to FHIR DiagnosticReportStatus (registered, partial, preliminary, final, amended, ...)
            report.Status = DiagnosticReportStatus.Final; // <<< FLAG (Defaulting to final, needs mapping)
            // report.Status = MapStatus(source.Status);

            // --- Category (Mandatory in US Core) ---
            // TODO: Set category, usually 'LAB' for lab reports.
            report.Category.Add(new CodeableConcept("http://terminology.hl7.org/CodeSystem/v2-0074", "LAB", "Laboratory")); // <<< FLAG (Confirm system/code)

            // --- Code (LOINC Code for the Order/Panel - Mandatory) ---
            // TODO: Map source.OrderCode/Name to LOINC CodeableConcept for the overall test/panel.
            if (!string.IsNullOrWhiteSpace(source.OrderCode) || !string.IsNullOrWhiteSpace(source.OrderName))
            {
                report.Code = new CodeableConcept();
                if (!string.IsNullOrWhiteSpace(source.OrderCode))
                {
                     report.Code.Coding.Add(new Coding("http://loinc.org", source.OrderCode)); // <<< FLAG (Confirm system is LOINC)
                }
                report.Code.Text = source.OrderName;
            }
            else
            {
                // TODO: Handle missing required report code (validation will fail)
            }

            // --- Subject (Patient Reference - Mandatory) ---
            // TODO: Create a Patient reference based on source.PatientId
            if (source.PatientId.HasValue)
            {
                report.Subject = new ResourceReference($"Patient/{source.PatientId.Value}"); // <<< FLAG: Ensure Patient ID strategy matches
            }
            else
            {
                // TODO: Handle missing patient link (validation will fail)
            }

            // --- Encounter Reference ---
            // TODO: Create an Encounter reference based on source.EncounterId (if available)
            if (!string.IsNullOrWhiteSpace(source.EncounterId))
            {
                report.Encounter = new ResourceReference($"Encounter/{source.EncounterId}"); // <<< FLAG: Define Encounter ID strategy
            }

            // --- Effective Date/Time/Period (Mandatory in US Core if known) ---
            // TODO: Map source.EffectiveDate (Clinically relevant time - collection or report time?)
            if (!string.IsNullOrWhiteSpace(source.EffectiveDate))
            {
                if (DateTime.TryParse(source.EffectiveDate, out DateTime effectiveDt))
                {
                    report.Effective = new FhirDateTime(new DateTimeOffset(effectiveDt));
                }
                else
                {
                     report.Effective = new FhirString(source.EffectiveDate);
                }
            }
            // else { /* TODO: Handle potentially missing effective date */ }

            // --- Issued Date/Time (When report was released - Mandatory in US Core if known) ---
            // TODO: Map issued time if available and different from effective time.
            // report.Issued = ...; // <<< FLAG

            // --- Performer ---
            // TODO: Map source.PerformerId (Performing Lab) to Organization/Practitioner reference.
            // if (!string.IsNullOrWhiteSpace(source.PerformerId))
            // {
            //     report.Performer.Add(ResolveOrganizationReference(source.PerformerId)); // <<< FLAG (Assuming Lab is Org)
            // }

            // --- Results Interpreter ---
            // TODO: Map interpreting provider if available.

            // --- Specimen ---
            // TODO: Link to Specimen resource(s) if available.

            // --- Result (Reference to Observations - Mandatory in US Core if Observations exist) ---
            // TODO: Link the associated Observation resources mapped from LabResultItem
            if (associatedResults != null && associatedResults.Any())
            {
                foreach (var obs in associatedResults)
                {
                    if (!string.IsNullOrWhiteSpace(obs.Id))
                    {
                        report.Result.Add(new ResourceReference($"Observation/{obs.Id}"));
                    }
                     // else { /* Handle Observations without IDs? Should not happen if mapped correctly */ }
                }
            }
            // else { /* TODO: Handle cases with no results? Report status might be 'registered' */ }

            // --- Imaging Study ---
            // TODO: Link to ImagingStudy if this is a radiology report.

            // --- Media ---
            // TODO: Link to Media resources (images, waveforms) if applicable.

            // --- Conclusion ---
            // TODO: Map overall report conclusion/summary text (source.ReportText?)
            if (!string.IsNullOrWhiteSpace(source.ReportText))
            {
                 report.Conclusion = source.ReportText;
            }

            // --- Conclusion Code ---
            // TODO: Map coded conclusion if available.

            // --- Presented Form (Attachment) ---
            // TODO: Attach original report (PDF, RTF, etc.) if available.
            // var attachment = new Attachment { ContentType = "application/pdf", Data = ... };
            // report.PresentedForm.Add(attachment);


            // --- Set Profile ---
            report.Meta = new Meta();
            report.Meta.Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-diagnosticreport-lab" };

            return report;
        }

         // --- TODO: Implement Helper Methods ---
        // private static DiagnosticReportStatus MapStatus(string sourceStatus) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolvePatientReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolveEncounterReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolveOrganizationReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }
    }
}
