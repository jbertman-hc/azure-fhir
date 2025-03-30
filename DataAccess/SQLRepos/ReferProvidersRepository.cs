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
    public class ReferProvidersRepository : IReferProvidersRepository
    {
        private readonly string _connectionString;

        public ReferProvidersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ReferProvidersDomain GetReferProviders(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ReferProviders WHERE ReferProviderID = @id";

                    var ReferProvidersPoco = cn.QueryFirstOrDefault<ReferProvidersPoco>(query, new { id = id }) ?? new ReferProvidersPoco();

                    return ReferProvidersPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ReferProvidersDomain> GetReferProviders(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ReferProviders WHERE @criteria";
                    List<ReferProvidersPoco> pocos = cn.Query<ReferProvidersPoco>(sql).ToList();
                    List<ReferProvidersDomain> domains = new List<ReferProvidersDomain>();

                    foreach (ReferProvidersPoco poco in pocos)
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

        public int DeleteReferProviders(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ReferProviders WHERE ReferProviderID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertReferProviders(ReferProvidersDomain domain)
        {
            int insertedId = 0;

            try
            {
                ReferProvidersPoco poco = new ReferProvidersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ReferProviders] " +
                        "([ReferProviderID], [ReferringNumber], [RefConOtherAll], [Specialty], [Lastname], " +
                        "[Firstname], [suffix], [prefix], [address1], [address2], [city], [state], [zip], [phone], " +
                        "[fax], [email], [comments], [other1], [other2], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded], [NPI]) " +
                        "VALUES " +
                        "(@ReferProviderID, @ReferringNumber, @RefConOtherAll, @Specialty, @Lastname, @Firstname, " +
                        "@suffix, @prefix, @address1, @address2, @city, @state, @zip, @phone, @fax, @email, " +
                        "@comments, @other1, @other2, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @NPI); " +
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

        public int UpdateReferProviders(ReferProvidersDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ReferProvidersPoco poco = new ReferProvidersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ReferProviders " +
                        "SET [ReferringNumber] = @ReferringNumber, [RefConOtherAll] = @RefConOtherAll, " +
                        "[Specialty] = @Specialty, [Lastname] = @Lastname, [Firstname] = @Firstname, " +
                        "[suffix] = @suffix, [prefix] = @prefix, [address1] = @address1, [address2] = @address2, " +
                        "[city] = @city, [state] = @state, [zip] = @zip, [phone] = @phone, [fax] = @fax, " +
                        "[email] = @email, [comments] = @comments, [other1] = @other1, [other2] = @other2, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [NPI] = @NPI " +
                        "WHERE ReferProviderID = @ReferProviderID;";

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
