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
    public class CreditCardsAcceptedRepository : ICreditCardsAcceptedRepository
    {
        private readonly string _connectionString;

        public CreditCardsAcceptedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CreditCardsAcceptedDomain GetCreditCardsAccepted(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM CreditCardsAccepted WHERE CreditCardID = @id";

                    var CreditCardsAcceptedPoco = cn.QueryFirstOrDefault<CreditCardsAcceptedPoco>(query, new { id = id }) ?? new CreditCardsAcceptedPoco();

                    return CreditCardsAcceptedPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CreditCardsAcceptedDomain> GetCreditCardsAccepteds(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM CreditCardsAccepted WHERE @criteria";
                    List<CreditCardsAcceptedPoco> pocos = cn.Query<CreditCardsAcceptedPoco>(sql).ToList();
                    List<CreditCardsAcceptedDomain> domains = new List<CreditCardsAcceptedDomain>();

                    foreach (CreditCardsAcceptedPoco poco in pocos)
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

        public int DeleteCreditCardsAccepted(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM CreditCardsAccepted WHERE CreditCardID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertCreditCardsAccepted(CreditCardsAcceptedDomain domain)
        {
            int insertedId = 0;

            try
            {
                CreditCardsAcceptedPoco poco = new CreditCardsAcceptedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[CreditCardsAccepted] " +
                        "([CreditCardID], [StatementSettingsID], [CreditCardTypeID], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@CreditCardID, @StatementSettingsID, @CreditCardTypeID, @DateLastTouched, " +
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

        public int UpdateCreditCardsAccepted(CreditCardsAcceptedDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                CreditCardsAcceptedPoco poco = new CreditCardsAcceptedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE CreditCardsAccepted " +
                        "SET [StatementSettingsID] = @StatementSettingsID, [CreditCardTypeID] = @CreditCardTypeID, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE CreditCardID = @CreditCardID;";

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
