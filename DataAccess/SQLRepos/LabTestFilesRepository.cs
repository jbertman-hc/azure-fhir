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
    public class LabTestFilesRepository : ILabTestFilesRepository
    {
        private readonly string _connectionString;

        public LabTestFilesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LabTestFilesDomain GetLabTestFiles(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM LabTestFiles WHERE LabTestID = @id";

                    var LabTestFilesPoco = cn.QueryFirstOrDefault<LabTestFilesPoco>(query, new { id = id }) ?? new LabTestFilesPoco();

                    return LabTestFilesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LabTestFilesDomain> GetLabTestFiles(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM LabTestFiles WHERE @criteria";
                    List<LabTestFilesPoco> pocos = cn.Query<LabTestFilesPoco>(sql).ToList();
                    List<LabTestFilesDomain> domains = new List<LabTestFilesDomain>();

                    foreach (LabTestFilesPoco poco in pocos)
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

        public int DeleteLabTestFiles(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM LabTestFiles WHERE LabTestID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLabTestFiles(LabTestFilesDomain domain)
        {
            int insertedId = 0;

            try
            {
                LabTestFilesPoco poco = new LabTestFilesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[LabTestFiles] " +
                        "([LabTestID], [FileData], [Uploaded], [Sent], [RequiresSignOff], [ExportFileName], " +
                        "[LogFilePath], [interfaceID]) " +
                        "VALUES " +
                        "(@LabTestID, @FileData, @Uploaded, @Sent, @RequiresSignOff, @ExportFileName, " +
                        "@LogFilePath, @interfaceID); " +
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

        public int UpdateLabTestFiles(LabTestFilesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LabTestFilesPoco poco = new LabTestFilesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE LabTestFiles " +
                        "SET [FileData] = @FileData, [Uploaded] = @Uploaded, [Sent] = @Sent, " +
                        "[RequiresSignOff] = @RequiresSignOff, [ExportFileName] = @ExportFileName, " +
                        "[LogFilePath] = @LogFilePath, [interfaceID] = @interfaceID " +
                        "WHERE LabTestID = @LabTestID;";

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
