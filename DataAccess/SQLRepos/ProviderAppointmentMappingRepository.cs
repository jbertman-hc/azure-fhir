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
    public class ProviderAppointmentMappingRepository : IProviderAppointmentMappingRepository
    {
        private readonly string _connectionString;

        public ProviderAppointmentMappingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProviderAppointmentMappingDomain GetProviderAppointmentMapping(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ProviderAppointmentMapping WHERE UniqueTableId = @id";

                    var ProviderAppointmentMappingPoco = cn.QueryFirstOrDefault<ProviderAppointmentMappingPoco>(query, new { id = id }) ?? new ProviderAppointmentMappingPoco();

                    return ProviderAppointmentMappingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ProviderAppointmentMappingDomain> GetProviderAppointmentMappings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ProviderAppointmentMapping WHERE @criteria";
                    List<ProviderAppointmentMappingPoco> pocos = cn.Query<ProviderAppointmentMappingPoco>(sql).ToList();
                    List<ProviderAppointmentMappingDomain> domains = new List<ProviderAppointmentMappingDomain>();

                    foreach (ProviderAppointmentMappingPoco poco in pocos)
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

        public int DeleteProviderAppointmentMapping(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ProviderAppointmentMapping WHERE UniqueTableId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertProviderAppointmentMapping(ProviderAppointmentMappingDomain domain)
        {
            int insertedId = 0;

            try
            {
                ProviderAppointmentMappingPoco poco = new ProviderAppointmentMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ProviderAppointmentMapping] " +
                        "([UniqueTableId], [ProviderId], [ExternalId], [SourceId], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) VALUES (@UniqueTableId, @ProviderId, @ExternalId, @SourceId, @DateLastTouched, " +
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

        public int UpdateProviderAppointmentMapping(ProviderAppointmentMappingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ProviderAppointmentMappingPoco poco = new ProviderAppointmentMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ProviderAppointmentMapping " +
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
