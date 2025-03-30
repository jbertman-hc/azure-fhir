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
    public class EventLogRepository : IEventLogRepository
    {
        private readonly string _connectionString;

        public EventLogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public EventLogDomain GetEventLog(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM EventLog WHERE id = @id";

                    var EventLogPoco = cn.QueryFirstOrDefault<EventLogPoco>(query, new { id = id }) ?? new EventLogPoco();

                    return EventLogPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<EventLogDomain> GetEventLogs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM EventLog WHERE @criteria";
                    List<EventLogPoco> pocos = cn.Query<EventLogPoco>(sql).ToList();
                    List<EventLogDomain> domains = new List<EventLogDomain>();

                    foreach (EventLogPoco poco in pocos)
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

        public int DeleteEventLog(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM EventLog WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertEventLog(EventLogDomain domain)
        {
            int insertedId = 0;

            try
            {
                EventLogPoco poco = new EventLogPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[EventLog] " +
                        "([ID], [EventName], [EventDate], [Comments], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @EventName, @EventDate, @Comments, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateEventLog(EventLogDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                EventLogPoco poco = new EventLogPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE EventLog " +
                        "SET [EventName] = @EventName, [EventDate] = @EventDate, [Comments] = @Comments, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE ID = @ID;";

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
