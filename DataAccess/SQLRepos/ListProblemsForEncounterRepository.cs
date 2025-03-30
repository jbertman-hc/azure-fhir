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
    public class ListProblemsForEncounterRepository : IListProblemsForEncounterRepository
    {
        private readonly string _connectionString;

        public ListProblemsForEncounterRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListProblemsForEncounterDomain GetListProblemsForEncounter(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListProblemsForEncounter WHERE ListProblemForEncounterID = @id";

                    var ListProblemsForEncounterPoco = cn.QueryFirstOrDefault<ListProblemsForEncounterPoco>(query, new { id = id }) ?? new ListProblemsForEncounterPoco();

                    return ListProblemsForEncounterPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListProblemsForEncounterDomain> GetListProblemsForEncounters(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListProblemsForEncounter WHERE @criteria";
                    List<ListProblemsForEncounterPoco> pocos = cn.Query<ListProblemsForEncounterPoco>(sql).ToList();
                    List<ListProblemsForEncounterDomain> domains = new List<ListProblemsForEncounterDomain>();

                    foreach (ListProblemsForEncounterPoco poco in pocos)
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

        public int DeleteListProblemsForEncounter(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListProblemsForEncounter WHERE ListProblemForEncounterID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListProblemsForEncounter(ListProblemsForEncounterDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListProblemsForEncounterPoco poco = new ListProblemsForEncounterPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListProblemsForEncounter] " +
                        "([ListProblemForEncounterID], [PatientID], [ProblemID], [EncounterDate], [Deleted], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ListProblemForEncounterID, @PatientID, @ProblemID, @EncounterDate, @Deleted, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListProblemsForEncounter(ListProblemsForEncounterDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListProblemsForEncounterPoco poco = new ListProblemsForEncounterPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListProblemsForEncounter " +
                        "SET [PatientID] = @PatientID, [ProblemID] = @ProblemID, [EncounterDate] = @EncounterDate, " +
                        "[Deleted] = @Deleted, [DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE ListProblemForEncounterID = @ListProblemForEncounterID;";

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
