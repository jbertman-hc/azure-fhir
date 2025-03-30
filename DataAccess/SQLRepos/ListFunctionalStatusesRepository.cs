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
    public class ListFunctionalStatusesRepository : IListFunctionalStatusesRepository
    {
        private readonly string _connectionString;

        public ListFunctionalStatusesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListFunctionalStatusesDomain GetListFunctionalStatuses(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListFunctionalStatuses WHERE id = @id";

                    var ListFunctionalStatusesPoco = cn.QueryFirstOrDefault<ListFunctionalStatusesPoco>(query, new { id = id }) ?? new ListFunctionalStatusesPoco();

                    return ListFunctionalStatusesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListFunctionalStatusesDomain> GetListFunctionalStatuses(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListFunctionalStatuses WHERE @criteria";
                    List<ListFunctionalStatusesPoco> pocos = cn.Query<ListFunctionalStatusesPoco>(sql).ToList();
                    List<ListFunctionalStatusesDomain> domains = new List<ListFunctionalStatusesDomain>();

                    foreach (ListFunctionalStatusesPoco poco in pocos)
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

        public int DeleteListFunctionalStatuses(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListFunctionalStatuses WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListFunctionalStatuses(ListFunctionalStatusesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListFunctionalStatusesPoco poco = new ListFunctionalStatusesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListFunctionalStatuses] " +
                        "([Id], [PatientId], [Description], [EnteringProviderId], [DateCreated], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@Id, @PatientId, @Description, @EnteringProviderId, @DateCreated, @DateLastTouched, " +
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

        public int UpdateListFunctionalStatuses(ListFunctionalStatusesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListFunctionalStatusesPoco poco = new ListFunctionalStatusesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListFunctionalStatuses " +
                        "SET [PatientId] = @PatientId, [Description] = @Description, " +
                        "[EnteringProviderId] = @EnteringProviderId, [DateCreated] = @DateCreated, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE Id = @Id;";

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
