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
    public class BillingDatesRepository : IBillingDatesRepository
    {
        private readonly string _connectionString;

        public BillingDatesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BillingDatesDomain GetBillingDates(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM BillingDates WHERE BillingDatesID = @id";

                    var BillingDatesPoco = cn.QueryFirstOrDefault<BillingDatesPoco>(query, new { id = id }) ?? new BillingDatesPoco();

                    return BillingDatesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BillingDatesDomain> GetBillingDates(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM BillingDates WHERE @criteria";
                    List<BillingDatesPoco> pocos = cn.Query<BillingDatesPoco>(sql).ToList();
                    List<BillingDatesDomain> domains = new List<BillingDatesDomain>();

                    foreach (BillingDatesPoco poco in pocos)
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

        public int DeleteBillingDates(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM BillingDates WHERE BillingDatesID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertBillingDates(BillingDatesDomain domain)
        {
            int insertedId = 0;

            try
            {
                BillingDatesPoco poco = new BillingDatesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[BillingDates] " +
                        "([BillingDatesID], [BillingID], [EventTypeID], [EventDate], [PayorID], " +
                        "[BillingActionID], [RemitProviderAdjustmentsHeaderID], [SlushAcctID], " +
                        "[InsuranceSnapshotDate], [MessageDetail], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@BillingDatesID, @BillingID, @EventTypeID, @EventDate, @PayorID, " +
                        "@BillingActionID, @RemitProviderAdjustmentsHeaderID, @SlushAcctID, " +
                        "@InsuranceSnapshotDate, @MessageDetail, @DateLastTouched, " +
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

        public int UpdateBillingDates(BillingDatesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                BillingDatesPoco poco = new BillingDatesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE BillingDates " +
                        "SET [BillingID] = @BillingID, [EventTypeID] = @EventTypeID, [EventDate] = @EventDate, " +
                        "[PayorID] = @PayorID, [BillingActionID] = @BillingActionID, " +
                        "[RemitProviderAdjustmentsHeaderID] = @RemitProviderAdjustmentsHeaderID, " +
                        "[SlushAcctID] = @SlushAcctID, [InsuranceSnapshotDate] = @InsuranceSnapshotDate, " +
                        "[MessageDetail] = @MessageDetail, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE BillingDatesID = @BillingDatesID;";

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
