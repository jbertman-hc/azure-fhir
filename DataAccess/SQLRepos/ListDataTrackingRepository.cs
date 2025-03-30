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
    public class ListDataTrackingRepository : IListDataTrackingRepository
    {
        private readonly string _connectionString;

        public ListDataTrackingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListDataTrackingDomain GetListDataTracking(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListDataTracking WHERE ListDataTrackID = @id";

                    var ListDataTrackingPoco = cn.QueryFirstOrDefault<ListDataTrackingPoco>(query, new { id = id }) ?? new ListDataTrackingPoco();

                    return ListDataTrackingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListDataTrackingDomain> GetListDataTrackings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListDataTracking WHERE @criteria";
                    List<ListDataTrackingPoco> pocos = cn.Query<ListDataTrackingPoco>(sql).ToList();
                    List<ListDataTrackingDomain> domains = new List<ListDataTrackingDomain>();

                    foreach (ListDataTrackingPoco poco in pocos)
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

        public int DeleteListDataTracking(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListDataTracking WHERE ListDataTrackID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListDataTracking(ListDataTrackingDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListDataTrackingPoco poco = new ListDataTrackingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListDataTracking] " +
                        "([ListDataTrackID], [PatientID], [Item], [Date], [Value], [Comments], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ListDataTrackID, @PatientID, @Item, @Date, @Value, @Comments, @DateLastTouched, " +
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

        public int UpdateListDataTracking(ListDataTrackingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListDataTrackingPoco poco = new ListDataTrackingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListDataTracking " +
                        "SET [PatientID] = @PatientID, [Item] = @Item, [Date] = @Date, [Value] = @Value, " +
                        "[Comments] = @Comments, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE ListDataTrackID = @ListDataTrackID;";

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
