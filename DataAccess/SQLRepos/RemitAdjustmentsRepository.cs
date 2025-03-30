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
    public class RemitAdjustmentsRepository : IRemitAdjustmentsRepository
    {
        private readonly string _connectionString;

        public RemitAdjustmentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RemitAdjustmentsDomain GetRemitAdjustments(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM RemitAdjustments WHERE AdjustmentID = @id";

                    var RemitAdjustmentsPoco = cn.QueryFirstOrDefault<RemitAdjustmentsPoco>(query, new { id = id }) ?? new RemitAdjustmentsPoco();

                    return RemitAdjustmentsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RemitAdjustmentsDomain> GetRemitAdjustments(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM RemitAdjustments WHERE @criteria";
                    List<RemitAdjustmentsPoco> pocos = cn.Query<RemitAdjustmentsPoco>(sql).ToList();
                    List<RemitAdjustmentsDomain> domains = new List<RemitAdjustmentsDomain>();

                    foreach (RemitAdjustmentsPoco poco in pocos)
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

        public int DeleteRemitAdjustments(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM RemitAdjustments WHERE AdjustmentID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertRemitAdjustments(RemitAdjustmentsDomain domain)
        {
            int insertedId = 0;

            try
            {
                RemitAdjustmentsPoco poco = new RemitAdjustmentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[RemitAdjustments] " +
                        "([AdjustmentID], [RemitClaimsID], [RemitServiceLinesID], [AdjustmentAMT], [AdjustmentCode], " +
                        "[AdjustmentReason], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@AdjustmentID, @RemitClaimsID, @RemitServiceLinesID, @AdjustmentAMT, @AdjustmentCode, " +
                        "@AdjustmentReason, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateRemitAdjustments(RemitAdjustmentsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                RemitAdjustmentsPoco poco = new RemitAdjustmentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE RemitAdjustments " +
                        "SET [RemitClaimsID] = @RemitClaimsID, [RemitServiceLinesID] = @RemitServiceLinesID, " +
                        "[AdjustmentAMT] = @AdjustmentAMT, [AdjustmentCode] = @AdjustmentCode, " +
                        "[AdjustmentReason] = @AdjustmentReason, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE AdjustmentID = @AdjustmentID;";

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
