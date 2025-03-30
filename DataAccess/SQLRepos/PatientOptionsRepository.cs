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
    public class PatientOptionsRepository : IPatientOptionsRepository
    {
        private readonly string _connectionString;

        public PatientOptionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PatientOptionsDomain GetPatientOptions(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PatientOptions WHERE PatientID = @id";

                    var PatientOptionsPoco = cn.QueryFirstOrDefault<PatientOptionsPoco>(query, new { id = id }) ?? new PatientOptionsPoco();

                    return PatientOptionsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PatientOptionsDomain> GetPatientOptions(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PatientOptions WHERE @criteria";
                    List<PatientOptionsPoco> pocos = cn.Query<PatientOptionsPoco>(sql).ToList();
                    List<PatientOptionsDomain> domains = new List<PatientOptionsDomain>();

                    foreach (PatientOptionsPoco poco in pocos)
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

        public int DeletePatientOptions(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PatientOptions WHERE PatientID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPatientOptions(PatientOptionsDomain domain)
        {
            int insertedId = 0;

            try
            {
                PatientOptionsPoco poco = new PatientOptionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PatientOptions] " +
                        "([PatientID], [DrugHistoryGranted], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES (@PatientID, @DrugHistoryGranted, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdatePatientOptions(PatientOptionsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PatientOptionsPoco poco = new PatientOptionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PatientOptions " +
                        "SET [DrugHistoryGranted] = @DrugHistoryGranted, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE PatientID = @PatientID;";

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
