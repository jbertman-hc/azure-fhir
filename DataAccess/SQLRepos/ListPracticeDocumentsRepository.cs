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
    public class ListPracticeDocumentsRepository : IListPracticeDocumentsRepository
    {
        private readonly string _connectionString;

        public ListPracticeDocumentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListPracticeDocumentsDomain GetListPracticeDocuments(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListPracticeDocuments WHERE id = @id";

                    var ListPracticeDocumentsPoco = cn.QueryFirstOrDefault<ListPracticeDocumentsPoco>(query, new { id = id }) ?? new ListPracticeDocumentsPoco();

                    return ListPracticeDocumentsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListPracticeDocumentsDomain> GetListPracticeDocuments(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListPracticeDocuments WHERE @criteria";
                    List<ListPracticeDocumentsPoco> pocos = cn.Query<ListPracticeDocumentsPoco>(sql).ToList();
                    List<ListPracticeDocumentsDomain> domains = new List<ListPracticeDocumentsDomain>();

                    foreach (ListPracticeDocumentsPoco poco in pocos)
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

        public int DeleteListPracticeDocuments(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListPracticeDocuments WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListPracticeDocuments(ListPracticeDocumentsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListPracticeDocumentsPoco poco = new ListPracticeDocumentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPracticeDocuments] " +
                        "([ID], [PatientID], [PracticeDocID], [PracticeDocAlias], [LastTouchedBy], " +
                        "[DateLastTouched], [DateRowAdded], [DateModified], [IsSigned]) " +
                        "VALUES " +
                        "(@ID, @PatientID, @PracticeDocID, @PracticeDocAlias, @LastTouchedBy, @DateLastTouched, " +
                        "@DateRowAdded, @DateModified, @IsSigned); " +
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

        public int UpdateListPracticeDocuments(ListPracticeDocumentsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListPracticeDocumentsPoco poco = new ListPracticeDocumentsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPracticeDocuments " +
                        "SET [PatientID] = @PatientID, [PracticeDocID] = @PracticeDocID, " +
                        "[PracticeDocAlias] = @PracticeDocAlias, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateLastTouched] = @DateLastTouched, [DateRowAdded] = @DateRowAdded, " +
                        "[DateModified] = @DateModified, [IsSigned] = @IsSigned " +
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
