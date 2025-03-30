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
    public class SchedulingMappingRepository : ISchedulingMappingRepository
    {
        private readonly string _connectionString;

        public SchedulingMappingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SchedulingMappingDomain GetSchedulingMapping(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SchedulingMapping WHERE UniqueTableId = @id";

                    var SchedulingMappingPoco = cn.QueryFirstOrDefault<SchedulingMappingPoco>(query, new { id = id }) ?? new SchedulingMappingPoco();

                    return SchedulingMappingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SchedulingMappingDomain> GetSchedulingMappings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SchedulingMapping WHERE @criteria";
                    List<SchedulingMappingPoco> pocos = cn.Query<SchedulingMappingPoco>(sql).ToList();
                    List<SchedulingMappingDomain> domains = new List<SchedulingMappingDomain>();

                    foreach (SchedulingMappingPoco poco in pocos)
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

        public int DeleteSchedulingMapping(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SchedulingMapping WHERE UniqueTableId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSchedulingMapping(SchedulingMappingDomain domain)
        {
            int insertedId = 0;

            try
            {
                SchedulingMappingPoco poco = new SchedulingMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SchedulingMapping] " +
                        "([UniqueTableId], [VisitId], [ExternalId], [SourceId], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@UniqueTableId, @VisitId, @ExternalId, @SourceId, @DateLastTouched, " +
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

        public int UpdateSchedulingMapping(SchedulingMappingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SchedulingMappingPoco poco = new SchedulingMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SchedulingMapping " +
                        "SET [VisitId] = @VisitId, [ExternalId] = @ExternalId, [SourceId] = @SourceId, " +
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
