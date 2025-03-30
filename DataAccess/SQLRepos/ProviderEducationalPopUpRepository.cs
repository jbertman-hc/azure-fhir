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
    public class ProviderEducationalPopUpRepository : IProviderEducationalPopUpRepository
    {
        private readonly string _connectionString;

        public ProviderEducationalPopUpRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProviderEducationalPopUpDomain GetProviderEducationalPopUp(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ProviderEducationalPopUp WHERE ProviderId = @id";

                    var ProviderEducationalPopUpPoco = cn.QueryFirstOrDefault<ProviderEducationalPopUpPoco>(query, new { id = id }) ?? new ProviderEducationalPopUpPoco();

                    return ProviderEducationalPopUpPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ProviderEducationalPopUpDomain> GetProviderEducationalPopUps(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ProviderEducationalPopUp WHERE @criteria";
                    List<ProviderEducationalPopUpPoco> pocos = cn.Query<ProviderEducationalPopUpPoco>(sql).ToList();
                    List<ProviderEducationalPopUpDomain> domains = new List<ProviderEducationalPopUpDomain>();

                    foreach (ProviderEducationalPopUpPoco poco in pocos)
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

        public int DeleteProviderEducationalPopUp(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ProviderEducationalPopUp WHERE ProviderId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertProviderEducationalPopUp(ProviderEducationalPopUpDomain domain)
        {
            int insertedId = 0;

            try
            {
                ProviderEducationalPopUpPoco poco = new ProviderEducationalPopUpPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ProviderEducationalPopUp] " +
                        "([ProviderId], [UrlInfoId], [HasSeen], [DateRecorded], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@ProviderId, @UrlInfoId, @HasSeen, @DateRecorded, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateProviderEducationalPopUp(ProviderEducationalPopUpDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ProviderEducationalPopUpPoco poco = new ProviderEducationalPopUpPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ProviderEducationalPopUp " +
                        "SET [UrlInfoId] = @UrlInfoId, [HasSeen] = @HasSeen, [DateRecorded] = @DateRecorded, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE ProviderId = @ProviderId;";

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
