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
    public class SystemSettingsRepository : ISystemSettingsRepository
    {
        private readonly string _connectionString;

        public SystemSettingsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SystemSettingsDomain GetSystemSettings(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SystemSettings WHERE SSID = @id";

                    var SystemSettingsPoco = cn.QueryFirstOrDefault<SystemSettingsPoco>(query, new { id = id }) ?? new SystemSettingsPoco();

                    return SystemSettingsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SystemSettingsDomain> GetSystemSettings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SystemSettings WHERE @criteria";
                    List<SystemSettingsPoco> pocos = cn.Query<SystemSettingsPoco>(sql).ToList();
                    List<SystemSettingsDomain> domains = new List<SystemSettingsDomain>();

                    foreach (SystemSettingsPoco poco in pocos)
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

        public int DeleteSystemSettings(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SystemSettings WHERE SSID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSystemSettings(SystemSettingsDomain domain)
        {
            int insertedId = 0;

            try
            {
                SystemSettingsPoco poco = new SystemSettingsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SystemSettings] " +
                        "([SSID], [SettingName], [SettingValue], [SettingCategory], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@SSID, @SettingName, @SettingValue, @SettingCategory, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded); " +
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

        public int UpdateSystemSettings(SystemSettingsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SystemSettingsPoco poco = new SystemSettingsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SystemSettings " +
                        "SET [SettingName] = @SettingName, [SettingValue] = @SettingValue, " +
                        "[SettingCategory] = @SettingCategory, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE SSID = @SSID;";

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
