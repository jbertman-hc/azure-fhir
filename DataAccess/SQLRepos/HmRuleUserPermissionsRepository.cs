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
    public class HmRuleUserPermissionsRepository : IHmRuleUserPermissionsRepository
    {
        private readonly string _connectionString;

        public HmRuleUserPermissionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public HmRuleUserPermissionsDomain GetHmRuleUserPermissions(Guid id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM HmRuleUserPermissions WHERE HMRuleGUID = @id";

                    var HmRuleUserPermissionsPoco = cn.QueryFirstOrDefault<HmRuleUserPermissionsPoco>(query, new { id = id }) ?? new HmRuleUserPermissionsPoco();

                    return HmRuleUserPermissionsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<HmRuleUserPermissionsDomain> GetHmRuleUserPermissions(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM HmRuleUserPermissions WHERE @criteria";
                    List<HmRuleUserPermissionsPoco> pocos = cn.Query<HmRuleUserPermissionsPoco>(sql).ToList();
                    List<HmRuleUserPermissionsDomain> domains = new List<HmRuleUserPermissionsDomain>();

                    foreach (HmRuleUserPermissionsPoco poco in pocos)
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

        public int DeleteHmRuleUserPermissions(Guid id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM HmRuleUserPermissions WHERE HMRuleGUID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertHmRuleUserPermissions(HmRuleUserPermissionsDomain domain)
        {
            int insertedId = 0;

            try
            {
                HmRuleUserPermissionsPoco poco = new HmRuleUserPermissionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[HmRuleUserPermissions] " +
                        "([HmRuleUserPermissionId], [HmRuleGUID], [UserPermission], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@HmRuleUserPermissionId, @HmRuleGUID, @UserPermission, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateHmRuleUserPermissions(HmRuleUserPermissionsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                HmRuleUserPermissionsPoco poco = new HmRuleUserPermissionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE HmRuleUserPermissions " +
                        "SET [HmRuleGUID] = @HmRuleGUID, [UserPermission] = @UserPermission, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE HmRuleUserPermissionId = @HmRuleUserPermissionId;";

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
