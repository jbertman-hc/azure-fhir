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
    public class ICD9CodesEditedRepository : IICD9CodesEditedRepository
    {
        private readonly string _connectionString;

        public ICD9CodesEditedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ICD9CodesEditedDomain GetICD9CodesEdited(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ICD9CodesEdited WHERE id = @id";

                    var ICD9CodesEditedPoco = cn.QueryFirstOrDefault<ICD9CodesEditedPoco>(query, new { id = id }) ?? new ICD9CodesEditedPoco();

                    return ICD9CodesEditedPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ICD9CodesEditedDomain> GetICD9CodesEditeds(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ICD9CodesEdited WHERE @criteria";
                    List<ICD9CodesEditedPoco> pocos = cn.Query<ICD9CodesEditedPoco>(sql).ToList();
                    List<ICD9CodesEditedDomain> domains = new List<ICD9CodesEditedDomain>();

                    foreach (ICD9CodesEditedPoco poco in pocos)
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

        public int DeleteICD9CodesEdited(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ICD9CodesEdited WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertICD9CodesEdited(ICD9CodesEditedDomain domain)
        {
            int insertedId = 0;

            try
            {
                ICD9CodesEditedPoco poco = new ICD9CodesEditedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ICD9CodesEdited] " +
                        "([ID], [Code], [Description], [personalcode], [common], [ShortDescription], " +
                        "[ShorterDescription], [IsComplete], [Deleted], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @Code, @Description, @personalcode, @common, @ShortDescription, " +
                        "@ShorterDescription, @IsComplete, @Deleted, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateICD9CodesEdited(ICD9CodesEditedDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ICD9CodesEditedPoco poco = new ICD9CodesEditedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ICD9CodesEdited " +
                        "SET [Code] = @Code, [Description] = @Description, [personalcode] = @personalcode, " +
                        "[common] = @common, [ShortDescription] = @ShortDescription, " +
                        "[ShorterDescription] = @ShorterDescription, [IsComplete] = @IsComplete, " +
                        "[Deleted] = @Deleted, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE ID = @ID;";

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
