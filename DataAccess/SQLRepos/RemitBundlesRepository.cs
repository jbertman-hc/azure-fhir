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
    public class RemitBundlesRepository : IRemitBundlesRepository
    {
        private readonly string _connectionString;

        public RemitBundlesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RemitBundlesDomain GetRemitBundles(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM RemitBundles WHERE BundleID = @id";

                    var RemitBundlesPoco = cn.QueryFirstOrDefault<RemitBundlesPoco>(query, new { id = id }) ?? new RemitBundlesPoco();

                    return RemitBundlesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RemitBundlesDomain> GetRemitBundles(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM RemitBundles WHERE @criteria";
                    List<RemitBundlesPoco> pocos = cn.Query<RemitBundlesPoco>(sql).ToList();
                    List<RemitBundlesDomain> domains = new List<RemitBundlesDomain>();

                    foreach (RemitBundlesPoco poco in pocos)
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

        public int DeleteRemitBundles(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM RemitBundles WHERE BundleID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertRemitBundles(RemitBundlesDomain domain)
        {
            int insertedId = 0;

            try
            {
                RemitBundlesPoco poco = new RemitBundlesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[RemitBundles] " +
                        "([BundleID], [RemitServiceLinesID], [BillingCPTsID], [WasFirst], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@BundleID, @RemitServiceLinesID, @BillingCPTsID, @WasFirst, @DateLastTouched, " +
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

        public int UpdateRemitBundles(RemitBundlesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                RemitBundlesPoco poco = new RemitBundlesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE RemitBundles " +
                        "SET [RemitServiceLinesID] = @RemitServiceLinesID, [BillingCPTsID] = @BillingCPTsID, " +
                        "[WasFirst] = @WasFirst, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE BundleID = @BundleID;";

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
