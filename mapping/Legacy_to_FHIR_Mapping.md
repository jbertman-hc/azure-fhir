# Legacy EHR to FHIR R4 Mapping Documentation

## Overview

This document outlines the comprehensive mapping strategy for transforming Amazing Charts EHR data to FHIR R4 resources. The mapping is designed to support a modern EHR frontend with billing and scheduling capabilities while ensuring compliance with healthcare interoperability standards.

## What We're Building

We are creating a FHIR façade over our existing legacy EHR system that will:

1. **Expose FHIR R4 APIs**: Provide standard RESTful FHIR endpoints for all core clinical, administrative, and financial resources.
2. **Transform Legacy Data**: Map data from our existing SQL database to FHIR resources in real-time.
3. **Support Modern Frontend**: Enable a new EHR frontend with enhanced billing and scheduling capabilities.
4. **Ensure Interoperability**: Comply with US Core Implementation Guide and other relevant standards.
5. **Maintain Data Integrity**: Preserve all clinical information during the transformation process.

The architecture follows a three-tier model:
- **Presentation Layer**: Modern EHR frontend consuming FHIR APIs
- **Business Logic Layer**: FHIR API service with mapping capabilities
- **Data Access Layer**: Legacy SQL database

This approach allows us to modernize our application while preserving our existing data infrastructure.

## Table of Contents

1. [Mapping Approach](#mapping-approach)
2. [Core Resource Mappings](#core-resource-mappings)
3. [Detailed Field Mappings](#detailed-field-mappings)
4. [Terminology Mappings](#terminology-mappings)
5. [Extensions and Profiles](#extensions-and-profiles)
6. [Implementation Considerations](#implementation-considerations)
7. [Validation Strategy](#validation-strategy)
8. [Implementation Roadmap](#implementation-roadmap)

## Mapping Approach

The mapping process follows these principles:

1. **Source of Truth**: The primary source of truth is the SQL database schema as represented by POCOs in the `DataAccess/SQLPocos` directory.
2. **Domain Model**: Transformations go through domain models before becoming FHIR resources.
3. **US Core Compliance**: All resources aim to comply with US Core Implementation Guide where applicable.
4. **Required vs. Optional**: Mappings distinguish between required FHIR elements and optional ones.
5. **Terminology Binding**: Coded values are mapped to standard terminologies where possible.

The mapping workflow is:

```
SQLPocos → Domain Models → FHIR Resources
```

## Core Resource Mappings

The following table outlines the primary entity-to-FHIR resource mappings:

| Legacy Entity | FHIR R4 Resource | Use Case | Priority |
|---------------|------------------|----------|----------|
| Demographics | Patient | Core patient information | High |
| PracticeInfo | Organization | Provider organizations | High |
| ProviderSecurity | Practitioner | Internal healthcare providers | High |
| ProviderSecurity | PractitionerRole | Provider roles and relationships | High |
| ReferProviders | Practitioner | External referring providers | Medium |
| Scheduling | Appointment | Patient appointments | High |
| SOAP | Encounter | Patient visits | High |
| ListProblem | Condition | Patient diagnoses | High |
| ListMEDS | MedicationRequest | Medication orders | High |
| ListAllergies | AllergyIntolerance | Patient allergies | Medium |
| LabResults | Observation | Lab results | Medium |
| VitalSigns | Observation | Patient measurements | Medium |
| ListProcedures | Procedure | Clinical procedures | Medium |
| BillingCPTs | Claim/ChargeItem | Billing information | High |
| ListPayors | Coverage | Insurance information | High |
| PatientCharges | Invoice | Patient billing | High |
| AssociatedParties | RelatedPerson | Patient contacts/guarantors | Medium |
| ListInjections | Immunization | Vaccines and injections | Medium |
| Locations | Location | Care delivery locations | Medium |
| Renewals | CarePlan | Care planning and renewals | Medium |
| Orders | ServiceRequest | Clinical orders | Medium |
| LabResults | DiagnosticReport | Diagnostic reports | Medium |
| VaccineLotNumbers | Device | Medical devices/supplies | Low |
| ListMessagedProvider | Communication | Provider communications | Low |

## Detailed Field Mappings

### Patient Resource (from Demographics)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| PatientID | Patient.identifier[0].value | string | Yes | Use system "urn:id:mrn" |
| ChartID | Patient.identifier[1].value | string | Yes | Use system "urn:id:chartid" |
| SS | Patient.identifier[2].value | string | No | Use system "http://hl7.org/fhir/sid/us-ssn" |
| First | Patient.name[0].given[0] | string | Yes | |
| Middle | Patient.name[0].given[1] | string | No | |
| Last | Patient.name[0].family | string | Yes | |
| Suffix | Patient.name[0].suffix[0] | string | No | |
| Gender | Patient.gender | code | Yes | Map "M" → "male", "F" → "female" |
| BirthDate | Patient.birthDate | date | Yes | Format as YYYY-MM-DD |
| DateOfDeath | Patient.deceasedDateTime | dateTime | No | Format as YYYY-MM-DDThh:mm:ss+zz:zz |
| PatientAddress | Patient.address[0].line[0] | string | No | |
| City | Patient.address[0].city | string | No | |
| State | Patient.address[0].state | string | No | |
| Zip | Patient.address[0].postalCode | string | No | |
| Phone | Patient.telecom[0].value | string | No | system=phone, use=home |
| WorkPhone | Patient.telecom[1].value | string | No | system=phone, use=work |
| Email | Patient.telecom[2].value | string | No | system=email |
| MaritalStatus | Patient.maritalStatus | CodeableConcept | No | Map to http://terminology.hl7.org/CodeSystem/v3-MaritalStatus |
| PatientRace | Patient.extension[0] | Extension | No | URL="http://hl7.org/fhir/us/core/StructureDefinition/us-core-race" |
| EthnicityID | Patient.extension[1] | Extension | No | URL="http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity" |
| LanguagePreference | Patient.communication.language | CodeableConcept | No | Use IETF language tags |
| Inactive | Patient.active | boolean | No | Inverse of Inactive value |

### Practitioner Resource (from ProviderSecurity)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| ProviderID | Practitioner.identifier[0].value | string | Yes | Use system "urn:id:internal-provider" |
| ProviderName | Practitioner.identifier[1].value | string | No | Use system "urn:id:username" |
| FirstName | Practitioner.name[0].given[0] | string | Yes | |
| MiddleName | Practitioner.name[0].given[1] | string | No | |
| LastName | Practitioner.name[0].family | string | Yes | |
| Degree | Practitioner.qualification[0].code.text | string | No | |
| NPI | Practitioner.identifier[2].value | string | No | Use system "http://hl7.org/fhir/sid/us-npi" |
| DEA | Practitioner.qualification[1].identifier.value | string | No | Use system "urn:oid:2.16.840.1.113883.4.814" |
| StateLicenseNumber | Practitioner.qualification[2].identifier.value | string | No | Use system "http://hl7.org/fhir/v2/0203" and code "MD" |
| Specialty | Practitioner.qualification[3].code | CodeableConcept | No | Map to standard specialty codes |
| TaxonomyCode1 | Practitioner.qualification[4].code | CodeableConcept | No | Use system "http://nucc.org/provider-taxonomy" |
| TaxonomyCode2 | Practitioner.qualification[5].code | CodeableConcept | No | Use system "http://nucc.org/provider-taxonomy" |
| Inactive | Practitioner.active | boolean | No | Inverse of Inactive value |

### PractitionerRole Resource (from ProviderSecurity)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| ProviderID | PractitionerRole.practitioner | Reference(Practitioner) | Yes | Reference to Practitioner |
| ProviderLevel | PractitionerRole.code[0] | CodeableConcept | Yes | Map to standard role codes |
| Supervisor | PractitionerRole.extension[0] | Extension | No | URL="http://example.org/fhir/StructureDefinition/supervisor" |
| CoSignReq | PractitionerRole.extension[1] | Extension | No | URL="http://example.org/fhir/StructureDefinition/cosign-required" |
| Inactive | PractitionerRole.active | boolean | No | Inverse of Inactive value |
| AllowBillingAccess | PractitionerRole.extension[2] | Extension | No | URL="http://example.org/fhir/StructureDefinition/billing-access" |
| AllowAccessToPatientHealthInfo | PractitionerRole.extension[3] | Extension | No | URL="http://example.org/fhir/StructureDefinition/phi-access" |

### External Practitioner Resource (from ReferProviders)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| ReferProviderID | Practitioner.identifier[0].value | string | Yes | Use system "urn:id:external-provider" |
| FirstName | Practitioner.name[0].given[0] | string | Yes | |
| LastName | Practitioner.name[0].family | string | Yes | |
| Credentials | Practitioner.qualification[0].code | CodeableConcept | No | |
| Specialty | Practitioner.qualification[1].code | CodeableConcept | No | Map to NUCC provider taxonomy |
| Address | Practitioner.address[0].line[0] | string | No | |
| City | Practitioner.address[0].city | string | No | |
| State | Practitioner.address[0].state | string | No | |
| Zip | Practitioner.address[0].postalCode | string | No | |
| Phone | Practitioner.telecom[0].value | string | No | system=phone |
| Fax | Practitioner.telecom[1].value | string | No | system=fax |
| Email | Practitioner.telecom[2].value | string | No | system=email |
| NPI | Practitioner.identifier[1].value | string | No | Use system "http://hl7.org/fhir/sid/us-npi" |

### Organization Resource (from PracticeInfo)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| PracticeID | Organization.identifier[0].value | string | Yes | |
| PracticeName | Organization.name | string | Yes | |
| Address | Organization.address[0].line[0] | string | No | |
| City | Organization.address[0].city | string | No | |
| State | Organization.address[0].state | string | No | |
| Zip | Organization.address[0].postalCode | string | No | |
| Phone | Organization.telecom[0].value | string | No | system=phone |
| Fax | Organization.telecom[1].value | string | No | system=fax |
| Email | Organization.telecom[2].value | string | No | system=email |
| TaxID | Organization.identifier[1].value | string | No | Use system "urn:oid:2.16.840.1.113883.4.4" |

### Appointment Resource (from Scheduling)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| AppointmentID | Appointment.identifier[0].value | string | Yes | |
| PatientID | Appointment.participant[0].actor | Reference(Patient) | Yes | |
| ProviderID | Appointment.participant[1].actor | Reference(Practitioner) | Yes | |
| AppointmentDate | Appointment.start | dateTime | Yes | |
| Duration | Appointment.end | dateTime | Yes | Calculate from start + duration |
| Status | Appointment.status | code | Yes | Map to FHIR appointment status codes |
| AppointmentType | Appointment.appointmentType | CodeableConcept | No | |
| Reason | Appointment.reason[0] | CodeableConcept | No | |
| Notes | Appointment.comment | string | No | |

### Encounter Resource (from SOAP)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| SOAPID | Encounter.identifier[0].value | string | Yes | |
| PatientID | Encounter.subject | Reference(Patient) | Yes | |
| VisitDate | Encounter.period.start | dateTime | Yes | |
| ProviderID | Encounter.participant[0].individual | Reference(Practitioner) | Yes | |
| VisitType | Encounter.type[0] | CodeableConcept | No | |
| Status | Encounter.status | code | Yes | Map to FHIR encounter status codes |
| Location | Encounter.location[0].location | Reference(Location) | No | |
| CC | Encounter.reasonCode[0] | CodeableConcept | No | Chief complaint as reason for visit |

### DocumentReference Resource (from SOAP narrative components)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| VisitNumber | DocumentReference.identifier[0].value | string | Yes | |
| PatientID | DocumentReference.subject | Reference(Patient) | Yes | |
| EncounterDate | DocumentReference.date | dateTime | Yes | |
| ProviderID | DocumentReference.author[0] | Reference(Practitioner) | Yes | |
| CC | DocumentReference.content[0].attachment.data | base64Binary | No | Section title: "Chief Complaint" |
| HPI | DocumentReference.content[0].attachment.data | base64Binary | No | Section title: "History of Present Illness" |
| ROS | DocumentReference.content[0].attachment.data | base64Binary | No | Section title: "Review of Systems" |
| PMH | DocumentReference.content[0].attachment.data | base64Binary | No | Section title: "Past Medical History" |
| SH | DocumentReference.content[0].attachment.data | base64Binary | No | Section title: "Social History" |
| FH | DocumentReference.content[0].attachment.data | base64Binary | No | Section title: "Family History" |
| PE | DocumentReference.content[0].attachment.data | base64Binary | No | Section title: "Physical Examination" |
| Ass | DocumentReference.content[0].attachment.data | base64Binary | No | Section title: "Assessment" |
| Plan | DocumentReference.content[0].attachment.data | base64Binary | No | Section title: "Plan" |
| Instructions | DocumentReference.content[0].attachment.data | base64Binary | No | Section title: "Instructions" |

### Observation Resources (from SOAP vital signs)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| BP | Observation.code | CodeableConcept | Yes | LOINC: 85354-9 (Blood pressure panel) |
| BP | Observation.component[0].code | CodeableConcept | Yes | LOINC: 8480-6 (Systolic blood pressure) |
| BP | Observation.component[0].valueQuantity | Quantity | Yes | Parse systolic value from BP string |
| BP | Observation.component[1].code | CodeableConcept | Yes | LOINC: 8462-4 (Diastolic blood pressure) |
| BP | Observation.component[1].valueQuantity | Quantity | Yes | Parse diastolic value from BP string |
| Temp | Observation.code | CodeableConcept | Yes | LOINC: 8310-5 (Body temperature) |
| Temp | Observation.valueQuantity | Quantity | Yes | |
| RR | Observation.code | CodeableConcept | Yes | LOINC: 9279-1 (Respiratory rate) |
| RR | Observation.valueQuantity | Quantity | Yes | |
| Pulse | Observation.code | CodeableConcept | Yes | LOINC: 8867-4 (Heart rate) |
| Pulse | Observation.valueQuantity | Quantity | Yes | |
| Weight | Observation.code | CodeableConcept | Yes | LOINC: 29463-7 (Body weight) |
| Weight | Observation.valueQuantity | Quantity | Yes | |
| Height | Observation.code | CodeableConcept | Yes | LOINC: 8302-2 (Body height) |
| Height | Observation.valueQuantity | Quantity | Yes | |
| BMI | Observation.code | CodeableConcept | Yes | LOINC: 39156-5 (Body mass index) |
| BMI | Observation.valueQuantity | Quantity | Yes | |
| HC | Observation.code | CodeableConcept | Yes | LOINC: 9843-4 (Head circumference) |
| HC | Observation.valueQuantity | Quantity | Yes | |
| Sat | Observation.code | CodeableConcept | Yes | LOINC: 59408-5 (Oxygen saturation) |
| Sat | Observation.valueQuantity | Quantity | Yes | |
| Pain | Observation.code | CodeableConcept | Yes | LOINC: 72514-3 (Pain severity score) |
| Pain | Observation.valueQuantity | Quantity | Yes | |
| PF | Observation.code | CodeableConcept | Yes | LOINC: 19935-1 (Peak flow rate) |
| PF | Observation.valueQuantity | Quantity | Yes | |

### ClinicalImpression Resource (from SOAP assessment and plan)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| VisitNumber | ClinicalImpression.identifier[0].value | string | Yes | |
| PatientID | ClinicalImpression.subject | Reference(Patient) | Yes | |
| EncounterDate | ClinicalImpression.date | dateTime | Yes | |
| ProviderID | ClinicalImpression.assessor | Reference(Practitioner) | Yes | |
| Ass | ClinicalImpression.summary | string | Yes | Assessment summary |
| Plan | ClinicalImpression.note[0].text | string | No | Plan details |

### Condition Resource (from ListProblem)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| ProblemID | Condition.identifier[0].value | string | Yes | |
| PatientID | Condition.subject | Reference(Patient) | Yes | |
| DiagnosisCode | Condition.code.coding[0].code | string | Yes | |
| DiagnosisCodeSystem | Condition.code.coding[0].system | uri | Yes | Map "ICD10" → "http://hl7.org/fhir/sid/icd-10-cm" |
| DiagnosisDescription | Condition.code.text | string | No | |
| DateOfOnset | Condition.onsetDateTime | dateTime | No | |
| Status | Condition.clinicalStatus | CodeableConcept | Yes | Map to http://terminology.hl7.org/CodeSystem/condition-clinical |
| EnteredBy | Condition.recorder | Reference(Practitioner) | No | |
| DateEntered | Condition.recordedDate | dateTime | No | |

### MedicationRequest Resource (from ListMEDS)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| MedID | MedicationRequest.identifier[0].value | string | Yes | |
| PatientID | MedicationRequest.subject | Reference(Patient) | Yes | |
| MedName | MedicationRequest.medicationCodeableConcept.text | string | Yes | |
| DrugCode | MedicationRequest.medicationCodeableConcept.coding[0].code | string | No | |
| Dosage | MedicationRequest.dosageInstruction[0].text | string | No | |
| Frequency | MedicationRequest.dosageInstruction[0].timing.code | CodeableConcept | No | |
| StartDate | MedicationRequest.authoredOn | dateTime | Yes | |
| EndDate | MedicationRequest.dispenseRequest.validityPeriod.end | dateTime | No | |
| Prescriber | MedicationRequest.requester | Reference(Practitioner) | No | |
| Status | MedicationRequest.status | code | Yes | Map to FHIR medication request status codes |
| Instructions | MedicationRequest.dosageInstruction[0].patientInstruction | string | No | |

### AllergyIntolerance Resource (from ListAllergies)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| AllergyID | AllergyIntolerance.identifier[0].value | string | Yes | |
| PatientID | AllergyIntolerance.patient | Reference(Patient) | Yes | |
| AllergyName | AllergyIntolerance.code.text | string | Yes | |
| AllergyType | AllergyIntolerance.category[0] | code | No | Map to FHIR allergy category codes |
| Reaction | AllergyIntolerance.reaction[0].manifestation[0].text | string | No | |
| Severity | AllergyIntolerance.reaction[0].severity | code | No | Map to FHIR severity codes |
| DateEntered | AllergyIntolerance.recordedDate | dateTime | No | |
| Status | AllergyIntolerance.clinicalStatus | CodeableConcept | Yes | Map to http://terminology.hl7.org/CodeSystem/allergyintolerance-clinical |

### Claim Resource (from BillingCPTs)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| BillingID | Claim.identifier[0].value | string | Yes | |
| PatientID | Claim.patient | Reference(Patient) | Yes | |
| EncounterID | Claim.encounter | Reference(Encounter) | No | |
| BillingDate | Claim.created | dateTime | Yes | |
| ProviderID | Claim.provider | Reference(Practitioner) | Yes | |
| PayorID | Claim.insurer | Reference(Organization) | Yes | |
| CPTCode | Claim.item[0].productOrService.coding[0].code | string | Yes | Use system "http://www.ama-assn.org/go/cpt" |
| Diagnosis | Claim.diagnosis[0].diagnosisCodeableConcept | CodeableConcept | No | |
| BillingAmount | Claim.item[0].unitPrice | Money | Yes | |
| Status | Claim.status | code | Yes | Map to FHIR claim status codes |

### Coverage Resource (from ListPayors)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| PayorID | Coverage.identifier[0].value | string | Yes | |
| PatientID | Coverage.beneficiary | Reference(Patient) | Yes | |
| InsurancePlan | Coverage.payor[0] | Reference(Organization) | Yes | |
| PolicyNumber | Coverage.subscriberId | string | No | |
| GroupNumber | Coverage.class[0].value | string | No | |
| StartDate | Coverage.period.start | date | No | |
| EndDate | Coverage.period.end | date | No | |
| RelationshipToSubscriber | Coverage.relationship | CodeableConcept | No | Map to http://terminology.hl7.org/CodeSystem/subscriber-relationship |
| CoverageType | Coverage.type | CodeableConcept | No | |
| Priority | Coverage.order | positiveInt | No | |

### RelatedPerson Resource (from AssociatedParties)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| AssociatedPartiesID | RelatedPerson.identifier[0].value | string | Yes | |
| PatientID | RelatedPerson.patient | Reference(Patient) | Yes | Reference to the associated patient |
| FirstName | RelatedPerson.name[0].given[0] | string | Yes | |
| MiddleInitial | RelatedPerson.name[0].given[1] | string | No | |
| LastName | RelatedPerson.name[0].family | string | Yes | |
| Gender | RelatedPerson.gender | code | No | Map "M" → "male", "F" → "female" |
| DOB | RelatedPerson.birthDate | date | No | |
| SAGAddress1 | RelatedPerson.address[0].line[0] | string | No | |
| SAGAddress2 | RelatedPerson.address[0].line[1] | string | No | |
| City | RelatedPerson.address[0].city | string | No | |
| StateOrRegion | RelatedPerson.address[0].state | string | No | |
| PostalCode | RelatedPerson.address[0].postalCode | string | No | |
| Country | RelatedPerson.address[0].country | string | No | |
| PhoneNumber | RelatedPerson.telecom[0].value | string | No | system=phone |
| Email | RelatedPerson.telecom[1].value | string | No | system=email |
| Relationship | RelatedPerson.relationship | CodeableConcept | Yes | Map to http://terminology.hl7.org/CodeSystem/v3-RoleCode |
| SSN | RelatedPerson.identifier[1].value | string | No | Use system "http://hl7.org/fhir/sid/us-ssn" |
| Notes | RelatedPerson.communication[0].text | string | No | |
| IsNonPersonEntity | RelatedPerson.active | boolean | No | Set to false if IsNonPersonEntity is true |

### Immunization Resource (from ListInjections)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| InjectionID | Immunization.identifier[0].value | string | Yes | |
| PatientID | Immunization.patient | Reference(Patient) | Yes | |
| InjectionName | Immunization.vaccineCode.text | string | Yes | |
| LotNo | Immunization.lotNumber | string | No | |
| DateGiven | Immunization.occurrenceDateTime | dateTime | Yes | |
| RecordedBy | Immunization.performer[0].actor | Reference(Practitioner) | No | |
| Volume | Immunization.doseQuantity.value | decimal | No | |
| Route | Immunization.route | CodeableConcept | No | Map to http://terminology.hl7.org/CodeSystem/v3-RouteOfAdministration |
| Site | Immunization.site | CodeableConcept | No | Map to http://terminology.hl7.org/CodeSystem/v3-ActSite |
| Manufacturer | Immunization.manufacturer.display | string | No | |
| Expiration | Immunization.expirationDate | date | No | |
| Comment | Immunization.note[0].text | string | No | |
| IsGivenElsewhere | Immunization.reportOrigin | CodeableConcept | No | Set if IsGivenElsewhere is true |
| Deleted | Immunization.status | code | Yes | Map to "entered-in-error" if Deleted is true, otherwise "completed" |
| NDCCode | Immunization.vaccineCode.coding[0].code | string | No | Use system "http://hl7.org/fhir/sid/ndc" |

### Location Resource (from Locations)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| LocationsID | Location.identifier[0].value | string | Yes | |
| Locations | Location.name | string | Yes | |
| Address1 | Location.address.line[0] | string | No | |
| Address2 | Location.address.line[1] | string | No | |
| City | Location.address.city | string | No | |
| StateOrRegion | Location.address.state | string | No | |
| PostalCode | Location.address.postalCode | string | No | |
| Country | Location.address.country | string | No | |
| IsDefault | Location.status | code | Yes | Map to "active" if IsDefault is true |
| Type | Location.type | CodeableConcept | No | Map to http://terminology.hl7.org/CodeSystem/v3-RoleCode |
| ManagingOrganization | Location.managingOrganization | Reference(Organization) | No | Reference to the managing organization |

### CarePlan Resource (from Renewals)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| RenewalID | CarePlan.identifier[0].value | string | Yes | |
| PatientID | CarePlan.subject | Reference(Patient) | Yes | |
| ProviderID | CarePlan.author | Reference(Practitioner) | No | |
| DateRequested | CarePlan.created | dateTime | Yes | |
| Status | CarePlan.status | code | Yes | Map to appropriate FHIR status codes |
| MedicationID | CarePlan.activity[0].reference | Reference(MedicationRequest) | No | |
| Instructions | CarePlan.activity[0].detail.description | string | No | |
| Comments | CarePlan.note[0].text | string | No | |
| DateCompleted | CarePlan.period.end | dateTime | No | |
| Category | CarePlan.category[0] | CodeableConcept | No | Set to "medication" for medication renewals |

### ServiceRequest Resource (from Orders)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| OrderID | ServiceRequest.identifier[0].value | string | Yes | |
| PatientID | ServiceRequest.subject | Reference(Patient) | Yes | |
| OrderingProviderID | ServiceRequest.requester | Reference(Practitioner) | No | |
| OrderDate | ServiceRequest.authoredOn | dateTime | Yes | |
| OrderType | ServiceRequest.category[0] | CodeableConcept | No | Map to appropriate order type |
| OrderStatus | ServiceRequest.status | code | Yes | Map to FHIR request status codes |
| OrderDetails | ServiceRequest.code | CodeableConcept | Yes | |
| Reason | ServiceRequest.reasonCode[0] | CodeableConcept | No | |
| Instructions | ServiceRequest.patientInstruction | string | No | |
| Priority | ServiceRequest.priority | code | No | Map to FHIR request priority codes |
| FacilityID | ServiceRequest.locationReference | Reference(Location) | No | |

### DiagnosticReport Resource (from LabResults)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| ResultID | DiagnosticReport.identifier[0].value | string | Yes | |
| PatientID | DiagnosticReport.subject | Reference(Patient) | Yes | |
| OrderingProviderID | DiagnosticReport.performer[0] | Reference(Practitioner) | No | |
| ResultDate | DiagnosticReport.effectiveDateTime | dateTime | Yes | |
| LabName | DiagnosticReport.performer[1] | Reference(Organization) | No | |
| TestName | DiagnosticReport.code | CodeableConcept | Yes | |
| TestStatus | DiagnosticReport.status | code | Yes | Map to FHIR diagnostic report status codes |
| ResultValue | DiagnosticReport.result | Reference(Observation) | No | References to individual observations |
| Interpretation | DiagnosticReport.conclusionCode[0] | CodeableConcept | No | |
| Comments | DiagnosticReport.conclusion | string | No | |
| ReportLink | DiagnosticReport.presentedForm[0].url | url | No | |

### Device Resource (from VaccineLotNumbers)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| LotNumberID | Device.identifier[0].value | string | Yes | |
| VaccineName | Device.type | CodeableConcept | Yes | |
| LotNumber | Device.lotNumber | string | No | |
| Manufacturer | Device.manufacturer | string | No | |
| ExpirationDate | Device.expirationDate | date | No | |
| NDCCode | Device.identifier[1].value | string | No | Use system "http://hl7.org/fhir/sid/ndc" |
| Status | Device.status | code | Yes | Map to FHIR device status codes |
| Location | Device.location | Reference(Location) | No | |

### Communication Resource (from ListMessagedProvider)

| Legacy Field | FHIR Path | Data Type | Required | Notes |
|--------------|-----------|-----------|----------|-------|
| MessageID | Communication.identifier[0].value | string | Yes | |
| SenderID | Communication.sender | Reference(Practitioner) | No | |
| RecipientID | Communication.recipient[0] | Reference(Practitioner) | No | |
| PatientID | Communication.subject | Reference(Patient) | Yes | |
| MessageDate | Communication.sent | dateTime | Yes | |
| MessageContent | Communication.payload[0].contentString | string | No | |
| MessageType | Communication.category[0] | CodeableConcept | No | |
| MessageStatus | Communication.status | code | Yes | Map to FHIR communication status codes |
| Priority | Communication.priority | code | No | Map to FHIR request priority codes |
| RelatedEncounterID | Communication.encounter | Reference(Encounter) | No | |

## Terminology Mappings

### Gender Codes
- "M" → "male"
- "F" → "female"
- "O" → "other"
- "U" → "unknown"

### Marital Status
- "S" → "S" (Single)
- "M" → "M" (Married)
- "D" → "D" (Divorced)
- "W" → "W" (Widowed)
- "L" → "L" (Legally Separated)

### Diagnosis Code Systems
- "ICD9" → "http://hl7.org/fhir/sid/icd-9-cm"
- "ICD10" → "http://hl7.org/fhir/sid/icd-10-cm"
- "SNOMED" → "http://snomed.info/sct"

### Medication Code Systems
- "NDC" → "http://hl7.org/fhir/sid/ndc"
- "RXNORM" → "http://www.nlm.nih.gov/research/umls/rxnorm"

### Procedure Code Systems
- "CPT" → "http://www.ama-assn.org/go/cpt"
- "HCPCS" → "http://www.cms.gov/Medicare/Coding/HCPCSReleaseCodeSets"

## Extensions and Profiles

The following US Core profiles will be implemented:

- US Core Patient Profile
- US Core Practitioner Profile
- US Core Organization Profile
- US Core Encounter Profile
- US Core Condition Profile
- US Core MedicationRequest Profile
- US Core AllergyIntolerance Profile
- US Core Vital Signs Profiles

Custom extensions may be needed for:

1. **Patient Insurance Information**: To capture additional insurance details
2. **Billing Codes**: To support legacy billing codes not in standard terminologies
3. **Practice-Specific Workflows**: To capture custom workflow states

## Implementation Considerations

### Technical Approach

1. **Mapper Pattern**: Implement dedicated mapper classes for each resource type that transform domain models to FHIR resources.
2. **Caching Strategy**: Consider caching frequently accessed reference data like code systems and value sets.
3. **Error Handling**: Implement robust error handling for missing or invalid data.
4. **Performance Optimization**: Use lazy loading for related resources and batch processing for large datasets.
5. **Versioning**: Support FHIR versioning to track changes to resources over time.

### Data Quality

1. **Required vs. Optional**: Handle missing data appropriately, especially for required FHIR elements.
2. **Data Validation**: Implement validation at both the domain model and FHIR resource levels.
3. **Default Values**: Define sensible defaults for missing data where appropriate.
4. **Data Cleaning**: Implement data cleaning routines for legacy data with quality issues.

### Security and Privacy

1. **Data Masking**: Implement appropriate data masking for sensitive information.
2. **Access Control**: Ensure proper access controls are in place for FHIR resources.
3. **Audit Logging**: Log all access and modifications to FHIR resources.
4. **Consent Management**: Consider how patient consent will be managed in the FHIR model.

### Integration Considerations

1. **Bulk Data**: Plan for efficient bulk data access for reporting and analytics.
2. **Subscriptions**: Implement FHIR subscription capabilities for real-time notifications.
3. **External Systems**: Consider integration with external systems like HIEs and patient portals.
4. **Mobile Access**: Ensure the API is optimized for mobile client access.

## Validation Strategy

To ensure the quality and compliance of the FHIR resources:

1. **Schema Validation**: Validate resources against the FHIR R4 schema
2. **Profile Validation**: Validate against US Core and other relevant profiles
3. **Business Rules**: Apply custom business rules specific to our implementation
4. **Reference Integrity**: Ensure all references resolve to valid resources
5. **Terminology Validation**: Validate codes against appropriate code systems
6. **Sample Data Testing**: Test with representative patient data across all resource types
7. **Round-Trip Testing**: Verify data integrity through read-write-read cycles
8. **Load Testing**: Ensure performance under expected load conditions

## Implementation Roadmap

### Phase 1: Foundation (Months 1-2)

1. **Setup Infrastructure**
   - Configure FHIR server environment
   - Set up CI/CD pipeline
   - Establish testing framework

2. **Implement Core Domain Models**
   - Create domain models for all legacy entities
   - Implement repository interfaces
   - Set up dependency injection framework

3. **Develop Base Mapper Framework**
   - Create base mapper interfaces and abstract classes
   - Implement common mapping utilities
   - Set up logging and error handling

4. **Implement Patient Resource**
   - Map Demographics to Patient
   - Implement search capabilities
   - Add validation

### Phase 2: Clinical Resources (Months 3-4)

1. **Implement Provider Resources**
   - Map ProviderSecurity to Practitioner
   - Map ProviderSecurity to PractitionerRole
   - Map ReferProviders to external Practitioners

2. **Implement Encounter and Documentation**
   - Map SOAP to Encounter
   - Map SOAP narrative components to DocumentReference
   - Map vital signs to Observations
   - Implement ClinicalImpression for assessment and plan

3. **Implement Problem-Oriented Resources**
   - Map ListProblem to Condition
   - Map ListAllergies to AllergyIntolerance
   - Map ListMEDS to MedicationRequest
   - Map ListProcedures to Procedure

4. **Implement Diagnostic Resources**
   - Map LabResults to Observation
   - Map VitalSigns to Observation
   - Implement DiagnosticReport as needed

### Phase 3: Administrative Resources (Months 5-6)

1. **Implement Scheduling Resources**
   - Map Scheduling to Appointment
   - Implement Schedule and Slot resources
   - Add search capabilities for scheduling

2. **Implement Organization Resources**
   - Map PracticeInfo to Organization
   - Implement Location resources
   - Set up organizational hierarchies

3. **Implement Patient Administration**
   - Add patient registration workflows
   - Implement RelatedPerson for contacts
   - Add consent management

### Phase 4: Financial Resources (Months 7-8)

1. **Implement Insurance Resources**
   - Map ListPayors to Coverage
   - Implement Patient Account resources
   - Set up insurance verification workflows

2. **Implement Billing Resources**
   - Map BillingCPTs to Claim
   - Map PatientCharges to Invoice
   - Implement ChargeItem resources

3. **Implement Financial Workflows**
   - Add claim submission capabilities
   - Implement payment processing
   - Add financial reporting

### Phase 5: Advanced Features (Months 9-12)

1. **Implement Bulk Data Access**
   - Add bulk export capabilities
   - Implement asynchronous processing
   - Add data analytics integration

2. **Add SMART on FHIR Support**
   - Implement OAuth2 authorization
   - Add app launch capabilities
   - Support standalone and EHR-launch modes

3. **Implement CDS Hooks**
   - Add clinical decision support integration
   - Implement service discovery
   - Add hooks for key clinical workflows

4. **Final Integration and Testing**
   - Conduct end-to-end testing
   - Perform security assessment
   - Optimize performance
   - Document API for consumers

## Next Steps

1. **Immediate Actions**
   - Finalize and review this mapping documentation
   - Set up development environment
   - Create initial project structure
   - Implement Patient mapper as proof of concept

2. **Short-Term Goals (Next 2 Weeks)**
   - Complete Patient, Practitioner, and Organization mappers
   - Implement basic FHIR API endpoints for these resources
   - Develop initial test cases
   - Review and refine mapping approach based on implementation experience

3. **Medium-Term Goals (Next 1-2 Months)**
   - Implement Encounter and clinical documentation mappers
   - Add support for problem-oriented resources
   - Begin work on administrative resources
   - Conduct first integration test with frontend

4. **Long-Term Vision**
   - Complete all resource mappers according to the phased approach
   - Achieve full US Core compliance
   - Support all required EHR frontend functionality
   - Enable interoperability with external systems
