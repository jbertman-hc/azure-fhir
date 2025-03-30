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
    public class TrackedTemplateRepository : ITrackedTemplateRepository
    {
        private readonly string _connectionString;

        public TrackedTemplateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TrackedTemplateDomain GetTrackedTemplate(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM TrackedTemplate WHERE id = @id";

                    var TrackedTemplatePoco = cn.QueryFirstOrDefault<TrackedTemplatePoco>(query, new { id = id }) ?? new TrackedTemplatePoco();

                    return TrackedTemplatePoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<TrackedTemplateDomain> GetTrackedTemplates(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM TrackedTemplate WHERE @criteria";
                    List<TrackedTemplatePoco> pocos = cn.Query<TrackedTemplatePoco>(sql).ToList();
                    List<TrackedTemplateDomain> domains = new List<TrackedTemplateDomain>();

                    foreach (TrackedTemplatePoco poco in pocos)
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

        public int DeleteTrackedTemplate(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM TrackedTemplate WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertTrackedTemplate(TrackedTemplateDomain domain)
        {
            int insertedId = 0;

            try
            {
                TrackedTemplatePoco poco = new TrackedTemplatePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[TrackedTemplate] " +
                        "([id], [Item], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES (@id, @Item, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateTrackedTemplate(TrackedTemplateDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                TrackedTemplatePoco poco = new TrackedTemplatePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE TrackedTemplate " +
                        "SET [Item] = @Item, [DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
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
