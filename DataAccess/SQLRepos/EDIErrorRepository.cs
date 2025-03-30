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
    public class EDIErrorRepository : IEDIErrorRepository
    {
        private readonly string _connectionString;

        public EDIErrorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public EDIErrorDomain GetEDIError(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM EDIError WHERE EDIErrorID = @id";

                    var EDIErrorPoco = cn.QueryFirstOrDefault<EDIErrorPoco>(query, new { id = id }) ?? new EDIErrorPoco();

                    return EDIErrorPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<EDIErrorDomain> GetEDIErrors(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM EDIError WHERE @criteria";
                    List<EDIErrorPoco> pocos = cn.Query<EDIErrorPoco>(sql).ToList();
                    List<EDIErrorDomain> domains = new List<EDIErrorDomain>();

                    foreach (EDIErrorPoco poco in pocos)
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

        public int DeleteEDIError(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM EDIError WHERE EDIErrorID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertEDIError(EDIErrorDomain domain)
        {
            int insertedId = 0;

            try
            {
                EDIErrorPoco poco = new EDIErrorPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[EDIError] " +
                        "([EDIErrorID], [CreatedDt], [CreatedBy], [LastUpdDt], [LastUpdBy], [EDITxnTypeCode], " +
                        "[LabCompany], [PatientID], [LabTestID], [EDIMsgDt], [ErrorMsg], [EDITxn], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [Reviewed], [ReviewedDate], " +
                        "[ReviewedBy], [ReviewedComment]) " +
                        "VALUES " +
                        "(@EDIErrorID, @CreatedDt, @CreatedBy, @LastUpdDt, @LastUpdBy, @EDITxnTypeCode, " +
                        "@LabCompany, @PatientID, @LabTestID, @EDIMsgDt, @ErrorMsg, @EDITxn, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @Reviewed, @ReviewedDate, @ReviewedBy, @ReviewedComment); " +
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

        public int UpdateEDIError(EDIErrorDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                EDIErrorPoco poco = new EDIErrorPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE EDIError " +
                        "SET [CreatedDt] = @CreatedDt, [CreatedBy] = @CreatedBy, [LastUpdDt] = @LastUpdDt, " +
                        "[LastUpdBy] = @LastUpdBy, [EDITxnTypeCode] = @EDITxnTypeCode, " +
                        "[LabCompany] = @LabCompany, [PatientID] = @PatientID, [LabTestID] = @LabTestID, " +
                        "[EDIMsgDt] = @EDIMsgDt, [ErrorMsg] = @ErrorMsg, [EDITxn] = @EDITxn, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [Reviewed] = @Reviewed, " +
                        "[ReviewedDate] = @ReviewedDate, [ReviewedBy] = @ReviewedBy, " +
                        "[ReviewedComment] = @ReviewedComment " +
                        "WHERE EDIErrorID = @EDIErrorID;";

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
