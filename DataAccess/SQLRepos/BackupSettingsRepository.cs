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
    public class BackupSettingsRepository : IBackupSettingsRepository
    {
        private readonly string _connectionString;

        public BackupSettingsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BackupSettingsDomain GetBackupSettings(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM BackupSettings WHERE RowID = @id";

                    var BackupSettingsPoco = cn.QueryFirstOrDefault<BackupSettingsPoco>(query, new { id = id }) ?? new BackupSettingsPoco();

                    return BackupSettingsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BackupSettingsDomain> GetBackupSettings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM BackupSettings WHERE @criteria";
                    List<BackupSettingsPoco> pocos = cn.Query<BackupSettingsPoco>(sql).ToList();
                    List<BackupSettingsDomain> domains = new List<BackupSettingsDomain>();

                    foreach (BackupSettingsPoco poco in pocos)
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

        public int DeleteBackupSettings(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM BackupSettings WHERE RowID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertBackupSettings(BackupSettingsDomain domain)
        {
            int insertedId = 0;

            try
            {
                BackupSettingsPoco poco = new BackupSettingsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[BackupSettings] " +
                        "([RowID], [BackupLocation1], [BackupLocation2], [BackupLocation3], [AutoBackupTime], " +
                        "[AutoBackupOn], [NoBackupImportedItems], [NoBackupImages], [BackupErrorCheck], " +
                        "[CheckZip], [CheckEncryption], [OSBUisOn], [SendConfirmationEmail], [ConfirmationProvider], " +
                        "[BackupLocation1UNC], [BackupLocation2UNC], [BackupLocation3UNC], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) VALUES (@RowID, @BackupLocation1, @BackupLocation2, " +
                        "@BackupLocation3, @AutoBackupTime, @AutoBackupOn, @NoBackupImportedItems, @NoBackupImages, " +
                        "@BackupErrorCheck, @CheckZip, @CheckEncryption, @OSBUisOn, @SendConfirmationEmail, " +
                        "@ConfirmationProvider, @BackupLocation1UNC, @BackupLocation2UNC, @BackupLocation3UNC, " +
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

        public int UpdateBackupSettings(BackupSettingsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                BackupSettingsPoco poco = new BackupSettingsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE BackupSettings " +
                        "SET [BackupLocation1] = @BackupLocation1, [BackupLocation2] = @BackupLocation2, " +
                        "[BackupLocation3] = @BackupLocation3, [AutoBackupTime] = @AutoBackupTime, " +
                        "[AutoBackupOn] = @AutoBackupOn, [NoBackupImportedItems] = @NoBackupImportedItems, " +
                        "[NoBackupImages] = @NoBackupImages, [BackupErrorCheck] = @BackupErrorCheck, " +
                        "[CheckZip] = @CheckZip, [CheckEncryption] = @CheckEncryption, " +
                        "[OSBUisOn] = @OSBUisOn, [SendConfirmationEmail] = @SendConfirmationEmail, " +
                        "[ConfirmationProvider] = @ConfirmationProvider, " +
                        "[BackupLocation1UNC] = @BackupLocation1UNC, " +
                        "[BackupLocation2UNC] = @BackupLocation2UNC, " +
                        "[BackupLocation3UNC] = @BackupLocation3UNC, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE RowID = @RowID;";

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
