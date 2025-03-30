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
    public class PayorsEditedRepository : IPayorsEditedRepository
    {
        private readonly string _connectionString;

        public PayorsEditedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PayorsEditedDomain GetPayorsEdited(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PayorsEdited WHERE PayorsID = @id";

                    var PayorsEditedPoco = cn.QueryFirstOrDefault<PayorsEditedPoco>(query, new { id = id }) ?? new PayorsEditedPoco();

                    return PayorsEditedPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PayorsEditedDomain> GetPayorsEditeds(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PayorsEdited WHERE @criteria";
                    List<PayorsEditedPoco> pocos = cn.Query<PayorsEditedPoco>(sql).ToList();
                    List<PayorsEditedDomain> domains = new List<PayorsEditedDomain>();

                    foreach (PayorsEditedPoco poco in pocos)
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

        public int DeletePayorsEdited(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PayorsEdited WHERE PayorsID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPayorsEdited(PayorsEditedDomain domain)
        {
            int insertedId = 0;

            try
            {
                PayorsEditedPoco poco = new PayorsEditedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PayorsEdited] " +
                        "([PayorsID], [PayorName], [PlanName], [PlanCode], [PayorType], [PhoneCountry], [PhoneNumber], " +
                        "[PhoneExt], [Address1], [Address2], [City], [StateOrRegion], [StateOrRegionText], [Country], " +
                        "[PostalCode], [ClaimID], [PayorsNotes], [PayorsSubmissionType], [Deleted], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [WebSite], [EligibilityID], [Active], " +
                        "[ClaimFilingIndicatorCode], [BillingProcessingOptionID]) " +
                        "VALUES " +
                        "(@PayorsID, @PayorName, @PlanName, @PlanCode, @PayorType, @PhoneCountry, @PhoneNumber, " +
                        "@PhoneExt, @Address1, @Address2, @City, @StateOrRegion, @StateOrRegionText, @Country, " +
                        "@PostalCode, @ClaimID, @PayorsNotes, @PayorsSubmissionType, @Deleted, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @WebSite, @EligibilityID, @Active, @ClaimFilingIndicatorCode, " +
                        "@BillingProcessingOptionID); " +
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

        public int UpdatePayorsEdited(PayorsEditedDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PayorsEditedPoco poco = new PayorsEditedPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PayorsEdited " +
                        "SET [PayorName] = @PayorName, [PlanName] = @PlanName, [PlanCode] = @PlanCode, " +
                        "[PayorType] = @PayorType, [PhoneCountry] = @PhoneCountry, [PhoneNumber] = @PhoneNumber, " +
                        "[PhoneExt] = @PhoneExt, [Address1] = @Address1, [Address2] = @Address2, [City] = @City, " +
                        "[StateOrRegion] = @StateOrRegion, [StateOrRegionText] = @StateOrRegionText, " +
                        "[Country] = @Country, [PostalCode] = @PostalCode, [ClaimID] = @ClaimID, " +
                        "[PayorsNotes] = @PayorsNotes, [PayorsSubmissionType] = @PayorsSubmissionType, " +
                        "[Deleted] = @Deleted, [DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [WebSite] = @WebSite, [EligibilityID] = @EligibilityID, " +
                        "[Active] = @Active, [ClaimFilingIndicatorCode] = @ClaimFilingIndicatorCode, " +
                        "[BillingProcessingOptionID] = @BillingProcessingOptionID " +
                        "WHERE PayorsID = @PayorsID;";

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
