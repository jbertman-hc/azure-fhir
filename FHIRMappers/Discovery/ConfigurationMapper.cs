using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClaudeAcDirectSql.FHIRMappers.Discovery
{
    /// <summary>
    /// Configuration-driven mapper that can transform legacy data to FHIR resources
    /// based on mapping configurations
    /// </summary>
    public class ConfigurationMapper
    {
        private readonly Dictionary<string, MappingConfiguration> _configurations;
        private readonly FhirJsonSerializer _serializer;
        private readonly FhirJsonParser _parser;

        public ConfigurationMapper()
        {
            _configurations = new Dictionary<string, MappingConfiguration>();
            _serializer = new FhirJsonSerializer();
            _parser = new FhirJsonParser();
        }

        /// <summary>
        /// Registers a mapping configuration for a specific resource type
        /// </summary>
        public void RegisterConfiguration(string resourceType, MappingConfiguration configuration)
        {
            _configurations[resourceType] = configuration;
        }

        /// <summary>
        /// Loads a mapping configuration from a JSON file
        /// </summary>
        public MappingConfiguration LoadConfigurationFromJson(string json)
        {
            return JsonConvert.DeserializeObject<MappingConfiguration>(json);
        }

        /// <summary>
        /// Maps a source object to a FHIR resource using the registered configuration
        /// </summary>
        public Resource MapToFhir(object source, string resourceType)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!_configurations.TryGetValue(resourceType, out var config))
                throw new InvalidOperationException($"No mapping configuration registered for resource type {resourceType}");

            // Create the target resource
            var resourceTypeName = resourceType;
            var resourceAssembly = typeof(Resource).Assembly;
            var resourceTypeObj = resourceAssembly.GetType($"Hl7.Fhir.Model.{resourceTypeName}");
            
            if (resourceTypeObj == null)
                throw new InvalidOperationException($"Resource type {resourceTypeName} not found in FHIR assembly");

            var resource = (Resource)Activator.CreateInstance(resourceTypeObj);

            // Apply the mappings
            ApplyMappings(source, resource, config);

            return resource;
        }

        private void ApplyMappings(object source, Resource target, MappingConfiguration config)
        {
            var sourceType = source.GetType();
            
            // Set meta profile if specified
            if (!string.IsNullOrEmpty(config.Profile))
            {
                if (target.Meta == null)
                    target.Meta = new Meta();
                
                target.Meta.Profile = new List<string> { config.Profile };
            }

            // Apply each field mapping
            foreach (var mapping in config.FieldMappings)
            {
                try
                {
                    // Get source value
                    var sourceProperty = sourceType.GetProperty(mapping.SourceField);
                    if (sourceProperty == null)
                        continue;

                    var sourceValue = sourceProperty.GetValue(source);
                    if (sourceValue == null && !mapping.AllowNull)
                        continue;

                    // Apply transformations if needed
                    var transformedValue = ApplyTransformations(sourceValue, mapping);
                    
                    // Set the target value using FhirPath
                    SetFhirValue(target, mapping.TargetFhirPath, transformedValue, mapping.TargetDataType);
                }
                catch (Exception ex)
                {
                    // Log the error but continue with other mappings
                    Console.WriteLine($"Error mapping {mapping.SourceField} to {mapping.TargetFhirPath}: {ex.Message}");
                }
            }
        }

        private object ApplyTransformations(object sourceValue, FieldMapping mapping)
        {
            if (sourceValue == null)
                return null;

            var value = sourceValue;

            // Apply value mappings if any
            if (mapping.ValueMappings != null && mapping.ValueMappings.Count > 0)
            {
                var stringValue = sourceValue.ToString();
                var valueMapping = mapping.ValueMappings.FirstOrDefault(vm => 
                    vm.SourceValue.Equals(stringValue, StringComparison.OrdinalIgnoreCase));
                
                if (valueMapping != null)
                    return valueMapping.TargetValue;
            }

            // Apply transformations based on target data type
            switch (mapping.TargetDataType)
            {
                case "date":
                case "dateTime":
                    if (sourceValue is DateTime dt)
                        return dt.ToString("yyyy-MM-dd");
                    else if (DateTime.TryParse(sourceValue.ToString(), out DateTime parsedDt))
                        return parsedDt.ToString("yyyy-MM-dd");
                    break;
                    
                case "boolean":
                    if (sourceValue is bool b)
                        return b;
                    else if (bool.TryParse(sourceValue.ToString(), out bool parsedBool))
                        return parsedBool;
                    break;
                    
                case "integer":
                    if (sourceValue is int i)
                        return i;
                    else if (int.TryParse(sourceValue.ToString(), out int parsedInt))
                        return parsedInt;
                    break;
                    
                case "string":
                    return sourceValue.ToString();
                    
                case "CodeableConcept":
                    // Create a CodeableConcept with the source value as display text
                    // and system/code from the mapping if available
                    var cc = new CodeableConcept();
                    var coding = new Coding
                    {
                        Display = sourceValue.ToString()
                    };
                    
                    if (!string.IsNullOrEmpty(mapping.System))
                        coding.System = mapping.System;
                        
                    if (mapping.ValueMappings != null && mapping.ValueMappings.Count > 0)
                    {
                        var vm = mapping.ValueMappings.FirstOrDefault(v => 
                            v.SourceValue.Equals(sourceValue.ToString(), StringComparison.OrdinalIgnoreCase));
                            
                        if (vm != null)
                        {
                            coding.Code = vm.TargetValue;
                            if (!string.IsNullOrEmpty(vm.TargetSystem))
                                coding.System = vm.TargetSystem;
                        }
                    }
                    
                    cc.Coding.Add(coding);
                    return cc;
                    
                case "Identifier":
                    // Create an Identifier with the source value and system from the mapping
                    var identifier = new Identifier
                    {
                        Value = sourceValue.ToString()
                    };
                    
                    if (!string.IsNullOrEmpty(mapping.System))
                        identifier.System = mapping.System;
                        
                    return identifier;
                    
                case "ContactPoint":
                    // Create a ContactPoint with the source value
                    var cp = new ContactPoint
                    {
                        Value = sourceValue.ToString()
                    };
                    
                    if (!string.IsNullOrEmpty(mapping.System))
                    {
                        if (Enum.TryParse<ContactPoint.ContactPointSystem>(mapping.System, true, out var system))
                            cp.System = system;
                    }
                    
                    if (!string.IsNullOrEmpty(mapping.Use))
                    {
                        if (Enum.TryParse<ContactPoint.ContactPointUse>(mapping.Use, true, out var use))
                            cp.Use = use;
                    }
                    
                    return cp;
            }

            // If no transformation applied, return the original value
            return value;
        }

        private void SetFhirValue(Resource target, string fhirPath, object value, string dataType)
        {
            // This is a simplified implementation - in a real system, you would use a proper FHIRPath library
            // or the ElementModel approach from the FHIR SDK
            
            // For now, we'll handle common patterns with a simple approach
            var parts = fhirPath.Split('.');
            
            if (parts.Length < 2 || parts[0] != target.TypeName)
                throw new ArgumentException($"Invalid FHIRPath: {fhirPath}. Must start with resource type.");
                
            // Handle common patterns
            if (parts.Length == 2)
            {
                // Simple property, e.g., "Patient.active"
                var property = target.GetType().GetProperty(parts[1]);
                if (property != null)
                {
                    try
                    {
                        // Convert value to the property type if needed
                        var convertedValue = Convert.ChangeType(value, property.PropertyType);
                        property.SetValue(target, convertedValue);
                    }
                    catch
                    {
                        // If direct conversion fails, try more complex approach
                        // This would be where you'd use the FHIR SDK's more advanced features
                    }
                }
            }
            else if (parts.Length == 3)
            {
                // Nested property, e.g., "Patient.name.family"
                var containerProp = target.GetType().GetProperty(parts[1]);
                if (containerProp != null)
                {
                    var container = containerProp.GetValue(target);
                    
                    // If container is null, create it
                    if (container == null)
                    {
                        container = Activator.CreateInstance(containerProp.PropertyType);
                        containerProp.SetValue(target, container);
                    }
                    
                    // Set the nested property
                    var nestedProp = container.GetType().GetProperty(parts[2]);
                    if (nestedProp != null)
                    {
                        try
                        {
                            var convertedValue = Convert.ChangeType(value, nestedProp.PropertyType);
                            nestedProp.SetValue(container, convertedValue);
                        }
                        catch
                        {
                            // Handle conversion failure
                        }
                    }
                }
            }
            
            // For more complex paths, you would need a proper FHIRPath implementation
            // This is just a simplified example
        }
    }

    public class MappingConfiguration
    {
        public string ResourceType { get; set; }
        public string Profile { get; set; }
        public List<FieldMapping> FieldMappings { get; set; } = new List<FieldMapping>();
    }

    public class FieldMapping
    {
        public string SourceField { get; set; }
        public string TargetFhirPath { get; set; }
        public string TargetDataType { get; set; }
        public string System { get; set; }
        public string Use { get; set; }
        public bool AllowNull { get; set; }
        public List<ValueMapping> ValueMappings { get; set; } = new List<ValueMapping>();
    }

    public class ValueMapping
    {
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }
        public string TargetSystem { get; set; }
    }
}
