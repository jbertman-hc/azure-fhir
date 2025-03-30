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
    public class VISInformationRepository : IVISInformationRepository
    {
        private readonly string _connectionString;

        public VISInformationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public VISInformationDomain GetVISInformation(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM VISInformation WHERE VISID = @id";

                    var VISInformationPoco = cn.QueryFirstOrDefault<VISInformationPoco>(query, new { id = id }) ?? new VISInformationPoco();

                    return VISInformationPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<VISInformationDomain> GetVISInformations(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM VISInformation WHERE @criteria";
                    List<VISInformationPoco> pocos = cn.Query<VISInformationPoco>(sql).ToList();
                    List<VISInformationDomain> domains = new List<VISInformationDomain>();

                    foreach (VISInformationPoco poco in pocos)
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

        public int DeleteVISInformation(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM VISInformation WHERE VISID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertVISInformation(VISInformationDomain domain)
        {
            int insertedId = 0;

            try
            {
                VISInformationPoco poco = new VISInformationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[VISInformation] " +
                        "([VISID], [ListHMID], [VISName], [VISVersion], [VISDateGiven], [CVXCode], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@VISID, @ListHMID, @VISName, @VISVersion, @VISDateGiven, @CVXCode, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateVISInformation(VISInformationDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                VISInformationPoco poco = new VISInformationPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE VISInformation " +
                        "SET [ListHMID] = @ListHMID, [VISName] = @VISName, [VISVersion] = @VISVersion, " +
                        "[VISDateGiven] = @VISDateGiven, [CVXCode] = @CVXCode, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE VISID = @VISID;";

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
