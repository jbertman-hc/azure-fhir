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
    public class ListRecordReleasesRepository : IListRecordReleasesRepository
    {
        private readonly string _connectionString;

        public ListRecordReleasesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListRecordReleasesDomain GetListRecordReleases(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListRecordReleases WHERE RowID = @id";

                    var ListRecordReleasesPoco = cn.QueryFirstOrDefault<ListRecordReleasesPoco>(query, new { id = id }) ?? new ListRecordReleasesPoco();

                    return ListRecordReleasesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListRecordReleasesDomain> GetListRecordReleases(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListRecordReleases WHERE @criteria";
                    List<ListRecordReleasesPoco> pocos = cn.Query<ListRecordReleasesPoco>(sql).ToList();
                    List<ListRecordReleasesDomain> domains = new List<ListRecordReleasesDomain>();

                    foreach (ListRecordReleasesPoco poco in pocos)
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

        public int DeleteListRecordReleases(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListRecordReleases WHERE RowID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListRecordReleases(ListRecordReleasesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListRecordReleasesPoco poco = new ListRecordReleasesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListRecordReleases] " +
                        "([RowID], [PatientID], [Name], [Address], [City], [State], [Zipcode], [Phone], [URL], " +
                        "[DateOfRelease], [AuthorizationField], [Reason], [ReleasedBy], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [Fax], [ReasonID], [Comments], [Method], " +
                        "[IsFullPatientRecord], [ReferralId]) " +
                        "VALUES " +
                        "(@RowID, @PatientID, @Name, @Address, @City, @State, @Zipcode, @Phone, @URL, " +
                        "@DateOfRelease, @AuthorizationField, @Reason, @ReleasedBy, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @Fax, @ReasonID, @Comments, @Method, " +
                        "@IsFullPatientRecord, @ReferralId); " +
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

        public int UpdateListRecordReleases(ListRecordReleasesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListRecordReleasesPoco poco = new ListRecordReleasesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListRecordReleases " +
                        "SET [PatientID] = @PatientID, [Name] = @Name, [Address] = @Address, [City] = @City, " +
                        "[State] = @State, [Zipcode] = @Zipcode, [Phone] = @Phone, [URL] = @URL, " +
                        "[DateOfRelease] = @DateOfRelease, [AuthorizationField] = @AuthorizationField, " +
                        "[Reason] = @Reason, [ReleasedBy] = @ReleasedBy, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [Fax] = @Fax, " +
                        "[ReasonID] = @ReasonID, [Comments] = @Comments, [Method] = @Method, " +
                        "[IsFullPatientRecord] = @IsFullPatientRecord, [ReferralId] = @ReferralId " +
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
