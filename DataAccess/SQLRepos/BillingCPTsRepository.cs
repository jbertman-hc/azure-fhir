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
    public class BillingCPTsRepository : IBillingCPTsRepository
    {
        private readonly string _connectionString;

        public BillingCPTsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BillingCPTsDomain GetBillingCPTs(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM BillingCPTs WHERE BillingCPTsID = @id";

                    var BillingCPTsPoco = cn.QueryFirstOrDefault<BillingCPTsPoco>(query, new { id = id }) ?? new BillingCPTsPoco();

                    return BillingCPTsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BillingCPTsDomain> GetBillingCPTs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM BillingCPTs WHERE @criteria";
                    List<BillingCPTsPoco> pocos = cn.Query<BillingCPTsPoco>(sql).ToList();
                    List<BillingCPTsDomain> domains = new List<BillingCPTsDomain>();

                    foreach (BillingCPTsPoco poco in pocos)
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

        public int DeleteBillingCPTs(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM BillingCPTs WHERE BillingCPTsID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertBillingCPTs(BillingCPTsDomain domain)
        {
            int insertedId = 0;

            try
            {
                BillingCPTsPoco poco = new BillingCPTsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[BillingCPTs] " +
                        "([BillingCPTsID], [BillingID], [CPTCode], [Units], [Price], [Comments], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [DatePerformed], " +
                        "[Resolved], [Sequence], [AppealFlag], [NDCCode], [NDCUnits]) " +
                        "VALUES " +
                        "(@BillingCPTsID, @BillingID, @CPTCode, @Units, @Price, @Comments, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @DatePerformed, " +
                        "@Resolved, @Sequence, @AppealFlag, @NDCCode, @NDCUnits); " +
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

        public int UpdateBillingCPTs(BillingCPTsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                BillingCPTsPoco poco = new BillingCPTsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE BillingCPTs " +
                        "SET [BillingID] = @BillingID, [CPTCode] = @CPTCode, [Units] = @Units, " +
                        "[Price] = @Price, [Comments] = @Comments, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[DatePerformed] = @DatePerformed, [Resolved] = @Resolved, " +
                        "[Sequence] = @Sequence, [AppealFlag] = @AppealFlag, [NDCCode] = @NDCCode, " +
                        "[NDCUnits] = @NDCUnits " +
                        "WHERE BillingCPTsID = @BillingCPTsID;";

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
