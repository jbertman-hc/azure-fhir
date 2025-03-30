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
    public class ClearinghouseLogRepository : IClearinghouseLogRepository
    {
        private readonly string _connectionString;

        public ClearinghouseLogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ClearinghouseLogDomain GetClearinghouseLog(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ClearinghouseLog WHERE id = @id";

                    var ClearinghouseLogPoco = cn.QueryFirstOrDefault<ClearinghouseLogPoco>(query, new { id = id }) ?? new ClearinghouseLogPoco();

                    return ClearinghouseLogPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ClearinghouseLogDomain> GetClearinghouseLogs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ClearinghouseLog WHERE @criteria";
                    List<ClearinghouseLogPoco> pocos = cn.Query<ClearinghouseLogPoco>(sql).ToList();
                    List<ClearinghouseLogDomain> domains = new List<ClearinghouseLogDomain>();

                    foreach (ClearinghouseLogPoco poco in pocos)
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

        public int DeleteClearinghouseLog(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ClearinghouseLog WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertClearinghouseLog(ClearinghouseLogDomain domain)
        {
            int insertedId = 0;

            try
            {
                ClearinghouseLogPoco poco = new ClearinghouseLogPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ClearinghouseLog] " +
                        "([ID], [LogTime], [TracePoint], [Clearinghouse], [Success], [StackTrace], " +
                        "[Comments], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @LogTime, @TracePoint, @Clearinghouse, @Success, @StackTrace, " +
                        "@Comments, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateClearinghouseLog(ClearinghouseLogDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ClearinghouseLogPoco poco = new ClearinghouseLogPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ClearinghouseLog " +
                        "SET [LogTime] = @LogTime, [TracePoint] = @TracePoint, " +
                        "[Clearinghouse] = @Clearinghouse, [Success] = @Success, " +
                        "[StackTrace] = @StackTrace, [Comments] = @Comments, " +
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
