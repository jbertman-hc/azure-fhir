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
    public class IgnoredInteractionsRepository : IIgnoredInteractionsRepository
    {
        private readonly string _connectionString;

        public IgnoredInteractionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IgnoredInteractionsDomain GetIgnoredInteractions(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM IgnoredInteractions WHERE IgnoredInteractionsID = @id";

                    var IgnoredInteractionsPoco = cn.QueryFirstOrDefault<IgnoredInteractionsPoco>(query, new { id = id }) ?? new IgnoredInteractionsPoco();

                    return IgnoredInteractionsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<IgnoredInteractionsDomain> GetIgnoredInteractions(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM IgnoredInteractions WHERE @criteria";
                    List<IgnoredInteractionsPoco> pocos = cn.Query<IgnoredInteractionsPoco>(sql).ToList();
                    List<IgnoredInteractionsDomain> domains = new List<IgnoredInteractionsDomain>();

                    foreach (IgnoredInteractionsPoco poco in pocos)
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

        public int DeleteIgnoredInteractions(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM IgnoredInteractions WHERE IgnoredInteractionsID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertIgnoredInteractions(IgnoredInteractionsDomain domain)
        {
            int insertedId = 0;

            try
            {
                IgnoredInteractionsPoco poco = new IgnoredInteractionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[IgnoredInteractions] " +
                        "([IgnoredInteractionsID], [PatientID], [AllergyID], [DrugID1], [DrugID2], " +
                        "[DiseaseID], [Pregnancy], [Exceedweight], [OverrideReasonID], [OverrideReason], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [OverrideComments], " +
                        "[TypeString], [InteractionName]) " +
                        "VALUES " +
                        "(@IgnoredInteractionsID, @PatientID, @AllergyID, @DrugID1, @DrugID2, @DiseaseID, " +
                        "@Pregnancy, @Exceedweight, @OverrideReasonID, @OverrideReason, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @OverrideComments, @TypeString, @InteractionName); " +
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

        public int UpdateIgnoredInteractions(IgnoredInteractionsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                IgnoredInteractionsPoco poco = new IgnoredInteractionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE IgnoredInteractions " +
                        "SET [PatientID] = @PatientID, [AllergyID] = @AllergyID, [DrugID1] = @DrugID1, " +
                        "[DrugID2] = @DrugID2, [DiseaseID] = @DiseaseID, [Pregnancy] = @Pregnancy, " +
                        "[Exceedweight] = @Exceedweight, [OverrideReasonID] = @OverrideReasonID, " +
                        "[OverrideReason] = @OverrideReason, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[OverrideComments] = @OverrideComments, [TypeString] = @TypeString, " +
                        "[InteractionName] = @InteractionName " +
                        "WHERE IgnoredInteractionsID = @IgnoredInteractionsID;";

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
