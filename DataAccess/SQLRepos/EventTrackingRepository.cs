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
    public class EventTrackingRepository : IEventTrackingRepository
    {
        private readonly string _connectionString;

        public EventTrackingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public EventTrackingDomain GetEventTracking(Guid id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM EventTracking WHERE EventTrackingGUID = @id";

                    var EventTrackingPoco = cn.QueryFirstOrDefault<EventTrackingPoco>(query, new { id = id }) ?? new EventTrackingPoco();

                    return EventTrackingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<EventTrackingDomain> GetEventTrackings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM EventTracking WHERE @criteria";
                    List<EventTrackingPoco> pocos = cn.Query<EventTrackingPoco>(sql).ToList();
                    List<EventTrackingDomain> domains = new List<EventTrackingDomain>();

                    foreach (EventTrackingPoco poco in pocos)
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

        public int DeleteEventTracking(Guid id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM EventTracking WHERE EventTrackingGUID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertEventTracking(EventTrackingDomain domain)
        {
            int insertedId = 0;

            try
            {
                EventTrackingPoco poco = new EventTrackingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[EventTracking] " +
                        "([EventTrackingGUID], [PatientID], [EventDefinitionID], [LastTouchedBy], " +
                        "[DateLastTouched], [DateRowAdded]) " +
                        "VALUES " +
                        "(@EventTrackingGUID, @PatientID, @EventDefinitionID, @LastTouchedBy, " +
                        "@DateLastTouched, @DateRowAdded); " +
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

        public int UpdateEventTracking(EventTrackingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                EventTrackingPoco poco = new EventTrackingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE EventTracking " +
                        "SET [PatientID] = @PatientID, [EventDefinitionID] = @EventDefinitionID, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateLastTouched] = @DateLastTouched, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE EventTrackingGUID = @EventTrackingGUID;";

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
