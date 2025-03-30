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
    public class InHouseTestsRepository : IInHouseTestsRepository
    {
        private readonly string _connectionString;

        public InHouseTestsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public InHouseTestsDomain GetInHouseTests(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM InHouseTests WHERE id = @id";

                    var InHouseTestsPoco = cn.QueryFirstOrDefault<InHouseTestsPoco>(query, new { id = id }) ?? new InHouseTestsPoco();

                    return InHouseTestsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<InHouseTestsDomain> GetInHouseTests(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM InHouseTests WHERE @criteria";
                    List<InHouseTestsPoco> pocos = cn.Query<InHouseTestsPoco>(sql).ToList();
                    List<InHouseTestsDomain> domains = new List<InHouseTestsDomain>();

                    foreach (InHouseTestsPoco poco in pocos)
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

        public int DeleteInHouseTests(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM InHouseTests WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertInHouseTests(InHouseTestsDomain domain)
        {
            int insertedId = 0;

            try
            {
                InHouseTestsPoco poco = new InHouseTestsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[InHouseTests] " +
                        "([ID], [TestName], [TestCode], [DateLastTouched], [LastTouchedBy], [DateRowAdded], [Migrated]) " +
                        "VALUES " +
                        "(@ID, @TestName, @TestCode, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @Migrated); " +
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

        public int UpdateInHouseTests(InHouseTestsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                InHouseTestsPoco poco = new InHouseTestsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE InHouseTests " +
                        "SET [TestName] = @TestName, [TestCode] = @TestCode, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [Migrated] = @Migrated " +
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
