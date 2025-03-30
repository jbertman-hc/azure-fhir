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
    public class PendingCcdaToGenerateRepository : IPendingCcdaToGenerateRepository
    {
        private readonly string _connectionString;

        public PendingCcdaToGenerateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PendingCcdaToGenerateDomain GetPendingCcdaToGenerate(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PendingCcdaToGenerate WHERE PendingCcdaId = @id";

                    var PendingCcdaToGeneratePoco = cn.QueryFirstOrDefault<PendingCcdaToGeneratePoco>(query, new { id = id }) ?? new PendingCcdaToGeneratePoco();

                    return PendingCcdaToGeneratePoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PendingCcdaToGenerateDomain> GetPendingCcdaToGenerates(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PendingCcdaToGenerate WHERE @criteria";
                    List<PendingCcdaToGeneratePoco> pocos = cn.Query<PendingCcdaToGeneratePoco>(sql).ToList();
                    List<PendingCcdaToGenerateDomain> domains = new List<PendingCcdaToGenerateDomain>();

                    foreach (PendingCcdaToGeneratePoco poco in pocos)
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

        public int DeletePendingCcdaToGenerate(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PendingCcdaToGenerate WHERE PendingCcdaId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPendingCcdaToGenerate(PendingCcdaToGenerateDomain domain)
        {
            int insertedId = 0;

            try
            {
                PendingCcdaToGeneratePoco poco = new PendingCcdaToGeneratePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PendingCcdaToGenerate] " +
                        "([PendingCcdaId], [EncounterId], [CcdaTypeId], [ComponentId], [PhixEventsId], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@PendingCcdaId, @EncounterId, @CcdaTypeId, @ComponentId, @PhixEventsId, @DateLastTouched, " +
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

        public int UpdatePendingCcdaToGenerate(PendingCcdaToGenerateDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PendingCcdaToGeneratePoco poco = new PendingCcdaToGeneratePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PendingCcdaToGenerate " +
                        "SET [EncounterId] = @EncounterId, [CcdaTypeId] = @CcdaTypeId, [ComponentId] = @ComponentId, " +
                        "[PhixEventsId] = @PhixEventsId, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE PendingCcdaId = @PendingCcdaId;";

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
