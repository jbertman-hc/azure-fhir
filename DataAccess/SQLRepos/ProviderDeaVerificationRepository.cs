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
    public class ProviderDeaVerificationRepository : IProviderDeaVerificationRepository
    {
        private readonly string _connectionString;

        public ProviderDeaVerificationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProviderDeaVerificationDomain GetProviderDeaVerification(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ProviderDeaVerification WHERE ProviderDeaVerificationId = @id";

                    var ProviderDeaVerificationPoco = cn.QueryFirstOrDefault<ProviderDeaVerificationPoco>(query, new { id = id }) ?? new ProviderDeaVerificationPoco();

                    return ProviderDeaVerificationPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ProviderDeaVerificationDomain> GetProviderDeaVerifications(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ProviderDeaVerification WHERE @criteria";
                    List<ProviderDeaVerificationPoco> pocos = cn.Query<ProviderDeaVerificationPoco>(sql).ToList();
                    List<ProviderDeaVerificationDomain> domains = new List<ProviderDeaVerificationDomain>();

                    foreach (ProviderDeaVerificationPoco poco in pocos)
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

        public int DeleteProviderDeaVerification(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ProviderDeaVerification WHERE ProviderDeaVerificationId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertProviderDeaVerification(ProviderDeaVerificationDomain domain)
        {
            int insertedId = 0;

            try
            {
                ProviderDeaVerificationPoco poco = new ProviderDeaVerificationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ProviderDeaVerification] " +
                        "([ProviderDeaVerificationId], [ProviderCode], [Status], [LastCheckedDate], [ExpirationDate], " +
                        "[UserNotifiedDate], [Message], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ProviderDeaVerificationId, @ProviderCode, @Status, @LastCheckedDate, @ExpirationDate, " +
                        "@UserNotifiedDate, @Message, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateProviderDeaVerification(ProviderDeaVerificationDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ProviderDeaVerificationPoco poco = new ProviderDeaVerificationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ProviderDeaVerification " +
                        "SET [ProviderCode] = @ProviderCode, [Status] = @Status, [LastCheckedDate] = @LastCheckedDate, " +
                        "[ExpirationDate] = @ExpirationDate, [UserNotifiedDate] = @UserNotifiedDate, " +
                        "[Message] = @Message, [DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE ProviderDeaVerificationId = @ProviderDeaVerificationId;";

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
