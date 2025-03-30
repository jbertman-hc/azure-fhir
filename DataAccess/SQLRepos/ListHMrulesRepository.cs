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
    public class ListHMrulesRepository : IListHMrulesRepository
    {
        private readonly string _connectionString;

        public ListHMrulesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListHMrulesDomain GetListHMrules(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListHMrules WHERE RowID = @id";

                    var ListHMrulesPoco = cn.QueryFirstOrDefault<ListHMrulesPoco>(query, new { id = id }) ?? new ListHMrulesPoco();

                    return ListHMrulesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListHMrulesDomain> GetListHMrules(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListHMrules WHERE @criteria";
                    List<ListHMrulesPoco> pocos = cn.Query<ListHMrulesPoco>(sql).ToList();
                    List<ListHMrulesDomain> domains = new List<ListHMrulesDomain>();

                    foreach (ListHMrulesPoco poco in pocos)
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

        public int DeleteListHMrules(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListHMrules WHERE RowID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListHMrules(ListHMrulesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListHMrulesPoco poco = new ListHMrulesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListHMrules] " +
                        "([RowID], [PatientID], [HMRuleGUID], [RuleName], [RecommendedAge], [MinAge], [MaxAge], " +
                        "[RecommendedInterval], [MinInterval], [MaxInterval], [RuleText], [FrequencyOfService], " +
                        "[RuleRationale], [Footnote], [Source], [Type], [Grade], [DoseNumber], [ApplicableGender], " +
                        "[ApplicableICDs], [RestrictedICDs], [LiveVaccine], [EggComponent], [GelatinComponent], " +
                        "[RiskCategory], [RiskFactors], [ApplicableAgeGroup], [CPT], [VaccineID], [Comment], " +
                        "[Inactive], [DateLastTouched], [LastTouchedBy], [DateRowAdded], [HmRuleID], [AgeSpecific], " +
                        "[FundingSource], [ReleaseRevisionDate], [BibliographicCitation], [RuleType], [RuleSql], " +
                        "[RuleSqlDescription]) " +
                        "VALUES " +
                        "(@RowID, @PatientID, @HMRuleGUID, @RuleName, @RecommendedAge, @MinAge, @MaxAge, " +
                        "@RecommendedInterval, @MinInterval, @MaxInterval, @RuleText, @FrequencyOfService, " +
                        "@RuleRationale, @Footnote, @Source, @Type, @Grade, @DoseNumber, @ApplicableGender, " +
                        "@ApplicableICDs, @RestrictedICDs, @LiveVaccine, @EggComponent, @GelatinComponent, " +
                        "@RiskCategory, @RiskFactors, @ApplicableAgeGroup, @CPT, @VaccineID, @Comment, @Inactive, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @HmRuleID, @AgeSpecific, @FundingSource, " +
                        "@ReleaseRevisionDate, @BibliographicCitation, @RuleType, @RuleSql, @RuleSqlDescription); " +
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

        public int UpdateListHMrules(ListHMrulesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListHMrulesPoco poco = new ListHMrulesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListHMrules " +
                        "SET [PatientID] = @PatientID, [HMRuleGUID] = @HMRuleGUID, [RuleName] = @RuleName, " +
                        "[RecommendedAge] = @RecommendedAge, [MinAge] = @MinAge, [MaxAge] = @MaxAge, " +
                        "[RecommendedInterval] = @RecommendedInterval, [MinInterval] = @MinInterval, " +
                        "[MaxInterval] = @MaxInterval, [RuleText] = @RuleText, " +
                        "[FrequencyOfService] = @FrequencyOfService, [RuleRationale] = @RuleRationale, " +
                        "[Footnote] = @Footnote, [Source] = @Source, [Type] = @Type, [Grade] = @Grade, " +
                        "[DoseNumber] = @DoseNumber, [ApplicableGender] = @ApplicableGender, " +
                        "[ApplicableICDs] = @ApplicableICDs, [RestrictedICDs] = @RestrictedICDs, " +
                        "[LiveVaccine] = @LiveVaccine, [EggComponent] = @EggComponent, " +
                        "[GelatinComponent] = @GelatinComponent, [RiskCategory] = @RiskCategory, " +
                        "[RiskFactors] = @RiskFactors, [ApplicableAgeGroup] = @ApplicableAgeGroup, " +
                        "[CPT] = @CPT, [VaccineID] = @VaccineID, [Comment] = @Comment, [Inactive] = @Inactive, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [HmRuleID] = @HmRuleID, [AgeSpecific] = @AgeSpecific, " +
                        "[FundingSource] = @FundingSource, [ReleaseRevisionDate] = @ReleaseRevisionDate, " +
                        "[BibliographicCitation] = @BibliographicCitation, [RuleType] = @RuleType, " +
                        "[RuleSql] = @RuleSql, [RuleSqlDescription] = @RuleSqlDescription " +
                        "WHERE RowID = @RowID;";

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
