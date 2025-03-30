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
    public class PracticeDocumentsRepository : IPracticeDocumentsRepository
    {
        private readonly string _connectionString;

        public PracticeDocumentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PracticeDocumentsDomain GetPracticeDocuments(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PracticeDocuments WHERE id = @id";

                    var PracticeDocumentsPoco = cn.QueryFirstOrDefault<PracticeDocumentsPoco>(query, new { id = id }) ?? new PracticeDocumentsPoco();

                    return PracticeDocumentsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PracticeDocumentsDomain> GetPracticeDocuments(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PracticeDocuments WHERE @criteria";
                    List<PracticeDocumentsPoco> pocos = cn.Query<PracticeDocumentsPoco>(sql).ToList();
                    List<PracticeDocumentsDomain> domains = new List<PracticeDocumentsDomain>();

                    foreach (PracticeDocumentsPoco poco in pocos)
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

        public int DeletePracticeDocuments(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PracticeDocuments WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPracticeDocuments(PracticeDocumentsDomain domain)
        {
            int insertedId = 0;

            try
            {
                PracticeDocumentsPoco poco = new PracticeDocumentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PracticeDocuments] " +
                        "([ID], [FileAlias], [FileName], [DateImported], [ParentID], [Type], [Permanent], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [Description], [ShowAtCheckIn]) " +
                        "VALUES " +
                        "(@ID, @FileAlias, @FileName, @DateImported, @ParentID, @Type, @Permanent, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @Description, @ShowAtCheckIn); " +
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

        public int UpdatePracticeDocuments(PracticeDocumentsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PracticeDocumentsPoco poco = new PracticeDocumentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PracticeDocuments " +
                        "SET [FileAlias] = @FileAlias, [FileName] = @FileName, [DateImported] = @DateImported, " +
                        "[ParentID] = @ParentID, [Type] = @Type, [Permanent] = @Permanent, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [Description] = @Description, [ShowAtCheckIn] = @ShowAtCheckIn " +
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
