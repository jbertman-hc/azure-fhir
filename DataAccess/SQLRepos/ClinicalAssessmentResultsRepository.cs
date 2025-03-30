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
    public class ClinicalAssessmentResultsRepository : IClinicalAssessmentResultsRepository
    {
        private readonly string _connectionString;

        public ClinicalAssessmentResultsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ClinicalAssessmentResultsDomain GetClinicalAssessmentResults(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ClinicalAssessmentResults WHERE ClinicalAssessmentResultsID = @id";

                    var ClinicalAssessmentResultsPoco = cn.QueryFirstOrDefault<ClinicalAssessmentResultsPoco>(query, new { id = id }) ?? new ClinicalAssessmentResultsPoco();

                    return ClinicalAssessmentResultsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ClinicalAssessmentResultsDomain> GetClinicalAssessmentResults(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ClinicalAssessmentResults WHERE @criteria";
                    List<ClinicalAssessmentResultsPoco> pocos = cn.Query<ClinicalAssessmentResultsPoco>(sql).ToList();
                    List<ClinicalAssessmentResultsDomain> domains = new List<ClinicalAssessmentResultsDomain>();

                    foreach (ClinicalAssessmentResultsPoco poco in pocos)
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

        public int DeleteClinicalAssessmentResults(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ClinicalAssessmentResults WHERE ClinicalAssessmentResultsID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertClinicalAssessmentResults(ClinicalAssessmentResultsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ClinicalAssessmentResultsPoco poco = new ClinicalAssessmentResultsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ClinicalAssessmentResults] " +
                        "([ClinicalAssessmentResultsID], [UniqueSurveyId], [ProviderId], [PatientId], " +
                        "[EncounterDate], [HMRuleID], [Score], [Comment], [ResultDate], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ClinicalAssessmentResultsID, @UniqueSurveyId, @ProviderId, @PatientId, " +
                        "@EncounterDate, @HMRuleID, @Score, @Comment, @ResultDate, @DateLastTouched, " +
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

        public int UpdateClinicalAssessmentResults(ClinicalAssessmentResultsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ClinicalAssessmentResultsPoco poco = new ClinicalAssessmentResultsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ClinicalAssessmentResults " +
                        "SET [UniqueSurveyId] = @UniqueSurveyId, [ProviderId] = @ProviderId, " +
                        "[PatientId] = @PatientId, [EncounterDate] = @EncounterDate, " +
                        "[HMRuleID] = @HMRuleID, [Score] = @Score, [Comment] = @Comment, " +
                        "[ResultDate] = @ResultDate, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE ClinicalAssessmentResultsID = @ClinicalAssessmentResultsID;";

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
