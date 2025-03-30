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
    public class SoapCostarRepository : ISoapCostarRepository
    {
        private readonly string _connectionString;

        public SoapCostarRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SoapCostarDomain GetSoapCostar(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SoapCostar WHERE SoapCostarId = @id";

                    var SoapCostarPoco = cn.QueryFirstOrDefault<SoapCostarPoco>(query, new { id = id }) ?? new SoapCostarPoco();

                    return SoapCostarPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SoapCostarDomain> GetSoapCostars(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SoapCostar WHERE @criteria";
                    List<SoapCostarPoco> pocos = cn.Query<SoapCostarPoco>(sql).ToList();
                    List<SoapCostarDomain> domains = new List<SoapCostarDomain>();

                    foreach (SoapCostarPoco poco in pocos)
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

        public int DeleteSoapCostar(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SoapCostar WHERE SoapCostarId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSoapCostar(SoapCostarDomain domain)
        {
            int insertedId = 0;

            try
            {
                SoapCostarPoco poco = new SoapCostarPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SoapCostar] " +
                        "([SoapCostarId], [SoapId], [Costar], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@SoapCostarId, @SoapId, @Costar, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateSoapCostar(SoapCostarDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SoapCostarPoco poco = new SoapCostarPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SoapCostar " +
                        "SET [SoapId] = @SoapId, [Costar] = @Costar, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE SoapCostarId = @SoapCostarId;";

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
