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
    public class RemitAmountsRepository : IRemitAmountsRepository
    {
        private readonly string _connectionString;

        public RemitAmountsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RemitAmountsDomain GetRemitAmounts(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM RemitAmounts WHERE id = @id";

                    var RemitAmountsPoco = cn.QueryFirstOrDefault<RemitAmountsPoco>(query, new { id = id }) ?? new RemitAmountsPoco();

                    return RemitAmountsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RemitAmountsDomain> GetRemitAmounts(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM RemitAmounts WHERE @criteria";
                    List<RemitAmountsPoco> pocos = cn.Query<RemitAmountsPoco>(sql).ToList();
                    List<RemitAmountsDomain> domains = new List<RemitAmountsDomain>();

                    foreach (RemitAmountsPoco poco in pocos)
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

        public int DeleteRemitAmounts(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM RemitAmounts WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertRemitAmounts(RemitAmountsDomain domain)
        {
            int insertedId = 0;

            try
            {
                RemitAmountsPoco poco = new RemitAmountsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[RemitAmounts] " +
                        "([ID], [OwnerID], [Code], [Amount], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES (@ID, @OwnerID, @Code, @Amount, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateRemitAmounts(RemitAmountsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                RemitAmountsPoco poco = new RemitAmountsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE RemitAmounts " +
                        "SET [OwnerID] = @OwnerID, [Code] = @Code, [Amount] = @Amount, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE ID = @ID;";

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
