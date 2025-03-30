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
    public class PHIXEventsComponentsRepository : IPHIXEventsComponentsRepository
    {
        private readonly string _connectionString;

        public PHIXEventsComponentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PHIXEventsComponentsDomain GetPHIXEventsComponents(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PHIXEventsComponents WHERE PHIXEventsComponentsId = @id";

                    var PHIXEventsComponentsPoco = cn.QueryFirstOrDefault<PHIXEventsComponentsPoco>(query, new { id = id }) ?? new PHIXEventsComponentsPoco();

                    return PHIXEventsComponentsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PHIXEventsComponentsDomain> GetPHIXEventsComponents(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PHIXEventsComponents WHERE @criteria";
                    List<PHIXEventsComponentsPoco> pocos = cn.Query<PHIXEventsComponentsPoco>(sql).ToList();
                    List<PHIXEventsComponentsDomain> domains = new List<PHIXEventsComponentsDomain>();

                    foreach (PHIXEventsComponentsPoco poco in pocos)
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

        public int DeletePHIXEventsComponents(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PHIXEventsComponents WHERE PHIXEventsComponentsId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPHIXEventsComponents(PHIXEventsComponentsDomain domain)
        {
            int insertedId = 0;

            try
            {
                PHIXEventsComponentsPoco poco = new PHIXEventsComponentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PHIXEventsComponents] " +
                        "([PHIXEventsComponentsId], [PHIXEventsId], [ViewPHIXId], [Status], [Attempts], " +
                        "[LastFailureTime], [LastFailureMessage], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES (@PHIXEventsComponentsId, @PHIXEventsId, @ViewPHIXId, @Status, @Attempts, " +
                        "@LastFailureTime, @LastFailureMessage, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdatePHIXEventsComponents(PHIXEventsComponentsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PHIXEventsComponentsPoco poco = new PHIXEventsComponentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PHIXEventsComponents SET [PHIXEventsId] = @PHIXEventsId, " +
                        "[ViewPHIXId] = @ViewPHIXId, [Status] = @Status, [Attempts] = @Attempts, " +
                        "[LastFailureTime] = @LastFailureTime, [LastFailureMessage] = @LastFailureMessage, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE PHIXEventsComponentsId = @PHIXEventsComponentsId;";

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
