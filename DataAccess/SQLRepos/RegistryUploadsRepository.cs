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
    public class RegistryUploadsRepository : IRegistryUploadsRepository
    {
        private readonly string _connectionString;

        public RegistryUploadsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RegistryUploadsDomain GetRegistryUploads(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM RegistryUploads WHERE ListHMID = @id";

                    var RegistryUploadsPoco = cn.QueryFirstOrDefault<RegistryUploadsPoco>(query, new { id = id }) ?? new RegistryUploadsPoco();

                    return RegistryUploadsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RegistryUploadsDomain> GetRegistryUploads(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM RegistryUploads WHERE @criteria";
                    List<RegistryUploadsPoco> pocos = cn.Query<RegistryUploadsPoco>(sql).ToList();
                    List<RegistryUploadsDomain> domains = new List<RegistryUploadsDomain>();

                    foreach (RegistryUploadsPoco poco in pocos)
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

        public int DeleteRegistryUploads(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM RegistryUploads WHERE ListHMID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertRegistryUploads(RegistryUploadsDomain domain)
        {
            int insertedId = 0;

            try
            {
                RegistryUploadsPoco poco = new RegistryUploadsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[RegistryUploads] " +
                        "([ListHMID], [InterfaceID], [DateSentToRegistry], [DateLastTouched], " +
                        "[DateRowAdded], [LastTouchedBy]) " +
                        "VALUES " +
                        "(@ListHMID, @InterfaceID, @DateSentToRegistry, @DateLastTouched, @DateRowAdded, @LastTouchedBy); " +
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

        public int UpdateRegistryUploads(RegistryUploadsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                RegistryUploadsPoco poco = new RegistryUploadsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE RegistryUploads " +
                        "SET [InterfaceID] = @InterfaceID, [DateSentToRegistry] = @DateSentToRegistry, " +
                        "[DateLastTouched] = @DateLastTouched, [DateRowAdded] = @DateRowAdded, " +
                        "[LastTouchedBy] = @LastTouchedBy " +
                        "WHERE ListHMID = @ListHMID;";

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
