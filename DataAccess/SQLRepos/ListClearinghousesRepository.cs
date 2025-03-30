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
    public class ListClearinghousesRepository : IListClearinghousesRepository
    {
        private readonly string _connectionString;

        public ListClearinghousesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListClearinghousesDomain GetListClearinghouses(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListClearinghouses WHERE ListClearinghousesID = @id";

                    var ListClearinghousesPoco = cn.QueryFirstOrDefault<ListClearinghousesPoco>(query, new { id = id }) ?? new ListClearinghousesPoco();

                    return ListClearinghousesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListClearinghousesDomain> GetListClearinghouses(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListClearinghouses WHERE @criteria";
                    List<ListClearinghousesPoco> pocos = cn.Query<ListClearinghousesPoco>(sql).ToList();
                    List<ListClearinghousesDomain> domains = new List<ListClearinghousesDomain>();

                    foreach (ListClearinghousesPoco poco in pocos)
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

        public int DeleteListClearinghouses(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListClearinghouses WHERE ListClearinghousesID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListClearinghouses(ListClearinghousesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListClearinghousesPoco poco = new ListClearinghousesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListClearinghouses] " +
                        "([ListClearinghousesID], [CHID], [CustomerID], [BillingID], [SubmitterID], " +
                        "[SubmitterName], [Login], [SUID], [PWD], [ContactID], [ContactPhone], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [DateIncomingLastOpened], [Active]) " +
                        "VALUES " +
                        "(@ListClearinghousesID, @CHID, @CustomerID, @BillingID, @SubmitterID, @SubmitterName, " +
                        "@Login, @SUID, @PWD, @ContactID, @ContactPhone, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @DateIncomingLastOpened, @Active); " +
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

        public int UpdateListClearinghouses(ListClearinghousesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListClearinghousesPoco poco = new ListClearinghousesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListClearinghouses " +
                        "SET [CHID] = @CHID, [CustomerID] = @CustomerID, [BillingID] = @BillingID, " +
                        "[SubmitterID] = @SubmitterID, [SubmitterName] = @SubmitterName, [Login] = @Login, " +
                        "[SUID] = @SUID, [PWD] = @PWD, [ContactID] = @ContactID, [ContactPhone] = @ContactPhone, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [DateIncomingLastOpened] = @DateIncomingLastOpened, " +
                        "[Active] = @Active " +
                        "WHERE ListClearinghousesID = @ListClearinghousesID;";

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
