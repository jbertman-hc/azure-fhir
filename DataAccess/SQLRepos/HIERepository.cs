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
    public class HIERepository : IHIERepository
    {
        private readonly string _connectionString;

        public HIERepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public HIEDomain GetHIE(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM HIE WHERE RowID = @id";

                    var HIEPoco = cn.QueryFirstOrDefault<HIEPoco>(query, new { id = id }) ?? new HIEPoco();

                    return HIEPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<HIEDomain> GetHIEs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM HIE WHERE @criteria";
                    List<HIEPoco> pocos = cn.Query<HIEPoco>(sql).ToList();
                    List<HIEDomain> domains = new List<HIEDomain>();

                    foreach (HIEPoco poco in pocos)
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

        public int DeleteHIE(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM HIE WHERE RowID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertHIE(HIEDomain domain)
        {
            int insertedId = 0;

            try
            {
                HIEPoco poco = new HIEPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[HIE] " +
                        "([RowID], [Name], [URL], [PageTitle], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded], [GlobalLogin]) " +
                        "VALUES " +
                        "(@RowID, @Name, @URL, @PageTitle, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @GlobalLogin); " +
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

        public int UpdateHIE(HIEDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                HIEPoco poco = new HIEPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE HIE " +
                        "SET [Name] = @Name, [URL] = @URL, [PageTitle] = @PageTitle, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [GlobalLogin] = @GlobalLogin " +
                        "WHERE RowID = @RowID;";

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
