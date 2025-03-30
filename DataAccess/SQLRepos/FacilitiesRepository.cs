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
    public class FacilitiesRepository : IFacilitiesRepository
    {
        private readonly string _connectionString;

        public FacilitiesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public FacilitiesDomain GetFacilities(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Facilities WHERE FacilitiesID = @id";

                    var FacilitiesPoco = cn.QueryFirstOrDefault<FacilitiesPoco>(query, new { id = id }) ?? new FacilitiesPoco();

                    return FacilitiesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<FacilitiesDomain> GetFacilities(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Facilities WHERE @criteria";
                    List<FacilitiesPoco> pocos = cn.Query<FacilitiesPoco>(sql).ToList();
                    List<FacilitiesDomain> domains = new List<FacilitiesDomain>();

                    foreach (FacilitiesPoco poco in pocos)
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

        public int DeleteFacilities(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Facilities WHERE FacilitiesID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertFacilities(FacilitiesDomain domain)
        {
            int insertedId = 0;

            try
            {
                FacilitiesPoco poco = new FacilitiesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Facilities] " +
                        "([FacilitiesID], [Name], [Address1], [Address2], [City], [StateOrRegion], " +
                        "[StateOrRegionText], [PostalCode], [Country], [NPI], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [FacilityTypeCode], [PracticeFacilityCode]) " +
                        "VALUES " +
                        "(@FacilitiesID, @Name, @Address1, @Address2, @City, @StateOrRegion, " +
                        "@StateOrRegionText, @PostalCode, @Country, @NPI, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @FacilityTypeCode, @PracticeFacilityCode); " +
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

        public int UpdateFacilities(FacilitiesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                FacilitiesPoco poco = new FacilitiesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Facilities " +
                        "SET [Name] = @Name, [Address1] = @Address1, [Address2] = @Address2, " +
                        "[City] = @City, [StateOrRegion] = @StateOrRegion, " +
                        "[StateOrRegionText] = @StateOrRegionText, [PostalCode] = @PostalCode, " +
                        "[Country] = @Country, [NPI] = @NPI, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[FacilityTypeCode] = @FacilityTypeCode, [PracticeFacilityCode] = @PracticeFacilityCode " +
                        "WHERE FacilitiesID = @FacilitiesID;";

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
