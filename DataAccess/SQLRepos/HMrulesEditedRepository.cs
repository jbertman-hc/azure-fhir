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
    public class HMrulesEditedRepository : IHMrulesEditedRepository
    {
        private readonly string _connectionString;

        public HMrulesEditedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public HMrulesEditedDomain GetHMrulesEdited(Guid id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM HMrulesEdited WHERE HMRuleGUID = @id";

                    var HMrulesEditedPoco = cn.QueryFirstOrDefault<HMrulesEditedPoco>(query, new { id = id }) ?? new HMrulesEditedPoco();

                    return HMrulesEditedPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<HMrulesEditedDomain> GetHMrulesEditeds(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM HMrulesEdited WHERE @criteria";
                    List<HMrulesEditedPoco> pocos = cn.Query<HMrulesEditedPoco>(sql).ToList();
                    List<HMrulesEditedDomain> domains = new List<HMrulesEditedDomain>();

                    foreach (HMrulesEditedPoco poco in pocos)
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

        public int DeleteHMrulesEdited(Guid id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM HMrulesEdited WHERE HMRuleGUID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertHMrulesEdited(HMrulesEditedDomain domain)
        {
            int insertedId = 0;

            try
            {
                HMrulesEditedPoco poco = new HMrulesEditedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[HMrulesEdited] " +
                        "([HMRuleGUID], [HmRuleID], [RuleName], [RecommendedAge], [MinAge], [MaxAge], " +
                        "[RecommendedInterval], [MinInterval], [MaxInterval], [RuleText], " +
                        "[FrequencyOfService], [RuleRationale], [Footnote], [Source], [Type], " +
                        "[Grade], [DoseNumber], [ApplicableGender], [ApplicableICDs], [RestrictedICDs], " +
                        "[LiveVaccine], [EggComponent], [GelatinComponent], [RiskCategory], [RiskFactors], " +
                        "[ApplicableAgeGroup], [CPT], [VaccineID], [Comment], [Inactive], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [AgeSpecific], [FundingSource], [ReleaseRevisionDate], " +
                        "[BibliographicCitation], [RuleType], [RuleSql], [RuleSqlDescription], " +
                        "[IntegrationPartner], [ClinicalAssessmentUrl]) " +
                        "VALUES " +
                        "(@HMRuleGUID, @HmRuleID, @RuleName, @RecommendedAge, @MinAge, @MaxAge, " +
                        "@RecommendedInterval, @MinInterval, @MaxInterval, @RuleText, @FrequencyOfService, " +
                        "@RuleRationale, @Footnote, @Source, @Type, @Grade, @DoseNumber, @ApplicableGender, " +
                        "@ApplicableICDs, @RestrictedICDs, @LiveVaccine, @EggComponent, @GelatinComponent, " +
                        "@RiskCategory, @RiskFactors, @ApplicableAgeGroup, @CPT, @VaccineID, @Comment, " +
                        "@Inactive, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @AgeSpecific, " +
                        "@FundingSource, @ReleaseRevisionDate, @BibliographicCitation, @RuleType, @RuleSql, " +
                        "@RuleSqlDescription, @IntegrationPartner, @ClinicalAssessmentUrl); " +
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

        public int UpdateHMrulesEdited(HMrulesEditedDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                HMrulesEditedPoco poco = new HMrulesEditedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE HMrulesEdited " +
                        "SET [HmRuleID] = @HmRuleID, [RuleName] = @RuleName, " +
                        "[RecommendedAge] = @RecommendedAge, [MinAge] = @MinAge, " +
                        "[MaxAge] = @MaxAge, [RecommendedInterval] = @RecommendedInterval, " +
                        "[MinInterval] = @MinInterval, [MaxInterval] = @MaxInterval, " +
                        "[RuleText] = @RuleText, [FrequencyOfService] = @FrequencyOfService, " +
                        "[RuleRationale] = @RuleRationale, [Footnote] = @Footnote, " +
                        "[Source] = @Source, [Type] = @Type, [Grade] = @Grade, " +
                        "[DoseNumber] = @DoseNumber, [ApplicableGender] = @ApplicableGender, " +
                        "[ApplicableICDs] = @ApplicableICDs, [RestrictedICDs] = @RestrictedICDs, " +
                        "[LiveVaccine] = @LiveVaccine, [EggComponent] = @EggComponent, " +
                        "[GelatinComponent] = @GelatinComponent, [RiskCategory] = @RiskCategory, " +
                        "[RiskFactors] = @RiskFactors, [ApplicableAgeGroup] = @ApplicableAgeGroup, " +
                        "[CPT] = @CPT, [VaccineID] = @VaccineID, [Comment] = @Comment, " +
                        "[Inactive] = @Inactive, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[AgeSpecific] = @AgeSpecific, [FundingSource] = @FundingSource, " +
                        "[ReleaseRevisionDate] = @ReleaseRevisionDate, " +
                        "[BibliographicCitation] = @BibliographicCitation, [RuleType] = @RuleType, " +
                        "[RuleSql] = @RuleSql, [RuleSqlDescription] = @RuleSqlDescription, " +
                        "[IntegrationPartner] = @IntegrationPartner, [ClinicalAssessmentUrl] = @ClinicalAssessmentUrl " +
                        "WHERE HMRuleGUID = @HMRuleGUID;";

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
