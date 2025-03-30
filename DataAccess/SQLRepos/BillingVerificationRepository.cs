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
    public class BillingVerificationRepository : IBillingVerificationRepository
    {
        private readonly string _connectionString;

        public BillingVerificationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BillingVerificationDomain GetBillingVerification(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM BillingVerification WHERE VerificationID = @id";

                    var BillingVerificationPoco = cn.QueryFirstOrDefault<BillingVerificationPoco>(query, new { id = id }) ?? new BillingVerificationPoco();

                    return BillingVerificationPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BillingVerificationDomain> GetBillingVerifications(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM BillingVerification WHERE @criteria";
                    List<BillingVerificationPoco> pocos = cn.Query<BillingVerificationPoco>(sql).ToList();
                    List<BillingVerificationDomain> domains = new List<BillingVerificationDomain>();

                    foreach (BillingVerificationPoco poco in pocos)
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

        public int DeleteBillingVerification(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM BillingVerification WHERE VerificationID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertBillingVerification(BillingVerificationDomain domain)
        {
            int insertedId = 0;

            try
            {
                BillingVerificationPoco poco = new BillingVerificationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[BillingVerification] " +
                        "([VerificationID], [PatientID], [ListPayorID], [DateVerified], [Result], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@VerificationID, @PatientID, @ListPayorID, @DateVerified, @Result, " +
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

        public int UpdateBillingVerification(BillingVerificationDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                BillingVerificationPoco poco = new BillingVerificationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE BillingVerification " +
                        "SET [PatientID] = @PatientID, [ListPayorID] = @ListPayorID, " +
                        "[DateVerified] = @DateVerified, [Result] = @Result, " +
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
