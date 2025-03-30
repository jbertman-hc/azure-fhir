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
    public class ListPaymentsRepository : IListPaymentsRepository
    {
        private readonly string _connectionString;

        public ListPaymentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListPaymentsDomain GetListPayments(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListPayments WHERE ListPaymentsID = @id";

                    var ListPaymentsPoco = cn.QueryFirstOrDefault<ListPaymentsPoco>(query, new { id = id }) ?? new ListPaymentsPoco();

                    return ListPaymentsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListPaymentsDomain> GetListPayments(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListPayments WHERE @criteria";
                    List<ListPaymentsPoco> pocos = cn.Query<ListPaymentsPoco>(sql).ToList();
                    List<ListPaymentsDomain> domains = new List<ListPaymentsDomain>();

                    foreach (ListPaymentsPoco poco in pocos)
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

        public int DeleteListPayments(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListPayments WHERE ListPaymentsID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListPayments(ListPaymentsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListPaymentsPoco poco = new ListPaymentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPayments] " +
                        "([ListPaymentsID], [PatientPaymentsID], [PatientChargesID], [Amount], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ListPaymentsID, @PatientPaymentsID, @PatientChargesID, @Amount, @DateLastTouched, " +
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

        public int UpdateListPayments(ListPaymentsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListPaymentsPoco poco = new ListPaymentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPayments " +
                        "SET [PatientPaymentsID] = @PatientPaymentsID, [PatientChargesID] = @PatientChargesID, " +
                        "[Amount] = @Amount, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE ListPaymentsID = @ListPaymentsID;";

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
