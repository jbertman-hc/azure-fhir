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
    public class ImportedItemDescriptionsRepository : IImportedItemDescriptionsRepository
    {
        private readonly string _connectionString;

        public ImportedItemDescriptionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ImportedItemDescriptionsDomain GetImportedItemDescriptions(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ImportedItemDescriptions WHERE id = @id";

                    var ImportedItemDescriptionsPoco = cn.QueryFirstOrDefault<ImportedItemDescriptionsPoco>(query, new { id = id }) ?? new ImportedItemDescriptionsPoco();

                    return ImportedItemDescriptionsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ImportedItemDescriptionsDomain> GetImportedItemDescriptions(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ImportedItemDescriptions WHERE @criteria";
                    List<ImportedItemDescriptionsPoco> pocos = cn.Query<ImportedItemDescriptionsPoco>(sql).ToList();
                    List<ImportedItemDescriptionsDomain> domains = new List<ImportedItemDescriptionsDomain>();

                    foreach (ImportedItemDescriptionsPoco poco in pocos)
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

        public int DeleteImportedItemDescriptions(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ImportedItemDescriptions WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertImportedItemDescriptions(ImportedItemDescriptionsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ImportedItemDescriptionsPoco poco = new ImportedItemDescriptionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ImportedItemDescriptions] " +
                        "([Id], [InitId], [Description], [IsReadOnly], [ShowOnAdd], [ShowOnEdit], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@Id, @InitId, @Description, @IsReadOnly, @ShowOnAdd, @ShowOnEdit, " +
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

        public int UpdateImportedItemDescriptions(ImportedItemDescriptionsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ImportedItemDescriptionsPoco poco = new ImportedItemDescriptionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ImportedItemDescriptions " +
                        "SET [InitId] = @InitId, [Description] = @Description, [IsReadOnly] = @IsReadOnly, " +
                        "[ShowOnAdd] = @ShowOnAdd, [ShowOnEdit] = @ShowOnEdit, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE Id = @Id;";

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
