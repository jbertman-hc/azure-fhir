using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using POC.DataAccess.SQLPocos;
using POC.Domain.DomainModels;
using POC.Domain.RepositoryDefinitions;

namespace POC.DataAccess.SQLRepos
{
    public class CCDSettingsRepository : ICCDSettingsRepository
    {
        private readonly string _connectionString;

        public CCDSettingsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CCDSettingsDomain GetCCDSettings(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM CCDSettings WHERE CCDSettingID = @id";

                    var CCDSettingsPoco = cn.QueryFirstOrDefault<CCDSettingsPoco>(query, new { id = id }) ?? new CCDSettingsPoco();

                    return CCDSettingsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CCDSettingsDomain> GetCCDSettings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM CCDSettings WHERE @criteria";
                    List<CCDSettingsPoco> pocos = cn.Query<CCDSettingsPoco>(sql).ToList();
                    List<CCDSettingsDomain> domains = new List<CCDSettingsDomain>();

                    foreach (CCDSettingsPoco poco in pocos)
                    {
                        domains.Add(poco.MapToDomainModel());
                    }

                    return domains;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteCCDSettings(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM CCDSettings WHERE CCDSettingID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertCCDSettings(CCDSettingsDomain domain)
        {
            int insertedId = 0;

            try
            {
                CCDSettingsPoco poco = new CCDSettingsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[CCDSettings] ([CCDSettingID], [SettingType], " +
                        "[ProviderName], [IncludeDemographics], [IncludePurpose], [IncludePayors], " +
                        "[IncludeAllergies], [IncludeProblems], [IncludeMeds], [IncludeDirectives], " +
                        "[IncludeImmunizations], [IncludeResults], [IncludePlanOfCare], [IncludeEncounters], " +
                        "[AllergyCodesToUse], [MedsCodesToUse], [IncludeInactiveMeds], " +
                        "[IncludeInactiveProblems], [IncludeResolvedProblems], " +
                        "[IncludeOnlyHighPriorityDirectives], [IncludeRefusedImmunizations], " +
                        "[ResultsDateRange], [EncountersDateRange], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@CCDSettingID, @SettingType, @ProviderName, @IncludeDemographics, " +
                        "@IncludePurpose, @IncludePayors, @IncludeAllergies, @IncludeProblems, " +
                        "@IncludeMeds, @IncludeDirectives, @IncludeImmunizations, @IncludeResults, " +
                        "@IncludePlanOfCare, @IncludeEncounters, @AllergyCodesToUse, @MedsCodesToUse, " +
                        "@IncludeInactiveMeds, @IncludeInactiveProblems, @IncludeResolvedProblems, " +
                        "@IncludeOnlyHighPriorityDirectives, @IncludeRefusedImmunizations, @ResultsDateRange, " +
                        "@EncountersDateRange, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    insertedId = cn.Query(sql, poco).Single();
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return insertedId;
        }

        public int UpdateCCDSettings(CCDSettingsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                CCDSettingsPoco poco = new CCDSettingsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE CCDSettings " +
                        "SET [SettingType] = @SettingType, [ProviderName] = @ProviderName, " +
                        "[IncludeDemographics] = @IncludeDemographics, [IncludePurpose] = @IncludePurpose, " +
                        "[IncludePayors] = @IncludePayors, [IncludeAllergies] = @IncludeAllergies, " +
                        "[IncludeProblems] = @IncludeProblems, [IncludeMeds] = @IncludeMeds, " +
                        "[IncludeDirectives] = @IncludeDirectives, " +
                        "[IncludeImmunizations] = @IncludeImmunizations, " +
                        "[IncludeResults] = @IncludeResults, [IncludePlanOfCare] = @IncludePlanOfCare, " +
                        "[IncludeEncounters] = @IncludeEncounters, [AllergyCodesToUse] = @AllergyCodesToUse, " +
                        "[MedsCodesToUse] = @MedsCodesToUse, [IncludeInactiveMeds] = @IncludeInactiveMeds, " +
                        "[IncludeInactiveProblems] = @IncludeInactiveProblems, " +
                        "[IncludeResolvedProblems] = @IncludeResolvedProblems, " +
                        "[IncludeOnlyHighPriorityDirectives] = @IncludeOnlyHighPriorityDirectives, " +
                        "[IncludeRefusedImmunizations] = @IncludeRefusedImmunizations, " +
                        "[ResultsDateRange] = @ResultsDateRange, [EncountersDateRange] = @EncountersDateRange, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE CCDSettingID = @CCDSettingID;";

                    rowsAffected = cn.Execute(sql, poco);
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return rowsAffected;
        }
    }
}
