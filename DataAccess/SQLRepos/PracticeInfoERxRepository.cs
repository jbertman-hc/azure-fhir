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
    public class PracticeInfoERxRepository : IPracticeInfoERxRepository
    {
        private readonly string _connectionString;

        public PracticeInfoERxRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PracticeInfoERxDomain GetPracticeInfoERx(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PracticeInfoERx WHERE id = @id";

                    var PracticeInfoERxPoco = cn.QueryFirstOrDefault<PracticeInfoERxPoco>(query, new { id = id }) ?? new PracticeInfoERxPoco();

                    return PracticeInfoERxPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PracticeInfoERxDomain> GetPracticeInfoERxs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PracticeInfoERx WHERE @criteria";
                    List<PracticeInfoERxPoco> pocos = cn.Query<PracticeInfoERxPoco>(sql).ToList();
                    List<PracticeInfoERxDomain> domains = new List<PracticeInfoERxDomain>();

                    foreach (PracticeInfoERxPoco poco in pocos)
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

        public int DeletePracticeInfoERx(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PracticeInfoERx WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPracticeInfoERx(PracticeInfoERxDomain domain)
        {
            int insertedId = 0;

            try
            {
                PracticeInfoERxPoco poco = new PracticeInfoERxPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PracticeInfoERx] " +
                        "([ID], [PracticeName], [StreetAddress1], [StreetAddress2], [City], [State], [Zip], " +
                        "[UniquePracticeID], [DateLastTouched], [LastTouchedBy], [DateRowAdded], [NewCropSiteID]) " +
                        "VALUES (@ID, @PracticeName, @StreetAddress1, @StreetAddress2, @City, @State, @Zip, " +
                        "@UniquePracticeID, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @NewCropSiteID); " +
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

        public int UpdatePracticeInfoERx(PracticeInfoERxDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PracticeInfoERxPoco poco = new PracticeInfoERxPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PracticeInfoERx " +
                        "SET [PracticeName] = @PracticeName, [StreetAddress1] = @StreetAddress1, " +
                        "[StreetAddress2] = @StreetAddress2, [City] = @City, [State] = @State, [Zip] = @Zip, " +
                        "[UniquePracticeID] = @UniquePracticeID, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[NewCropSiteID] = @NewCropSiteID " +
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
