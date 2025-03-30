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
    public class ImportLocationRepository : IImportLocationRepository
    {
        private readonly string _connectionString;

        public ImportLocationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ImportLocationDomain GetImportLocation(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ImportLocation WHERE id = @id";

                    var ImportLocationPoco = cn.QueryFirstOrDefault<ImportLocationPoco>(query, new { id = id }) ?? new ImportLocationPoco();

                    return ImportLocationPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ImportLocationDomain> GetImportLocations(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ImportLocation WHERE @criteria";
                    List<ImportLocationPoco> pocos = cn.Query<ImportLocationPoco>(sql).ToList();
                    List<ImportLocationDomain> domains = new List<ImportLocationDomain>();

                    foreach (ImportLocationPoco poco in pocos)
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

        public int DeleteImportLocation(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ImportLocation WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertImportLocation(ImportLocationDomain domain)
        {
            int insertedId = 0;

            try
            {
                ImportLocationPoco poco = new ImportLocationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ImportLocation] " +
                        "([id], [Location], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@id, @Location, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateImportLocation(ImportLocationDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ImportLocationPoco poco = new ImportLocationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ImportLocation " +
                        "SET [Location] = @Location, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE id = @id;";

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
