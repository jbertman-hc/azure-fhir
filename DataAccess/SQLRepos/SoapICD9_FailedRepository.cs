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
    public class SoapICD9_FailedRepository : ISoapICD9_FailedRepository
    {
        private readonly string _connectionString;

        public SoapICD9_FailedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SoapICD9_FailedDomain GetSoapICD9_Failed(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SoapICD9_Failed WHERE SoapICD9ID = @id";

                    var SoapICD9_FailedPoco = cn.QueryFirstOrDefault<SoapICD9_FailedPoco>(query, new { id = id }) ?? new SoapICD9_FailedPoco();

                    return SoapICD9_FailedPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SoapICD9_FailedDomain> GetSoapICD9_Faileds(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SoapICD9_Failed WHERE @criteria";
                    List<SoapICD9_FailedPoco> pocos = cn.Query<SoapICD9_FailedPoco>(sql).ToList();
                    List<SoapICD9_FailedDomain> domains = new List<SoapICD9_FailedDomain>();

                    foreach (SoapICD9_FailedPoco poco in pocos)
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

        public int DeleteSoapICD9_Failed(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SoapICD9_Failed WHERE SoapICD9ID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSoapICD9_Failed(SoapICD9_FailedDomain domain)
        {
            int insertedId = 0;

            try
            {
                SoapICD9_FailedPoco poco = new SoapICD9_FailedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SoapICD9_Failed] " +
                        "([SoapICD9ID], [SoapID], [ICD9], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@SoapICD9ID, @SoapID, @ICD9, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateSoapICD9_Failed(SoapICD9_FailedDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SoapICD9_FailedPoco poco = new SoapICD9_FailedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SoapICD9_Failed " +
                        "SET [SoapID] = @SoapID, [ICD9] = @ICD9, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE SoapICD9ID = @SoapICD9ID;";

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
