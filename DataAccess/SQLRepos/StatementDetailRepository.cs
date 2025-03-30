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
    public class StatementDetailRepository : IStatementDetailRepository
    {
        private readonly string _connectionString;

        public StatementDetailRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public StatementDetailDomain GetStatementDetail(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM StatementDetail WHERE StatementID = @id";

                    var StatementDetailPoco = cn.QueryFirstOrDefault<StatementDetailPoco>(query, new { id = id }) ?? new StatementDetailPoco();

                    return StatementDetailPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<StatementDetailDomain> GetStatementDetails(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM StatementDetail WHERE @criteria";
                    List<StatementDetailPoco> pocos = cn.Query<StatementDetailPoco>(sql).ToList();
                    List<StatementDetailDomain> domains = new List<StatementDetailDomain>();

                    foreach (StatementDetailPoco poco in pocos)
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

        public int DeleteStatementDetail(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM StatementDetail WHERE StatementID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertStatementDetail(StatementDetailDomain domain)
        {
            int insertedId = 0;

            try
            {
                StatementDetailPoco poco = new StatementDetailPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[StatementDetail] " +
                        "([StatementID], [BillingID], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[Sequence], [PatientChargesID], [DOS], [CPTCode], [CPTDescription], [ChargeType], " +
                        "[PatientCharges], [PatientPayment], [PatientBalance]) " +
                        "VALUES " +
                        "(@StatementID, @BillingID, @DateLastTouched, @LastTouchedBy, @DateRowAdded, " +
                        "@Sequence, @PatientChargesID, @DOS, @CPTCode, @CPTDescription, @ChargeType, " +
                        "@PatientCharges, @PatientPayment, @PatientBalance); " +
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

        public int UpdateStatementDetail(StatementDetailDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                StatementDetailPoco poco = new StatementDetailPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE StatementDetail " +
                        "SET [BillingID] = @BillingID, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [Sequence] = @Sequence, " +
                        "[PatientChargesID] = @PatientChargesID, [DOS] = @DOS, [CPTCode] = @CPTCode, " +
                        "[CPTDescription] = @CPTDescription, [ChargeType] = @ChargeType, " +
                        "[PatientCharges] = @PatientCharges, [PatientPayment] = @PatientPayment, " +
                        "[PatientBalance] = @PatientBalance " +
                        "WHERE StatementID = @StatementID;";

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
