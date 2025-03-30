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
    public class GuardianPatientsRepository : IGuardianPatientsRepository
    {
        private readonly string _connectionString;

        public GuardianPatientsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public GuardianPatientsDomain GetGuardianPatients(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM GuardianPatients WHERE GuardianPatientsID = @id";

                    var GuardianPatientsPoco = cn.QueryFirstOrDefault<GuardianPatientsPoco>(query, new { id = id }) ?? new GuardianPatientsPoco();

                    return GuardianPatientsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<GuardianPatientsDomain> GetGuardianPatients(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM GuardianPatients WHERE @criteria";
                    List<GuardianPatientsPoco> pocos = cn.Query<GuardianPatientsPoco>(sql).ToList();
                    List<GuardianPatientsDomain> domains = new List<GuardianPatientsDomain>();

                    foreach (GuardianPatientsPoco poco in pocos)
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

        public int DeleteGuardianPatients(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM GuardianPatients WHERE GuardianPatientsID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertGuardianPatients(GuardianPatientsDomain domain)
        {
            int insertedId = 0;

            try
            {
                GuardianPatientsPoco poco = new GuardianPatientsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[GuardianPatients] " +
                        "([GuardianPatientsID], [PatientID], [OtherPatientID], [RelationID], " +
                        "[IsPrimaryGuardian], [DateLastTouched], [LastTouchedBy], [DateRowAdded], [Comments]) " +
                        "VALUES " +
                        "(@GuardianPatientsID, @PatientID, @OtherPatientID, @RelationID, @IsPrimaryGuardian, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @Comments); " +
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

        public int UpdateGuardianPatients(GuardianPatientsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                GuardianPatientsPoco poco = new GuardianPatientsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE GuardianPatients " +
                        "SET [PatientID] = @PatientID, [OtherPatientID] = @OtherPatientID, " +
                        "[RelationID] = @RelationID, [IsPrimaryGuardian] = @IsPrimaryGuardian, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [Comments] = @Comments " +
                        "WHERE GuardianPatientsID = @GuardianPatientsID;";

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
