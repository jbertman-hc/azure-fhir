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
    public class EligibilityVerificationRepository : IEligibilityVerificationRepository
    {
        private readonly string _connectionString;

        public EligibilityVerificationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public EligibilityVerificationDomain GetEligibilityVerification(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM EligibilityVerification WHERE VerificationID = @id";

                    var EligibilityVerificationPoco = cn.QueryFirstOrDefault<EligibilityVerificationPoco>(query, new { id = id }) ?? new EligibilityVerificationPoco();

                    return EligibilityVerificationPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<EligibilityVerificationDomain> GetEligibilityVerifications(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM EligibilityVerification WHERE @criteria";
                    List<EligibilityVerificationPoco> pocos = cn.Query<EligibilityVerificationPoco>(sql).ToList();
                    List<EligibilityVerificationDomain> domains = new List<EligibilityVerificationDomain>();

                    foreach (EligibilityVerificationPoco poco in pocos)
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

        public int DeleteEligibilityVerification(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM EligibilityVerification WHERE VerificationID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertEligibilityVerification(EligibilityVerificationDomain domain)
        {
            int insertedId = 0;

            try
            {
                EligibilityVerificationPoco poco = new EligibilityVerificationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[EligibilityVerification] " +
                        "([VerificationID], [PatientID], [ListPayorID], [VerifiedDate], [Result], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@VerificationID, @PatientID, @ListPayorID, @VerifiedDate, @Result, " +
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

        public int UpdateEligibilityVerification(EligibilityVerificationDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                EligibilityVerificationPoco poco = new EligibilityVerificationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE EligibilityVerification " +
                        "SET [PatientID] = @PatientID, [ListPayorID] = @ListPayorID, " +
                        "[VerifiedDate] = @VerifiedDate, [Result] = @Result, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
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
