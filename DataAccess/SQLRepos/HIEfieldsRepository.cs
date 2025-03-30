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
    public class HIEfieldsRepository : IHIEfieldsRepository
    {
        private readonly string _connectionString;

        public HIEfieldsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public HIEfieldsDomain GetHIEfields(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM HIEfields WHERE RowID = @id";

                    var HIEfieldsPoco = cn.QueryFirstOrDefault<HIEfieldsPoco>(query, new { id = id }) ?? new HIEfieldsPoco();

                    return HIEfieldsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<HIEfieldsDomain> GetHIEfields(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM HIEfields WHERE @criteria";
                    List<HIEfieldsPoco> pocos = cn.Query<HIEfieldsPoco>(sql).ToList();
                    List<HIEfieldsDomain> domains = new List<HIEfieldsDomain>();

                    foreach (HIEfieldsPoco poco in pocos)
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

        public int DeleteHIEfields(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM HIEfields WHERE RowID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertHIEfields(HIEfieldsDomain domain)
        {
            int insertedId = 0;

            try
            {
                HIEfieldsPoco poco = new HIEfieldsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[HIEfields] " +
                        "([RowID], [HieID], [FieldName], [FieldIndex], [FieldValue], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [ProviderCode]) " +
                        "VALUES " +
                        "(@RowID, @HieID, @FieldName, @FieldIndex, @FieldValue, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @ProviderCode); " +
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

        public int UpdateHIEfields(HIEfieldsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                HIEfieldsPoco poco = new HIEfieldsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE HIEfields " +
                        "SET [HieID] = @HieID, [FieldName] = @FieldName, [FieldIndex] = @FieldIndex, " +
                        "[FieldValue] = @FieldValue, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[ProviderCode] = @ProviderCode " +
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
