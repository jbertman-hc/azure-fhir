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
    public class NewFeaturesViewedRepository : INewFeaturesViewedRepository
    {
        private readonly string _connectionString;

        public NewFeaturesViewedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public NewFeaturesViewedDomain GetNewFeaturesViewed(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM NewFeaturesViewed WHERE id = @id";

                    var NewFeaturesViewedPoco = cn.QueryFirstOrDefault<NewFeaturesViewedPoco>(query, new { id = id }) ?? new NewFeaturesViewedPoco();

                    return NewFeaturesViewedPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<NewFeaturesViewedDomain> GetNewFeaturesVieweds(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM NewFeaturesViewed WHERE @criteria";
                    List<NewFeaturesViewedPoco> pocos = cn.Query<NewFeaturesViewedPoco>(sql).ToList();
                    List<NewFeaturesViewedDomain> domains = new List<NewFeaturesViewedDomain>();

                    foreach (NewFeaturesViewedPoco poco in pocos)
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

        public int DeleteNewFeaturesViewed(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM NewFeaturesViewed WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertNewFeaturesViewed(NewFeaturesViewedDomain domain)
        {
            int insertedId = 0;

            try
            {
                NewFeaturesViewedPoco poco = new NewFeaturesViewedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[NewFeaturesViewed] " +
                        "([ID], [NewFeaturesDisplayId], [HasBeenViewed], [UserId], [LastTouchedBy], " +
                        "[DateLastTouched], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @NewFeaturesDisplayId, @HasBeenViewed, @UserId, @LastTouchedBy, @DateLastTouched, " +
                        "@DateRowAdded); " +
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

        public int UpdateNewFeaturesViewed(NewFeaturesViewedDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                NewFeaturesViewedPoco poco = new NewFeaturesViewedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE NewFeaturesViewed " +
                        "SET [NewFeaturesDisplayId] = @NewFeaturesDisplayId, [HasBeenViewed] = @HasBeenViewed, " +
                        "[UserId] = @UserId, [LastTouchedBy] = @LastTouchedBy, [DateLastTouched] = @DateLastTouched, " +
                        "[DateRowAdded] = @DateRowAdded " +
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
