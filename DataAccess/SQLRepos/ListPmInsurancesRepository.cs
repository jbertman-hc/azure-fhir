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
    public class ListPmInsurancesRepository : IListPmInsurancesRepository
    {
        private readonly string _connectionString;

        public ListPmInsurancesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListPmInsurancesDomain GetListPmInsurances(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListPmInsurances WHERE PmInsuranceId = @id";

                    var ListPmInsurancesPoco = cn.QueryFirstOrDefault<ListPmInsurancesPoco>(query, new { id = id }) ?? new ListPmInsurancesPoco();

                    return ListPmInsurancesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListPmInsurancesDomain> GetListPmInsurances(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListPmInsurances WHERE @criteria";
                    List<ListPmInsurancesPoco> pocos = cn.Query<ListPmInsurancesPoco>(sql).ToList();
                    List<ListPmInsurancesDomain> domains = new List<ListPmInsurancesDomain>();

                    foreach (ListPmInsurancesPoco poco in pocos)
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

        public int DeleteListPmInsurances(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListPmInsurances WHERE PmInsuranceId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListPmInsurances(ListPmInsurancesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListPmInsurancesPoco poco = new ListPmInsurancesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPmInsurances] " +
                        "([PmInsuranceId], [PatientID], [InsuranceSeqNo], [PlanName], [PolicyNo], [GroupNo], " +
                        "[GroupName], [EffectiveDate], [ExpirationDate], [PatientRelToSubscriber], " +
                        "[SubscriberSalutation], [SubscriberFirstName], [SubscriberMiddleName], [SubscriberLastName], " +
                        "[SubscriberSuffix], [SubscriberDob], [CopayAmount], [InsuranceLastUpdated], [PlanPhone], " +
                        "[PlanAddressLine1], [PlanAddressLine2], [PlanCity], [PlanState], [PlanZip], [PmAccountId], " +
                        "[PayerId], [InsuranceId], [AccountId], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@PmInsuranceId, @PatientID, @InsuranceSeqNo, @PlanName, @PolicyNo, @GroupNo, @GroupName, " +
                        "@EffectiveDate, @ExpirationDate, @PatientRelToSubscriber, @SubscriberSalutation, " +
                        "@SubscriberFirstName, @SubscriberMiddleName, @SubscriberLastName, @SubscriberSuffix, " +
                        "@SubscriberDob, @CopayAmount, @InsuranceLastUpdated, @PlanPhone, @PlanAddressLine1, " +
                        "@PlanAddressLine2, @PlanCity, @PlanState, @PlanZip, @PmAccountId, @PayerId, @InsuranceId, " +
                        "@AccountId, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListPmInsurances(ListPmInsurancesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListPmInsurancesPoco poco = new ListPmInsurancesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPmInsurances " +
                        "SET [PatientID] = @PatientID, [InsuranceSeqNo] = @InsuranceSeqNo, [PlanName] = @PlanName, " +
                        "[PolicyNo] = @PolicyNo, [GroupNo] = @GroupNo, [GroupName] = @GroupName, " +
                        "[EffectiveDate] = @EffectiveDate, [ExpirationDate] = @ExpirationDate, " +
                        "[PatientRelToSubscriber] = @PatientRelToSubscriber, " +
                        "[SubscriberSalutation] = @SubscriberSalutation, " +
                        "[SubscriberFirstName] = @SubscriberFirstName, " +
                        "[SubscriberMiddleName] = @SubscriberMiddleName, " +
                        "[SubscriberLastName] = @SubscriberLastName, [SubscriberSuffix] = @SubscriberSuffix, " +
                        "[SubscriberDob] = @SubscriberDob, [CopayAmount] = @CopayAmount, " +
                        "[InsuranceLastUpdated] = @InsuranceLastUpdated, [PlanPhone] = @PlanPhone, " +
                        "[PlanAddressLine1] = @PlanAddressLine1, [PlanAddressLine2] = @PlanAddressLine2, " +
                        "[PlanCity] = @PlanCity, [PlanState] = @PlanState, [PlanZip] = @PlanZip, " +
                        "[PmAccountId] = @PmAccountId, [PayerId] = @PayerId, [InsuranceId] = @InsuranceId, " +
                        "[AccountId] = @AccountId, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE PmInsuranceId = @PmInsuranceId;";

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
