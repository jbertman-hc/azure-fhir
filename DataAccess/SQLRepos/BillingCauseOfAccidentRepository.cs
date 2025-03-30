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
    public class BillingCauseOfAccidentRepository : IBillingCauseOfAccidentRepository
    {
        private readonly string _connectionString;

        public BillingCauseOfAccidentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BillingCauseOfAccidentDomain GetBillingCauseOfAccident(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM BillingCauseOfAccident WHERE BillingCauseOfAccidentID = @id";

                    var BillingCauseOfAccidentPoco = cn.QueryFirstOrDefault<BillingCauseOfAccidentPoco>(query, new { id = id }) ?? new BillingCauseOfAccidentPoco();

                    return BillingCauseOfAccidentPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BillingCauseOfAccidentDomain> GetBillingCauseOfAccidents(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM BillingCauseOfAccident WHERE @criteria";
                    List<BillingCauseOfAccidentPoco> pocos = cn.Query<BillingCauseOfAccidentPoco>(sql).ToList();
                    List<BillingCauseOfAccidentDomain> domains = new List<BillingCauseOfAccidentDomain>();

                    foreach (BillingCauseOfAccidentPoco poco in pocos)
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

        public int DeleteBillingCauseOfAccident(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM BillingCauseOfAccident WHERE BillingCauseOfAccidentID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertBillingCauseOfAccident(BillingCauseOfAccidentDomain domain)
        {
            int insertedId = 0;

            try
            {
                BillingCauseOfAccidentPoco poco = new BillingCauseOfAccidentPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[BillingCauseOfAccident] " +
                        "([BillingCauseOfAccidentID], [BillingOtherInformationID], [CauseOfAccidentID], [AccidentState], " +
                        "[AccidentCountry], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@BillingCauseOfAccidentID, @BillingOtherInformationID, @CauseOfAccidentID, @AccidentState, " +
                        "@AccidentCountry, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateBillingCauseOfAccident(BillingCauseOfAccidentDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                BillingCauseOfAccidentPoco poco = new BillingCauseOfAccidentPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE BillingCauseOfAccident " +
                        "SET [BillingOtherInformationID] = @BillingOtherInformationID, " +
                        "[CauseOfAccidentID] = @CauseOfAccidentID, [AccidentState] = @AccidentState, " +
                        "[AccidentCountry] = @AccidentCountry, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE BillingCauseOfAccidentID = @BillingCauseOfAccidentID;";

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
