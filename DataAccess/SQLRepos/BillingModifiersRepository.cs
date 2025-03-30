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
    public class BillingModifiersRepository : IBillingModifiersRepository
    {
        private readonly string _connectionString;

        public BillingModifiersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BillingModifiersDomain GetBillingModifiers(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM BillingModifiers WHERE BillingModifiersID = @id";

                    var BillingModifiersPoco = cn.QueryFirstOrDefault<BillingModifiersPoco>(query, new { id = id }) ?? new BillingModifiersPoco();

                    return BillingModifiersPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BillingModifiersDomain> GetBillingModifiers(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM BillingModifiers WHERE @criteria";
                    List<BillingModifiersPoco> pocos = cn.Query<BillingModifiersPoco>(sql).ToList();
                    List<BillingModifiersDomain> domains = new List<BillingModifiersDomain>();

                    foreach (BillingModifiersPoco poco in pocos)
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

        public int DeleteBillingModifiers(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM BillingModifiers WHERE BillingModifiersID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertBillingModifiers(BillingModifiersDomain domain)
        {
            int insertedId = 0;

            try
            {
                BillingModifiersPoco poco = new BillingModifiersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[BillingModifiers] " +
                        "([BillingModifiersID], [BillingCPTsID], [Code], [Comments], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [Sequence]) " +
                        "VALUES " +
                        "(@BillingModifiersID, @BillingCPTsID, @Code, @Comments, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @Sequence); " +
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

        public int UpdateBillingModifiers(BillingModifiersDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                BillingModifiersPoco poco = new BillingModifiersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE BillingModifiers " +
                        "SET [BillingCPTsID] = @BillingCPTsID, [Code] = @Code, [Comments] = @Comments, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [Sequence] = @Sequence " +
                        "WHERE BillingModifiersID = @BillingModifiersID;";

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
