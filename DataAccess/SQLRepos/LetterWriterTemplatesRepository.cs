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
    public class LetterWriterTemplatesRepository : ILetterWriterTemplatesRepository
    {
        private readonly string _connectionString;

        public LetterWriterTemplatesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LetterWriterTemplatesDomain GetLetterWriterTemplates(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM LetterWriterTemplates WHERE LetterWriterTemplateId = @id";

                    var LetterWriterTemplatesPoco = cn.QueryFirstOrDefault<LetterWriterTemplatesPoco>(query, new { id = id }) ?? new LetterWriterTemplatesPoco();

                    return LetterWriterTemplatesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LetterWriterTemplatesDomain> GetLetterWriterTemplates(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM LetterWriterTemplates WHERE @criteria";
                    List<LetterWriterTemplatesPoco> pocos = cn.Query<LetterWriterTemplatesPoco>(sql).ToList();
                    List<LetterWriterTemplatesDomain> domains = new List<LetterWriterTemplatesDomain>();

                    foreach (LetterWriterTemplatesPoco poco in pocos)
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

        public int DeleteLetterWriterTemplates(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM LetterWriterTemplates WHERE LetterWriterTemplateId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLetterWriterTemplates(LetterWriterTemplatesDomain domain)
        {
            int insertedId = 0;

            try
            {
                LetterWriterTemplatesPoco poco = new LetterWriterTemplatesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[LetterWriterTemplates] " +
                        "([LetterWriterTemplateId], [Name], [Document], [TypeOfTemplate], [CreatedBy], " +
                        "[CreatedDate], [LastModifiedBy], [LastModifiedDate], [Owner], [IsPracticeWide], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@LetterWriterTemplateId, @Name, @Document, @TypeOfTemplate, @CreatedBy, " +
                        "@CreatedDate, @LastModifiedBy, @LastModifiedDate, @Owner, @IsPracticeWide, " +
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

        public int UpdateLetterWriterTemplates(LetterWriterTemplatesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LetterWriterTemplatesPoco poco = new LetterWriterTemplatesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE LetterWriterTemplates " +
                        "SET [Name] = @Name, [Document] = @Document, [TypeOfTemplate] = @TypeOfTemplate, " +
                        "[CreatedBy] = @CreatedBy, [CreatedDate] = @CreatedDate, [LastModifiedBy] = @LastModifiedBy, " +
                        "[LastModifiedDate] = @LastModifiedDate, [Owner] = @Owner, " +
                        "[IsPracticeWide] = @IsPracticeWide, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE LetterWriterTemplateId = @LetterWriterTemplateId;";

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
