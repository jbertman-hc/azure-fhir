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
    public class AssociatedPartiesHistoryRepository : IAssociatedPartiesHistoryRepository
    {
        private readonly string _connectionString;

        public AssociatedPartiesHistoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AssociatedPartiesHistoryDomain GetAssociatedPartiesHistory(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM AssociatedPartiesHistory WHERE APHistoryID = @id";

                    var AssociatedPartiesHistoryPoco = cn.QueryFirstOrDefault<AssociatedPartiesHistoryPoco>(query, new { id = id }) ?? new AssociatedPartiesHistoryPoco();

                    return AssociatedPartiesHistoryPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AssociatedPartiesHistoryDomain> GetAssociatedPartiesHistorys(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM AssociatedPartiesHistory WHERE @criteria";
                    List<AssociatedPartiesHistoryPoco> pocos = cn.Query<AssociatedPartiesHistoryPoco>(sql).ToList();
                    List<AssociatedPartiesHistoryDomain> domains = new List<AssociatedPartiesHistoryDomain>();

                    foreach (AssociatedPartiesHistoryPoco poco in pocos)
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

        public int DeleteAssociatedPartiesHistory(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM AssociatedPartiesHistory WHERE APHistoryID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertAssociatedPartiesHistory(AssociatedPartiesHistoryDomain domain)
        {
            int insertedId = 0;

            try
            {
                AssociatedPartiesHistoryPoco poco = new AssociatedPartiesHistoryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[AssociatedParties_History] " +
                        "([APHistoryID], [AssociatedPartiesID], [RelationPatientID], [IsPatient], [FieldID], " +
                        "[OldValue], [NewValue], [DateEdited], [EditedBy], [DateLastTouched], [DateRowAdded], " +
                        "[LastTouchedBy]) VALUES (@APHistoryID, @AssociatedPartiesID, @RelationPatientID, " +
                        "@IsPatient, @FieldID, @OldValue, @NewValue, @DateEdited, @EditedBy, @DateLastTouched, " +
                        "@DateRowAdded, @LastTouchedBy); " +
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

        public int UpdateAssociatedPartiesHistory(AssociatedPartiesHistoryDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                AssociatedPartiesHistoryPoco poco = new AssociatedPartiesHistoryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE AssociatedParties_History " +
                        "SET [AssociatedPartiesID] = @AssociatedPartiesID, " +
                        "[RelationPatientID] = @RelationPatientID, [IsPatient] = @IsPatient, " +
                        "[FieldID] = @FieldID, [OldValue] = @OldValue, [NewValue] = @NewValue, " +
                        "[DateEdited] = @DateEdited, [EditedBy] = @EditedBy, " +
                        "[DateLastTouched] = @DateLastTouched, [DateRowAdded] = @DateRowAdded, " +
                        "[LastTouchedBy] = @LastTouchedBy " +
                        "WHERE APHistoryID = @APHistoryID;";

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
