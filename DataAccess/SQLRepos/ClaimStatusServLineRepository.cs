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
    public class ClaimStatusServLineRepository : IClaimStatusServLineRepository
    {
        private readonly string _connectionString;

        public ClaimStatusServLineRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ClaimStatusServLineDomain GetClaimStatusServLine(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ClaimStatusServLine WHERE ClaimStatusServLineID = @id";

                    var ClaimStatusServLinePoco = cn.QueryFirstOrDefault<ClaimStatusServLinePoco>(query, new { id = id }) ?? new ClaimStatusServLinePoco();

                    return ClaimStatusServLinePoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ClaimStatusServLineDomain> GetClaimStatusServLines(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ClaimStatusServLine WHERE @criteria";
                    List<ClaimStatusServLinePoco> pocos = cn.Query<ClaimStatusServLinePoco>(sql).ToList();
                    List<ClaimStatusServLineDomain> domains = new List<ClaimStatusServLineDomain>();

                    foreach (ClaimStatusServLinePoco poco in pocos)
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

        public int DeleteClaimStatusServLine(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ClaimStatusServLine WHERE ClaimStatusServLineID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertClaimStatusServLine(ClaimStatusServLineDomain domain)
        {
            int insertedId = 0;

            try
            {
                ClaimStatusServLinePoco poco = new ClaimStatusServLinePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ClaimStatusServLine] " +
                        "([ClaimStatusServLineID], [ClaimStatusID], [CPTandMods], [ChargeAMT], " +
                        "[PaidAMT], [EffectiveDate], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ClaimStatusServLineID, @ClaimStatusID, @CPTandMods, @ChargeAMT, @PaidAMT, " +
                        "@EffectiveDate, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateClaimStatusServLine(ClaimStatusServLineDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ClaimStatusServLinePoco poco = new ClaimStatusServLinePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ClaimStatusServLine " +
                        "SET [ClaimStatusID] = @ClaimStatusID, [CPTandMods] = @CPTandMods, " +
                        "[ChargeAMT] = @ChargeAMT, [PaidAMT] = @PaidAMT, [EffectiveDate] = @EffectiveDate, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE ClaimStatusServLineID = @ClaimStatusServLineID;";

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
