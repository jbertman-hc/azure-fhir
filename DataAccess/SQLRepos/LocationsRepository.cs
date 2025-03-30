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
    public class LocationsRepository : ILocationsRepository
    {
        private readonly string _connectionString;

        public LocationsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LocationsDomain GetLocations(Guid id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Locations WHERE LocationsID = @id";

                    var LocationsPoco = cn.QueryFirstOrDefault<LocationsPoco>(query, new { id = id }) ?? new LocationsPoco();

                    return LocationsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LocationsDomain> GetLocations(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Locations WHERE @criteria";
                    List<LocationsPoco> pocos = cn.Query<LocationsPoco>(sql).ToList();
                    List<LocationsDomain> domains = new List<LocationsDomain>();

                    foreach (LocationsPoco poco in pocos)
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

        public int DeleteLocations(Guid id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Locations WHERE LocationsID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLocations(LocationsDomain domain)
        {
            int insertedId = 0;

            try
            {
                LocationsPoco poco = new LocationsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Locations] ([LocationsID], [Locations], [Address1], " +
                        "[Address2], [City], [StateOrRegion], [StateOrRegionText], [PostalCode], [Country], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [IsDefault]) " +
                        "VALUES " +
                        "(@LocationsID, @Locations, @Address1, @Address2, @City, @StateOrRegion, " +
                        "@StateOrRegionText, @PostalCode, @Country, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @IsDefault); " +
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

        public int UpdateLocations(LocationsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LocationsPoco poco = new LocationsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Locations " +
                        "SET [Locations] = @Locations, [Address1] = @Address1, [Address2] = @Address2, " +
                        "[City] = @City, [StateOrRegion] = @StateOrRegion, [StateOrRegionText] = @StateOrRegionText, " +
                        "[PostalCode] = @PostalCode, [Country] = @Country, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [IsDefault] = @IsDefault " +
                        "WHERE LocationsID = @LocationsID;";

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
