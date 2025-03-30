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
    public class SuperbillPaymentsRepository : ISuperbillPaymentsRepository
    {
        private readonly string _connectionString;

        public SuperbillPaymentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SuperbillPaymentsDomain GetSuperbillPayments(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SuperbillPayments WHERE SuperbillPaymentID = @id";

                    var SuperbillPaymentsPoco = cn.QueryFirstOrDefault<SuperbillPaymentsPoco>(query, new { id = id }) ?? new SuperbillPaymentsPoco();

                    return SuperbillPaymentsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SuperbillPaymentsDomain> GetSuperbillPayments(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SuperbillPayments WHERE @criteria";
                    List<SuperbillPaymentsPoco> pocos = cn.Query<SuperbillPaymentsPoco>(sql).ToList();
                    List<SuperbillPaymentsDomain> domains = new List<SuperbillPaymentsDomain>();

                    foreach (SuperbillPaymentsPoco poco in pocos)
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

        public int DeleteSuperbillPayments(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SuperbillPayments WHERE SuperbillPaymentID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSuperbillPayments(SuperbillPaymentsDomain domain)
        {
            int insertedId = 0;

            try
            {
                SuperbillPaymentsPoco poco = new SuperbillPaymentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SuperbillPayments] " +
                        "([SuperbillPaymentID], [BillingID], [PatientID], [Copay], [Other], [Adjustments], " +
                        "[CopayComment], [OtherComment], [AdjustmentComment], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded], [BalanceComment]) " +
                        "VALUES " +
                        "(@SuperbillPaymentID, @BillingID, @PatientID, @Copay, @Other, @Adjustments, @CopayComment, " +
                        "@OtherComment, @AdjustmentComment, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @BalanceComment); " +
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

        public int UpdateSuperbillPayments(SuperbillPaymentsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SuperbillPaymentsPoco poco = new SuperbillPaymentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SuperbillPayments " +
                        "SET [BillingID] = @BillingID, [PatientID] = @PatientID, [Copay] = @Copay, [Other] = @Other, " +
                        "[Adjustments] = @Adjustments, [CopayComment] = @CopayComment, [OtherComment] = @OtherComment, " +
                        "[AdjustmentComment] = @AdjustmentComment, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[BalanceComment] = @BalanceComment " +
                        "WHERE SuperbillPaymentID = @SuperbillPaymentID;";

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
