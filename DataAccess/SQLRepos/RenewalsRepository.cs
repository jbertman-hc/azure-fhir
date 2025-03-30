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
    public class RenewalsRepository : IRenewalsRepository
    {
        private readonly string _connectionString;

        public RenewalsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RenewalsDomain GetRenewals(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Renewals WHERE RenewalID = @id";

                    var RenewalsPoco = cn.QueryFirstOrDefault<RenewalsPoco>(query, new { id = id }) ?? new RenewalsPoco();

                    return RenewalsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RenewalsDomain> GetRenewals(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Renewals WHERE @criteria";
                    List<RenewalsPoco> pocos = cn.Query<RenewalsPoco>(sql).ToList();
                    List<RenewalsDomain> domains = new List<RenewalsDomain>();

                    foreach (RenewalsPoco poco in pocos)
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

        public int DeleteRenewals(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Renewals WHERE RenewalID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertRenewals(RenewalsDomain domain)
        {
            int insertedId = 0;

            try
            {
                RenewalsPoco poco = new RenewalsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Renewals] " +
                        "([RenewalID], [RenewRequestID], [ResponseID], [SentToProviderCode], [DenyReasonCode], " +
                        "[DenyReason], [Comments], [DateReceived], [DateResponded], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [ListMedID]) " +
                        "VALUES " +
                        "(@RenewalID, @RenewRequestID, @ResponseID, @SentToProviderCode, @DenyReasonCode, " +
                        "@DenyReason, @Comments, @DateReceived, @DateResponded, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @ListMedID); " +
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

        public int UpdateRenewals(RenewalsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                RenewalsPoco poco = new RenewalsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Renewals " +
                        "SET [RenewRequestID] = @RenewRequestID, [ResponseID] = @ResponseID, " +
                        "[SentToProviderCode] = @SentToProviderCode, [DenyReasonCode] = @DenyReasonCode, " +
                        "[DenyReason] = @DenyReason, [Comments] = @Comments, [DateReceived] = @DateReceived, " +
                        "[DateResponded] = @DateResponded, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [ListMedID] = @ListMedID " +
                        "WHERE RenewalID = @RenewalID;";

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
