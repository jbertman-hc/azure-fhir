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
    public class SuperbillPayorPaymentsRepository : ISuperbillPayorPaymentsRepository
    {
        private readonly string _connectionString;

        public SuperbillPayorPaymentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SuperbillPayorPaymentsDomain GetSuperbillPayorPayments(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SuperbillPayorPayments WHERE SuperbillPayorPaymentID = @id";

                    var SuperbillPayorPaymentsPoco = cn.QueryFirstOrDefault<SuperbillPayorPaymentsPoco>(query, new { id = id }) ?? new SuperbillPayorPaymentsPoco();

                    return SuperbillPayorPaymentsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SuperbillPayorPaymentsDomain> GetSuperbillPayorPayments(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SuperbillPayorPayments WHERE @criteria";
                    List<SuperbillPayorPaymentsPoco> pocos = cn.Query<SuperbillPayorPaymentsPoco>(sql).ToList();
                    List<SuperbillPayorPaymentsDomain> domains = new List<SuperbillPayorPaymentsDomain>();

                    foreach (SuperbillPayorPaymentsPoco poco in pocos)
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

        public int DeleteSuperbillPayorPayments(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SuperbillPayorPayments WHERE SuperbillPayorPaymentID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSuperbillPayorPayments(SuperbillPayorPaymentsDomain domain)
        {
            int insertedId = 0;

            try
            {
                SuperbillPayorPaymentsPoco poco = new SuperbillPayorPaymentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SuperbillPayorPayments] " +
                        "([SuperbillPayorPaymentID], [SuperbillPaymentID], [PayorsID], [PayorAmount], [PayorComment], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@SuperbillPayorPaymentID, @SuperbillPaymentID, @PayorsID, @PayorAmount, @PayorComment, " +
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

        public int UpdateSuperbillPayorPayments(SuperbillPayorPaymentsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SuperbillPayorPaymentsPoco poco = new SuperbillPayorPaymentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SuperbillPayorPayments " +
                        "SET [SuperbillPaymentID] = @SuperbillPaymentID, [PayorsID] = @PayorsID, " +
                        "[PayorAmount] = @PayorAmount, [PayorComment] = @PayorComment, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE SuperbillPayorPaymentID = @SuperbillPayorPaymentID;";

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
