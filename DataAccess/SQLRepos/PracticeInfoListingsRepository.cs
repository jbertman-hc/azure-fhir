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
    public class PracticeInfoListingsRepository : IPracticeInfoListingsRepository
    {
        private readonly string _connectionString;

        public PracticeInfoListingsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PracticeInfoListingsDomain GetPracticeInfoListings(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PracticeInfoListings WHERE id = @id";

                    var PracticeInfoListingsPoco = cn.QueryFirstOrDefault<PracticeInfoListingsPoco>(query, new { id = id }) ?? new PracticeInfoListingsPoco();

                    return PracticeInfoListingsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PracticeInfoListingsDomain> GetPracticeInfoListings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PracticeInfoListings WHERE @criteria";
                    List<PracticeInfoListingsPoco> pocos = cn.Query<PracticeInfoListingsPoco>(sql).ToList();
                    List<PracticeInfoListingsDomain> domains = new List<PracticeInfoListingsDomain>();

                    foreach (PracticeInfoListingsPoco poco in pocos)
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

        public int DeletePracticeInfoListings(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PracticeInfoListings WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPracticeInfoListings(PracticeInfoListingsDomain domain)
        {
            int insertedId = 0;

            try
            {
                PracticeInfoListingsPoco poco = new PracticeInfoListingsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PracticeInfoListings] " +
                        "([ID], [DataHeadings], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @DataHeadings, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdatePracticeInfoListings(PracticeInfoListingsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PracticeInfoListingsPoco poco = new PracticeInfoListingsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PracticeInfoListings " +
                        "SET [DataHeadings] = @DataHeadings, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
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
