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
    public class GuardianAssociatedRepository : IGuardianAssociatedRepository
    {
        private readonly string _connectionString;

        public GuardianAssociatedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public GuardianAssociatedDomain GetGuardianAssociated(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM GuardianAssociated WHERE GuardianAssociatedID = @id";

                    var GuardianAssociatedPoco = cn.QueryFirstOrDefault<GuardianAssociatedPoco>(query, new { id = id }) ?? new GuardianAssociatedPoco();

                    return GuardianAssociatedPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<GuardianAssociatedDomain> GetGuardianAssociateds(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM GuardianAssociated WHERE @criteria";
                    List<GuardianAssociatedPoco> pocos = cn.Query<GuardianAssociatedPoco>(sql).ToList();
                    List<GuardianAssociatedDomain> domains = new List<GuardianAssociatedDomain>();

                    foreach (GuardianAssociatedPoco poco in pocos)
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

        public int DeleteGuardianAssociated(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM GuardianAssociated WHERE GuardianAssociatedID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertGuardianAssociated(GuardianAssociatedDomain domain)
        {
            int insertedId = 0;

            try
            {
                GuardianAssociatedPoco poco = new GuardianAssociatedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[GuardianAssociated] " +
                        "([GuardianAssociatedID], [PatientID], [AssociatedPartiesID], [RelationID], " +
                        "[IsPrimaryGuardian], [DateLastTouched], [LastTouchedBy], [DateRowAdded], [Comments]) " +
                        "VALUES " +
                        "(@GuardianAssociatedID, @PatientID, @AssociatedPartiesID, @RelationID, " +
                        "@IsPrimaryGuardian, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @Comments); " +
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

        public int UpdateGuardianAssociated(GuardianAssociatedDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                GuardianAssociatedPoco poco = new GuardianAssociatedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE GuardianAssociated " +
                        "SET [PatientID] = @PatientID, [AssociatedPartiesID] = @AssociatedPartiesID, " +
                        "[RelationID] = @RelationID, [IsPrimaryGuardian] = @IsPrimaryGuardian, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [Comments] = @Comments " +
                        "WHERE GuardianAssociatedID = @GuardianAssociatedID;";

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
