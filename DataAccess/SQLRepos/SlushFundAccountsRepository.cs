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
    public class SlushFundAccountsRepository : ISlushFundAccountsRepository
    {
        private readonly string _connectionString;

        public SlushFundAccountsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SlushFundAccountsDomain GetSlushFundAccounts(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SlushFundAccounts WHERE SlushAccountID = @id";

                    var SlushFundAccountsPoco = cn.QueryFirstOrDefault<SlushFundAccountsPoco>(query, new { id = id }) ?? new SlushFundAccountsPoco();

                    return SlushFundAccountsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SlushFundAccountsDomain> GetSlushFundAccounts(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SlushFundAccounts WHERE @criteria";
                    List<SlushFundAccountsPoco> pocos = cn.Query<SlushFundAccountsPoco>(sql).ToList();
                    List<SlushFundAccountsDomain> domains = new List<SlushFundAccountsDomain>();

                    foreach (SlushFundAccountsPoco poco in pocos)
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

        public int DeleteSlushFundAccounts(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SlushFundAccounts WHERE SlushAccountID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSlushFundAccounts(SlushFundAccountsDomain domain)
        {
            int insertedId = 0;

            try
            {
                SlushFundAccountsPoco poco = new SlushFundAccountsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SlushFundAccounts] " +
                        "([SlushAccountID], [OldSlushAccountID], [SlushAccountGuid], [ProviderCode], [PayorID], " +
                        "[OldPayorID], [DateRowAdded], [DateLastTouched], [LastTouchedBy]) " +
                        "VALUES " +
                        "(@SlushAccountID, @OldSlushAccountID, @SlushAccountGuid, @ProviderCode, @PayorID, " +
                        "@OldPayorID, @DateRowAdded, @DateLastTouched, @LastTouchedBy); " +
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

        public int UpdateSlushFundAccounts(SlushFundAccountsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SlushFundAccountsPoco poco = new SlushFundAccountsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SlushFundAccounts " +
                        "SET [OldSlushAccountID] = @OldSlushAccountID, [SlushAccountGuid] = @SlushAccountGuid, " +
                        "[ProviderCode] = @ProviderCode, [PayorID] = @PayorID, [OldPayorID] = @OldPayorID, " +
                        "[DateRowAdded] = @DateRowAdded, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy " +
                        "WHERE SlushAccountID = @SlushAccountID;";

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
