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
    public class ImportedItemsRepository : IImportedItemsRepository
    {
        private readonly string _connectionString;

        public ImportedItemsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ImportedItemsDomain GetImportedItems(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ImportedItems WHERE id = @id";

                    var ImportedItemsPoco = cn.QueryFirstOrDefault<ImportedItemsPoco>(query, new { id = id }) ?? new ImportedItemsPoco();

                    return ImportedItemsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ImportedItemsDomain> GetImportedItems(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ImportedItems WHERE @criteria";
                    List<ImportedItemsPoco> pocos = cn.Query<ImportedItemsPoco>(sql).ToList();
                    List<ImportedItemsDomain> domains = new List<ImportedItemsDomain>();

                    foreach (ImportedItemsPoco poco in pocos)
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

        public int DeleteImportedItems(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ImportedItems WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertImportedItems(ImportedItemsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ImportedItemsPoco poco = new ImportedItemsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ImportedItems] " +
                        "([id], [PatientID], [ImportedBy], [DateOfItem], [DateImported], [TypeOfItem], " +
                        "[ItemRE], [ItemFrom], [ItemComments], [ItemOriginalPath], [ItemCurrentPath], " +
                        "[ToBeSignedByID], [SignOffID], [SignOffDt], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded], [IsLetter], [IsLabEmbeddedRpt]) " +
                        "VALUES " +
                        "(@id, @PatientID, @ImportedBy, @DateOfItem, @DateImported, @TypeOfItem, " +
                        "@ItemRE, @ItemFrom, @ItemComments, @ItemOriginalPath, @ItemCurrentPath, " +
                        "@ToBeSignedByID, @SignOffID, @SignOffDt, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @IsLetter, @IsLabEmbeddedRpt); " +
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

        public int UpdateImportedItems(ImportedItemsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ImportedItemsPoco poco = new ImportedItemsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ImportedItems " +
                        "SET [PatientID] = @PatientID, [ImportedBy] = @ImportedBy, " +
                        "[DateOfItem] = @DateOfItem, [DateImported] = @DateImported, " +
                        "[TypeOfItem] = @TypeOfItem, [ItemRE] = @ItemRE, [ItemFrom] = @ItemFrom, " +
                        "[ItemComments] = @ItemComments, [ItemOriginalPath] = @ItemOriginalPath, " +
                        "[ItemCurrentPath] = @ItemCurrentPath, [ToBeSignedByID] = @ToBeSignedByID, " +
                        "[SignOffID] = @SignOffID, [SignOffDt] = @SignOffDt, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [IsLetter] = @IsLetter, " +
                        "[IsLabEmbeddedRpt] = @IsLabEmbeddedRpt " +
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
