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
    public class PatientIndexRepository : IPatientIndexRepository
    {
        private readonly string _connectionString;

        public PatientIndexRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PatientIndexDomain GetPatientIndex(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PatientIndex WHERE PatientIndexId = @id";

                    var PatientIndexPoco = cn.QueryFirstOrDefault<PatientIndexPoco>(query, new { id = id }) ?? new PatientIndexPoco();

                    return PatientIndexPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PatientIndexDomain> GetPatientIndexs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PatientIndex WHERE @criteria";
                    List<PatientIndexPoco> pocos = cn.Query<PatientIndexPoco>(sql).ToList();
                    List<PatientIndexDomain> domains = new List<PatientIndexDomain>();

                    foreach (PatientIndexPoco poco in pocos)
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

        public int DeletePatientIndex(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PatientIndex WHERE PatientIndexId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPatientIndex(PatientIndexDomain domain)
        {
            int insertedId = 0;

            try
            {
                PatientIndexPoco poco = new PatientIndexPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PatientIndex] " +
                        "([PatientIndexId], [AcPatientId], [ExternalPatientId], [Source], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [ExternalData]) " +
                        "VALUES " +
                        "(@PatientIndexId, @AcPatientId, @ExternalPatientId, @Source, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @ExternalData); " +
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

        public int UpdatePatientIndex(PatientIndexDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PatientIndexPoco poco = new PatientIndexPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PatientIndex " +
                        "SET [AcPatientId] = @AcPatientId, [ExternalPatientId] = @ExternalPatientId, " +
                        "[Source] = @Source, [DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [ExternalData] = @ExternalData " +
                        "WHERE PatientIndexId = @PatientIndexId;";

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
