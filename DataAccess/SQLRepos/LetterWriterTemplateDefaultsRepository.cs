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
    public class LetterWriterTemplateDefaultsRepository : ILetterWriterTemplateDefaultsRepository
    {
        private readonly string _connectionString;

        public LetterWriterTemplateDefaultsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LetterWriterTemplateDefaultsDomain GetLetterWriterTemplateDefaults(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM LetterWriterTemplateDefaults WHERE LetterWriterTemplateDefaultId = @id";

                    var LetterWriterTemplateDefaultsPoco = cn.QueryFirstOrDefault<LetterWriterTemplateDefaultsPoco>(query, new { id = id }) ?? new LetterWriterTemplateDefaultsPoco();

                    return LetterWriterTemplateDefaultsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LetterWriterTemplateDefaultsDomain> GetLetterWriterTemplateDefaults(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM LetterWriterTemplateDefaults WHERE @criteria";
                    List<LetterWriterTemplateDefaultsPoco> pocos = cn.Query<LetterWriterTemplateDefaultsPoco>(sql).ToList();
                    List<LetterWriterTemplateDefaultsDomain> domains = new List<LetterWriterTemplateDefaultsDomain>();

                    foreach (LetterWriterTemplateDefaultsPoco poco in pocos)
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

        public int DeleteLetterWriterTemplateDefaults(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM LetterWriterTemplateDefaults WHERE LetterWriterTemplateDefaultId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLetterWriterTemplateDefaults(LetterWriterTemplateDefaultsDomain domain)
        {
            int insertedId = 0;

            try
            {
                LetterWriterTemplateDefaultsPoco poco = new LetterWriterTemplateDefaultsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[LetterWriterTemplateDefaults] " +
                        "([LetterWriterTemplateDefaultId], [ProviderCode], [TemplateType], [LetterWriterTemplateId], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [PracticeWide]) " +
                        "VALUES " +
                        "(@LetterWriterTemplateDefaultId, @ProviderCode, @TemplateType, @LetterWriterTemplateId, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @PracticeWide); " +
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

        public int UpdateLetterWriterTemplateDefaults(LetterWriterTemplateDefaultsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LetterWriterTemplateDefaultsPoco poco = new LetterWriterTemplateDefaultsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE LetterWriterTemplateDefaults " +
                        "SET [ProviderCode] = @ProviderCode, [TemplateType] = @TemplateType, " +
                        "[LetterWriterTemplateId] = @LetterWriterTemplateId, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[PracticeWide] = @PracticeWide " +
                        "WHERE LetterWriterTemplateDefaultId = @LetterWriterTemplateDefaultId;";

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
