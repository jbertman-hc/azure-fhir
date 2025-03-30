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
    public class BillingICDsRepository : IBillingICDsRepository
    {
        private readonly string _connectionString;

        public BillingICDsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BillingICDsDomain GetBillingICDs(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM BillingICDs WHERE BillingICDsID = @id";

                    var BillingICDsPoco = cn.QueryFirstOrDefault<BillingICDsPoco>(query, new { id = id }) ?? new BillingICDsPoco();

                    return BillingICDsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BillingICDsDomain> GetBillingICDs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM BillingICDs WHERE @criteria";
                    List<BillingICDsPoco> pocos = cn.Query<BillingICDsPoco>(sql).ToList();
                    List<BillingICDsDomain> domains = new List<BillingICDsDomain>();

                    foreach (BillingICDsPoco poco in pocos)
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

        public int DeleteBillingICDs(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM BillingICDs WHERE BillingICDsID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertBillingICDs(BillingICDsDomain domain)
        {
            int insertedId = 0;

            try
            {
                BillingICDsPoco poco = new BillingICDsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[BillingICDs] " +
                        "([BillingICDsID], [BillingCPTsID], [Code], [Comments], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [SortOrder]) " +
                        "VALUES " +
                        "(@BillingICDsID, @BillingCPTsID, @Code, @Comments, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @SortOrder); " +
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

        public int UpdateBillingICDs(BillingICDsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                BillingICDsPoco poco = new BillingICDsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE BillingICDs " +
                        "SET [BillingCPTsID] = @BillingCPTsID, [Code] = @Code, [Comments] = @Comments, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [SortOrder] = @SortOrder " +
                        "WHERE BillingICDsID = @BillingICDsID;";

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
