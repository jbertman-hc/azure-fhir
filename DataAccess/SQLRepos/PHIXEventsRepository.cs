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
    public class PHIXEventsRepository : IPHIXEventsRepository
    {
        private readonly string _connectionString;

        public PHIXEventsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PHIXEventsDomain GetPHIXEvents(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PHIXEvents WHERE PHIXEventsId = @id";

                    var PHIXEventsPoco = cn.QueryFirstOrDefault<PHIXEventsPoco>(query, new { id = id }) ?? new PHIXEventsPoco();

                    return PHIXEventsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PHIXEventsDomain> GetPHIXEvents(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PHIXEvents WHERE @criteria";
                    List<PHIXEventsPoco> pocos = cn.Query<PHIXEventsPoco>(sql).ToList();
                    List<PHIXEventsDomain> domains = new List<PHIXEventsDomain>();

                    foreach (PHIXEventsPoco poco in pocos)
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

        public int DeletePHIXEvents(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PHIXEvents WHERE PHIXEventsId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPHIXEvents(PHIXEventsDomain domain)
        {
            int insertedId = 0;

            try
            {
                PHIXEventsPoco poco = new PHIXEventsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PHIXEvents] " +
                        "([PHIXEventsId], [ProviderCode], [PatientID], [EventCreated], [EventType], [Demographics], " +
                        "[CCD], [CCR], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@PHIXEventsId, @ProviderCode, @PatientID, @EventCreated, @EventType, @Demographics, " +
                        "@CCD, @CCR, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdatePHIXEvents(PHIXEventsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PHIXEventsPoco poco = new PHIXEventsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PHIXEvents " +
                        "SET [ProviderCode] = @ProviderCode, [PatientID] = @PatientID, [EventCreated] = @EventCreated, " +
                        "[EventType] = @EventType, [Demographics] = @Demographics, [CCD] = @CCD, [CCR] = @CCR, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE PHIXEventsId = @PHIXEventsId;";

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
