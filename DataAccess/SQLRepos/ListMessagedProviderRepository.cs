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
    public class ListMessagedProviderRepository : IListMessagedProviderRepository
    {
        private readonly string _connectionString;

        public ListMessagedProviderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListMessagedProviderDomain GetListMessagedProvider(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListMessagedProvider WHERE ListMessagedProviderId = @id";

                    var ListMessagedProviderPoco = cn.QueryFirstOrDefault<ListMessagedProviderPoco>(query, new { id = id }) ?? new ListMessagedProviderPoco();

                    return ListMessagedProviderPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListMessagedProviderDomain> GetListMessagedProviders(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListMessagedProvider WHERE @criteria";
                    List<ListMessagedProviderPoco> pocos = cn.Query<ListMessagedProviderPoco>(sql).ToList();
                    List<ListMessagedProviderDomain> domains = new List<ListMessagedProviderDomain>();

                    foreach (ListMessagedProviderPoco poco in pocos)
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

        public int DeleteListMessagedProvider(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListMessagedProvider WHERE ListMessagedProviderId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListMessagedProvider(ListMessagedProviderDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListMessagedProviderPoco poco = new ListMessagedProviderPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListMessagedProvider] " +
                        "([PatientId], [ListMessagedProviderId], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@PatientId, @ListMessagedProviderId, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListMessagedProvider(ListMessagedProviderDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListMessagedProviderPoco poco = new ListMessagedProviderPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListMessagedProvider " +
                        "SET [ListMessagedProviderId] = @ListMessagedProviderId, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE PatientId = @PatientId;";

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
