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
    public class LatestVitalsTemporaryRepository : ILatestVitalsTemporaryRepository
    {
        private readonly string _connectionString;

        public LatestVitalsTemporaryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LatestVitalsTemporaryDomain GetLatestVitalsTemporary(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM LatestVitalsTemporary WHERE VitalsId = @id";

                    var LatestVitalsTemporaryPoco = cn.QueryFirstOrDefault<LatestVitalsTemporaryPoco>(query, new { id = id }) ?? new LatestVitalsTemporaryPoco();

                    return LatestVitalsTemporaryPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LatestVitalsTemporaryDomain> GetLatestVitalsTemporarys(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM LatestVitalsTemporary WHERE @criteria";
                    List<LatestVitalsTemporaryPoco> pocos = cn.Query<LatestVitalsTemporaryPoco>(sql).ToList();
                    List<LatestVitalsTemporaryDomain> domains = new List<LatestVitalsTemporaryDomain>();

                    foreach (LatestVitalsTemporaryPoco poco in pocos)
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

        public int DeleteLatestVitalsTemporary(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM LatestVitalsTemporary WHERE VitalsId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLatestVitalsTemporary(LatestVitalsTemporaryDomain domain)
        {
            int insertedId = 0;

            try
            {
                LatestVitalsTemporaryPoco poco = new LatestVitalsTemporaryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[LatestVitalsTemporary] " +
                        "([VitalsId], [PatientID], [Systolic], [Diastolic], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@VitalsId, @PatientID, @Systolic, @Diastolic, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateLatestVitalsTemporary(LatestVitalsTemporaryDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LatestVitalsTemporaryPoco poco = new LatestVitalsTemporaryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE LatestVitalsTemporary " +
                        "SET [PatientID] = @PatientID, [Systolic] = @Systolic, [Diastolic] = @Diastolic, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE VitalsId = @VitalsId;";

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
