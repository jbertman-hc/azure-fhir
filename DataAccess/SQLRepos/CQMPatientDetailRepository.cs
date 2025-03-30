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
    public class CQMPatientDetailRepository : ICQMPatientDetailRepository
    {
        private readonly string _connectionString;

        public CQMPatientDetailRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CQMPatientDetailDomain GetCQMPatientDetail(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM CQMPatientDetail WHERE CQMPatientDetailId = @id";

                    var CQMPatientDetailPoco = cn.QueryFirstOrDefault<CQMPatientDetailPoco>(query, new { id = id }) ?? new CQMPatientDetailPoco();

                    return CQMPatientDetailPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CQMPatientDetailDomain> GetCQMPatientDetails(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM CQMPatientDetail WHERE @criteria";
                    List<CQMPatientDetailPoco> pocos = cn.Query<CQMPatientDetailPoco>(sql).ToList();
                    List<CQMPatientDetailDomain> domains = new List<CQMPatientDetailDomain>();

                    foreach (CQMPatientDetailPoco poco in pocos)
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

        public int DeleteCQMPatientDetail(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM CQMPatientDetail WHERE CQMPatientDetailId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertCQMPatientDetail(CQMPatientDetailDomain domain)
        {
            int insertedId = 0;

            try
            {
                CQMPatientDetailPoco poco = new CQMPatientDetailPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[CQMPatientDetail] " +
                        "([CQMPatientDetailId], [CQMPatientId], [ValueSetName], [ValueSetKey], " +
                        "[ValueSetDate], [ValueSetPrefix], [Value], [UOM], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@CQMPatientDetailId, @CQMPatientId, @ValueSetName, @ValueSetKey, " +
                        "@ValueSetDate, @ValueSetPrefix, @Value, @UOM, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateCQMPatientDetail(CQMPatientDetailDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                CQMPatientDetailPoco poco = new CQMPatientDetailPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE CQMPatientDetail " +
                        "SET [CQMPatientId] = @CQMPatientId, [ValueSetName] = @ValueSetName, " +
                        "[ValueSetKey] = @ValueSetKey, [ValueSetDate] = @ValueSetDate, " +
                        "[ValueSetPrefix] = @ValueSetPrefix, [Value] = @Value, [UOM] = @UOM, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE CQMPatientDetailId = @CQMPatientDetailId;";

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
