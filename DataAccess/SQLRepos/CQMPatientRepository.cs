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
    public class CQMPatientRepository : ICQMPatientRepository
    {
        private readonly string _connectionString;

        public CQMPatientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CQMPatientDomain GetCQMPatient(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM CQMPatient WHERE CQMPatientId = @id";

                    var CQMPatientPoco = cn.QueryFirstOrDefault<CQMPatientPoco>(query, new { id = id }) ?? new CQMPatientPoco();

                    return CQMPatientPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CQMPatientDomain> GetCQMPatients(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM CQMPatient WHERE @criteria";
                    List<CQMPatientPoco> pocos = cn.Query<CQMPatientPoco>(sql).ToList();
                    List<CQMPatientDomain> domains = new List<CQMPatientDomain>();

                    foreach (CQMPatientPoco poco in pocos)
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

        public int DeleteCQMPatient(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM CQMPatient WHERE CQMPatientId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertCQMPatient(CQMPatientDomain domain)
        {
            int insertedId = 0;

            try
            {
                CQMPatientPoco poco = new CQMPatientPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[CQMPatient] " +
                        "([CQMPatientId], [CQMId], [CQMSection], [PatientId], [EncounterDate], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@CQMPatientId, @CQMId, @CQMSection, @PatientId, @EncounterDate, " +
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

        public int UpdateCQMPatient(CQMPatientDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                CQMPatientPoco poco = new CQMPatientPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE CQMPatient SET [CQMId] = @CQMId, [CQMSection] = @CQMSection, " +
                        "[PatientId] = @PatientId, [EncounterDate] = @EncounterDate, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE CQMPatientId = @CQMPatientId;";

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
