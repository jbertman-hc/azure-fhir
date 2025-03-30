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
    public class LetterHistoryRepository : ILetterHistoryRepository
    {
        private readonly string _connectionString;

        public LetterHistoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LetterHistoryDomain GetLetterHistory(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM LetterHistory WHERE LetterHistoryId = @id";

                    var LetterHistoryPoco = cn.QueryFirstOrDefault<LetterHistoryPoco>(query, new { id = id }) ?? new LetterHistoryPoco();

                    return LetterHistoryPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LetterHistoryDomain> GetLetterHistorys(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM LetterHistory WHERE @criteria";
                    List<LetterHistoryPoco> pocos = cn.Query<LetterHistoryPoco>(sql).ToList();
                    List<LetterHistoryDomain> domains = new List<LetterHistoryDomain>();

                    foreach (LetterHistoryPoco poco in pocos)
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

        public int DeleteLetterHistory(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM LetterHistory WHERE LetterHistoryId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLetterHistory(LetterHistoryDomain domain)
        {
            int insertedId = 0;

            try
            {
                LetterHistoryPoco poco = new LetterHistoryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[LetterHistory] " +
                        "([LetterHistoryId], [Name], [PatientId], [Document], [DateSaved], [SavedBy], " +
                        "[RecipientId], [LetterType], [EncounterId], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@LetterHistoryId, @Name, @PatientId, @Document, @DateSaved, @SavedBy, @RecipientId, " +
                        "@LetterType, @EncounterId, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateLetterHistory(LetterHistoryDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LetterHistoryPoco poco = new LetterHistoryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE LetterHistory " +
                        "SET [Name] = @Name, [PatientId] = @PatientId, [Document] = @Document, " +
                        "[DateSaved] = @DateSaved, [SavedBy] = @SavedBy, [RecipientId] = @RecipientId, " +
                        "[LetterType] = @LetterType, [EncounterId] = @EncounterId, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE LetterHistoryId = @LetterHistoryId;";

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
