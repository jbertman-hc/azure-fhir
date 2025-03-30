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
    public class SoapICD9Repository : ISoapICD9Repository
    {
        private readonly string _connectionString;

        public SoapICD9Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SoapICD9Domain GetSoapICD9(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SoapICD9 WHERE SoapICD9ID = @id";

                    var SoapICD9Poco = cn.QueryFirstOrDefault<SoapICD9Poco>(query, new { id = id }) ?? new SoapICD9Poco();

                    return SoapICD9Poco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SoapICD9Domain> GetSoapICD9s(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SoapICD9 WHERE @criteria";
                    List<SoapICD9Poco> pocos = cn.Query<SoapICD9Poco>(sql).ToList();
                    List<SoapICD9Domain> domains = new List<SoapICD9Domain>();

                    foreach (SoapICD9Poco poco in pocos)
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

        public int DeleteSoapICD9(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SoapICD9 WHERE SoapICD9ID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSoapICD9(SoapICD9Domain domain)
        {
            int insertedId = 0;

            try
            {
                SoapICD9Poco poco = new SoapICD9Poco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SoapICD9] " +
                        "([SoapICD9ID], [SoapID], [ICD9], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES (@SoapICD9ID, @SoapID, @ICD9, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateSoapICD9(SoapICD9Domain domain)
        {
            int rowsAffected = 0;

            try
            {
                SoapICD9Poco poco = new SoapICD9Poco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SoapICD9 " +
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
