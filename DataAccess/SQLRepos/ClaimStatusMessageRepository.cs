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
    public class ClaimStatusMessageRepository : IClaimStatusMessageRepository
    {
        private readonly string _connectionString;

        public ClaimStatusMessageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ClaimStatusMessageDomain GetClaimStatusMessage(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ClaimStatusMessage WHERE ClaimStatusMessageID = @id";

                    var ClaimStatusMessagePoco = cn.QueryFirstOrDefault<ClaimStatusMessagePoco>(query, new { id = id }) ?? new ClaimStatusMessagePoco();

                    return ClaimStatusMessagePoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ClaimStatusMessageDomain> GetClaimStatusMessages(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ClaimStatusMessage WHERE @criteria";
                    List<ClaimStatusMessagePoco> pocos = cn.Query<ClaimStatusMessagePoco>(sql).ToList();
                    List<ClaimStatusMessageDomain> domains = new List<ClaimStatusMessageDomain>();

                    foreach (ClaimStatusMessagePoco poco in pocos)
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

        public int DeleteClaimStatusMessage(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ClaimStatusMessage WHERE ClaimStatusMessageID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertClaimStatusMessage(ClaimStatusMessageDomain domain)
        {
            int insertedId = 0;

            try
            {
                ClaimStatusMessagePoco poco = new ClaimStatusMessagePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ClaimStatusMessage] " +
                        "([ClaimStatusMessageID], [Category], [Code], [Entity], [TextMessage], " +
                        "[ClaimStatusID], [ClaimStatusServLineID], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@ClaimStatusMessageID, @Category, @Code, @Entity, @TextMessage, " +
                        "@ClaimStatusID, @ClaimStatusServLineID, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded); " +
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

        public int UpdateClaimStatusMessage(ClaimStatusMessageDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ClaimStatusMessagePoco poco = new ClaimStatusMessagePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ClaimStatusMessage " +
                        "SET [Category] = @Category, [Code] = @Code, [Entity] = @Entity, " +
                        "[TextMessage] = @TextMessage, [ClaimStatusID] = @ClaimStatusID, " +
                        "[ClaimStatusServLineID] = @ClaimStatusServLineID, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE ClaimStatusMessageID = @ClaimStatusMessageID;";

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
