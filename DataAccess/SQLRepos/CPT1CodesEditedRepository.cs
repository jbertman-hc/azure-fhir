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
    public class CPT1CodesEditedRepository : ICPT1CodesEditedRepository
    {
        private readonly string _connectionString;

        public CPT1CodesEditedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CPT1CodesEditedDomain GetCPT1CodesEdited(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM CPT1CodesEdited WHERE CPTID = @id";

                    var CPT1CodesEditedPoco = cn.QueryFirstOrDefault<CPT1CodesEditedPoco>(query, new { id = id }) ?? new CPT1CodesEditedPoco();

                    return CPT1CodesEditedPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CPT1CodesEditedDomain> GetCPT1CodesEditeds(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM CPT1CodesEdited WHERE @criteria";
                    List<CPT1CodesEditedPoco> pocos = cn.Query<CPT1CodesEditedPoco>(sql).ToList();
                    List<CPT1CodesEditedDomain> domains = new List<CPT1CodesEditedDomain>();

                    foreach (CPT1CodesEditedPoco poco in pocos)
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

        public int DeleteCPT1CodesEdited(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM CPT1CodesEdited WHERE CPTID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertCPT1CodesEdited(CPT1CodesEditedDomain domain)
        {
            int insertedId = 0;

            try
            {
                CPT1CodesEditedPoco poco = new CPT1CodesEditedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[CPT1CodesEdited] " +
                        "([CPTID], [CPTcode], [CPTdescription], [CPTcommon], [Fee], [RVU], " +
                        "[personalcode], [Deleted], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[PayorID], [Charge], [CPTShortDescription]) " +
                        "VALUES " +
                        "(@CPTID, @CPTcode, @CPTdescription, @CPTcommon, @Fee, @RVU, @personalcode, " +
                        "@Deleted, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @PayorID, @Charge, " +
                        "@CPTShortDescription); " +
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

        public int UpdateCPT1CodesEdited(CPT1CodesEditedDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                CPT1CodesEditedPoco poco = new CPT1CodesEditedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE CPT1CodesEdited " +
                        "SET [CPTcode] = @CPTcode, [CPTdescription] = @CPTdescription, " +
                        "[CPTcommon] = @CPTcommon, [Fee] = @Fee, [RVU] = @RVU, " +
                        "[personalcode] = @personalcode, [Deleted] = @Deleted, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [PayorID] = @PayorID, [Charge] = @Charge, " +
                        "[CPTShortDescription] = @CPTShortDescription " +
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
