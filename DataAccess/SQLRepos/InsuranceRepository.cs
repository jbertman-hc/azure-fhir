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
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly string _connectionString;

        public InsuranceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public InsuranceDomain GetInsurance(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Insurance WHERE patientID = @id";

                    var InsurancePoco = cn.QueryFirstOrDefault<InsurancePoco>(query, new { id = id }) ?? new InsurancePoco();

                    return InsurancePoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<InsuranceDomain> GetInsurances(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Insurance WHERE @criteria";
                    List<InsurancePoco> pocos = cn.Query<InsurancePoco>(sql).ToList();
                    List<InsuranceDomain> domains = new List<InsuranceDomain>();

                    foreach (InsurancePoco poco in pocos)
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

        public int DeleteInsurance(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Insurance WHERE patientID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertInsurance(InsuranceDomain domain)
        {
            int insertedId = 0;

            try
            {
                InsurancePoco poco = new InsurancePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Insurance] " +
                        "([patientID], [Insurance1], [Insurance1_number], [Insurance2], [Insurance2_number], [notes]) " +
                        "VALUES " +
                        "(@patientID, @Insurance1, @Insurance1_number, @Insurance2, @Insurance2_number, @notes); " +
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

        public int UpdateInsurance(InsuranceDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                InsurancePoco poco = new InsurancePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Insurance " +
                        "SET [Insurance1] = @Insurance1, [Insurance1_number] = @Insurance1_number, " +
                        "[Insurance2] = @Insurance2, [Insurance2_number] = @Insurance2_number, [notes] = @notes " +
                        "WHERE patientID = @patientID;";

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
