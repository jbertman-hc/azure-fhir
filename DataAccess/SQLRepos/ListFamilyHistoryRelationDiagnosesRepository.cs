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
    public class ListFamilyHistoryRelationDiagnosesRepository : IListFamilyHistoryRelationDiagnosesRepository
    {
        private readonly string _connectionString;

        public ListFamilyHistoryRelationDiagnosesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListFamilyHistoryRelationDiagnosesDomain GetListFamilyHistoryRelationDiagnoses(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListFamilyHistoryRelationDiagnoses WHERE DiagnosisId = @id";

                    var ListFamilyHistoryRelationDiagnosesPoco = cn.QueryFirstOrDefault<ListFamilyHistoryRelationDiagnosesPoco>(query, new { id = id }) ?? new ListFamilyHistoryRelationDiagnosesPoco();

                    return ListFamilyHistoryRelationDiagnosesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListFamilyHistoryRelationDiagnosesDomain> GetListFamilyHistoryRelationDiagnoses(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListFamilyHistoryRelationDiagnoses WHERE @criteria";
                    List<ListFamilyHistoryRelationDiagnosesPoco> pocos = cn.Query<ListFamilyHistoryRelationDiagnosesPoco>(sql).ToList();
                    List<ListFamilyHistoryRelationDiagnosesDomain> domains = new List<ListFamilyHistoryRelationDiagnosesDomain>();

                    foreach (ListFamilyHistoryRelationDiagnosesPoco poco in pocos)
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

        public int DeleteListFamilyHistoryRelationDiagnoses(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListFamilyHistoryRelationDiagnoses WHERE DiagnosisId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListFamilyHistoryRelationDiagnoses(ListFamilyHistoryRelationDiagnosesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListFamilyHistoryRelationDiagnosesPoco poco = new ListFamilyHistoryRelationDiagnosesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListFamilyHistoryRelationDiagnoses] " +
                        "([DiagnosisId], [FamilyHistoryRelationId], [DiagnosisCode], [Diagnosis], " +
                        "[DiagnosisDate], [AgeAtDiagnosis], [AgeUnitAtDiagnosis], [WasDiagnosisCauseOfDeath], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [DiagnosisNotes], [SnomedCode]) " +
                        "VALUES " +
                        "(@DiagnosisId, @FamilyHistoryRelationId, @DiagnosisCode, @Diagnosis, @DiagnosisDate, " +
                        "@AgeAtDiagnosis, @AgeUnitAtDiagnosis, @WasDiagnosisCauseOfDeath, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @DiagnosisNotes, @SnomedCode); " +
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

        public int UpdateListFamilyHistoryRelationDiagnoses(ListFamilyHistoryRelationDiagnosesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListFamilyHistoryRelationDiagnosesPoco poco = new ListFamilyHistoryRelationDiagnosesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListFamilyHistoryRelationDiagnoses " +
                        "SET [FamilyHistoryRelationId] = @FamilyHistoryRelationId, [DiagnosisCode] = @DiagnosisCode, " +
                        "[Diagnosis] = @Diagnosis, [DiagnosisDate] = @DiagnosisDate, [AgeAtDiagnosis] = @AgeAtDiagnosis, " +
                        "[AgeUnitAtDiagnosis] = @AgeUnitAtDiagnosis, " +
                        "[WasDiagnosisCauseOfDeath] = @WasDiagnosisCauseOfDeath, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[DiagnosisNotes] = @DiagnosisNotes, [SnomedCode] = @SnomedCode " +
                        "WHERE DiagnosisId = @DiagnosisId;";

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
