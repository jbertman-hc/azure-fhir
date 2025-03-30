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
    public class ProviderNpiVerificationRepository : IProviderNpiVerificationRepository
    {
        private readonly string _connectionString;

        public ProviderNpiVerificationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProviderNpiVerificationDomain GetProviderNpiVerification(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ProviderNpiVerification WHERE ProviderNpiVerificationId = @id";

                    var ProviderNpiVerificationPoco = cn.QueryFirstOrDefault<ProviderNpiVerificationPoco>(query, new { id = id }) ?? new ProviderNpiVerificationPoco();

                    return ProviderNpiVerificationPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ProviderNpiVerificationDomain> GetProviderNpiVerifications(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ProviderNpiVerification WHERE @criteria";
                    List<ProviderNpiVerificationPoco> pocos = cn.Query<ProviderNpiVerificationPoco>(sql).ToList();
                    List<ProviderNpiVerificationDomain> domains = new List<ProviderNpiVerificationDomain>();

                    foreach (ProviderNpiVerificationPoco poco in pocos)
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

        public int DeleteProviderNpiVerification(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ProviderNpiVerification WHERE ProviderNpiVerificationId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertProviderNpiVerification(ProviderNpiVerificationDomain domain)
        {
            int insertedId = 0;

            try
            {
                ProviderNpiVerificationPoco poco = new ProviderNpiVerificationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ProviderNpiVerification] " +
                        "([ProviderNpiVerificationId], [ProviderCode], [Status], [LastCheckedDate], [ExpirationDate], " +
                        "[UserNotifiedDate], [Message], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ProviderNpiVerificationId, @ProviderCode, @Status, @LastCheckedDate, @ExpirationDate, " +
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

        public int UpdateProviderNpiVerification(ProviderNpiVerificationDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ProviderNpiVerificationPoco poco = new ProviderNpiVerificationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ProviderNpiVerification " +
                        "SET [ProviderCode] = @ProviderCode, [Status] = @Status, [LastCheckedDate] = @LastCheckedDate, " +
                        "[ExpirationDate] = @ExpirationDate, [UserNotifiedDate] = @UserNotifiedDate, " +
                        "[Message] = @Message, [DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE ProviderNpiVerificationId = @ProviderNpiVerificationId;";

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
