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
    public class EDIExceptionRepository : IEDIExceptionRepository
    {
        private readonly string _connectionString;

        public EDIExceptionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public EDIExceptionDomain GetEDIException(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM EDIException WHERE EDIExceptionID = @id";

                    var EDIExceptionPoco = cn.QueryFirstOrDefault<EDIExceptionPoco>(query, new { id = id }) ?? new EDIExceptionPoco();

                    return EDIExceptionPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<EDIExceptionDomain> GetEDIExceptions(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM EDIException WHERE @criteria";
                    List<EDIExceptionPoco> pocos = cn.Query<EDIExceptionPoco>(sql).ToList();
                    List<EDIExceptionDomain> domains = new List<EDIExceptionDomain>();

                    foreach (EDIExceptionPoco poco in pocos)
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

        public int DeleteEDIException(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM EDIException WHERE EDIExceptionID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertEDIException(EDIExceptionDomain domain)
        {
            int insertedId = 0;

            try
            {
                EDIExceptionPoco poco = new EDIExceptionPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[EDIException] " +
                        "([EDIExceptionID], [InterfaceName], [InterfaceType], [InterfaceDirection], " +
                        "[PatientChartID], [PatientFirst], [PatientMiddle], [PatientLast], [PatientDOB], " +
                        "[PatientGender], [Resolved], [ResolvedDate], [ResolvedBy], [Deleted], " +
                        "[DeletedDate], [DeletedBy], [Processed], [ProcessedDate], [ProcessedBy], " +
                        "[PatientIDMatch], [EDITxn], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@EDIExceptionID, @InterfaceName, @InterfaceType, @InterfaceDirection, " +
                        "@PatientChartID, @PatientFirst, @PatientMiddle, @PatientLast, @PatientDOB, " +
                        "@PatientGender, @Resolved, @ResolvedDate, @ResolvedBy, @Deleted, @DeletedDate, " +
                        "@DeletedBy, @Processed, @ProcessedDate, @ProcessedBy, @PatientIDMatch, @EDITxn, " +
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

        public int UpdateEDIException(EDIExceptionDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                EDIExceptionPoco poco = new EDIExceptionPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE EDIException " +
                        "SET [InterfaceName] = @InterfaceName, [InterfaceType] = @InterfaceType, " +
                        "[InterfaceDirection] = @InterfaceDirection, [PatientChartID] = @PatientChartID, " +
                        "[PatientFirst] = @PatientFirst, [PatientMiddle] = @PatientMiddle, " +
                        "[PatientLast] = @PatientLast, [PatientDOB] = @PatientDOB, " +
                        "[PatientGender] = @PatientGender, [Resolved] = @Resolved, " +
                        "[ResolvedDate] = @ResolvedDate, [ResolvedBy] = @ResolvedBy, [Deleted] = @Deleted, " +
                        "[DeletedDate] = @DeletedDate, [DeletedBy] = @DeletedBy, [Processed] = @Processed, " +
                        "[ProcessedDate] = @ProcessedDate, [ProcessedBy] = @ProcessedBy, " +
                        "[PatientIDMatch] = @PatientIDMatch, [EDITxn] = @EDITxn, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE EDIExceptionID = @EDIExceptionID;";

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
