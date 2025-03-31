using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ClaudeAcDirectSql.Models;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json;

namespace ClaudeAcDirectSql.FHIRMappers.Discovery
{
    /// <summary>
    /// Service that provides mapping discovery and validation capabilities
    /// </summary>
    public class MappingDiscoveryService
    {
        private readonly DataAnalyzer _analyzer;
        private readonly MappingConfigGenerator _configGenerator;
        private readonly ConfigurationMapper _configMapper;
        private readonly FhirJsonSerializer _serializer;
        private readonly Dictionary<string, MappingConfiguration> _configurations;
        
        public MappingDiscoveryService()
        {
            _analyzer = new DataAnalyzer();
            _configGenerator = new MappingConfigGenerator();
            _configMapper = new ConfigurationMapper();
            _serializer = new FhirJsonSerializer();
            _configurations = new Dictionary<string, MappingConfiguration>();
        }
        
        /// <summary>
        /// Analyzes a sample object and returns potential FHIR mappings
        /// </summary>
        public List<PotentialMapping> DiscoverMappings(object sampleObject, string resourceType)
        {
            return _analyzer.AnalyzeObject(sampleObject, resourceType);
        }
        
        /// <summary>
        /// Generates a mapping configuration from a sample object
        /// </summary>
        public MappingConfiguration GenerateConfiguration(object sampleObject, string resourceType, string profile = null)
        {
            return _configGenerator.GenerateConfiguration(sampleObject, resourceType, profile);
        }
        
        /// <summary>
        /// Maps a source object to a FHIR resource using a generated configuration
        /// </summary>
        public Resource MapToFhir(object source, string resourceType)
        {
            // Generate or retrieve configuration
            MappingConfiguration config;
            if (!_configurations.TryGetValue(resourceType, out config))
            {
                config = _configGenerator.GenerateConfiguration(source, resourceType);
                _configMapper.RegisterConfiguration(resourceType, config);
                _configurations[resourceType] = config;
            }
            
            return _configMapper.MapToFhir(source, resourceType);
        }
        
        /// <summary>
        /// Saves a mapping configuration to a file
        /// </summary>
        public void SaveConfiguration(MappingConfiguration config, string filePath)
        {
            _configGenerator.SaveConfigurationToFile(config, filePath);
        }
        
        /// <summary>
        /// Generates a C# mapper class from a configuration
        /// </summary>
        public string GenerateMapperClass(MappingConfiguration config)
        {
            return _configGenerator.GenerateMapperClass(config);
        }
        
        /// <summary>
        /// Validates a FHIR resource against profiles
        /// </summary>
        public ValidationResult ValidateResource(Resource resource)
        {
            // In a real implementation, this would use the FHIR Validator
            // For now, we'll just do some basic checks
            var result = new ValidationResult();
            
            try
            {
                // Serialize to JSON to check for any serialization issues
                var json = _serializer.SerializeToString(resource);
                result.IsValid = true;
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Issues.Add(new ValidationIssue
                {
                    Severity = "error",
                    Message = $"Serialization error: {ex.Message}"
                });
            }
            
            // Check for required fields based on resource type
            switch (resource)
            {
                case Patient patient:
                    // Check US Core Patient requirements
                    if (patient.Name == null || !patient.Name.Any())
                    {
                        result.IsValid = false;
                        result.Issues.Add(new ValidationIssue
                        {
                            Severity = "error",
                            Message = "Patient.name is required"
                        });
                    }
                    
                    if (patient.Gender == null)
                    {
                        result.IsValid = false;
                        result.Issues.Add(new ValidationIssue
                        {
                            Severity = "error",
                            Message = "Patient.gender is required"
                        });
                    }
                    break;
                    
                // Add checks for other resource types
            }
            
            return result;
        }
        
        /// <summary>
        /// Analyzes a mapping configuration and provides suggestions for improvement
        /// </summary>
        public List<MappingSuggestion> AnalyzeConfiguration(MappingConfiguration config)
        {
            var suggestions = new List<MappingSuggestion>();
            
            // Check for missing required fields based on resource type
            switch (config.ResourceType)
            {
                case "Patient":
                    // Check for name mappings
                    var hasNameFamily = config.FieldMappings.Any(m => m.TargetFhirPath == "Patient.name.family");
                    var hasNameGiven = config.FieldMappings.Any(m => m.TargetFhirPath == "Patient.name.given[0]");
                    
                    if (!hasNameFamily)
                    {
                        suggestions.Add(new MappingSuggestion
                        {
                            Severity = "error",
                            Message = "Missing mapping for Patient.name.family (required by US Core)",
                            SuggestedSourceFields = new List<string> { "Last", "LastName", "Surname" }
                        });
                    }
                    
                    if (!hasNameGiven)
                    {
                        suggestions.Add(new MappingSuggestion
                        {
                            Severity = "error",
                            Message = "Missing mapping for Patient.name.given (required by US Core)",
                            SuggestedSourceFields = new List<string> { "First", "FirstName", "GivenName" }
                        });
                    }
                    
                    // Check for gender mapping
                    var hasGender = config.FieldMappings.Any(m => m.TargetFhirPath == "Patient.gender");
                    if (!hasGender)
                    {
                        suggestions.Add(new MappingSuggestion
                        {
                            Severity = "error",
                            Message = "Missing mapping for Patient.gender (required by US Core)",
                            SuggestedSourceFields = new List<string> { "Gender", "Sex" }
                        });
                    }
                    
                    // Check for birthDate mapping
                    var hasBirthDate = config.FieldMappings.Any(m => m.TargetFhirPath == "Patient.birthDate");
                    if (!hasBirthDate)
                    {
                        suggestions.Add(new MappingSuggestion
                        {
                            Severity = "warning",
                            Message = "Missing mapping for Patient.birthDate (recommended by US Core)",
                            SuggestedSourceFields = new List<string> { "BirthDate", "DOB", "DateOfBirth" }
                        });
                    }
                    break;
                    
                // Add checks for other resource types
            }
            
            return suggestions;
        }
    }
    
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<ValidationIssue> Issues { get; set; } = new List<ValidationIssue>();
    }
    
    public class ValidationIssue
    {
        public string Severity { get; set; } // error, warning, info
        public string Message { get; set; }
    }
    
    public class MappingSuggestion
    {
        public string Severity { get; set; } // error, warning, info
        public string Message { get; set; }
        public List<string> SuggestedSourceFields { get; set; } = new List<string>();
    }
}
