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
    public class PatientMessagesRepository : IPatientMessagesRepository
    {
        private readonly string _connectionString;

        public PatientMessagesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PatientMessagesDomain GetPatientMessages(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PatientMessages WHERE msgID = @id";

                    var PatientMessagesPoco = cn.QueryFirstOrDefault<PatientMessagesPoco>(query, new { id = id }) ?? new PatientMessagesPoco();

                    return PatientMessagesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PatientMessagesDomain> GetPatientMessages(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PatientMessages WHERE @criteria";
                    List<PatientMessagesPoco> pocos = cn.Query<PatientMessagesPoco>(sql).ToList();
                    List<PatientMessagesDomain> domains = new List<PatientMessagesDomain>();

                    foreach (PatientMessagesPoco poco in pocos)
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

        public int DeletePatientMessages(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PatientMessages WHERE msgID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPatientMessages(PatientMessagesDomain domain)
        {
            int insertedId = 0;

            try
            {
                PatientMessagesPoco poco = new PatientMessagesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PatientMessages] " +
                        "([msgID], [To], [From], [Date], [Re], [CC], [PatientName], [Body], [PatientID], " +
                        "[ProviderSignature], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@msgID, @To, @From, @Date, @Re, @CC, @PatientName, @Body, @PatientID, @ProviderSignature, " +
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

        public int UpdatePatientMessages(PatientMessagesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PatientMessagesPoco poco = new PatientMessagesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PatientMessages " +
                        "SET [To] = @To, [From] = @From, [Date] = @Date, [Re] = @Re, [CC] = @CC, " +
                        "[PatientName] = @PatientName, [Body] = @Body, [PatientID] = @PatientID, " +
                        "[ProviderSignature] = @ProviderSignature, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE msgID = @msgID;";

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
