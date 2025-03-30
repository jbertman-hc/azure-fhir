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
    public class ListOrderDiagnosesRepository : IListOrderDiagnosesRepository
    {
        private readonly string _connectionString;

        public ListOrderDiagnosesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListOrderDiagnosesDomain GetListOrderDiagnoses(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListOrderDiagnoses WHERE ListOrderDiagnosisId = @id";

                    var ListOrderDiagnosesPoco = cn.QueryFirstOrDefault<ListOrderDiagnosesPoco>(query, new { id = id }) ?? new ListOrderDiagnosesPoco();

                    return ListOrderDiagnosesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListOrderDiagnosesDomain> GetListOrderDiagnoses(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListOrderDiagnoses WHERE @criteria";
                    List<ListOrderDiagnosesPoco> pocos = cn.Query<ListOrderDiagnosesPoco>(sql).ToList();
                    List<ListOrderDiagnosesDomain> domains = new List<ListOrderDiagnosesDomain>();

                    foreach (ListOrderDiagnosesPoco poco in pocos)
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

        public int DeleteListOrderDiagnoses(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListOrderDiagnoses WHERE ListOrderDiagnosisId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListOrderDiagnoses(ListOrderDiagnosesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListOrderDiagnosesPoco poco = new ListOrderDiagnosesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListOrderDiagnoses] " +
                        "([ListOrderDiagnosisId], [ListOrderId], [IcdCode], [CostarCode], [ProblemDescription], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ListOrderDiagnosisId, @ListOrderId, @IcdCode, @CostarCode, @ProblemDescription, " +
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

        public int UpdateListOrderDiagnoses(ListOrderDiagnosesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListOrderDiagnosesPoco poco = new ListOrderDiagnosesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListOrderDiagnoses " +
                        "SET [ListOrderId] = @ListOrderId, [IcdCode] = @IcdCode, [CostarCode] = @CostarCode, " +
                        "[ProblemDescription] = @ProblemDescription, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE ListOrderDiagnosisId = @ListOrderDiagnosisId;";

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
