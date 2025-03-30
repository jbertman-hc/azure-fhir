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
    public class ChartAuditRepository : IChartAuditRepository
    {
        private readonly string _connectionString;

        public ChartAuditRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ChartAuditDomain GetChartAudit(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ChartAudit WHERE RowID = @id";

                    var ChartAuditPoco = cn.QueryFirstOrDefault<ChartAuditPoco>(query, new { id = id }) ?? new ChartAuditPoco();

                    return ChartAuditPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ChartAuditDomain> GetChartAudits(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ChartAudit WHERE @criteria";
                    List<ChartAuditPoco> pocos = cn.Query<ChartAuditPoco>(sql).ToList();
                    List<ChartAuditDomain> domains = new List<ChartAuditDomain>();

                    foreach (ChartAuditPoco poco in pocos)
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

        public int DeleteChartAudit(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ChartAudit WHERE RowID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertChartAudit(ChartAuditDomain domain)
        {
            int insertedId = 0;

            try
            {
                ChartAuditPoco poco = new ChartAuditPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ChartAudit] " +
                        "([RowID], [SoapID], [PatientID], [ProviderCode], [Action], [EventDateTime], " +
                        "[DestinationProvider], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@RowID, @SoapID, @PatientID, @ProviderCode, @Action, @EventDateTime, " +
                        "@DestinationProvider, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateChartAudit(ChartAuditDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ChartAuditPoco poco = new ChartAuditPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ChartAudit " +
                        "SET [SoapID] = @SoapID, [PatientID] = @PatientID, [ProviderCode] = @ProviderCode, " +
                        "[Action] = @Action, [EventDateTime] = @EventDateTime, " +
                        "[DestinationProvider] = @DestinationProvider, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE RowID = @RowID;";

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
