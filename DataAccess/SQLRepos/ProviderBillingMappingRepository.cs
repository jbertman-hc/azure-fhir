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
    public class ProviderBillingMappingRepository : IProviderBillingMappingRepository
    {
        private readonly string _connectionString;

        public ProviderBillingMappingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProviderBillingMappingDomain GetProviderBillingMapping(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ProviderBillingMapping WHERE UniqueTableId = @id";

                    var ProviderBillingMappingPoco = cn.QueryFirstOrDefault<ProviderBillingMappingPoco>(query, new { id = id }) ?? new ProviderBillingMappingPoco();

                    return ProviderBillingMappingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ProviderBillingMappingDomain> GetProviderBillingMappings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ProviderBillingMapping WHERE @criteria";
                    List<ProviderBillingMappingPoco> pocos = cn.Query<ProviderBillingMappingPoco>(sql).ToList();
                    List<ProviderBillingMappingDomain> domains = new List<ProviderBillingMappingDomain>();

                    foreach (ProviderBillingMappingPoco poco in pocos)
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

        public int DeleteProviderBillingMapping(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ProviderBillingMapping WHERE UniqueTableId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertProviderBillingMapping(ProviderBillingMappingDomain domain)
        {
            int insertedId = 0;

            try
            {
                ProviderBillingMappingPoco poco = new ProviderBillingMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ProviderBillingMapping] " +
                        "([UniqueTableId], [ProviderId], [ExternalId], [SourceId], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@UniqueTableId, @ProviderId, @ExternalId, @SourceId, @DateLastTouched, " +
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

        public int UpdateProviderBillingMapping(ProviderBillingMappingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ProviderBillingMappingPoco poco = new ProviderBillingMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ProviderBillingMapping " +
                        "SET [ProviderId] = @ProviderId, [ExternalId] = @ExternalId, [SourceId] = @SourceId, " +
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
