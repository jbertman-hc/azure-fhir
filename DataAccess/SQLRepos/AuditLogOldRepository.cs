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
    public class AuditLogOldRepository : IAuditLogOldRepository
    {
        private readonly string _connectionString;

        public AuditLogOldRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AuditLogOldDomain GetAuditLogOld(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM AuditLogOld WHERE AuditId = @id";

                    var AuditLogOldPoco = cn.QueryFirstOrDefault<AuditLogOldPoco>(query, new { id = id }) ?? new AuditLogOldPoco();

                    return AuditLogOldPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AuditLogOldDomain> GetAuditLogOlds(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM AuditLogOld WHERE @criteria";
                    List<AuditLogOldPoco> pocos = cn.Query<AuditLogOldPoco>(sql).ToList();
                    List<AuditLogOldDomain> domains = new List<AuditLogOldDomain>();

                    foreach (AuditLogOldPoco poco in pocos)
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

        public int DeleteAuditLogOld(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM AuditLogOld WHERE AuditId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertAuditLogOld(AuditLogOldDomain domain)
        {
            int insertedId = 0;

            try
            {
                AuditLogOldPoco poco = new AuditLogOldPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[AuditLogOld] " +
                        "([TableName], [RowID], [PatientID], [UserName], [Operation], [DateTime], " +
                        "[MiscInfo], [DateLastTouched], [LastTouchedBy], [DateRowAdded], [AuditID], " +
                        "[Location], [EventOutcome]) " +
                        "VALUES " +
                        "(@TableName, @RowID, @PatientID, @UserName, @Operation, @DateTime, @MiscInfo, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @AuditID, @Location, @EventOutcome); " +
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

        public int UpdateAuditLogOld(AuditLogOldDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                AuditLogOldPoco poco = new AuditLogOldPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE AuditLogOld " +
                        "SET [RowID] = @RowID, [PatientID] = @PatientID, [UserName] = @UserName, " +
                        "[Operation] = @Operation, [DateTime] = @DateTime, [MiscInfo] = @MiscInfo, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [AuditID] = @AuditID, " +
                        "[Location] = @Location, [EventOutcome] = @EventOutcome " +
                        "WHERE TableName = @TableName;";

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
