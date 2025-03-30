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
    public class EDITxnTypeRepository : IEDITxnTypeRepository
    {
        private readonly string _connectionString;

        public EDITxnTypeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public EDITxnTypeDomain GetEDITxnType(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM EDITxnType WHERE EDITxnTypeCode = @id";

                    var EDITxnTypePoco = cn.QueryFirstOrDefault<EDITxnTypePoco>(query, new { id = id }) ?? new EDITxnTypePoco();

                    return EDITxnTypePoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<EDITxnTypeDomain> GetEDITxnTypes(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM EDITxnType WHERE @criteria";
                    List<EDITxnTypePoco> pocos = cn.Query<EDITxnTypePoco>(sql).ToList();
                    List<EDITxnTypeDomain> domains = new List<EDITxnTypeDomain>();

                    foreach (EDITxnTypePoco poco in pocos)
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

        public int DeleteEDITxnType(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM EDITxnType WHERE EDITxnTypeCode = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertEDITxnType(EDITxnTypeDomain domain)
        {
            int insertedId = 0;

            try
            {
                EDITxnTypePoco poco = new EDITxnTypePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[EDITxnType] " +
                        "([EDITxnTypeCode], [EDITxnTypeDesc], [CreatedDt], [CreatedBy], [LastUpdDt], " +
                        "[LastUpdBy], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@EDITxnTypeCode, @EDITxnTypeDesc, @CreatedDt, @CreatedBy, @LastUpdDt, " +
                        "@LastUpdBy, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateEDITxnType(EDITxnTypeDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                EDITxnTypePoco poco = new EDITxnTypePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE EDITxnType " +
                        "SET [EDITxnTypeDesc] = @EDITxnTypeDesc, [CreatedDt] = @CreatedDt, " +
                        "[CreatedBy] = @CreatedBy, [LastUpdDt] = @LastUpdDt, [LastUpdBy] = @LastUpdBy, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE EDITxnTypeCode = @EDITxnTypeCode;";

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
