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
    public class ListFamilyHistoryRepository : IListFamilyHistoryRepository
    {
        private readonly string _connectionString;

        public ListFamilyHistoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListFamilyHistoryDomain GetListFamilyHistory(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListFamilyHistory WHERE FamilyHistoryID = @id";

                    var ListFamilyHistoryPoco = cn.QueryFirstOrDefault<ListFamilyHistoryPoco>(query, new { id = id }) ?? new ListFamilyHistoryPoco();

                    return ListFamilyHistoryPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListFamilyHistoryDomain> GetListFamilyHistorys(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListFamilyHistory WHERE @criteria";
                    List<ListFamilyHistoryPoco> pocos = cn.Query<ListFamilyHistoryPoco>(sql).ToList();
                    List<ListFamilyHistoryDomain> domains = new List<ListFamilyHistoryDomain>();

                    foreach (ListFamilyHistoryPoco poco in pocos)
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

        public int DeleteListFamilyHistory(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListFamilyHistory WHERE FamilyHistoryID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListFamilyHistory(ListFamilyHistoryDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListFamilyHistoryPoco poco = new ListFamilyHistoryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListFamilyHistory] " +
                        "([FamilyHistoryID], [PatientId], [FamilyHistory], [PendingFlag], [ImportedDate], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [NoSignificantFamilyHealthHistory], " +
                        "[UnknownFamilyHealthHistory]) " +
                        "VALUES " +
                        "(@FamilyHistoryID, @PatientId, @FamilyHistory, @PendingFlag, @ImportedDate, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @NoSignificantFamilyHealthHistory, " +
                        "@UnknownFamilyHealthHistory); " +
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

        public int UpdateListFamilyHistory(ListFamilyHistoryDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListFamilyHistoryPoco poco = new ListFamilyHistoryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListFamilyHistory " +
                        "SET [PatientId] = @PatientId, [FamilyHistory] = @FamilyHistory, " +
                        "[PendingFlag] = @PendingFlag, [ImportedDate] = @ImportedDate, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, " +
                        "[NoSignificantFamilyHealthHistory] = @NoSignificantFamilyHealthHistory, " +
                        "[UnknownFamilyHealthHistory] = @UnknownFamilyHealthHistory " +
                        "WHERE FamilyHistoryID = @FamilyHistoryID;";

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
