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
    public class ListRxHubEligibilityRepository : IListRxHubEligibilityRepository
    {
        private readonly string _connectionString;

        public ListRxHubEligibilityRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListRxHubEligibilityDomain GetListRxHubEligibility(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListRxHubEligibility WHERE RxHubEligibilityID = @id";

                    var ListRxHubEligibilityPoco = cn.QueryFirstOrDefault<ListRxHubEligibilityPoco>(query, new { id = id }) ?? new ListRxHubEligibilityPoco();

                    return ListRxHubEligibilityPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListRxHubEligibilityDomain> GetListRxHubEligibilitys(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListRxHubEligibility WHERE @criteria";
                    List<ListRxHubEligibilityPoco> pocos = cn.Query<ListRxHubEligibilityPoco>(sql).ToList();
                    List<ListRxHubEligibilityDomain> domains = new List<ListRxHubEligibilityDomain>();

                    foreach (ListRxHubEligibilityPoco poco in pocos)
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

        public int DeleteListRxHubEligibility(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListRxHubEligibility WHERE RxHubEligibilityID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListRxHubEligibility(ListRxHubEligibilityDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListRxHubEligibilityPoco poco = new ListRxHubEligibilityPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListRxHubEligibility] " +
                        "([RxHubEligibilityID], [PatientID], [EligibilityGUID], [EligibilityDate], [PharmacyBenefit], " +
                        "[MailOrderBenefit], [PlanID], [PlanName], [EligibilityIndex], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [LastName], [FirstName], [MiddleName], [Suffix], " +
                        "[Address1], [Address2], [City], [State], [Zip], [DOB], [Gender], [DemographicsChanged], " +
                        "[EligibilityExpireTimestamp], [SpecialtyPharmacyBenefit], [LTCBenefit]) " +
                        "VALUES " +
                        "(@RxHubEligibilityID, @PatientID, @EligibilityGUID, @EligibilityDate, @PharmacyBenefit, " +
                        "@MailOrderBenefit, @PlanID, @PlanName, @EligibilityIndex, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @LastName, @FirstName, @MiddleName, @Suffix, @Address1, @Address2, @City, " +
                        "@State, @Zip, @DOB, @Gender, @DemographicsChanged, @EligibilityExpireTimestamp, " +
                        "@SpecialtyPharmacyBenefit, @LTCBenefit); " +
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

        public int UpdateListRxHubEligibility(ListRxHubEligibilityDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListRxHubEligibilityPoco poco = new ListRxHubEligibilityPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListRxHubEligibility " +
                        "SET [PatientID] = @PatientID, [EligibilityGUID] = @EligibilityGUID, " +
                        "[EligibilityDate] = @EligibilityDate, [PharmacyBenefit] = @PharmacyBenefit, " +
                        "[MailOrderBenefit] = @MailOrderBenefit, [PlanID] = @PlanID, [PlanName] = @PlanName, " +
                        "[EligibilityIndex] = @EligibilityIndex, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[LastName] = @LastName, [FirstName] = @FirstName, [MiddleName] = @MiddleName, " +
                        "[Suffix] = @Suffix, [Address1] = @Address1, [Address2] = @Address2, [City] = @City, " +
                        "[State] = @State, [Zip] = @Zip, [DOB] = @DOB, [Gender] = @Gender, " +
                        "[DemographicsChanged] = @DemographicsChanged, " +
                        "[EligibilityExpireTimestamp] = @EligibilityExpireTimestamp, " +
                        "[SpecialtyPharmacyBenefit] = @SpecialtyPharmacyBenefit, [LTCBenefit] = @LTCBenefit " +
                        "WHERE RxHubEligibilityID = @RxHubEligibilityID;";

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
