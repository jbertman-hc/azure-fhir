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
    public class CustomFieldsRepository : ICustomFieldsRepository
    {
        private readonly string _connectionString;

        public CustomFieldsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CustomFieldsDomain GetCustomFields(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM CustomFields WHERE id = @id";

                    var CustomFieldsPoco = cn.QueryFirstOrDefault<CustomFieldsPoco>(query, new { id = id }) ?? new CustomFieldsPoco();

                    return CustomFieldsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CustomFieldsDomain> GetCustomFields(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM CustomFields WHERE @criteria";
                    List<CustomFieldsPoco> pocos = cn.Query<CustomFieldsPoco>(sql).ToList();
                    List<CustomFieldsDomain> domains = new List<CustomFieldsDomain>();

                    foreach (CustomFieldsPoco poco in pocos)
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

        public int DeleteCustomFields(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM CustomFields WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertCustomFields(CustomFieldsDomain domain)
        {
            int insertedId = 0;

            try
            {
                CustomFieldsPoco poco = new CustomFieldsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[CustomFields] " +
                        "([ID], [FieldName], [DemoFieldName], [Type], [DateModified], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @FieldName, @DemoFieldName, @Type, @DateModified, @DateLastTouched, " +
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

        public int UpdateCustomFields(CustomFieldsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                CustomFieldsPoco poco = new CustomFieldsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE CustomFields " +
                        "SET [FieldName] = @FieldName, [DemoFieldName] = @DemoFieldName, [Type] = @Type, " +
                        "[DateModified] = @DateModified, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
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
