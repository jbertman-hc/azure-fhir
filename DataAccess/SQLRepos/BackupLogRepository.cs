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
    public class BackupLogRepository : IBackupLogRepository
    {
        private readonly string _connectionString;

        public BackupLogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BackupLogDomain GetBackupLog(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM BackupLog WHERE id = @id";

                    var BackupLogPoco = cn.QueryFirstOrDefault<BackupLogPoco>(query, new { id = id }) ?? new BackupLogPoco();

                    return BackupLogPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BackupLogDomain> GetBackupLogs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM BackupLog WHERE @criteria";
                    List<BackupLogPoco> pocos = cn.Query<BackupLogPoco>(sql).ToList();
                    List<BackupLogDomain> domains = new List<BackupLogDomain>();

                    foreach (BackupLogPoco poco in pocos)
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

        public int DeleteBackupLog(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM BackupLog WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertBackupLog(BackupLogDomain domain)
        {
            int insertedId = 0;

            try
            {
                BackupLogPoco poco = new BackupLogPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[BackupLog] " +
                        "([ID], [Type], [TimeStart], [TimeEndBackup], [TimeEndZipEncrypt], [TimeEndUpload], " +
                        "[ElapsedTimeBackup], [ElapsedTimeUpload], [FileName], [FileSize], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [ProcessSuccessful], [Messages], [TimeStartUpload]) " +
                        "VALUES " +
                        "(@ID, @Type, @TimeStart, @TimeEndBackup, @TimeEndZipEncrypt, @TimeEndUpload, " +
                        "@ElapsedTimeBackup, @ElapsedTimeUpload, @FileName, @FileSize, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @ProcessSuccessful, @Messages, @TimeStartUpload); " +
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

        public int UpdateBackupLog(BackupLogDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                BackupLogPoco poco = new BackupLogPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE BackupLog " +
                        "SET [Type] = @Type, [TimeStart] = @TimeStart, [TimeEndBackup] = @TimeEndBackup, " +
                        "[TimeEndZipEncrypt] = @TimeEndZipEncrypt, [TimeEndUpload] = @TimeEndUpload, " +
                        "[ElapsedTimeBackup] = @ElapsedTimeBackup, [ElapsedTimeUpload] = @ElapsedTimeUpload, " +
                        "[FileName] = @FileName, [FileSize] = @FileSize, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [ProcessSuccessful] = @ProcessSuccessful, " +
                        "[Messages] = @Messages, [TimeStartUpload] = @TimeStartUpload " +
                        "WHERE ID = @ID;";

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
