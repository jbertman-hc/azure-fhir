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
    public class ListProceduresRepository : IListProceduresRepository
    {
        private readonly string _connectionString;

        public ListProceduresRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListProceduresDomain GetListProcedures(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListProcedures WHERE id = @id";

                    var ListProceduresPoco = cn.QueryFirstOrDefault<ListProceduresPoco>(query, new { id = id }) ?? new ListProceduresPoco();

                    return ListProceduresPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListProceduresDomain> GetListProcedures(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListProcedures WHERE @criteria";
                    List<ListProceduresPoco> pocos = cn.Query<ListProceduresPoco>(sql).ToList();
                    List<ListProceduresDomain> domains = new List<ListProceduresDomain>();

                    foreach (ListProceduresPoco poco in pocos)
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

        public int DeleteListProcedures(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListProcedures WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListProcedures(ListProceduresDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListProceduresPoco poco = new ListProceduresPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListProcedures] " +
                        "([Id], [Code], [CodeType], [TypeName], [ProcedureDate], [Site], [Narrative], [Findings], " +
                        "[PatientId], [PatientProcedurePerformerId], [PendingFlag], [ImportedDate], [MoodCode], " +
                        "[StatusCode], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@Id, @Code, @CodeType, @TypeName, @ProcedureDate, @Site, @Narrative, @Findings, " +
                        "@PatientId, @PatientProcedurePerformerId, @PendingFlag, @ImportedDate, @MoodCode, " +
                        "@StatusCode, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListProcedures(ListProceduresDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListProceduresPoco poco = new ListProceduresPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListProcedures " +
                        "SET [Code] = @Code, [CodeType] = @CodeType, [TypeName] = @TypeName, " +
                        "[ProcedureDate] = @ProcedureDate, [Site] = @Site, [Narrative] = @Narrative, " +
                        "[Findings] = @Findings, [PatientId] = @PatientId, " +
                        "[PatientProcedurePerformerId] = @PatientProcedurePerformerId, " +
                        "[PendingFlag] = @PendingFlag, [ImportedDate] = @ImportedDate, " +
                        "[MoodCode] = @MoodCode, [StatusCode] = @StatusCode, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE Id = @Id;";

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
