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
    public class TemplatesRepository : ITemplatesRepository
    {
        private readonly string _connectionString;

        public TemplatesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TemplatesDomain GetTemplates(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Templates WHERE id = @id";

                    var TemplatesPoco = cn.QueryFirstOrDefault<TemplatesPoco>(query, new { id = id }) ?? new TemplatesPoco();

                    return TemplatesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<TemplatesDomain> GetTemplates(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Templates WHERE @criteria";
                    List<TemplatesPoco> pocos = cn.Query<TemplatesPoco>(sql).ToList();
                    List<TemplatesDomain> domains = new List<TemplatesDomain>();

                    foreach (TemplatesPoco poco in pocos)
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

        public int DeleteTemplates(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Templates WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertTemplates(TemplatesDomain domain)
        {
            int insertedId = 0;

            try
            {
                TemplatesPoco poco = new TemplatesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Templates] " +
                        "([ID], [ProviderName], [TemplateName], [TemplateText], [TemplateLocation], [Default], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [TemplateWeighting]) " +
                        "VALUES " +
                        "(@ID, @ProviderName, @TemplateName, @TemplateText, @TemplateLocation, @Default, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @TemplateWeighting); " +
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

        public int UpdateTemplates(TemplatesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                TemplatesPoco poco = new TemplatesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Templates " +
                        "SET [ProviderName] = @ProviderName, [TemplateName] = @TemplateName, " +
                        "[TemplateText] = @TemplateText, [TemplateLocation] = @TemplateLocation, " +
                        "[Default] = @Default, [DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [TemplateWeighting] = @TemplateWeighting " +
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
