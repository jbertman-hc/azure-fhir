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
    public class PracticeInfoERxTempRepository : IPracticeInfoERxTempRepository
    {
        private readonly string _connectionString;

        public PracticeInfoERxTempRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PracticeInfoERxTempDomain GetPracticeInfoERxTemp(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PracticeInfoERxTemp WHERE id = @id";

                    var PracticeInfoERxTempPoco = cn.QueryFirstOrDefault<PracticeInfoERxTempPoco>(query, new { id = id }) ?? new PracticeInfoERxTempPoco();

                    return PracticeInfoERxTempPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PracticeInfoERxTempDomain> GetPracticeInfoERxTemps(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PracticeInfoERxTemp WHERE @criteria";
                    List<PracticeInfoERxTempPoco> pocos = cn.Query<PracticeInfoERxTempPoco>(sql).ToList();
                    List<PracticeInfoERxTempDomain> domains = new List<PracticeInfoERxTempDomain>();

                    foreach (PracticeInfoERxTempPoco poco in pocos)
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

        public int DeletePracticeInfoERxTemp(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PracticeInfoERxTemp WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPracticeInfoERxTemp(PracticeInfoERxTempDomain domain)
        {
            int insertedId = 0;

            try
            {
                PracticeInfoERxTempPoco poco = new PracticeInfoERxTempPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PracticeInfoERxTemp] " +
                        "([ID], [PracticeName], [StreetAddress1], [StreetAddress2], [City], [State], [Zip], " +
                        "[NewCropSiteID], [DateLastTouched], [LastTouchedBy], [DateRowAdded], [UniquePracticeID]) " +
                        "VALUES " +
                        "(@ID, @PracticeName, @StreetAddress1, @StreetAddress2, @City, @State, @Zip, @NewCropSiteID, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @UniquePracticeID); " +
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

        public int UpdatePracticeInfoERxTemp(PracticeInfoERxTempDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PracticeInfoERxTempPoco poco = new PracticeInfoERxTempPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PracticeInfoERxTemp " +
                        "SET [PracticeName] = @PracticeName, [StreetAddress1] = @StreetAddress1, " +
                        "[StreetAddress2] = @StreetAddress2, [City] = @City, [State] = @State, [Zip] = @Zip, " +
                        "[NewCropSiteID] = @NewCropSiteID, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[UniquePracticeID] = @UniquePracticeID " +
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
