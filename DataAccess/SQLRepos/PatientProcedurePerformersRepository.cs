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
    public class PatientProcedurePerformersRepository : IPatientProcedurePerformersRepository
    {
        private readonly string _connectionString;

        public PatientProcedurePerformersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PatientProcedurePerformersDomain GetPatientProcedurePerformers(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PatientProcedurePerformers WHERE id = @id";

                    var PatientProcedurePerformersPoco = cn.QueryFirstOrDefault<PatientProcedurePerformersPoco>(query, new { id = id }) ?? new PatientProcedurePerformersPoco();

                    return PatientProcedurePerformersPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PatientProcedurePerformersDomain> GetPatientProcedurePerformers(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PatientProcedurePerformers WHERE @criteria";
                    List<PatientProcedurePerformersPoco> pocos = cn.Query<PatientProcedurePerformersPoco>(sql).ToList();
                    List<PatientProcedurePerformersDomain> domains = new List<PatientProcedurePerformersDomain>();

                    foreach (PatientProcedurePerformersPoco poco in pocos)
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

        public int DeletePatientProcedurePerformers(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PatientProcedurePerformers WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPatientProcedurePerformers(PatientProcedurePerformersDomain domain)
        {
            int insertedId = 0;

            try
            {
                PatientProcedurePerformersPoco poco = new PatientProcedurePerformersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PatientProcedurePerformers] " +
                        "([Id], [Name], [Address], [PhoneNumber], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES (@Id, @Name, @Address, @PhoneNumber, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdatePatientProcedurePerformers(PatientProcedurePerformersDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PatientProcedurePerformersPoco poco = new PatientProcedurePerformersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PatientProcedurePerformers " +
                        "SET [Name] = @Name, [Address] = @Address, [PhoneNumber] = @PhoneNumber, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE Id = @Id;";

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
