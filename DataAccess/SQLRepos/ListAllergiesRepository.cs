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
    public class ListAllergiesRepository : IListAllergiesRepository
    {
        private readonly string _connectionString;

        public ListAllergiesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListAllergiesDomain GetListAllergies(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListAllergies WHERE id = @id";

                    var ListAllergiesPoco = cn.QueryFirstOrDefault<ListAllergiesPoco>(query, new { id = id }) ?? new ListAllergiesPoco();

                    return ListAllergiesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListAllergiesDomain> GetListAllergies(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListAllergies WHERE @criteria";
                    List<ListAllergiesPoco> pocos = cn.Query<ListAllergiesPoco>(sql).ToList();
                    List<ListAllergiesDomain> domains = new List<ListAllergiesDomain>();

                    foreach (ListAllergiesPoco poco in pocos)
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

        public int DeleteListAllergies(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListAllergies WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListAllergies(ListAllergiesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListAllergiesPoco poco = new ListAllergiesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListAllergies] " +
                        "([ID], [PatientID], [AllergyID], [AllergyDescription], [AllergySource], [Reaction], " +
                        "[Comments], [AddedBy], [DateAdded], [EditedBy], [DateEdited], [Inactive], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [Migrated], [LastConfirmedBy], " +
                        "[LastConfirmedDate], [Severity], [Source]) " +
                        "VALUES " +
                        "(@ID, @PatientID, @AllergyID, @AllergyDescription, @AllergySource, @Reaction, " +
                        "@Comments, @AddedBy, @DateAdded, @EditedBy, @DateEdited, @Inactive, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @Migrated, @LastConfirmedBy, @LastConfirmedDate, " +
                        "@Severity, @Source); " +
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

        public int UpdateListAllergies(ListAllergiesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListAllergiesPoco poco = new ListAllergiesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListAllergies " +
                        "SET [PatientID] = @PatientID, [AllergyID] = @AllergyID, " +
                        "[AllergyDescription] = @AllergyDescription, [AllergySource] = @AllergySource, " +
                        "[Reaction] = @Reaction, [Comments] = @Comments, [AddedBy] = @AddedBy, " +
                        "[DateAdded] = @DateAdded, [EditedBy] = @EditedBy, [DateEdited] = @DateEdited, " +
                        "[Inactive] = @Inactive, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[Migrated] = @Migrated, [LastConfirmedBy] = @LastConfirmedBy, " +
                        "[LastConfirmedDate] = @LastConfirmedDate, [Severity] = @Severity, [Source] = @Source " +
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
