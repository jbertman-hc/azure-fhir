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
    public class ListSmokingStatusesRepository : IListSmokingStatusesRepository
    {
        private readonly string _connectionString;

        public ListSmokingStatusesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListSmokingStatusesDomain GetListSmokingStatuses(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListSmokingStatuses WHERE SmokingStatusId = @id";

                    var ListSmokingStatusesPoco = cn.QueryFirstOrDefault<ListSmokingStatusesPoco>(query, new { id = id }) ?? new ListSmokingStatusesPoco();

                    return ListSmokingStatusesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListSmokingStatusesDomain> GetListSmokingStatuses(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListSmokingStatuses WHERE @criteria";
                    List<ListSmokingStatusesPoco> pocos = cn.Query<ListSmokingStatusesPoco>(sql).ToList();
                    List<ListSmokingStatusesDomain> domains = new List<ListSmokingStatusesDomain>();

                    foreach (ListSmokingStatusesPoco poco in pocos)
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

        public int DeleteListSmokingStatuses(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListSmokingStatuses WHERE SmokingStatusId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListSmokingStatuses(ListSmokingStatusesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListSmokingStatusesPoco poco = new ListSmokingStatusesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListSmokingStatuses] " +
                        "([SmokingStatusId], [PatientId], [TobaccoCDCCode], [StartDate], [EndDate], [PendingFlag], " +
                        "[ImportedDate], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@SmokingStatusId, @PatientId, @TobaccoCDCCode, @StartDate, @EndDate, @PendingFlag, " +
                        "@ImportedDate, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListSmokingStatuses(ListSmokingStatusesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListSmokingStatusesPoco poco = new ListSmokingStatusesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListSmokingStatuses " +
                        "SET [PatientId] = @PatientId, [TobaccoCDCCode] = @TobaccoCDCCode, " +
                        "[StartDate] = @StartDate, [EndDate] = @EndDate, [PendingFlag] = @PendingFlag, " +
                        "[ImportedDate] = @ImportedDate, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE SmokingStatusId = @SmokingStatusId;";

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
