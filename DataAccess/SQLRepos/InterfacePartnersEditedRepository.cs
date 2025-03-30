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
    public class InterfacePartnersEditedRepository : IInterfacePartnersEditedRepository
    {
        private readonly string _connectionString;

        public InterfacePartnersEditedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public InterfacePartnersEditedDomain GetInterfacePartnersEdited(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM InterfacePartnersEdited WHERE InterfacePartnerId = @id";

                    var InterfacePartnersEditedPoco = cn.QueryFirstOrDefault<InterfacePartnersEditedPoco>(query, new { id = id }) ?? new InterfacePartnersEditedPoco();

                    return InterfacePartnersEditedPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<InterfacePartnersEditedDomain> GetInterfacePartnersEditeds(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM InterfacePartnersEdited WHERE @criteria";
                    List<InterfacePartnersEditedPoco> pocos = cn.Query<InterfacePartnersEditedPoco>(sql).ToList();
                    List<InterfacePartnersEditedDomain> domains = new List<InterfacePartnersEditedDomain>();

                    foreach (InterfacePartnersEditedPoco poco in pocos)
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

        public int DeleteInterfacePartnersEdited(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM InterfacePartnersEdited WHERE InterfacePartnerId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertInterfacePartnersEdited(InterfacePartnersEditedDomain domain)
        {
            int insertedId = 0;

            try
            {
                InterfacePartnersEditedPoco poco = new InterfacePartnersEditedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[InterfacePartnersEdited] " +
                        "([InterfacePartnerId], [InterfaceName], [InterfaceType], [CompanyName], " +
                        "[CompanyURL], [CompanyDesc], [RequiresCredentials], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [IsActive], [IsDeleted]) " +
                        "VALUES " +
                        "(@InterfacePartnerId, @InterfaceName, @InterfaceType, @CompanyName, @CompanyURL, " +
                        "@CompanyDesc, @RequiresCredentials, @DateLastTouched, @LastTouchedBy, @DateRowAdded, " +
                        "@IsActive, @IsDeleted); " +
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

        public int UpdateInterfacePartnersEdited(InterfacePartnersEditedDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                InterfacePartnersEditedPoco poco = new InterfacePartnersEditedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE InterfacePartnersEdited " +
                        "SET [InterfaceName] = @InterfaceName, [InterfaceType] = @InterfaceType, " +
                        "[CompanyName] = @CompanyName, [CompanyURL] = @CompanyURL, " +
                        "[CompanyDesc] = @CompanyDesc, [RequiresCredentials] = @RequiresCredentials, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [IsActive] = @IsActive, [IsDeleted] = @IsDeleted " +
                        "WHERE InterfacePartnerId = @InterfacePartnerId;";

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
