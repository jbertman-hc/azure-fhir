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
    public class AllergyLibraryRepository : IAllergyLibraryRepository
    {
        private readonly string _connectionString;

        public AllergyLibraryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AllergyLibraryDomain GetAllergyLibrary(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM AllergyLibrary WHERE CompositeAllergyID = @id";

                    var AllergyLibraryPoco = cn.QueryFirstOrDefault<AllergyLibraryPoco>(query, new { id = id }) ?? new AllergyLibraryPoco();

                    return AllergyLibraryPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AllergyLibraryDomain> GetAllergyLibrarys(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM AllergyLibrary WHERE @criteria";
                    List<AllergyLibraryPoco> pocos = cn.Query<AllergyLibraryPoco>(sql).ToList();
                    List<AllergyLibraryDomain> domains = new List<AllergyLibraryDomain>();

                    foreach (AllergyLibraryPoco poco in pocos)
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

        public int DeleteAllergyLibrary(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM AllergyLibrary WHERE CompositeAllergyID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertAllergyLibrary(AllergyLibraryDomain domain)
        {
            int insertedId = 0;

            try
            {
                AllergyLibraryPoco poco = new AllergyLibraryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[AllergyLibrary] " +
                        "([CompositeAllergyID], [Description]) " +
                        "VALUES " +
                        "(@CompositeAllergyID, @Description); " +
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

        public int UpdateAllergyLibrary(AllergyLibraryDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                AllergyLibraryPoco poco = new AllergyLibraryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE AllergyLibrary " +
                        "SET [Description] = @Description " +
                        "WHERE CompositeAllergyID = @CompositeAllergyID;";

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
