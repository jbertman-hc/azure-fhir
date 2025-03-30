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
    public class ListViewedCCDRepository : IListViewedCCDRepository
    {
        private readonly string _connectionString;

        public ListViewedCCDRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListViewedCCDDomain GetListViewedCCD(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListViewedCCD WHERE ListViewedCCDId = @id";

                    var ListViewedCCDPoco = cn.QueryFirstOrDefault<ListViewedCCDPoco>(query, new { id = id }) ?? new ListViewedCCDPoco();

                    return ListViewedCCDPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListViewedCCDDomain> GetListViewedCCDs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListViewedCCD WHERE @criteria";
                    List<ListViewedCCDPoco> pocos = cn.Query<ListViewedCCDPoco>(sql).ToList();
                    List<ListViewedCCDDomain> domains = new List<ListViewedCCDDomain>();

                    foreach (ListViewedCCDPoco poco in pocos)
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

        public int DeleteListViewedCCD(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListViewedCCD WHERE ListViewedCCDId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListViewedCCD(ListViewedCCDDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListViewedCCDPoco poco = new ListViewedCCDPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListViewedCCD] " +
                        "([PatientId], [ListViewedCCDId], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES (@PatientId, @ListViewedCCDId, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListViewedCCD(ListViewedCCDDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListViewedCCDPoco poco = new ListViewedCCDPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListViewedCCD " +
                        "SET [ListViewedCCDId] = @ListViewedCCDId, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE PatientId = @PatientId;";

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
