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
    public class OpenChartsRepository : IOpenChartsRepository
    {
        private readonly string _connectionString;

        public OpenChartsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public OpenChartsDomain GetOpenCharts(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM OpenCharts WHERE PatientID = @id";

                    var OpenChartsPoco = cn.QueryFirstOrDefault<OpenChartsPoco>(query, new { id = id }) ?? new OpenChartsPoco();

                    return OpenChartsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<OpenChartsDomain> GetOpenCharts(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM OpenCharts WHERE @criteria";
                    List<OpenChartsPoco> pocos = cn.Query<OpenChartsPoco>(sql).ToList();
                    List<OpenChartsDomain> domains = new List<OpenChartsDomain>();

                    foreach (OpenChartsPoco poco in pocos)
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

        public int DeleteOpenCharts(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM OpenCharts WHERE PatientID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertOpenCharts(OpenChartsDomain domain)
        {
            int insertedId = 0;

            try
            {
                OpenChartsPoco poco = new OpenChartsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[OpenCharts] " +
                        "([ProviderCode], [TimeOpen], [PatientID]) " +
                        "VALUES " +
                        "(@ProviderCode, @TimeOpen, @PatientID); " +
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

        public int UpdateOpenCharts(OpenChartsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                OpenChartsPoco poco = new OpenChartsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE OpenCharts " +
                        "SET [TimeOpen] = @TimeOpen, [PatientID] = @PatientID " +
                        "WHERE ProviderCode = @ProviderCode;";

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
