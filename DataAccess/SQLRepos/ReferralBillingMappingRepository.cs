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
    public class ReferralBillingMappingRepository : IReferralBillingMappingRepository
    {
        private readonly string _connectionString;

        public ReferralBillingMappingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ReferralBillingMappingDomain GetReferralBillingMapping(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ReferralBillingMapping WHERE UniqueTableId = @id";

                    var ReferralBillingMappingPoco = cn.QueryFirstOrDefault<ReferralBillingMappingPoco>(query, new { id = id }) ?? new ReferralBillingMappingPoco();

                    return ReferralBillingMappingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ReferralBillingMappingDomain> GetReferralBillingMappings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ReferralBillingMapping WHERE @criteria";
                    List<ReferralBillingMappingPoco> pocos = cn.Query<ReferralBillingMappingPoco>(sql).ToList();
                    List<ReferralBillingMappingDomain> domains = new List<ReferralBillingMappingDomain>();

                    foreach (ReferralBillingMappingPoco poco in pocos)
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

        public int DeleteReferralBillingMapping(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ReferralBillingMapping WHERE UniqueTableId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertReferralBillingMapping(ReferralBillingMappingDomain domain)
        {
            int insertedId = 0;

            try
            {
                ReferralBillingMappingPoco poco = new ReferralBillingMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ReferralBillingMapping] " +
                        "([UniqueTableId], [ReferralId], [ExternalId], [SourceId], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@UniqueTableId, @ReferralId, @ExternalId, @SourceId, @DateLastTouched, " +
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

        public int UpdateReferralBillingMapping(ReferralBillingMappingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ReferralBillingMappingPoco poco = new ReferralBillingMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ReferralBillingMapping " +
                        "SET [ReferralId] = @ReferralId, [ExternalId] = @ExternalId, [SourceId] = @SourceId, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE UniqueTableId = @UniqueTableId;";

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
