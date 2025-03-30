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
    public class ListOrdersRefIdsRepository : IListOrdersRefIdsRepository
    {
        private readonly string _connectionString;

        public ListOrdersRefIdsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListOrdersRefIdsDomain GetListOrdersRefIds(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListOrdersRefIds WHERE LORI_Id = @id";

                    var ListOrdersRefIdsPoco = cn.QueryFirstOrDefault<ListOrdersRefIdsPoco>(query, new { id = id }) ?? new ListOrdersRefIdsPoco();

                    return ListOrdersRefIdsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListOrdersRefIdsDomain> GetListOrdersRefIds(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListOrdersRefIds WHERE @criteria";
                    List<ListOrdersRefIdsPoco> pocos = cn.Query<ListOrdersRefIdsPoco>(sql).ToList();
                    List<ListOrdersRefIdsDomain> domains = new List<ListOrdersRefIdsDomain>();

                    foreach (ListOrdersRefIdsPoco poco in pocos)
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

        public int DeleteListOrdersRefIds(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListOrdersRefIds WHERE LORI_Id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListOrdersRefIds(ListOrdersRefIdsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListOrdersRefIdsPoco poco = new ListOrdersRefIdsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListOrdersRefIds] " +
                        "([LORI_Id], [RefId], [LabOrderRowId], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@LORI_Id, @RefId, @LabOrderRowId, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListOrdersRefIds(ListOrdersRefIdsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListOrdersRefIdsPoco poco = new ListOrdersRefIdsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListOrdersRefIds " +
                        "SET [RefId] = @RefId, [LabOrderRowId] = @LabOrderRowId, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE LORI_Id = @LORI_Id;";

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
