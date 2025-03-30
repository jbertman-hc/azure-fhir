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
    public class RemitClaimsRepository : IRemitClaimsRepository
    {
        private readonly string _connectionString;

        public RemitClaimsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RemitClaimsDomain GetRemitClaims(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM RemitClaims WHERE RemitClaimsID = @id";

                    var RemitClaimsPoco = cn.QueryFirstOrDefault<RemitClaimsPoco>(query, new { id = id }) ?? new RemitClaimsPoco();

                    return RemitClaimsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RemitClaimsDomain> GetRemitClaims(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM RemitClaims WHERE @criteria";
                    List<RemitClaimsPoco> pocos = cn.Query<RemitClaimsPoco>(sql).ToList();
                    List<RemitClaimsDomain> domains = new List<RemitClaimsDomain>();

                    foreach (RemitClaimsPoco poco in pocos)
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

        public int DeleteRemitClaims(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM RemitClaims WHERE RemitClaimsID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertRemitClaims(RemitClaimsDomain domain)
        {
            int insertedId = 0;

            try
            {
                RemitClaimsPoco poco = new RemitClaimsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[RemitClaims] " +
                        "([RemitClaimsID], [PayorPaymentID], [BillingID], [TotalCharge], [TotalPayment], " +
                        "[DateRecieved], [ClaimStatusCode], [FinalPayor], [DeniedLines], [Comments], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [PayerInternalControlNum]) " +
                        "VALUES " +
                        "(@RemitClaimsID, @PayorPaymentID, @BillingID, @TotalCharge, @TotalPayment, " +
                        "@DateRecieved, @ClaimStatusCode, @FinalPayor, @DeniedLines, @Comments, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @PayerInternalControlNum); " +
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

        public int UpdateRemitClaims(RemitClaimsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                RemitClaimsPoco poco = new RemitClaimsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE RemitClaims " +
                        "SET [PayorPaymentID] = @PayorPaymentID, [BillingID] = @BillingID, [TotalCharge] = @TotalCharge, " +
                        "[TotalPayment] = @TotalPayment, [DateRecieved] = @DateRecieved, " +
                        "[ClaimStatusCode] = @ClaimStatusCode, [FinalPayor] = @FinalPayor, " +
                        "[DeniedLines] = @DeniedLines, [Comments] = @Comments, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[PayerInternalControlNum] = @PayerInternalControlNum " +
                        "WHERE RemitClaimsID = @RemitClaimsID;";

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
