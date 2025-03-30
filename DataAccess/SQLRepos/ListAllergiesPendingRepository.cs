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
    public class ListAllergiesPendingRepository : IListAllergiesPendingRepository
    {
        private readonly string _connectionString;

        public ListAllergiesPendingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListAllergiesPendingDomain GetListAllergiesPending(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListAllergiesPending WHERE id = @id";

                    var ListAllergiesPendingPoco = cn.QueryFirstOrDefault<ListAllergiesPendingPoco>(query, new { id = id }) ?? new ListAllergiesPendingPoco();

                    return ListAllergiesPendingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListAllergiesPendingDomain> GetListAllergiesPendings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListAllergiesPending WHERE @criteria";
                    List<ListAllergiesPendingPoco> pocos = cn.Query<ListAllergiesPendingPoco>(sql).ToList();
                    List<ListAllergiesPendingDomain> domains = new List<ListAllergiesPendingDomain>();

                    foreach (ListAllergiesPendingPoco poco in pocos)
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

        public int DeleteListAllergiesPending(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListAllergiesPending WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListAllergiesPending(ListAllergiesPendingDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListAllergiesPendingPoco poco = new ListAllergiesPendingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListAllergiesPending] " +
                        "([ID], [PatientID], [AllergyID], [AllergyDescription], [AllergySource], [Reaction], " +
                        "[Comments], [AddedBy], [DateAdded], [EditedBy], [DateEdited], [Inactive], [Migrated], " +
                        "[LastConfirmedBy], [LastConfirmedDate], [Severity], [PendingFlag], [ImportedDate], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [Source]) " +
                        "VALUES " +
                        "(@ID, @PatientID, @AllergyID, @AllergyDescription, @AllergySource, @Reaction, @Comments, " +
                        "@AddedBy, @DateAdded, @EditedBy, @DateEdited, @Inactive, @Migrated, @LastConfirmedBy, " +
                        "@LastConfirmedDate, @Severity, @PendingFlag, @ImportedDate, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @Source); " +
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

        public int UpdateListAllergiesPending(ListAllergiesPendingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListAllergiesPendingPoco poco = new ListAllergiesPendingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListAllergiesPending " +
                        "SET [PatientID] = @PatientID, [AllergyID] = @AllergyID, " +
                        "[AllergyDescription] = @AllergyDescription, [AllergySource] = @AllergySource, " +
                        "[Reaction] = @Reaction, [Comments] = @Comments, [AddedBy] = @AddedBy, " +
                        "[DateAdded] = @DateAdded, [EditedBy] = @EditedBy, [DateEdited] = @DateEdited, " +
                        "[Inactive] = @Inactive, [Migrated] = @Migrated, [LastConfirmedBy] = @LastConfirmedBy, " +
                        "[LastConfirmedDate] = @LastConfirmedDate, [Severity] = @Severity, " +
                        "[PendingFlag] = @PendingFlag, [ImportedDate] = @ImportedDate, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [Source] = @Source " +
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
