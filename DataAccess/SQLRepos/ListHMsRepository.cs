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
    public class ListHMsRepository : IListHMsRepository
    {
        private readonly string _connectionString;

        public ListHMsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListHMsDomain GetListHMs(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListHMs WHERE ListHMID = @id";

                    var ListHMsPoco = cn.QueryFirstOrDefault<ListHMsPoco>(query, new { id = id }) ?? new ListHMsPoco();

                    return ListHMsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListHMsDomain> GetListHMs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListHMs WHERE @criteria";
                    List<ListHMsPoco> pocos = cn.Query<ListHMsPoco>(sql).ToList();
                    List<ListHMsDomain> domains = new List<ListHMsDomain>();

                    foreach (ListHMsPoco poco in pocos)
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

        public int DeleteListHMs(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListHMs WHERE ListHMID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListHMs(ListHMsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListHMsPoco poco = new ListHMsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListHMs] " +
                        "([ListHMID], [PatientID], [HMruleName], [HmRuleID], [VaccineID], [VaccineName], " +
                        "[CompositeVaccineID], [LotNo], [DateGiven], [RecordedBy], [Volume], [Route], " +
                        "[Site], [Manufacturer], [Expiration], [Comment], [Sequence], [Result], [Type], " +
                        "[CPT], [LastTouchedBy], [DateLastTouched], [DateRowAdded], [IsGivenElsewhere], " +
                        "[PatientRefused], [VISname], [VISversion], [VISDateGiven], [Deleted], " +
                        "[DateSentToRegistry], [PatientParentRefused], [PatientHadInfection], [HowMigrated], " +
                        "[Reaction], [ReactionDate], [Locations], [NIP001], [ImmunizationId]) " +
                        "VALUES " +
                        "(@ListHMID, @PatientID, @HMruleName, @HmRuleID, @VaccineID, @VaccineName, " +
                        "@CompositeVaccineID, @LotNo, @DateGiven, @RecordedBy, @Volume, @Route, @Site, " +
                        "@Manufacturer, @Expiration, @Comment, @Sequence, @Result, @Type, @CPT, " +
                        "@LastTouchedBy, @DateLastTouched, @DateRowAdded, @IsGivenElsewhere, @PatientRefused, " +
                        "@VISname, @VISversion, @VISDateGiven, @Deleted, @DateSentToRegistry, " +
                        "@PatientParentRefused, @PatientHadInfection, @HowMigrated, @Reaction, @ReactionDate, " +
                        "@Locations, @NIP001, @ImmunizationId); " +
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

        public int UpdateListHMs(ListHMsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListHMsPoco poco = new ListHMsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListHMs " +
                        "SET [PatientID] = @PatientID, [HMruleName] = @HMruleName, [HmRuleID] = @HmRuleID, " +
                        "[VaccineID] = @VaccineID, [VaccineName] = @VaccineName, " +
                        "[CompositeVaccineID] = @CompositeVaccineID, [LotNo] = @LotNo, " +
                        "[DateGiven] = @DateGiven, [RecordedBy] = @RecordedBy, [Volume] = @Volume, " +
                        "[Route] = @Route, [Site] = @Site, [Manufacturer] = @Manufacturer, " +
                        "[Expiration] = @Expiration, [Comment] = @Comment, [Sequence] = @Sequence, " +
                        "[Result] = @Result, [Type] = @Type, [CPT] = @CPT, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateLastTouched] = @DateLastTouched, [DateRowAdded] = @DateRowAdded, " +
                        "[IsGivenElsewhere] = @IsGivenElsewhere, [PatientRefused] = @PatientRefused, " +
                        "[VISname] = @VISname, [VISversion] = @VISversion, [VISDateGiven] = @VISDateGiven, " +
                        "[Deleted] = @Deleted, [DateSentToRegistry] = @DateSentToRegistry, " +
                        "[PatientParentRefused] = @PatientParentRefused, " +
                        "[PatientHadInfection] = @PatientHadInfection, [HowMigrated] = @HowMigrated, " +
                        "[Reaction] = @Reaction, [ReactionDate] = @ReactionDate, [Locations] = @Locations, " +
                        "[NIP001] = @NIP001, [ImmunizationId] = @ImmunizationId " +
                        "WHERE ListHMID = @ListHMID;";

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
