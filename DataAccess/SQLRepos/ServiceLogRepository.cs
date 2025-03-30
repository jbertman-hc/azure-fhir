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
    public class ServiceLogRepository : IServiceLogRepository
    {
        private readonly string _connectionString;

        public ServiceLogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ServiceLogDomain GetServiceLog(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ServiceLog WHERE ServiceLogID = @id";

                    var ServiceLogPoco = cn.QueryFirstOrDefault<ServiceLogPoco>(query, new { id = id }) ?? new ServiceLogPoco();

                    return ServiceLogPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ServiceLogDomain> GetServiceLogs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ServiceLog WHERE @criteria";
                    List<ServiceLogPoco> pocos = cn.Query<ServiceLogPoco>(sql).ToList();
                    List<ServiceLogDomain> domains = new List<ServiceLogDomain>();

                    foreach (ServiceLogPoco poco in pocos)
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

        public int DeleteServiceLog(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ServiceLog WHERE ServiceLogID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertServiceLog(ServiceLogDomain domain)
        {
            int insertedId = 0;

            try
            {
                ServiceLogPoco poco = new ServiceLogPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ServiceLog] " +
                        "([ServiceLogID], [ScheduleID], [StartDt], [EndDt], [Status], [LastUpdatedDt], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ServiceLogID, @ScheduleID, @StartDt, @EndDt, @Status, @LastUpdatedDt, " +
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

        public int UpdateServiceLog(ServiceLogDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ServiceLogPoco poco = new ServiceLogPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ServiceLog " +
                        "SET [ScheduleID] = @ScheduleID, [StartDt] = @StartDt, [EndDt] = @EndDt, " +
                        "[Status] = @Status, [LastUpdatedDt] = @LastUpdatedDt, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE ServiceLogID = @ServiceLogID;";

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
