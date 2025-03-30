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
    public class ListDirectivesRepository : IListDirectivesRepository
    {
        private readonly string _connectionString;

        public ListDirectivesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListDirectivesDomain GetListDirectives(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListDirectives WHERE id = @id";

                    var ListDirectivesPoco = cn.QueryFirstOrDefault<ListDirectivesPoco>(query, new { id = id }) ?? new ListDirectivesPoco();

                    return ListDirectivesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListDirectivesDomain> GetListDirectives(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListDirectives WHERE @criteria";
                    List<ListDirectivesPoco> pocos = cn.Query<ListDirectivesPoco>(sql).ToList();
                    List<ListDirectivesDomain> domains = new List<ListDirectivesDomain>();

                    foreach (ListDirectivesPoco poco in pocos)
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

        public int DeleteListDirectives(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListDirectives WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListDirectives(ListDirectivesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListDirectivesPoco poco = new ListDirectivesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListDirectives] " +
                        "([id], [PatientID], [ProviderID], [SavedBy], [DateSaved], [DirectiveName], " +
                        "[DirectiveText], [DateActive], [DateInactive], [DirectiveCode], [DirectiveLevel], " +
                        "[IsActive], [IsValidDirective], [Comments], [PathToDirective], [History], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@id, @PatientID, @ProviderID, @SavedBy, @DateSaved, @DirectiveName, @DirectiveText, " +
                        "@DateActive, @DateInactive, @DirectiveCode, @DirectiveLevel, @IsActive, " +
                        "@IsValidDirective, @Comments, @PathToDirective, @History, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListDirectives(ListDirectivesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListDirectivesPoco poco = new ListDirectivesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListDirectives " +
                        "SET [PatientID] = @PatientID, [ProviderID] = @ProviderID, [SavedBy] = @SavedBy, " +
                        "[DateSaved] = @DateSaved, [DirectiveName] = @DirectiveName, " +
                        "[DirectiveText] = @DirectiveText, [DateActive] = @DateActive, " +
                        "[DateInactive] = @DateInactive, [DirectiveCode] = @DirectiveCode, " +
                        "[DirectiveLevel] = @DirectiveLevel, [IsActive] = @IsActive, " +
                        "[IsValidDirective] = @IsValidDirective, [Comments] = @Comments, " +
                        "[PathToDirective] = @PathToDirective, [History] = @History, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
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
