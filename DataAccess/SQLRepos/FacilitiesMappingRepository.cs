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
    public class FacilitiesMappingRepository : IFacilitiesMappingRepository
    {
        private readonly string _connectionString;

        public FacilitiesMappingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public FacilitiesMappingDomain GetFacilitiesMapping(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM FacilitiesMapping WHERE UniqueTableId = @id";

                    var FacilitiesMappingPoco = cn.QueryFirstOrDefault<FacilitiesMappingPoco>(query, new { id = id }) ?? new FacilitiesMappingPoco();

                    return FacilitiesMappingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<FacilitiesMappingDomain> GetFacilitiesMappings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM FacilitiesMapping WHERE @criteria";
                    List<FacilitiesMappingPoco> pocos = cn.Query<FacilitiesMappingPoco>(sql).ToList();
                    List<FacilitiesMappingDomain> domains = new List<FacilitiesMappingDomain>();

                    foreach (FacilitiesMappingPoco poco in pocos)
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

        public int DeleteFacilitiesMapping(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM FacilitiesMapping WHERE UniqueTableId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertFacilitiesMapping(FacilitiesMappingDomain domain)
        {
            int insertedId = 0;

            try
            {
                FacilitiesMappingPoco poco = new FacilitiesMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[FacilitiesMapping] " +
                        "([UniqueTableId], [FacilitiesID], [ExternalId], [SourceId], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@UniqueTableId, @FacilitiesID, @ExternalId, @SourceId, @DateLastTouched, " +
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

        public int UpdateFacilitiesMapping(FacilitiesMappingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                FacilitiesMappingPoco poco = new FacilitiesMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE FacilitiesMapping " +
                        "SET [FacilitiesID] = @FacilitiesID, [ExternalId] = @ExternalId, " +
                        "[SourceId] = @SourceId, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE UniqueTableId = @UniqueTableId;";

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
