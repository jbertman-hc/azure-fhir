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
    public class BillingTransactionsRepository : IBillingTransactionsRepository
    {
        private readonly string _connectionString;

        public BillingTransactionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BillingTransactionsDomain GetBillingTransactions(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM BillingTransactions WHERE BillingTransactionID = @id";

                    var BillingTransactionsPoco = cn.QueryFirstOrDefault<BillingTransactionsPoco>(query, new { id = id }) ?? new BillingTransactionsPoco();

                    return BillingTransactionsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BillingTransactionsDomain> GetBillingTransactions(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM BillingTransactions WHERE @criteria";
                    List<BillingTransactionsPoco> pocos = cn.Query<BillingTransactionsPoco>(sql).ToList();
                    List<BillingTransactionsDomain> domains = new List<BillingTransactionsDomain>();

                    foreach (BillingTransactionsPoco poco in pocos)
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

        public int DeleteBillingTransactions(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM BillingTransactions WHERE BillingTransactionID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertBillingTransactions(BillingTransactionsDomain domain)
        {
            int insertedId = 0;

            try
            {
                BillingTransactionsPoco poco = new BillingTransactionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[BillingTransactions] " +
                        "([BillingTransactionID], [BillingID], [TransactionDate], [TransactionAmount], " +
                        "[PaymentMethodID], [CheckNo], [TransactionComment], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [PaymentTypeID]) " +
                        "VALUES " +
                        "(@BillingTransactionID, @BillingID, @TransactionDate, @TransactionAmount, " +
                        "@PaymentMethodID, @CheckNo, @TransactionComment, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @PaymentTypeID); " +
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

        public int UpdateBillingTransactions(BillingTransactionsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                BillingTransactionsPoco poco = new BillingTransactionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE BillingTransactions " +
                        "SET [BillingID] = @BillingID, [TransactionDate] = @TransactionDate, " +
                        "[TransactionAmount] = @TransactionAmount, [PaymentMethodID] = @PaymentMethodID, " +
                        "[CheckNo] = @CheckNo, [TransactionComment] = @TransactionComment, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [PaymentTypeID] = @PaymentTypeID " +
                        "WHERE BillingTransactionID = @BillingTransactionID;";

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
