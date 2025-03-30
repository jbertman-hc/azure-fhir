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
    public class RemitServiceLinesRepository : IRemitServiceLinesRepository
    {
        private readonly string _connectionString;

        public RemitServiceLinesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RemitServiceLinesDomain GetRemitServiceLines(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM RemitServiceLines WHERE RemitServiceLinesID = @id";

                    var RemitServiceLinesPoco = cn.QueryFirstOrDefault<RemitServiceLinesPoco>(query, new { id = id }) ?? new RemitServiceLinesPoco();

                    return RemitServiceLinesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RemitServiceLinesDomain> GetRemitServiceLines(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM RemitServiceLines WHERE @criteria";
                    List<RemitServiceLinesPoco> pocos = cn.Query<RemitServiceLinesPoco>(sql).ToList();
                    List<RemitServiceLinesDomain> domains = new List<RemitServiceLinesDomain>();

                    foreach (RemitServiceLinesPoco poco in pocos)
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

        public int DeleteRemitServiceLines(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM RemitServiceLines WHERE RemitServiceLinesID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertRemitServiceLines(RemitServiceLinesDomain domain)
        {
            int insertedId = 0;

            try
            {
                RemitServiceLinesPoco poco = new RemitServiceLinesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[RemitServiceLines] " +
                        "([RemitServiceLinesID], [RemitClaimsID], [CPTCode], [Units], [Charge], [Payment], " +
                        "[DeniedAmount], [DeniedCodes], [AllowedAmt], [RemarkCode], [BillingCPTsID], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [Bundled], [PatientChargesID]) " +
                        "VALUES " +
                        "(@RemitServiceLinesID, @RemitClaimsID, @CPTCode, @Units, @Charge, @Payment, " +
                        "@DeniedAmount, @DeniedCodes, @AllowedAmt, @RemarkCode, @BillingCPTsID, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @Bundled, @PatientChargesID); " +
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

        public int UpdateRemitServiceLines(RemitServiceLinesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                RemitServiceLinesPoco poco = new RemitServiceLinesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE RemitServiceLines " +
                        "SET [RemitClaimsID] = @RemitClaimsID, [CPTCode] = @CPTCode, [Units] = @Units, " +
                        "[Charge] = @Charge, [Payment] = @Payment, [DeniedAmount] = @DeniedAmount, " +
                        "[DeniedCodes] = @DeniedCodes, [AllowedAmt] = @AllowedAmt, [RemarkCode] = @RemarkCode, " +
                        "[BillingCPTsID] = @BillingCPTsID, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [Bundled] = @Bundled, " +
                        "[PatientChargesID] = @PatientChargesID " +
                        "WHERE RemitServiceLinesID = @RemitServiceLinesID;";

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
