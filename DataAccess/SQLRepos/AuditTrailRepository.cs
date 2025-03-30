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
    public class AuditTrailRepository : IAuditTrailRepository
    {
        private readonly string _connectionString;

        public AuditTrailRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AuditTrailDomain GetAuditTrail(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM AuditTrail WHERE AuditId = @id";

                    var AuditTrailPoco = cn.QueryFirstOrDefault<AuditTrailPoco>(query, new { id = id }) ?? new AuditTrailPoco();

                    return AuditTrailPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AuditTrailDomain> GetAuditTrails(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM AuditTrail WHERE @criteria";
                    List<AuditTrailPoco> pocos = cn.Query<AuditTrailPoco>(sql).ToList();
                    List<AuditTrailDomain> domains = new List<AuditTrailDomain>();

                    foreach (AuditTrailPoco poco in pocos)
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

        public int DeleteAuditTrail(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM AuditTrail WHERE AuditId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertAuditTrail(AuditTrailDomain domain)
        {
            int insertedId = 0;

            try
            {
                AuditTrailPoco poco = new AuditTrailPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[AuditTrail] " +
                        "([AuditID], [AuditDate], [ProviderID], [AuditTable], [ActionTaken], " +
                        "[PatientID], [RecNo], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@AuditID, @AuditDate, @ProviderID, @AuditTable, @ActionTaken, @PatientID, " +
                        "@RecNo, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateAuditTrail(AuditTrailDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                AuditTrailPoco poco = new AuditTrailPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE AuditTrail " +
                        "SET [AuditDate] = @AuditDate, [ProviderID] = @ProviderID, " +
                        "[AuditTable] = @AuditTable, [ActionTaken] = @ActionTaken, " +
                        "[PatientID] = @PatientID, [RecNo] = @RecNo, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE AuditID = @AuditID;";

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
