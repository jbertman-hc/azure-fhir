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
    public class ArchiveScheduleRepository : IArchiveScheduleRepository
    {
        private readonly string _connectionString;

        public ArchiveScheduleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ArchiveScheduleDomain GetArchiveSchedule(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ArchiveSchedule WHERE VisitId = @id";

                    var ArchiveSchedulePoco = cn.QueryFirstOrDefault<ArchiveSchedulePoco>(query, new { id = id }) ?? new ArchiveSchedulePoco();

                    return ArchiveSchedulePoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ArchiveScheduleDomain> GetArchiveSchedules(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ArchiveSchedule WHERE @criteria";
                    List<ArchiveSchedulePoco> pocos = cn.Query<ArchiveSchedulePoco>(sql).ToList();
                    List<ArchiveScheduleDomain> domains = new List<ArchiveScheduleDomain>();

                    foreach (ArchiveSchedulePoco poco in pocos)
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

        public int DeleteArchiveSchedule(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ArchiveSchedule WHERE VisitId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertArchiveSchedule(ArchiveScheduleDomain domain)
        {
            int insertedId = 0;

            try
            {
                ArchiveSchedulePoco poco = new ArchiveSchedulePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ArchiveSchedule] " +
                        "([VisitID], [Date], [PatientID], [Name], [Phone], [VisitType], " +
                        "[Comments], [Booker], [DateBooked], [ProviderID], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@VisitID, @Date, @PatientID, @Name, @Phone, @VisitType, @Comments, " +
                        "@Booker, @DateBooked, @ProviderID, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateArchiveSchedule(ArchiveScheduleDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ArchiveSchedulePoco poco = new ArchiveSchedulePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ArchiveSchedule SET [Date] = @Date, [PatientID] = @PatientID, " +
                        "[Name] = @Name, [Phone] = @Phone, [VisitType] = @VisitType, [Comments] = @Comments, " +
                        "[Booker] = @Booker, [DateBooked] = @DateBooked, [ProviderID] = @ProviderID, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE VisitID = @VisitID;";

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
