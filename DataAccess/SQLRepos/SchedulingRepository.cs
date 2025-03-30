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
    public class SchedulingRepository : ISchedulingRepository
    {
        private readonly string _connectionString;

        public SchedulingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SchedulingDomain GetScheduling(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Scheduling WHERE VisitID = @id";

                    var SchedulingPoco = cn.QueryFirstOrDefault<SchedulingPoco>(query, new { id = id }) ?? new SchedulingPoco();

                    return SchedulingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SchedulingDomain> GetSchedulings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Scheduling WHERE @criteria";
                    List<SchedulingPoco> pocos = cn.Query<SchedulingPoco>(sql).ToList();
                    List<SchedulingDomain> domains = new List<SchedulingDomain>();

                    foreach (SchedulingPoco poco in pocos)
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

        public int DeleteScheduling(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Scheduling WHERE VisitID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertScheduling(SchedulingDomain domain)
        {
            int insertedId = 0;

            try
            {
                SchedulingPoco poco = new SchedulingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Scheduling] " +
                        "([VisitID], [Date], [PatientID], [Name], [Phone], [VisitType], [Comments], [Booker], " +
                        "[DateBooked], [ProviderID], [Duration], [XLinkProviderID], [VisitIdExternal], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [IsEditable], [SourceSystemId]) " +
                        "VALUES " +
                        "(@VisitID, @Date, @PatientID, @Name, @Phone, @VisitType, @Comments, @Booker, " +
                        "@DateBooked, @ProviderID, @Duration, @XLinkProviderID, @VisitIdExternal, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @IsEditable, @SourceSystemId); " +
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

        public int UpdateScheduling(SchedulingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SchedulingPoco poco = new SchedulingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Scheduling " +
                        "SET [Date] = @Date, [PatientID] = @PatientID, [Name] = @Name, [Phone] = @Phone, " +
                        "[VisitType] = @VisitType, [Comments] = @Comments, [Booker] = @Booker, " +
                        "[DateBooked] = @DateBooked, [ProviderID] = @ProviderID, [Duration] = @Duration, " +
                        "[XLinkProviderID] = @XLinkProviderID, [VisitIdExternal] = @VisitIdExternal, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [IsEditable] = @IsEditable, [SourceSystemId] = @SourceSystemId " +
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
