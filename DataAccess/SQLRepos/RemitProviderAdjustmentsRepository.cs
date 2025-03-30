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
    public class RemitProviderAdjustmentsRepository : IRemitProviderAdjustmentsRepository
    {
        private readonly string _connectionString;

        public RemitProviderAdjustmentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RemitProviderAdjustmentsDomain GetRemitProviderAdjustments(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM RemitProviderAdjustments WHERE id = @id";

                    var RemitProviderAdjustmentsPoco = cn.QueryFirstOrDefault<RemitProviderAdjustmentsPoco>(query, new { id = id }) ?? new RemitProviderAdjustmentsPoco();

                    return RemitProviderAdjustmentsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RemitProviderAdjustmentsDomain> GetRemitProviderAdjustments(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM RemitProviderAdjustments WHERE @criteria";
                    List<RemitProviderAdjustmentsPoco> pocos = cn.Query<RemitProviderAdjustmentsPoco>(sql).ToList();
                    List<RemitProviderAdjustmentsDomain> domains = new List<RemitProviderAdjustmentsDomain>();

                    foreach (RemitProviderAdjustmentsPoco poco in pocos)
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

        public int DeleteRemitProviderAdjustments(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM RemitProviderAdjustments WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertRemitProviderAdjustments(RemitProviderAdjustmentsDomain domain)
        {
            int insertedId = 0;

            try
            {
                RemitProviderAdjustmentsPoco poco = new RemitProviderAdjustmentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[RemitProviderAdjustments] " +
                        "([ID], [RemitProviderAdjustementsHeaderID], [AdjustmentReasonCode], [AdjustmentRefID], " +
                        "[AdjustmentAMT], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @RemitProviderAdjustementsHeaderID, @AdjustmentReasonCode, @AdjustmentRefID, " +
                        "@AdjustmentAMT, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateRemitProviderAdjustments(RemitProviderAdjustmentsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                RemitProviderAdjustmentsPoco poco = new RemitProviderAdjustmentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE RemitProviderAdjustments " +
                        "SET [RemitProviderAdjustementsHeaderID] = @RemitProviderAdjustementsHeaderID, " +
                        "[AdjustmentReasonCode] = @AdjustmentReasonCode, [AdjustmentRefID] = @AdjustmentRefID, " +
                        "[AdjustmentAMT] = @AdjustmentAMT, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
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
