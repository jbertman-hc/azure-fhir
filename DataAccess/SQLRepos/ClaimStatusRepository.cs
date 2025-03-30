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
    public class ClaimStatusRepository : IClaimStatusRepository
    {
        private readonly string _connectionString;

        public ClaimStatusRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ClaimStatusDomain GetClaimStatus(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ClaimStatus WHERE ClaimStatusID = @id";

                    var ClaimStatusPoco = cn.QueryFirstOrDefault<ClaimStatusPoco>(query, new { id = id }) ?? new ClaimStatusPoco();

                    return ClaimStatusPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ClaimStatusDomain> GetClaimStatus(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ClaimStatus WHERE @criteria";
                    List<ClaimStatusPoco> pocos = cn.Query<ClaimStatusPoco>(sql).ToList();
                    List<ClaimStatusDomain> domains = new List<ClaimStatusDomain>();

                    foreach (ClaimStatusPoco poco in pocos)
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

        public int DeleteClaimStatus(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ClaimStatus WHERE ClaimStatusID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertClaimStatus(ClaimStatusDomain domain)
        {
            int insertedId = 0;

            try
            {
                ClaimStatusPoco poco = new ClaimStatusPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ClaimStatus] " +
                        "([ClaimStatusID], [BillingDatesID], [PayorICN], [PaidOrDeniedDate], " +
                        "[PaymentType], [CheckNum], [CheckIssueDate], [PayorID], [ChargeAMT], " +
                        "[PaidAMT], [DOS], [EffectiveDate], [IsRejection], [Unread], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ClaimStatusID, @BillingDatesID, @PayorICN, @PaidOrDeniedDate, " +
                        "@PaymentType, @CheckNum, @CheckIssueDate, @PayorID, @ChargeAMT, @PaidAMT, " +
                        "@DOS, @EffectiveDate, @IsRejection, @Unread, @DateLastTouched, " +
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

        public int UpdateClaimStatus(ClaimStatusDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ClaimStatusPoco poco = new ClaimStatusPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ClaimStatus " +
                        "SET [BillingDatesID] = @BillingDatesID, [PayorICN] = @PayorICN, " +
                        "[PaidOrDeniedDate] = @PaidOrDeniedDate, [PaymentType] = @PaymentType, " +
                        "[CheckNum] = @CheckNum, [CheckIssueDate] = @CheckIssueDate, " +
                        "[PayorID] = @PayorID, [ChargeAMT] = @ChargeAMT, [PaidAMT] = @PaidAMT, " +
                        "[DOS] = @DOS, [EffectiveDate] = @EffectiveDate, [IsRejection] = @IsRejection, " +
                        "[Unread] = @Unread, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE ClaimStatusID = @ClaimStatusID;";

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
