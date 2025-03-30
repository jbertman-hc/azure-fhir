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
    public class ListHMrulesIgnoredRepository : IListHMrulesIgnoredRepository
    {
        private readonly string _connectionString;

        public ListHMrulesIgnoredRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListHMrulesIgnoredDomain GetListHMrulesIgnored(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListHMrulesIgnored WHERE id = @id";

                    var ListHMrulesIgnoredPoco = cn.QueryFirstOrDefault<ListHMrulesIgnoredPoco>(query, new { id = id }) ?? new ListHMrulesIgnoredPoco();

                    return ListHMrulesIgnoredPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListHMrulesIgnoredDomain> GetListHMrulesIgnoreds(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListHMrulesIgnored WHERE @criteria";
                    List<ListHMrulesIgnoredPoco> pocos = cn.Query<ListHMrulesIgnoredPoco>(sql).ToList();
                    List<ListHMrulesIgnoredDomain> domains = new List<ListHMrulesIgnoredDomain>();

                    foreach (ListHMrulesIgnoredPoco poco in pocos)
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

        public int DeleteListHMrulesIgnored(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListHMrulesIgnored WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListHMrulesIgnored(ListHMrulesIgnoredDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListHMrulesIgnoredPoco poco = new ListHMrulesIgnoredPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListHMrulesIgnored] " +
                        "([ID], [PatientID], [HmRuleID], [LastTouchedBy], [DateLastTouched], [DateRowAdded], [Comments]) " +
                        "VALUES " +
                        "(@ID, @PatientID, @HmRuleID, @LastTouchedBy, @DateLastTouched, @DateRowAdded, @Comments); " +
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

        public int UpdateListHMrulesIgnored(ListHMrulesIgnoredDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListHMrulesIgnoredPoco poco = new ListHMrulesIgnoredPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListHMrulesIgnored " +
                        "SET [PatientID] = @PatientID, [HmRuleID] = @HmRuleID, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateLastTouched] = @DateLastTouched, [DateRowAdded] = @DateRowAdded, [Comments] = @Comments " +
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
