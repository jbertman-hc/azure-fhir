using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Hl7.Fhir.Model;

namespace ClaudeAcDirectSql.FHIRMappers.Discovery
{
    /// <summary>
    /// Analyzes legacy data models to discover potential FHIR mappings
    /// </summary>
    public class DataAnalyzer
    {
        private readonly Dictionary<string, List<PropertyPattern>> _knownPatterns;
        
        public DataAnalyzer()
        {
            // Initialize with common field naming patterns
            _knownPatterns = new Dictionary<string, List<PropertyPattern>>
            {
                ["Patient"] = new List<PropertyPattern>
                {
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(first|fname|firstname|first_name)", 
                        FhirPath = "Patient.name.given[0]", 
                        FhirDataType = "string",
                        Confidence = 0.9
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(last|lname|lastname|last_name|surname)", 
                        FhirPath = "Patient.name.family", 
                        FhirDataType = "string",
                        Confidence = 0.9
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(dob|birth|birthdate|dateofbirth|birth_date)", 
                        FhirPath = "Patient.birthDate", 
                        FhirDataType = "date",
                        Confidence = 0.9
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(gender|sex)", 
                        FhirPath = "Patient.gender", 
                        FhirDataType = "code",
                        Confidence = 0.8
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(ssn|socialsecurity|ss|social)", 
                        FhirPath = "Patient.identifier[system='http://hl7.org/fhir/sid/us-ssn']", 
                        FhirDataType = "Identifier",
                        Confidence = 0.8
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(mrn|medical|record|chart|chartid)", 
                        FhirPath = "Patient.identifier[system='http://hospital.smarthealthit.org']", 
                        FhirDataType = "Identifier",
                        Confidence = 0.7
                    },
                    // Address components
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(address|addr|street|line1)", 
                        FhirPath = "Patient.address.line[0]", 
                        FhirDataType = "string",
                        Confidence = 0.8
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(city|town)", 
                        FhirPath = "Patient.address.city", 
                        FhirDataType = "string",
                        Confidence = 0.9
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(state|province)", 
                        FhirPath = "Patient.address.state", 
                        FhirDataType = "string",
                        Confidence = 0.9
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(zip|postal|postalcode|zip_code)", 
                        FhirPath = "Patient.address.postalCode", 
                        FhirDataType = "string",
                        Confidence = 0.9
                    },
                    // Contact information
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(phone|homephone|telephone)", 
                        FhirPath = "Patient.telecom[system='phone'][use='home']", 
                        FhirDataType = "ContactPoint",
                        Confidence = 0.8
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(work|workphone)", 
                        FhirPath = "Patient.telecom[system='phone'][use='work']", 
                        FhirDataType = "ContactPoint",
                        Confidence = 0.8
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(mobile|cell|cellphone)", 
                        FhirPath = "Patient.telecom[system='phone'][use='mobile']", 
                        FhirDataType = "ContactPoint",
                        Confidence = 0.8
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(email|mail)", 
                        FhirPath = "Patient.telecom[system='email']", 
                        FhirDataType = "ContactPoint",
                        Confidence = 0.8
                    }
                },
                ["Condition"] = new List<PropertyPattern>
                {
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(icd|diagnosis|code)", 
                        FhirPath = "Condition.code", 
                        FhirDataType = "CodeableConcept",
                        Confidence = 0.8
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(onset|start|begin|from)", 
                        FhirPath = "Condition.onset[x]", 
                        FhirDataType = "dateTime",
                        Confidence = 0.7
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(end|stop|resolved|abate)", 
                        FhirPath = "Condition.abatement[x]", 
                        FhirDataType = "dateTime",
                        Confidence = 0.7
                    },
                    new PropertyPattern { 
                        RegexPattern = @"(?i)(status|active|inactive)", 
                        FhirPath = "Condition.clinicalStatus", 
                        FhirDataType = "CodeableConcept",
                        Confidence = 0.7
                    }
                },
                // Add patterns for other resource types as needed
            };
        }

        /// <summary>
        /// Analyzes a source object to discover potential FHIR mappings
        /// </summary>
        /// <param name="sourceObject">The legacy data object to analyze</param>
        /// <param name="targetResourceType">The target FHIR resource type</param>
        /// <returns>A list of potential field mappings</returns>
        public List<PotentialMapping> AnalyzeObject(object sourceObject, string targetResourceType)
        {
            if (sourceObject == null)
                throw new ArgumentNullException(nameof(sourceObject));

            var result = new List<PotentialMapping>();
            var sourceType = sourceObject.GetType();
            var properties = sourceType.GetProperties();

            // Get patterns for the target resource type
            if (!_knownPatterns.TryGetValue(targetResourceType, out var patterns))
            {
                // If no specific patterns for this resource, return empty list
                return result;
            }

            // Analyze each property in the source object
            foreach (var prop in properties)
            {
                // Skip null properties
                var value = prop.GetValue(sourceObject);
                if (value == null)
                    continue;

                // Find matching patterns for this property
                var matchingPatterns = patterns
                    .Where(p => Regex.IsMatch(prop.Name, p.RegexPattern))
                    .OrderByDescending(p => p.Confidence)
                    .ToList();

                if (matchingPatterns.Any())
                {
                    // Add all potential matches, sorted by confidence
                    foreach (var pattern in matchingPatterns)
                    {
                        result.Add(new PotentialMapping
                        {
                            SourceProperty = prop.Name,
                            SourceValue = value.ToString(),
                            SourceType = prop.PropertyType.Name,
                            TargetFhirPath = pattern.FhirPath,
                            TargetFhirType = pattern.FhirDataType,
                            Confidence = pattern.Confidence,
                            TransformationNeeded = DetermineTransformationNeeded(prop.PropertyType.Name, pattern.FhirDataType)
                        });
                    }
                }
                else
                {
                    // No pattern match, but still record the property for manual review
                    result.Add(new PotentialMapping
                    {
                        SourceProperty = prop.Name,
                        SourceValue = value.ToString(),
                        SourceType = prop.PropertyType.Name,
                        TargetFhirPath = "Unknown",
                        TargetFhirType = "Unknown",
                        Confidence = 0.1,
                        TransformationNeeded = true
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Analyzes sample data to identify potential value mappings (e.g., code systems)
        /// </summary>
        public Dictionary<string, List<ValueMapping>> AnalyzeValues(object sourceObject, string targetResourceType)
        {
            var result = new Dictionary<string, List<ValueMapping>>();
            var sourceType = sourceObject.GetType();
            var properties = sourceType.GetProperties();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(sourceObject)?.ToString();
                if (string.IsNullOrEmpty(value))
                    continue;

                // Analyze specific properties based on resource type
                if (targetResourceType == "Patient" && 
                    (prop.Name.Equals("Gender", StringComparison.OrdinalIgnoreCase) ||
                     prop.Name.Equals("Sex", StringComparison.OrdinalIgnoreCase)))
                {
                    var mappings = new List<ValueMapping>();
                    
                    // Map gender values
                    switch (value.ToLowerInvariant())
                    {
                        case "m":
                        case "male":
                            mappings.Add(new ValueMapping { 
                                SourceValue = value,
                                TargetValue = "male",
                                TargetSystem = "http://hl7.org/fhir/administrative-gender",
                                Confidence = 0.9
                            });
                            break;
                        case "f":
                        case "female":
                            mappings.Add(new ValueMapping { 
                                SourceValue = value,
                                TargetValue = "female",
                                TargetSystem = "http://hl7.org/fhir/administrative-gender",
                                Confidence = 0.9
                            });
                            break;
                        case "o":
                        case "other":
                            mappings.Add(new ValueMapping { 
                                SourceValue = value,
                                TargetValue = "other",
                                TargetSystem = "http://hl7.org/fhir/administrative-gender",
                                Confidence = 0.9
                            });
                            break;
                        default:
                            mappings.Add(new ValueMapping { 
                                SourceValue = value,
                                TargetValue = "unknown",
                                TargetSystem = "http://hl7.org/fhir/administrative-gender",
                                Confidence = 0.5
                            });
                            break;
                    }
                    
                    result[prop.Name] = mappings;
                }
                
                // Add more value mapping logic for other fields and resource types
            }

            return result;
        }

        private bool DetermineTransformationNeeded(string sourceType, string targetType)
        {
            // Simple check - if types don't match exactly, transformation is needed
            if (sourceType == targetType)
                return false;

            // Special cases where transformation might be simple
            if ((sourceType == "String" && targetType == "string") ||
                (sourceType == "Int32" && targetType == "integer") ||
                (sourceType == "Boolean" && targetType == "boolean"))
                return false;

            // For all other cases, assume transformation is needed
            return true;
        }
    }

    public class PropertyPattern
    {
        public string RegexPattern { get; set; }
        public string FhirPath { get; set; }
        public string FhirDataType { get; set; }
        public double Confidence { get; set; }
    }

    public class PotentialMapping
    {
        public string SourceProperty { get; set; }
        public string SourceValue { get; set; }
        public string SourceType { get; set; }
        public string TargetFhirPath { get; set; }
        public string TargetFhirType { get; set; }
        public double Confidence { get; set; }
        public bool TransformationNeeded { get; set; }
    }

    public class ValueMapping
    {
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }
        public string TargetSystem { get; set; }
        public double Confidence { get; set; }
    }
}
