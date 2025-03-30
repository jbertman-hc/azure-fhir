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
    public class TaskScheduleRepository : ITaskScheduleRepository
    {
        private readonly string _connectionString;

        public TaskScheduleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TaskScheduleDomain GetTaskSchedule(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM TaskSchedule WHERE TaskScheduleID = @id";

                    var TaskSchedulePoco = cn.QueryFirstOrDefault<TaskSchedulePoco>(query, new { id = id }) ?? new TaskSchedulePoco();

                    return TaskSchedulePoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<TaskScheduleDomain> GetTaskSchedules(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM TaskSchedule WHERE @criteria";
                    List<TaskSchedulePoco> pocos = cn.Query<TaskSchedulePoco>(sql).ToList();
                    List<TaskScheduleDomain> domains = new List<TaskScheduleDomain>();

                    foreach (TaskSchedulePoco poco in pocos)
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

        public int DeleteTaskSchedule(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM TaskSchedule WHERE TaskScheduleID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertTaskSchedule(TaskScheduleDomain domain)
        {
            int insertedId = 0;

            try
            {
                TaskSchedulePoco poco = new TaskSchedulePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[TaskSchedule] " +
                        "([TaskScheduleID], [TaskID], [FrequencyNumber], [FrequencyUnit], [IsRecurring], " +
                        "[DateTimeLastRun], [DateTimeToRun], [TaskEnabled], [CurrentFailures], [LastFailureTime], " +
                        "[LastFailureMessage], [LastFailureNotificationTime], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@TaskScheduleID, @TaskID, @FrequencyNumber, @FrequencyUnit, @IsRecurring, " +
                        "@DateTimeLastRun, @DateTimeToRun, @TaskEnabled, @CurrentFailures, @LastFailureTime, " +
                        "@LastFailureMessage, @LastFailureNotificationTime, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded); " +
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

        public int UpdateTaskSchedule(TaskScheduleDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                TaskSchedulePoco poco = new TaskSchedulePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE TaskSchedule " +
                        "SET [TaskID] = @TaskID, [FrequencyNumber] = @FrequencyNumber, " +
                        "[FrequencyUnit] = @FrequencyUnit, [IsRecurring] = @IsRecurring, " +
                        "[DateTimeLastRun] = @DateTimeLastRun, [DateTimeToRun] = @DateTimeToRun, " +
                        "[TaskEnabled] = @TaskEnabled, [CurrentFailures] = @CurrentFailures, " +
                        "[LastFailureTime] = @LastFailureTime, [LastFailureMessage] = @LastFailureMessage, " +
                        "[LastFailureNotificationTime] = @LastFailureNotificationTime, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE TaskScheduleID = @TaskScheduleID;";

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
