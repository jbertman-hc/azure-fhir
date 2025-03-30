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
    public class PracticeFlagsRepository : IPracticeFlagsRepository
    {
        private readonly string _connectionString;

        public PracticeFlagsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PracticeFlagsDomain GetPracticeFlags(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PracticeFlags WHERE RowID = @id";

                    var PracticeFlagsPoco = cn.QueryFirstOrDefault<PracticeFlagsPoco>(query, new { id = id }) ?? new PracticeFlagsPoco();

                    return PracticeFlagsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PracticeFlagsDomain> GetPracticeFlags(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PracticeFlags WHERE @criteria";
                    List<PracticeFlagsPoco> pocos = cn.Query<PracticeFlagsPoco>(sql).ToList();
                    List<PracticeFlagsDomain> domains = new List<PracticeFlagsDomain>();

                    foreach (PracticeFlagsPoco poco in pocos)
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

        public int DeletePracticeFlags(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PracticeFlags WHERE RowID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPracticeFlags(PracticeFlagsDomain domain)
        {
            int insertedId = 0;

            try
            {
                PracticeFlagsPoco poco = new PracticeFlagsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PracticeFlags] " +
                        "([RowID], [FlagName], [Inactive], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@RowID, @FlagName, @Inactive, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdatePracticeFlags(PracticeFlagsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PracticeFlagsPoco poco = new PracticeFlagsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PracticeFlags " +
                        "SET [FlagName] = @FlagName, [Inactive] = @Inactive, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
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
