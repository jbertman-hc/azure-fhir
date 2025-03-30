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
    public class AppInfoRepository : IAppInfoRepository
    {
        private readonly string _connectionString;

        public AppInfoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AppInfoDomain GetAppInfo(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM AppInfo WHERE AppID = @id";

                    var AppInfoPoco = cn.QueryFirstOrDefault<AppInfoPoco>(query, new { id = id }) ?? new AppInfoPoco();

                    return AppInfoPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AppInfoDomain> GetAppInfos(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM AppInfo WHERE @criteria";
                    List<AppInfoPoco> pocos = cn.Query<AppInfoPoco>(sql).ToList();
                    List<AppInfoDomain> domains = new List<AppInfoDomain>();

                    foreach (AppInfoPoco poco in pocos)
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

        public int DeleteAppInfo(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM AppInfo WHERE AppId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertAppInfo(AppInfoDomain domain)
        {
            int insertedId = 0;

            try
            {
                AppInfoPoco poco = new AppInfoPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[AppInfo] " +
                        "([AppID], [AppName], [AppVersion], [AppInstallDateTime], [AppInstallUser], " +
                        "[AppComment], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@AppID, @AppName, @AppVersion, @AppInstallDateTime, @AppInstallUser, " +
                        "@AppComment, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateAppInfo(AppInfoDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                AppInfoPoco poco = new AppInfoPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE AppInfo " +
                        "SET [AppName] = @AppName, [AppVersion] = @AppVersion, " +
                        "[AppInstallDateTime] = @AppInstallDateTime, " +
                        "[AppInstallUser] = @AppInstallUser, [AppComment] = @AppComment, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE AppID = @AppID;";

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
