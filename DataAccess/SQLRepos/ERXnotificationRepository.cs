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
    public class ERXnotificationRepository : IERXnotificationRepository
    {
        private readonly string _connectionString;

        public ERXnotificationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ERXnotificationDomain GetERXnotification(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ERXnotification WHERE NotificationID = @id";

                    var ERXnotificationPoco = cn.QueryFirstOrDefault<ERXnotificationPoco>(query, new { id = id }) ?? new ERXnotificationPoco();

                    return ERXnotificationPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ERXnotificationDomain> GetERXnotifications(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ERXnotification WHERE @criteria";
                    List<ERXnotificationPoco> pocos = cn.Query<ERXnotificationPoco>(sql).ToList();
                    List<ERXnotificationDomain> domains = new List<ERXnotificationDomain>();

                    foreach (ERXnotificationPoco poco in pocos)
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

        public int DeleteERXnotification(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ERXnotification WHERE NotificationID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertERXnotification(ERXnotificationDomain domain)
        {
            int insertedId = 0;

            try
            {
                ERXnotificationPoco poco = new ERXnotificationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ERXnotification] " +
                        "([NotificationID], [IncomingProviderCode], [DestinationProviderCode], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@NotificationID, @IncomingProviderCode, @DestinationProviderCode, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateERXnotification(ERXnotificationDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ERXnotificationPoco poco = new ERXnotificationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ERXnotification " +
                        "SET [IncomingProviderCode] = @IncomingProviderCode, " +
                        "[DestinationProviderCode] = @DestinationProviderCode, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE NotificationID = @NotificationID;";

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
