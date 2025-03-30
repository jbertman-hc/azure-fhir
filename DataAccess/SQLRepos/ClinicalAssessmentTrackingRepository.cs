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
    public class ClinicalAssessmentTrackingRepository : IClinicalAssessmentTrackingRepository
    {
        private readonly string _connectionString;

        public ClinicalAssessmentTrackingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ClinicalAssessmentTrackingDomain GetClinicalAssessmentTracking(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ClinicalAssessmentTracking WHERE id = @id";

                    var ClinicalAssessmentTrackingPoco = cn.QueryFirstOrDefault<ClinicalAssessmentTrackingPoco>(query, new { id = id }) ?? new ClinicalAssessmentTrackingPoco();

                    return ClinicalAssessmentTrackingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ClinicalAssessmentTrackingDomain> GetClinicalAssessmentTrackings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ClinicalAssessmentTracking WHERE @criteria";
                    List<ClinicalAssessmentTrackingPoco> pocos = cn.Query<ClinicalAssessmentTrackingPoco>(sql).ToList();
                    List<ClinicalAssessmentTrackingDomain> domains = new List<ClinicalAssessmentTrackingDomain>();

                    foreach (ClinicalAssessmentTrackingPoco poco in pocos)
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

        public int DeleteClinicalAssessmentTracking(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ClinicalAssessmentTracking WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertClinicalAssessmentTracking(ClinicalAssessmentTrackingDomain domain)
        {
            int insertedId = 0;

            try
            {
                ClinicalAssessmentTrackingPoco poco = new ClinicalAssessmentTrackingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ClinicalAssessmentTracking] " +
                        "([Id], [ProviderId], [ProviderName], [ClinicalAssessmentId], " +
                        "[ClinicalAssessmentName], [DateAccessed], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) VALUES (@Id, @ProviderId, @ProviderName, @ClinicalAssessmentId, " +
                        "@ClinicalAssessmentName, @DateAccessed, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);\r\n";

                    insertedId = cn.Query(sql, poco).Single();
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return insertedId;
        }

        public int UpdateClinicalAssessmentTracking(ClinicalAssessmentTrackingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ClinicalAssessmentTrackingPoco poco = new ClinicalAssessmentTrackingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ClinicalAssessmentTracking " +
                        "SET [ProviderId] = @ProviderId, [ProviderName] = @ProviderName, " +
                        "[ClinicalAssessmentId] = @ClinicalAssessmentId, " +
                        "[ClinicalAssessmentName] = @ClinicalAssessmentName, " +
                        "[DateAccessed] = @DateAccessed, [DateLastTouched] = @DateLastTouched, " +
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
