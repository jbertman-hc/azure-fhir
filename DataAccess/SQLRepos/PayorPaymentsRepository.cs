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
    public class PayorPaymentsRepository : IPayorPaymentsRepository
    {
        private readonly string _connectionString;

        public PayorPaymentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PayorPaymentsDomain GetPayorPayments(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PayorPayments WHERE PayorPaymentID = @id";

                    var PayorPaymentsPoco = cn.QueryFirstOrDefault<PayorPaymentsPoco>(query, new { id = id }) ?? new PayorPaymentsPoco();

                    return PayorPaymentsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PayorPaymentsDomain> GetPayorPayments(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PayorPayments WHERE @criteria";
                    List<PayorPaymentsPoco> pocos = cn.Query<PayorPaymentsPoco>(sql).ToList();
                    List<PayorPaymentsDomain> domains = new List<PayorPaymentsDomain>();

                    foreach (PayorPaymentsPoco poco in pocos)
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

        public int DeletePayorPayments(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PayorPayments WHERE PayorPaymentID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPayorPayments(PayorPaymentsDomain domain)
        {
            int insertedId = 0;

            try
            {
                PayorPaymentsPoco poco = new PayorPaymentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PayorPayments] " +
                        "([PayorPaymentID], [PayorID], [CheckNo], [Amount], [PaymentDate], [Comments], " +
                        "[PayorContactID], [Reconciled], [Hidden], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded], [Historical]) " +
                        "VALUES " +
                        "(@PayorPaymentID, @PayorID, @CheckNo, @Amount, @PaymentDate, @Comments, " +
                        "@PayorContactID, @Reconciled, @Hidden, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @Historical); " +
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

        public int UpdatePayorPayments(PayorPaymentsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PayorPaymentsPoco poco = new PayorPaymentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PayorPayments " +
                        "SET [PayorID] = @PayorID, [CheckNo] = @CheckNo, [Amount] = @Amount, " +
                        "[PaymentDate] = @PaymentDate, [Comments] = @Comments, [PayorContactID] = @PayorContactID, " +
                        "[Reconciled] = @Reconciled, [Hidden] = @Hidden, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [Historical] = @Historical " +
                        "WHERE PayorPaymentID = @PayorPaymentID;";

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
