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
    public class SchedulingVerificationRepository : ISchedulingVerificationRepository
    {
        private readonly string _connectionString;

        public SchedulingVerificationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SchedulingVerificationDomain GetSchedulingVerification(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SchedulingVerification WHERE VerificationID = @id";

                    var SchedulingVerificationPoco = cn.QueryFirstOrDefault<SchedulingVerificationPoco>(query, new { id = id }) ?? new SchedulingVerificationPoco();

                    return SchedulingVerificationPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SchedulingVerificationDomain> GetSchedulingVerifications(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SchedulingVerification WHERE @criteria";
                    List<SchedulingVerificationPoco> pocos = cn.Query<SchedulingVerificationPoco>(sql).ToList();
                    List<SchedulingVerificationDomain> domains = new List<SchedulingVerificationDomain>();

                    foreach (SchedulingVerificationPoco poco in pocos)
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

        public int DeleteSchedulingVerification(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SchedulingVerification WHERE VerificationID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSchedulingVerification(SchedulingVerificationDomain domain)
        {
            int insertedId = 0;

            try
            {
                SchedulingVerificationPoco poco = new SchedulingVerificationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SchedulingVerification] " +
                        "([VerificationID], [PatientID], [ScheduledDate], [Result], [Details], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@VerificationID, @PatientID, @ScheduledDate, @Result, @Details, @DateLastTouched, " +
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

        public int UpdateSchedulingVerification(SchedulingVerificationDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SchedulingVerificationPoco poco = new SchedulingVerificationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SchedulingVerification " +
                        "SET [PatientID] = @PatientID, [ScheduledDate] = @ScheduledDate, [Result] = @Result, " +
                        "[Details] = @Details, [DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE VerificationID = @VerificationID;";

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
