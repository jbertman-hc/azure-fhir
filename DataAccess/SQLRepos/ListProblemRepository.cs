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
    public class ListProblemRepository : IListProblemRepository
    {
        private readonly string _connectionString;

        public ListProblemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListProblemDomain GetListProblem(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListProblem WHERE ListProblemID = @id";

                    var ListProblemPoco = cn.QueryFirstOrDefault<ListProblemPoco>(query, new { id = id }) ?? new ListProblemPoco();

                    return ListProblemPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListProblemDomain> GetListProblems(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListProblem WHERE @criteria";
                    List<ListProblemPoco> pocos = cn.Query<ListProblemPoco>(sql).ToList();
                    List<ListProblemDomain> domains = new List<ListProblemDomain>();

                    foreach (ListProblemPoco poco in pocos)
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

        public int DeleteListProblem(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListProblem WHERE ListProblemID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListProblem(ListProblemDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListProblemPoco poco = new ListProblemPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPROBLEM] " +
                        "([ListProblemID], [PatientID], [PatientName], [ProblemName], [ProblemICD], [DateActive], " +
                        "[DateInactive], [AddingProvider], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[Chronicity], [DateLastActivated], [DateSentToRegistry], [DateResolved], [Source], " +
                        "[COSTAR], [SNOMED], [Historical], [IcdType]) " +
                        "VALUES " +
                        "(@ListProblemID, @PatientID, @PatientName, @ProblemName, @ProblemICD, @DateActive, " +
                        "@DateInactive, @AddingProvider, @DateLastTouched, @LastTouchedBy, @DateRowAdded, " +
                        "@Chronicity, @DateLastActivated, @DateSentToRegistry, @DateResolved, @Source, @COSTAR, " +
                        "@SNOMED, @Historical, @IcdType); " +
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

        public int UpdateListProblem(ListProblemDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListProblemPoco poco = new ListProblemPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPROBLEM " +
                        "SET [PatientID] = @PatientID, [PatientName] = @PatientName, [ProblemName] = @ProblemName, " +
                        "[ProblemICD] = @ProblemICD, [DateActive] = @DateActive, [DateInactive] = @DateInactive, " +
                        "[AddingProvider] = @AddingProvider, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[Chronicity] = @Chronicity, [DateLastActivated] = @DateLastActivated, " +
                        "[DateSentToRegistry] = @DateSentToRegistry, [DateResolved] = @DateResolved, " +
                        "[Source] = @Source, [COSTAR] = @COSTAR, [SNOMED] = @SNOMED, [Historical] = @Historical, " +
                        "[IcdType] = @IcdType " +
                        "WHERE ListProblemID = @ListProblemID;";

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
