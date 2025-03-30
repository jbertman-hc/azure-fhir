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
    public class AssociatedPartiesRepository : IAssociatedPartiesRepository
    {
        private readonly string _connectionString;

        public AssociatedPartiesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AssociatedPartiesDomain GetAssociatedParties(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM AssociatedParties WHERE AssociatedPartiesID = @id";

                    var AssociatedPartiesPoco = cn.QueryFirstOrDefault<AssociatedPartiesPoco>(query, new { id = id }) ?? new AssociatedPartiesPoco();

                    return AssociatedPartiesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AssociatedPartiesDomain> GetAssociatedParties(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM AssociatedParties WHERE @criteria";
                    List<AssociatedPartiesPoco> pocos = cn.Query<AssociatedPartiesPoco>(sql).ToList();
                    List<AssociatedPartiesDomain> domains = new List<AssociatedPartiesDomain>();

                    foreach (AssociatedPartiesPoco poco in pocos)
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

        public int DeleteAssociatedParties(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM AssociatedParties WHERE AssociatedPartiesID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertAssociatedParties(AssociatedPartiesDomain domain)
        {
            int insertedId = 0;

            try
            {
                AssociatedPartiesPoco poco = new AssociatedPartiesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[AssociatedParties] " +
                        "([AssociatedPartiesID], [FirstName], [MiddleInitial], [LastName], [SSN], " +
                        "[SAGAddress1], [SAGAddress2], [City], [StateOrRegion], [StateOrRegionText], " +
                        "[PostalCode], [Country], [PhoneCountry], [PhoneNumber], [PhoneExt], [Email], " +
                        "[DOB], [Notes], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[IsNonPersonEntity], [Gender], [Employer]) " +
                        "VALUES " +
                        "(@AssociatedPartiesID, @FirstName, @MiddleInitial, @LastName, @SSN, @SAGAddress1, " +
                        "@SAGAddress2, @City, @StateOrRegion, @StateOrRegionText, @PostalCode, @Country, " +
                        "@PhoneCountry, @PhoneNumber, @PhoneExt, @Email, @DOB, @Notes, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @IsNonPersonEntity, @Gender, @Employer); " +
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

        public int UpdateAssociatedParties(AssociatedPartiesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                AssociatedPartiesPoco poco = new AssociatedPartiesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE AssociatedParties " +
                        "SET [FirstName] = @FirstName, [MiddleInitial] = @MiddleInitial, " +
                        "[LastName] = @LastName, [SSN] = @SSN, [SAGAddress1] = @SAGAddress1, " +
                        "[SAGAddress2] = @SAGAddress2, [City] = @City, [StateOrRegion] = @StateOrRegion, " +
                        "[StateOrRegionText] = @StateOrRegionText, [PostalCode] = @PostalCode, " +
                        "[Country] = @Country, [PhoneCountry] = @PhoneCountry, " +
                        "[PhoneNumber] = @PhoneNumber, [PhoneExt] = @PhoneExt, [Email] = @Email, " +
                        "[DOB] = @DOB, [Notes] = @Notes, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[IsNonPersonEntity] = @IsNonPersonEntity, [Gender] = @Gender, " +
                        "[Employer] = @Employer " +
                        "WHERE AssociatedPartiesID = @AssociatedPartiesID;";

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
