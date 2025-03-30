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
    public class PharmaciesRepository : IPharmaciesRepository
    {
        private readonly string _connectionString;

        public PharmaciesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PharmaciesDomain GetPharmacies(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Pharmacies WHERE PharmacyID = @id";

                    var PharmaciesPoco = cn.QueryFirstOrDefault<PharmaciesPoco>(query, new { id = id }) ?? new PharmaciesPoco();

                    return PharmaciesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PharmaciesDomain> GetPharmacies(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Pharmacies WHERE @criteria";
                    List<PharmaciesPoco> pocos = cn.Query<PharmaciesPoco>(sql).ToList();
                    List<PharmaciesDomain> domains = new List<PharmaciesDomain>();

                    foreach (PharmaciesPoco poco in pocos)
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

        public int DeletePharmacies(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Pharmacies WHERE PharmacyID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPharmacies(PharmaciesDomain domain)
        {
            int insertedId = 0;

            try
            {
                PharmaciesPoco poco = new PharmaciesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Pharmacies] " +
                        "([PharmacyID], [PharmacyName], [PharmacyPhone], [PharmacyFax], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@PharmacyID, @PharmacyName, @PharmacyPhone, @PharmacyFax, @DateLastTouched, " +
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

        public int UpdatePharmacies(PharmaciesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PharmaciesPoco poco = new PharmaciesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Pharmacies SET col1 = @col1, col2 = @col2, col3 = @col3, etc WHERE id = @id";

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
