using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ClaudeAcDirectSql.FHIRMappers.Discovery
{
    /// <summary>
    /// Generates mapping configurations based on data analysis
    /// </summary>
    public class MappingConfigGenerator
    {
        private readonly DataAnalyzer _analyzer;
        
        public MappingConfigGenerator()
        {
            _analyzer = new DataAnalyzer();
        }
        
        /// <summary>
        /// Analyzes a sample object and generates a mapping configuration
        /// </summary>
        public MappingConfiguration GenerateConfiguration(object sampleObject, string resourceType, string profile = null)
        {
            var potentialMappings = _analyzer.AnalyzeObject(sampleObject, resourceType);
            var valueMappings = _analyzer.AnalyzeValues(sampleObject, resourceType);
            
            var config = new MappingConfiguration
            {
                ResourceType = resourceType,
                Profile = profile,
                FieldMappings = new List<FieldMapping>()
            };
            
            // Group by source property to handle multiple potential mappings
            var groupedMappings = potentialMappings
                .GroupBy(m => m.SourceProperty)
                .ToDictionary(g => g.Key, g => g.OrderByDescending(m => m.Confidence).ToList());
                
            foreach (var group in groupedMappings)
            {
                // Take the highest confidence mapping for each source property
                var bestMapping = group.Value.First();
                
                // Skip very low confidence mappings
                if (bestMapping.Confidence < 0.3 && bestMapping.TargetFhirPath == "Unknown")
                    continue;
                    
                var fieldMapping = new FieldMapping
                {
                    SourceField = bestMapping.SourceProperty,
                    TargetFhirPath = bestMapping.TargetFhirPath,
                    TargetDataType = bestMapping.TargetFhirType,
                    AllowNull = false
                };
                
                // Add system information for specific data types
                if (bestMapping.TargetFhirType == "Identifier")
                {
                    if (bestMapping.TargetFhirPath.Contains("us-ssn"))
                        fieldMapping.System = "http://hl7.org/fhir/sid/us-ssn";
                    else if (bestMapping.TargetFhirPath.Contains("hospital"))
                        fieldMapping.System = "http://hospital.smarthealthit.org";
                }
                else if (bestMapping.TargetFhirType == "ContactPoint")
                {
                    if (bestMapping.TargetFhirPath.Contains("phone"))
                        fieldMapping.System = "phone";
                    else if (bestMapping.TargetFhirPath.Contains("email"))
                        fieldMapping.System = "email";
                        
                    if (bestMapping.TargetFhirPath.Contains("[use='home']"))
                        fieldMapping.Use = "home";
                    else if (bestMapping.TargetFhirPath.Contains("[use='work']"))
                        fieldMapping.Use = "work";
                    else if (bestMapping.TargetFhirPath.Contains("[use='mobile']"))
                        fieldMapping.Use = "mobile";
                }
                
                // Add value mappings if available
                if (valueMappings.TryGetValue(bestMapping.SourceProperty, out var valueMapList))
                {
                    fieldMapping.ValueMappings = valueMapList.Select(vm => new ValueMapping
                    {
                        SourceValue = vm.SourceValue,
                        TargetValue = vm.TargetValue,
                        TargetSystem = vm.TargetSystem
                    }).ToList();
                }
                
                config.FieldMappings.Add(fieldMapping);
            }
            
            return config;
        }
        
        /// <summary>
        /// Saves a mapping configuration to a JSON file
        /// </summary>
        public void SaveConfigurationToFile(MappingConfiguration config, string filePath)
        {
            var json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        
        /// <summary>
        /// Generates a C# mapper class based on a mapping configuration
        /// </summary>
        public string GenerateMapperClass(MappingConfiguration config)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("using Hl7.Fhir.Model;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using ClaudeAcDirectSql.Models;");
            sb.AppendLine("using ClaudeAcDirectSql.FHIRMappers.Discovery;");
            sb.AppendLine();
            sb.AppendLine("namespace ClaudeAcDirectSql.FHIRMappers");
            sb.AppendLine("{");
            sb.AppendLine($"    public static class {config.ResourceType}Mapper");
            sb.AppendLine("    {");
            sb.AppendLine($"        private static readonly ConfigurationMapper _configMapper = new ConfigurationMapper();");
            sb.AppendLine($"        private static readonly MappingConfiguration _config;");
            sb.AppendLine();
            sb.AppendLine($"        static {config.ResourceType}Mapper()");
            sb.AppendLine("        {");
            sb.AppendLine($"            // Initialize the mapping configuration");
            sb.AppendLine($"            _config = new MappingConfiguration");
            sb.AppendLine("            {");
            sb.AppendLine($"                ResourceType = \"{config.ResourceType}\",");
            
            if (!string.IsNullOrEmpty(config.Profile))
                sb.AppendLine($"                Profile = \"{config.Profile}\",");
                
            sb.AppendLine("                FieldMappings = new List<FieldMapping>");
            sb.AppendLine("                {");
            
            foreach (var mapping in config.FieldMappings)
            {
                sb.AppendLine("                    new FieldMapping");
                sb.AppendLine("                    {");
                sb.AppendLine($"                        SourceField = \"{mapping.SourceField}\",");
                sb.AppendLine($"                        TargetFhirPath = \"{mapping.TargetFhirPath}\",");
                sb.AppendLine($"                        TargetDataType = \"{mapping.TargetDataType}\",");
                
                if (!string.IsNullOrEmpty(mapping.System))
                    sb.AppendLine($"                        System = \"{mapping.System}\",");
                    
                if (!string.IsNullOrEmpty(mapping.Use))
                    sb.AppendLine($"                        Use = \"{mapping.Use}\",");
                    
                sb.AppendLine($"                        AllowNull = {mapping.AllowNull.ToString().ToLower()}");
                
                if (mapping.ValueMappings != null && mapping.ValueMappings.Count > 0)
                {
                    sb.AppendLine("                        ValueMappings = new List<ValueMapping>");
                    sb.AppendLine("                        {");
                    
                    foreach (var valueMapping in mapping.ValueMappings)
                    {
                        sb.AppendLine("                            new ValueMapping");
                        sb.AppendLine("                            {");
                        sb.AppendLine($"                                SourceValue = \"{valueMapping.SourceValue}\",");
                        sb.AppendLine($"                                TargetValue = \"{valueMapping.TargetValue}\",");
                        
                        if (!string.IsNullOrEmpty(valueMapping.TargetSystem))
                            sb.AppendLine($"                                TargetSystem = \"{valueMapping.TargetSystem}\"");
                            
                        sb.AppendLine("                            },");
                    }
                    
                    sb.AppendLine("                        }");
                }
                
                sb.AppendLine("                    },");
            }
            
            sb.AppendLine("                }");
            sb.AppendLine("            };");
            sb.AppendLine();
            sb.AppendLine("            _configMapper.RegisterConfiguration(_config.ResourceType, _config);");
            sb.AppendLine("        }");
            sb.AppendLine();
            
            // Generate the mapping method
            var sourceType = config.ResourceType == "Patient" ? "DemographicsDomain" : $"{config.ResourceType}Item";
            
            sb.AppendLine($"        public static {config.ResourceType} ToFhir{config.ResourceType}({sourceType} source)");
            sb.AppendLine("        {");
            sb.AppendLine("            if (source == null)");
            sb.AppendLine("                return null;");
            sb.AppendLine();
            sb.AppendLine($"            // Use the configuration mapper to create the {config.ResourceType} resource");
            sb.AppendLine($"            var resource = ({config.ResourceType})_configMapper.MapToFhir(source, \"{config.ResourceType}\");");
            sb.AppendLine();
            sb.AppendLine("            // Add any custom mapping logic here");
            sb.AppendLine("            // This is where you can handle complex mappings that can't be expressed in the configuration");
            sb.AppendLine();
            sb.AppendLine("            return resource;");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            
            return sb.ToString();
        }
    }
}
