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
    public class LocationsMappingRepository : ILocationsMappingRepository
    {
        private readonly string _connectionString;

        public LocationsMappingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LocationsMappingDomain GetLocationsMapping(Guid id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM LocationsMapping WHERE LocationsID = @id";

                    var LocationsMappingPoco = cn.QueryFirstOrDefault<LocationsMappingPoco>(query, new { id = id }) ?? new LocationsMappingPoco();

                    return LocationsMappingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LocationsMappingDomain> GetLocationsMappings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM LocationsMapping WHERE @criteria";
                    List<LocationsMappingPoco> pocos = cn.Query<LocationsMappingPoco>(sql).ToList();
                    List<LocationsMappingDomain> domains = new List<LocationsMappingDomain>();

                    foreach (LocationsMappingPoco poco in pocos)
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

        public int DeleteLocationsMapping(Guid id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM LocationsMapping WHERE LocationsID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLocationsMapping(LocationsMappingDomain domain)
        {
            int insertedId = 0;

            try
            {
                LocationsMappingPoco poco = new LocationsMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[LocationsMapping] ([UniqueTableId], [LocationsID], " +
                        "[ExternalId], [SourceId], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES (@UniqueTableId, @LocationsID, @ExternalId, @SourceId, @DateLastTouched, " +
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

        public int UpdateLocationsMapping(LocationsMappingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LocationsMappingPoco poco = new LocationsMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE LocationsMapping " +
                        "SET [LocationsID] = @LocationsID, [ExternalId] = @ExternalId, [SourceId] = @SourceId, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
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
