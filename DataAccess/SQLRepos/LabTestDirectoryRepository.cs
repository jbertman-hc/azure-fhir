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
    public class LabTestDirectoryRepository : ILabTestDirectoryRepository
    {
        private readonly string _connectionString;

        public LabTestDirectoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LabTestDirectoryDomain GetLabTestDirectory(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM LabTestDirectory WHERE TestCode = @id";

                    var LabTestDirectoryPoco = cn.QueryFirstOrDefault<LabTestDirectoryPoco>(query, new { id = id }) ?? new LabTestDirectoryPoco();

                    return LabTestDirectoryPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LabTestDirectoryDomain> GetLabTestDirectorys(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM LabTestDirectory WHERE @criteria";
                    List<LabTestDirectoryPoco> pocos = cn.Query<LabTestDirectoryPoco>(sql).ToList();
                    List<LabTestDirectoryDomain> domains = new List<LabTestDirectoryDomain>();

                    foreach (LabTestDirectoryPoco poco in pocos)
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

        public int DeleteLabTestDirectory(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM LabTestDirectory WHERE TestCode = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLabTestDirectory(LabTestDirectoryDomain domain)
        {
            int insertedId = 0;

            try
            {
                LabTestDirectoryPoco poco = new LabTestDirectoryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[LabTestDirectory] " +
                        "([TestCode], [LabCompany], [TestName], [CreatedDt], [CreatedBy], [LastUpdDt], " +
                        "[LastUpdBy], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@TestCode, @LabCompany, @TestName, @CreatedDt, @CreatedBy, @LastUpdDt, @LastUpdBy, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateLabTestDirectory(LabTestDirectoryDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LabTestDirectoryPoco poco = new LabTestDirectoryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE LabTestDirectory " +
                        "SET [LabCompany] = @LabCompany, [TestName] = @TestName, [CreatedDt] = @CreatedDt, " +
                        "[CreatedBy] = @CreatedBy, [LastUpdDt] = @LastUpdDt, [LastUpdBy] = @LastUpdBy, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE TestCode = @TestCode;";

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
