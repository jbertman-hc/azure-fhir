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
    public class ListPmAccountBalancesRepository : IListPmAccountBalancesRepository
    {
        private readonly string _connectionString;

        public ListPmAccountBalancesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListPmAccountBalancesDomain GetListPmAccountBalances(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListPmAccountBalances WHERE PmAccountBalancesId = @id";

                    var ListPmAccountBalancesPoco = cn.QueryFirstOrDefault<ListPmAccountBalancesPoco>(query, new { id = id }) ?? new ListPmAccountBalancesPoco();

                    return ListPmAccountBalancesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListPmAccountBalancesDomain> GetListPmAccountBalances(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListPmAccountBalances WHERE @criteria";
                    List<ListPmAccountBalancesPoco> pocos = cn.Query<ListPmAccountBalancesPoco>(sql).ToList();
                    List<ListPmAccountBalancesDomain> domains = new List<ListPmAccountBalancesDomain>();

                    foreach (ListPmAccountBalancesPoco poco in pocos)
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

        public int DeleteListPmAccountBalances(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListPmAccountBalances WHERE PmAccountBalancesId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListPmAccountBalances(ListPmAccountBalancesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListPmAccountBalancesPoco poco = new ListPmAccountBalancesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPmAccountBalances] " +
                        "([PmAccountBalancesId], [PmAccountId], [InsuranceBalance], [PatientBalance], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@PmAccountBalancesId, @PmAccountId, @InsuranceBalance, @PatientBalance, " +
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

        public int UpdateListPmAccountBalances(ListPmAccountBalancesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListPmAccountBalancesPoco poco = new ListPmAccountBalancesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPmAccountBalances " +
                        "SET [PmAccountId] = @PmAccountId, [InsuranceBalance] = @InsuranceBalance, " +
                        "[PatientBalance] = @PatientBalance, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE PmAccountBalancesId = @PmAccountBalancesId;";

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
