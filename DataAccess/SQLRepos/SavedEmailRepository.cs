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
    public class SavedEmailRepository : ISavedEmailRepository
    {
        private readonly string _connectionString;

        public SavedEmailRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SavedEmailDomain GetSavedEmail(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SavedEmail WHERE msgID = @id";

                    var SavedEmailPoco = cn.QueryFirstOrDefault<SavedEmailPoco>(query, new { id = id }) ?? new SavedEmailPoco();

                    return SavedEmailPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SavedEmailDomain> GetSavedEmails(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SavedEmail WHERE @criteria";
                    List<SavedEmailPoco> pocos = cn.Query<SavedEmailPoco>(sql).ToList();
                    List<SavedEmailDomain> domains = new List<SavedEmailDomain>();

                    foreach (SavedEmailPoco poco in pocos)
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

        public int DeleteSavedEmail(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SavedEmail WHERE msgID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSavedEmail(SavedEmailDomain domain)
        {
            int insertedId = 0;

            try
            {
                SavedEmailPoco poco = new SavedEmailPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SavedEmail] " +
                        "([msgID], [SavedTo], [To], [From], [Date], [Re], [CC], [PatientName], [Body], [PatientID], " +
                        "[MsgHighlightColor], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@msgID, @SavedTo, @To, @From, @Date, @Re, @CC, @PatientName, @Body, @PatientID, " +
                        "@MsgHighlightColor, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateSavedEmail(SavedEmailDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SavedEmailPoco poco = new SavedEmailPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SavedEmail " +
                        "SET [SavedTo] = @SavedTo, [To] = @To, [From] = @From, [Date] = @Date, [Re] = @Re, " +
                        "[CC] = @CC, [PatientName] = @PatientName, [Body] = @Body, [PatientID] = @PatientID, " +
                        "[MsgHighlightColor] = @MsgHighlightColor, [DateLastTouched] = @DateLastTouched, " +
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
