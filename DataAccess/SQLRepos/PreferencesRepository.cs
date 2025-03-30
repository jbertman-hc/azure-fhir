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
    public class PreferencesRepository : IPreferencesRepository
    {
        private readonly string _connectionString;

        public PreferencesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PreferencesDomain GetPreferences(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Preferences WHERE UniqueTableID = @id";

                    var PreferencesPoco = cn.QueryFirstOrDefault<PreferencesPoco>(query, new { id = id }) ?? new PreferencesPoco();

                    return PreferencesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PreferencesDomain> GetPreferences(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Preferences WHERE @criteria";
                    List<PreferencesPoco> pocos = cn.Query<PreferencesPoco>(sql).ToList();
                    List<PreferencesDomain> domains = new List<PreferencesDomain>();

                    foreach (PreferencesPoco poco in pocos)
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

        public int DeletePreferences(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Preferences WHERE UniqueTableID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPreferences(PreferencesDomain domain)
        {
            int insertedId = 0;

            try
            {
                PreferencesPoco poco = new PreferencesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Preferences] " +
                        "([UniqueTableID], [ProviderName], [PreferenceName], [PreferenceLocation], [PreferenceValue], " +
                        "[PracticeWide], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@UniqueTableID, @ProviderName, @PreferenceName, @PreferenceLocation, @PreferenceValue, " +
                        "@PracticeWide, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdatePreferences(PreferencesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PreferencesPoco poco = new PreferencesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Preferences " +
                        "SET [ProviderName] = @ProviderName, [PreferenceName] = @PreferenceName, " +
                        "[PreferenceLocation] = @PreferenceLocation, [PreferenceValue] = @PreferenceValue, " +
                        "[PracticeWide] = @PracticeWide, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE UniqueTableID = @UniqueTableID;";

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
