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
    public class PatientPaymentsRepository : IPatientPaymentsRepository
    {
        private readonly string _connectionString;

        public PatientPaymentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PatientPaymentsDomain GetPatientPayments(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PatientPayments WHERE PatientPaymentsID = @id";

                    var PatientPaymentsPoco = cn.QueryFirstOrDefault<PatientPaymentsPoco>(query, new { id = id }) ?? new PatientPaymentsPoco();

                    return PatientPaymentsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PatientPaymentsDomain> GetPatientPayments(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = $"SELECT * FROM PatientPayments WHERE {@criteria}";

                    List<PatientPaymentsPoco> pocos = cn.Query<PatientPaymentsPoco>(sql).ToList();
                    List<PatientPaymentsDomain> domains = new List<PatientPaymentsDomain>();

                    foreach (PatientPaymentsPoco poco in pocos)
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

        public int DeletePatientPayments(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PatientPayments WHERE PatientPaymentsID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPatientPayments(PatientPaymentsDomain domain)
        {
            int insertedId = 0;

            try
            {
                PatientPaymentsPoco poco = new PatientPaymentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PatientPayments] " +
                        "([PatientPaymentsID], [PatientID], [Amount], [Comments], [PaymentDate], [CheckNo], " +
                        "[CreditCardType], [AcctCorrection], [Assigned], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded], [Historical]) " +
                        "VALUES " +
                        "(@PatientPaymentsID, @PatientID, @Amount, @Comments, @PaymentDate, @CheckNo, " +
                        "@CreditCardType, @AcctCorrection, @Assigned, @DateLastTouched, @LastTouchedBy, " +
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

        public int UpdatePatientPayments(PatientPaymentsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PatientPaymentsPoco poco = new PatientPaymentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PatientPayments " +
                        "SET [PatientID] = @PatientID, [Amount] = @Amount, [Comments] = @Comments, " +
                        "[PaymentDate] = @PaymentDate, [CheckNo] = @CheckNo, [CreditCardType] = @CreditCardType, " +
                        "[AcctCorrection] = @AcctCorrection, [Assigned] = @Assigned, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [Historical] = @Historical " +
                        "WHERE PatientPaymentsID = @PatientPaymentsID;";

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
