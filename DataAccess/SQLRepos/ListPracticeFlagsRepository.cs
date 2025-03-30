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
    public class ListPracticeFlagsRepository : IListPracticeFlagsRepository
    {
        private readonly string _connectionString;

        public ListPracticeFlagsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListPracticeFlagsDomain GetListPracticeFlags(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListPracticeFlags WHERE RowID = @id";

                    var ListPracticeFlagsPoco = cn.QueryFirstOrDefault<ListPracticeFlagsPoco>(query, new { id = id }) ?? new ListPracticeFlagsPoco();

                    return ListPracticeFlagsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListPracticeFlagsDomain> GetListPracticeFlags(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListPracticeFlags WHERE @criteria";
                    List<ListPracticeFlagsPoco> pocos = cn.Query<ListPracticeFlagsPoco>(sql).ToList();
                    List<ListPracticeFlagsDomain> domains = new List<ListPracticeFlagsDomain>();

                    foreach (ListPracticeFlagsPoco poco in pocos)
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

        public int DeleteListPracticeFlags(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListPracticeFlags WHERE RowID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListPracticeFlags(ListPracticeFlagsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListPracticeFlagsPoco poco = new ListPracticeFlagsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPracticeFlags] " +
                        "([RowID], [PatientID], [PracticeFlagID], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES (@RowID, @PatientID, @PracticeFlagID, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListPracticeFlags(ListPracticeFlagsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListPracticeFlagsPoco poco = new ListPracticeFlagsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPracticeFlags " +
                        "SET [PatientID] = @PatientID, [PracticeFlagID] = @PracticeFlagID, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE RowID = @RowID;";

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
