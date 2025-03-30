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
    public class ProviderClinicalSummaryPreferencesRepository : IProviderClinicalSummaryPreferencesRepository
    {
        private readonly string _connectionString;

        public ProviderClinicalSummaryPreferencesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProviderClinicalSummaryPreferencesDomain GetProviderClinicalSummaryPreferences(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ProviderClinicalSummaryPreferences WHERE id = @id";

                    var ProviderClinicalSummaryPreferencesPoco = cn.QueryFirstOrDefault<ProviderClinicalSummaryPreferencesPoco>(query, new { id = id }) ?? new ProviderClinicalSummaryPreferencesPoco();

                    return ProviderClinicalSummaryPreferencesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ProviderClinicalSummaryPreferencesDomain> GetProviderClinicalSummaryPreferences(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ProviderClinicalSummaryPreferences WHERE @criteria";
                    List<ProviderClinicalSummaryPreferencesPoco> pocos = cn.Query<ProviderClinicalSummaryPreferencesPoco>(sql).ToList();
                    List<ProviderClinicalSummaryPreferencesDomain> domains = new List<ProviderClinicalSummaryPreferencesDomain>();

                    foreach (ProviderClinicalSummaryPreferencesPoco poco in pocos)
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

        public int DeleteProviderClinicalSummaryPreferences(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ProviderClinicalSummaryPreferences WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertProviderClinicalSummaryPreferences(ProviderClinicalSummaryPreferencesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ProviderClinicalSummaryPreferencesPoco poco = new ProviderClinicalSummaryPreferencesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ProviderClinicalSummaryPreferences] " +
                        "([Id], [ProviderId], [IsPatientDemographics], [IsProviderOfficeInformation], " +
                        "[IsDateAndVisitLocation], [IsChiefComplaint], [IsProblems], [IsProblemsIncludeInactive], " +
                        "[IsMedications], [IsMedicationsIncludeInactive], [IsAllergies], [IsAllergiesIncludeInactive], " +
                        "[IsVitalSigns], [IsSmokingStatus], [IsProceduresDoneDuringVisit], [IsClinicalInstructions], " +
                        "[IsPlanOfCare], [IsImmunizations], [IsLabTestResults], [ExportLocation], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@Id, @ProviderId, @IsPatientDemographics, @IsProviderOfficeInformation, " +
                        "@IsDateAndVisitLocation, @IsChiefComplaint, @IsProblems, @IsProblemsIncludeInactive, " +
                        "@IsMedications, @IsMedicationsIncludeInactive, @IsAllergies, @IsAllergiesIncludeInactive, " +
                        "@IsVitalSigns, @IsSmokingStatus, @IsProceduresDoneDuringVisit, @IsClinicalInstructions, " +
                        "@IsPlanOfCare, @IsImmunizations, @IsLabTestResults, @ExportLocation, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateProviderClinicalSummaryPreferences(ProviderClinicalSummaryPreferencesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ProviderClinicalSummaryPreferencesPoco poco = new ProviderClinicalSummaryPreferencesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ProviderClinicalSummaryPreferences " +
                        "SET [ProviderId] = @ProviderId, [IsPatientDemographics] = @IsPatientDemographics, " +
                        "[IsProviderOfficeInformation] = @IsProviderOfficeInformation, " +
                        "[IsDateAndVisitLocation] = @IsDateAndVisitLocation, [IsChiefComplaint] = @IsChiefComplaint, " +
                        "[IsProblems] = @IsProblems, [IsProblemsIncludeInactive] = @IsProblemsIncludeInactive, " +
                        "[IsMedications] = @IsMedications, [IsMedicationsIncludeInactive] = @IsMedicationsIncludeInactive, " +
                        "[IsAllergies] = @IsAllergies, [IsAllergiesIncludeInactive] = @IsAllergiesIncludeInactive, " +
                        "[IsVitalSigns] = @IsVitalSigns, [IsSmokingStatus] = @IsSmokingStatus, " +
                        "[IsProceduresDoneDuringVisit] = @IsProceduresDoneDuringVisit, " +
                        "[IsClinicalInstructions] = @IsClinicalInstructions, [IsPlanOfCare] = @IsPlanOfCare, " +
                        "[IsImmunizations] = @IsImmunizations, [IsLabTestResults] = @IsLabTestResults, " +
                        "[ExportLocation] = @ExportLocation, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE Id = @Id;";

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
