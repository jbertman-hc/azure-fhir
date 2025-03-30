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
    public class ListCustomFieldsRepository : IListCustomFieldsRepository
    {
        private readonly string _connectionString;

        public ListCustomFieldsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListCustomFieldsDomain GetListCustomFields(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListCustomFields WHERE id = @id";

                    var ListCustomFieldsPoco = cn.QueryFirstOrDefault<ListCustomFieldsPoco>(query, new { id = id }) ?? new ListCustomFieldsPoco();

                    return ListCustomFieldsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListCustomFieldsDomain> GetListCustomFields(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListCustomFields WHERE @criteria";
                    List<ListCustomFieldsPoco> pocos = cn.Query<ListCustomFieldsPoco>(sql).ToList();
                    List<ListCustomFieldsDomain> domains = new List<ListCustomFieldsDomain>();

                    foreach (ListCustomFieldsPoco poco in pocos)
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

        public int DeleteListCustomFields(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListCustomFields WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListCustomFields(ListCustomFieldsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListCustomFieldsPoco poco = new ListCustomFieldsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListCustomFields] " +
                        "([ID], [PatientID], [CustomFieldID], [Value], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @PatientID, @CustomFieldID, @Value, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListCustomFields(ListCustomFieldsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListCustomFieldsPoco poco = new ListCustomFieldsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListCustomFields " +
                        "SET [PatientID] = @PatientID, [CustomFieldID] = @CustomFieldID, [Value] = @Value, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
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
