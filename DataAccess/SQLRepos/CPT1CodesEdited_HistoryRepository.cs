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
    public class CPT1CodesEdited_HistoryRepository : ICPT1CodesEdited_HistoryRepository
    {
        private readonly string _connectionString;

        public CPT1CodesEdited_HistoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CPT1CodesEdited_HistoryDomain GetCPT1CodesEdited_History(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM CPT1CodesEdited_History WHERE CPTID = @id";

                    var CPT1CodesEdited_HistoryPoco = cn.QueryFirstOrDefault<CPT1CodesEdited_HistoryPoco>(query, new { id = id }) ?? new CPT1CodesEdited_HistoryPoco();

                    return CPT1CodesEdited_HistoryPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CPT1CodesEdited_HistoryDomain> GetCPT1CodesEdited_Historys(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM CPT1CodesEdited_History WHERE @criteria";
                    List<CPT1CodesEdited_HistoryPoco> pocos = cn.Query<CPT1CodesEdited_HistoryPoco>(sql).ToList();
                    List<CPT1CodesEdited_HistoryDomain> domains = new List<CPT1CodesEdited_HistoryDomain>();

                    foreach (CPT1CodesEdited_HistoryPoco poco in pocos)
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

        public int DeleteCPT1CodesEdited_History(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM CPT1CodesEdited_History WHERE CPTID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertCPT1CodesEdited_History(CPT1CodesEdited_HistoryDomain domain)
        {
            int insertedId = 0;

            try
            {
                CPT1CodesEdited_HistoryPoco poco = new CPT1CodesEdited_HistoryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[CPT1CodesEdited_History] " +
                        "([CPTID], [CPTcode], [CPTdescription], [CPTcommon], [Fee], [RVU], " +
                        "[personalcode], [Deleted], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded], [PayorID], [Charge]) " +
                        "VALUES " +
                        "(@CPTID, @CPTcode, @CPTdescription, @CPTcommon, @Fee, @RVU, @personalcode, " +
                        "@Deleted, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @PayorID, @Charge); " +
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

        public int UpdateCPT1CodesEdited_History(CPT1CodesEdited_HistoryDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                CPT1CodesEdited_HistoryPoco poco = new CPT1CodesEdited_HistoryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE CPT1CodesEdited_History " +
                        "SET [CPTcode] = @CPTcode, [CPTdescription] = @CPTdescription, " +
                        "[CPTcommon] = @CPTcommon, [Fee] = @Fee, [RVU] = @RVU, " +
                        "[personalcode] = @personalcode, [Deleted] = @Deleted, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [PayorID] = @PayorID, [Charge] = @Charge " +
                        "WHERE CPTID = @CPTID;";

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
