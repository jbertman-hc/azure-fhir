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
    public class ListFamilyHistoryRelationsRepository : IListFamilyHistoryRelationsRepository
    {
        private readonly string _connectionString;

        public ListFamilyHistoryRelationsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListFamilyHistoryRelationsDomain GetListFamilyHistoryRelations(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListFamilyHistoryRelations WHERE FamilyHistoryRelationId = @id";

                    var ListFamilyHistoryRelationsPoco = cn.QueryFirstOrDefault<ListFamilyHistoryRelationsPoco>(query, new { id = id }) ?? new ListFamilyHistoryRelationsPoco();

                    return ListFamilyHistoryRelationsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListFamilyHistoryRelationsDomain> GetListFamilyHistoryRelations(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListFamilyHistoryRelations WHERE @criteria";
                    List<ListFamilyHistoryRelationsPoco> pocos = cn.Query<ListFamilyHistoryRelationsPoco>(sql).ToList();
                    List<ListFamilyHistoryRelationsDomain> domains = new List<ListFamilyHistoryRelationsDomain>();

                    foreach (ListFamilyHistoryRelationsPoco poco in pocos)
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

        public int DeleteListFamilyHistoryRelations(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListFamilyHistoryRelations WHERE FamilyHistoryRelationId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListFamilyHistoryRelations(ListFamilyHistoryRelationsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListFamilyHistoryRelationsPoco poco = new ListFamilyHistoryRelationsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListFamilyHistoryRelations] " +
                        "([FamilyHistoryRelationId], [FamilyHistoryID], [RelationCode], [RelationName], " +
                        "[Gender], [BirthDate], [DateOfDeath], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded], [HasNoSignificantHealthHistory], [HasUnknownHealthHistory], [Notes]) " +
                        "VALUES " +
                        "(@FamilyHistoryRelationId, @FamilyHistoryID, @RelationCode, @RelationName, @Gender, " +
                        "@BirthDate, @DateOfDeath, @DateLastTouched, @LastTouchedBy, @DateRowAdded, " +
                        "@HasNoSignificantHealthHistory, @HasUnknownHealthHistory, @Notes); " +
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

        public int UpdateListFamilyHistoryRelations(ListFamilyHistoryRelationsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListFamilyHistoryRelationsPoco poco = new ListFamilyHistoryRelationsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListFamilyHistoryRelations " +
                        "SET [FamilyHistoryID] = @FamilyHistoryID, [RelationCode] = @RelationCode, " +
                        "[RelationName] = @RelationName, [Gender] = @Gender, [BirthDate] = @BirthDate, " +
                        "[DateOfDeath] = @DateOfDeath, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[HasNoSignificantHealthHistory] = @HasNoSignificantHealthHistory, " +
                        "[HasUnknownHealthHistory] = @HasUnknownHealthHistory, [Notes] = @Notes " +
                        "WHERE FamilyHistoryRelationId = @FamilyHistoryRelationId;";

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
