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
    public class LabDemographicsRepository : ILabDemographicsRepository
    {
        private readonly string _connectionString;

        public LabDemographicsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LabDemographicsDomain GetLabDemographics(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM LabDemographics WHERE LabDemographicsId = @id";

                    var LabDemographicsPoco = cn.QueryFirstOrDefault<LabDemographicsPoco>(query, new { id = id }) ?? new LabDemographicsPoco();

                    return LabDemographicsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LabDemographicsDomain> GetLabDemographics(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM LabDemographics WHERE @criteria";
                    List<LabDemographicsPoco> pocos = cn.Query<LabDemographicsPoco>(sql).ToList();
                    List<LabDemographicsDomain> domains = new List<LabDemographicsDomain>();

                    foreach (LabDemographicsPoco poco in pocos)
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

        public int DeleteLabDemographics(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM LabDemographics WHERE LabDemographicsId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLabDemographics(LabDemographicsDomain domain)
        {
            int insertedId = 0;

            try
            {
                LabDemographicsPoco poco = new LabDemographicsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[LabDemographics] " +
                        "([LabLocationCode], [LabCompany], [CreatedDt], [CreatedBy], [LastUpdDt], [LastUpdBy], " +
                        "[LabName], [LabAddress1], [LabAddress2], [LabCity], [LabState], [LabZip], [LabPhone], " +
                        "[LabDirectorTitle], [LabDirectorNameLast], [LabDirectorNameFirst], [LabDirectorNameMI], " +
                        "[LabDirectorDegree], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[FacilityUniversalId], [FacilityUniversalIdType], [LabDirectorSuffix], " +
                        "[LabDirectorPrefix], [LabDirectorId], [LabLocationIdTypeCode], [LabCountry], " +
                        "[LabCountyParish], [LabDemographicsId], [SpecimenNbr]) " +
                        "VALUES " +
                        "(@LabLocationCode, @LabCompany, @CreatedDt, @CreatedBy, @LastUpdDt, @LastUpdBy, " +
                        "@LabName, @LabAddress1, @LabAddress2, @LabCity, @LabState, @LabZip, @LabPhone, " +
                        "@LabDirectorTitle, @LabDirectorNameLast, @LabDirectorNameFirst, @LabDirectorNameMI, " +
                        "@LabDirectorDegree, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @FacilityUniversalId, " +
                        "@FacilityUniversalIdType, @LabDirectorSuffix, @LabDirectorPrefix, @LabDirectorId, " +
                        "@LabLocationIdTypeCode, @LabCountry, @LabCountyParish, @LabDemographicsId, @SpecimenNbr); " +
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

        public int UpdateLabDemographics(LabDemographicsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LabDemographicsPoco poco = new LabDemographicsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE LabDemographics " +
                        "SET [LabCompany] = @LabCompany, [CreatedDt] = @CreatedDt, [CreatedBy] = @CreatedBy, " +
                        "[LastUpdDt] = @LastUpdDt, [LastUpdBy] = @LastUpdBy, [LabName] = @LabName, " +
                        "[LabAddress1] = @LabAddress1, [LabAddress2] = @LabAddress2, [LabCity] = @LabCity, " +
                        "[LabState] = @LabState, [LabZip] = @LabZip, [LabPhone] = @LabPhone, " +
                        "[LabDirectorTitle] = @LabDirectorTitle, [LabDirectorNameLast] = @LabDirectorNameLast, " +
                        "[LabDirectorNameFirst] = @LabDirectorNameFirst, [LabDirectorNameMI] = @LabDirectorNameMI, " +
                        "[LabDirectorDegree] = @LabDirectorDegree, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[FacilityUniversalId] = @FacilityUniversalId, " +
                        "[FacilityUniversalIdType] = @FacilityUniversalIdType, " +
                        "[LabDirectorSuffix] = @LabDirectorSuffix, [LabDirectorPrefix] = @LabDirectorPrefix, " +
                        "[LabDirectorId] = @LabDirectorId, [LabLocationIdTypeCode] = @LabLocationIdTypeCode, " +
                        "[LabCountry] = @LabCountry, [LabCountyParish] = @LabCountyParish, " +
                        "[LabDemographicsId] = @LabDemographicsId, [SpecimenNbr] = @SpecimenNbr " +
                        "WHERE LabLocationCode = @LabLocationCode;";

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
