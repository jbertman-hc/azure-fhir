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
    public class SpecialtyQuickCodesRepository : ISpecialtyQuickCodesRepository
    {
        private readonly string _connectionString;

        public SpecialtyQuickCodesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SpecialtyQuickCodesDomain GetSpecialtyQuickCodes(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SpecialtyQuickCodes WHERE QuickCodeID = @id";

                    var SpecialtyQuickCodesPoco = cn.QueryFirstOrDefault<SpecialtyQuickCodesPoco>(query, new { id = id }) ?? new SpecialtyQuickCodesPoco();

                    return SpecialtyQuickCodesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SpecialtyQuickCodesDomain> GetSpecialtyQuickCodes(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SpecialtyQuickCodes WHERE @criteria";
                    List<SpecialtyQuickCodesPoco> pocos = cn.Query<SpecialtyQuickCodesPoco>(sql).ToList();
                    List<SpecialtyQuickCodesDomain> domains = new List<SpecialtyQuickCodesDomain>();

                    foreach (SpecialtyQuickCodesPoco poco in pocos)
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

        public int DeleteSpecialtyQuickCodes(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SpecialtyQuickCodes WHERE QuickCodeID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSpecialtyQuickCodes(SpecialtyQuickCodesDomain domain)
        {
            int insertedId = 0;

            try
            {
                SpecialtyQuickCodesPoco poco = new SpecialtyQuickCodesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SpecialtyQuickCodes] " +
                        "([QuickCodeID], [SpecialtyID], [QuickCode], [CodeDesc], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@QuickCodeID, @SpecialtyID, @QuickCode, @CodeDesc, @DateLastTouched, " +
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

        public int UpdateSpecialtyQuickCodes(SpecialtyQuickCodesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SpecialtyQuickCodesPoco poco = new SpecialtyQuickCodesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SpecialtyQuickCodes " +
                        "SET [SpecialtyID] = @SpecialtyID, [QuickCode] = @QuickCode, [CodeDesc] = @CodeDesc, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE QuickCodeID = @QuickCodeID;";

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
