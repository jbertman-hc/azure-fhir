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
    public class ListRegistrySiteIDsRepository : IListRegistrySiteIDsRepository
    {
        private readonly string _connectionString;

        public ListRegistrySiteIDsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListRegistrySiteIDsDomain GetListRegistrySiteIDs(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListRegistrySiteIDs WHERE ListRegistrySiteIDsId = @id";

                    var ListRegistrySiteIDsPoco = cn.QueryFirstOrDefault<ListRegistrySiteIDsPoco>(query, new { id = id }) ?? new ListRegistrySiteIDsPoco();

                    return ListRegistrySiteIDsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListRegistrySiteIDsDomain> GetListRegistrySiteIDs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListRegistrySiteIDs WHERE @criteria";
                    List<ListRegistrySiteIDsPoco> pocos = cn.Query<ListRegistrySiteIDsPoco>(sql).ToList();
                    List<ListRegistrySiteIDsDomain> domains = new List<ListRegistrySiteIDsDomain>();

                    foreach (ListRegistrySiteIDsPoco poco in pocos)
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

        public int DeleteListRegistrySiteIDs(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListRegistrySiteIDs WHERE ListRegistrySiteIDsId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListRegistrySiteIDs(ListRegistrySiteIDsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListRegistrySiteIDsPoco poco = new ListRegistrySiteIDsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListRegistrySiteIDs] " +
                        "([ListRegistrySiteIDsId], [RegistryInterfaceId], [LocationsId], [SiteID], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ListRegistrySiteIDsId, @RegistryInterfaceId, @LocationsId, @SiteID, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListRegistrySiteIDs(ListRegistrySiteIDsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListRegistrySiteIDsPoco poco = new ListRegistrySiteIDsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListRegistrySiteIDs " +
                        "SET [RegistryInterfaceId] = @RegistryInterfaceId, [LocationsId] = @LocationsId, " +
                        "[SiteID] = @SiteID, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE ListRegistrySiteIDsId = @ListRegistrySiteIDsId;";

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
