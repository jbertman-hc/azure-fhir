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
    public class PayorContactsRepository : IPayorContactsRepository
    {
        private readonly string _connectionString;

        public PayorContactsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PayorContactsDomain GetPayorContacts(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PayorContacts WHERE PayorContactID = @id";

                    var PayorContactsPoco = cn.QueryFirstOrDefault<PayorContactsPoco>(query, new { id = id }) ?? new PayorContactsPoco();

                    return PayorContactsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PayorContactsDomain> GetPayorContacts(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PayorContacts WHERE @criteria";
                    List<PayorContactsPoco> pocos = cn.Query<PayorContactsPoco>(sql).ToList();
                    List<PayorContactsDomain> domains = new List<PayorContactsDomain>();

                    foreach (PayorContactsPoco poco in pocos)
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

        public int DeletePayorContacts(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PayorContacts WHERE PayorContactID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPayorContacts(PayorContactsDomain domain)
        {
            int insertedId = 0;

            try
            {
                PayorContactsPoco poco = new PayorContactsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PayorContacts] " +
                        "([PayorContactID], [PayorsID], [FirstName], [LastName], [Address1], [Address2], [City], " +
                        "[State], [PostalCode], [PhoneNumber], [PhoneExt], [Fax], [Email], [Notes], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [JobTitle], [IsPrimaryContact]) " +
                        "VALUES " +
                        "(@PayorContactID, @PayorsID, @FirstName, @LastName, @Address1, @Address2, @City, @State, " +
                        "@PostalCode, @PhoneNumber, @PhoneExt, @Fax, @Email, @Notes, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @JobTitle, @IsPrimaryContact); " +
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

        public int UpdatePayorContacts(PayorContactsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PayorContactsPoco poco = new PayorContactsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PayorContacts " +
                        "SET [PayorsID] = @PayorsID, [FirstName] = @FirstName, [LastName] = @LastName, " +
                        "[Address1] = @Address1, [Address2] = @Address2, [City] = @City, [State] = @State, " +
                        "[PostalCode] = @PostalCode, [PhoneNumber] = @PhoneNumber, [PhoneExt] = @PhoneExt, " +
                        "[Fax] = @Fax, [Email] = @Email, [Notes] = @Notes, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [JobTitle] = @JobTitle, " +
                        "[IsPrimaryContact] = @IsPrimaryContact " +
                        "WHERE PayorContactID = @PayorContactID;";

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
