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
    public class ListProblemsPendingRepository : IListProblemsPendingRepository
    {
        private readonly string _connectionString;

        public ListProblemsPendingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListProblemsPendingDomain GetListProblemsPending(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListProblemsPending WHERE ListProblemID = @id";

                    var ListProblemsPendingPoco = cn.QueryFirstOrDefault<ListProblemsPendingPoco>(query, new { id = id }) ?? new ListProblemsPendingPoco();

                    return ListProblemsPendingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListProblemsPendingDomain> GetListProblemsPendings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListProblemsPending WHERE @criteria";
                    List<ListProblemsPendingPoco> pocos = cn.Query<ListProblemsPendingPoco>(sql).ToList();
                    List<ListProblemsPendingDomain> domains = new List<ListProblemsPendingDomain>();

                    foreach (ListProblemsPendingPoco poco in pocos)
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

        public int DeleteListProblemsPending(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListProblemsPending WHERE ListProblemID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListProblemsPending(ListProblemsPendingDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListProblemsPendingPoco poco = new ListProblemsPendingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListProblemsPending] " +
                        "([ListProblemID], [PatientID], [ProblemICD], [ProblemName], [DateActive], [DateInactive], " +
                        "[AddingProvider], [Chronicity], [DateLastActivated], [DateSentToRegistry], [DateResolved], " +
                        "[PendingFlag], [ImportedDate], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[Source], [COSTAR], [SNOMED], [IcdType]) " +
                        "VALUES " +
                        "(@ListProblemID, @PatientID, @ProblemICD, @ProblemName, @DateActive, @DateInactive, " +
                        "@AddingProvider, @Chronicity, @DateLastActivated, @DateSentToRegistry, @DateResolved, " +
                        "@PendingFlag, @ImportedDate, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @Source, " +
                        "@COSTAR, @SNOMED, @IcdType); " +
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

        public int UpdateListProblemsPending(ListProblemsPendingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListProblemsPendingPoco poco = new ListProblemsPendingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListProblemsPending " +
                        "SET [PatientID] = @PatientID, [ProblemICD] = @ProblemICD, [ProblemName] = @ProblemName, " +
                        "[DateActive] = @DateActive, [DateInactive] = @DateInactive, [AddingProvider] = @AddingProvider, " +
                        "[Chronicity] = @Chronicity, [DateLastActivated] = @DateLastActivated, " +
                        "[DateSentToRegistry] = @DateSentToRegistry, [DateResolved] = @DateResolved, " +
                        "[PendingFlag] = @PendingFlag, [ImportedDate] = @ImportedDate, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [Source] = @Source, [COSTAR] = @COSTAR, " +
                        "[SNOMED] = @SNOMED, [IcdType] = @IcdType " +
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
