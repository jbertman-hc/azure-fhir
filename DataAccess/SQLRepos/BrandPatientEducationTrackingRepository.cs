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
    public class BrandPatientEducationTrackingRepository : IBrandPatientEducationTrackingRepository
    {
        private readonly string _connectionString;

        public BrandPatientEducationTrackingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BrandPatientEducationTrackingDomain GetBrandPatientEducationTracking(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM BrandPatientEducationTracking WHERE id = @id";

                    var BrandPatientEducationTrackingPoco = cn.QueryFirstOrDefault<BrandPatientEducationTrackingPoco>(query, new { id = id }) ?? new BrandPatientEducationTrackingPoco();

                    return BrandPatientEducationTrackingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BrandPatientEducationTrackingDomain> GetBrandPatientEducationTrackings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM BrandPatientEducationTracking WHERE @criteria";
                    List<BrandPatientEducationTrackingPoco> pocos = cn.Query<BrandPatientEducationTrackingPoco>(sql).ToList();
                    List<BrandPatientEducationTrackingDomain> domains = new List<BrandPatientEducationTrackingDomain>();

                    foreach (BrandPatientEducationTrackingPoco poco in pocos)
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

        public int DeleteBrandPatientEducationTracking(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM BrandPatientEducationTracking WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertBrandPatientEducationTracking(BrandPatientEducationTrackingDomain domain)
        {
            int insertedId = 0;

            try
            {
                BrandPatientEducationTrackingPoco poco = new BrandPatientEducationTrackingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[BrandPatientEducationTracking] " +
                        "([Id], [ProviderName], [PatientId], [DrugId], [PharmaSource], [DateAccessed], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@Id, @ProviderName, @PatientId, @DrugId, @PharmaSource, @DateAccessed, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateBrandPatientEducationTracking(BrandPatientEducationTrackingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                BrandPatientEducationTrackingPoco poco = new BrandPatientEducationTrackingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE BrandPatientEducationTracking " +
                        "SET [ProviderName] = @ProviderName, [PatientId] = @PatientId, [DrugId] = @DrugId, " +
                        "[PharmaSource] = @PharmaSource, [DateAccessed] = @DateAccessed, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
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
