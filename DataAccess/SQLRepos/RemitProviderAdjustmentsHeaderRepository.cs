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
    public class RemitProviderAdjustmentsHeaderRepository : IRemitProviderAdjustmentsHeaderRepository
    {
        private readonly string _connectionString;

        public RemitProviderAdjustmentsHeaderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RemitProviderAdjustmentsHeaderDomain GetRemitProviderAdjustmentsHeader(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM RemitProviderAdjustmentsHeader WHERE id = @id";

                    var RemitProviderAdjustmentsHeaderPoco = cn.QueryFirstOrDefault<RemitProviderAdjustmentsHeaderPoco>(query, new { id = id }) ?? new RemitProviderAdjustmentsHeaderPoco();

                    return RemitProviderAdjustmentsHeaderPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RemitProviderAdjustmentsHeaderDomain> GetRemitProviderAdjustmentsHeaders(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM RemitProviderAdjustmentsHeader WHERE @criteria";
                    List<RemitProviderAdjustmentsHeaderPoco> pocos = cn.Query<RemitProviderAdjustmentsHeaderPoco>(sql).ToList();
                    List<RemitProviderAdjustmentsHeaderDomain> domains = new List<RemitProviderAdjustmentsHeaderDomain>();

                    foreach (RemitProviderAdjustmentsHeaderPoco poco in pocos)
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

        public int DeleteRemitProviderAdjustmentsHeader(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM RemitProviderAdjustmentsHeader WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertRemitProviderAdjustmentsHeader(RemitProviderAdjustmentsHeaderDomain domain)
        {
            int insertedId = 0;

            try
            {
                RemitProviderAdjustmentsHeaderPoco poco = new RemitProviderAdjustmentsHeaderPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[RemitProviderAdjustmentsHeader] " +
                        "([ID], [PayorPaymentID], [ProviderNumber], [LastDayFiscalYear], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @PayorPaymentID, @ProviderNumber, @LastDayFiscalYear, @DateLastTouched, " +
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

        public int UpdateRemitProviderAdjustmentsHeader(RemitProviderAdjustmentsHeaderDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                RemitProviderAdjustmentsHeaderPoco poco = new RemitProviderAdjustmentsHeaderPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE RemitProviderAdjustmentsHeader " +
                        "SET [PayorPaymentID] = @PayorPaymentID, [ProviderNumber] = @ProviderNumber, " +
                        "[LastDayFiscalYear] = @LastDayFiscalYear, [DateLastTouched] = @DateLastTouched, " +
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
