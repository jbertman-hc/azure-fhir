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
    public class ListMedsPendingRepository : IListMedsPendingRepository
    {
        private readonly string _connectionString;

        public ListMedsPendingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListMedsPendingDomain GetListMedsPending(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListMedsPending WHERE ScriptID = @id";

                    var ListMedsPendingPoco = cn.QueryFirstOrDefault<ListMedsPendingPoco>(query, new { id = id }) ?? new ListMedsPendingPoco();

                    return ListMedsPendingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListMedsPendingDomain> GetListMedsPendings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListMedsPending WHERE @criteria";
                    List<ListMedsPendingPoco> pocos = cn.Query<ListMedsPendingPoco>(sql).ToList();
                    List<ListMedsPendingDomain> domains = new List<ListMedsPendingDomain>();

                    foreach (ListMedsPendingPoco poco in pocos)
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

        public int DeleteListMedsPending(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListMedsPending WHERE ScriptID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListMedsPending(ListMedsPendingDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListMedsPendingPoco poco = new ListMedsPendingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListMedsPending] " +
                        "([ScriptID], [PatientID], [PrescribingProvider], [MedName], [MedSig], [MedNo], " +
                        "[MedRefill], [MedDNS], [DateInitiated], [DateLastRefilled], [MedComments], " +
                        "[PriorRefills], [Refillable], [Inactive], [DrugID], [MedSource], [ExternalID], " +
                        "[PharmacyID], [PharmacyTransactionID], [QuickAddWhoPrescribed], " +
                        "[QuickAddReasonPrescribed], [Deleted], [DateInactivated], [RxGUID], [DateStarted], " +
                        "[DispenseQualifier], [ERXstatus], [DAW], [IsFormularyChecked], [SentBySureScripts], " +
                        "[PharmacyTransmitFailed], [InactivateReason], [ScriptPrinted], [ScriptFaxed], " +
                        "[PendingFlag], [ImportedDate], [DateLastTouched], [LastTouchedBy], [DateRowAdded], [Source]) " +
                        "VALUES (@ScriptID, @PatientID, @PrescribingProvider, @MedName, @MedSig, @MedNo, " +
                        "@MedRefill, @MedDNS, @DateInitiated, @DateLastRefilled, @MedComments, @PriorRefills, " +
                        "@Refillable, @Inactive, @DrugID, @MedSource, @ExternalID, @PharmacyID, " +
                        "@PharmacyTransactionID, @QuickAddWhoPrescribed, @QuickAddReasonPrescribed, @Deleted, " +
                        "@DateInactivated, @RxGUID, @DateStarted, @DispenseQualifier, @ERXstatus, @DAW, " +
                        "@IsFormularyChecked, @SentBySureScripts, @PharmacyTransmitFailed, @InactivateReason, " +
                        "@ScriptPrinted, @ScriptFaxed, @PendingFlag, @ImportedDate, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @Source); " +
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

        public int UpdateListMedsPending(ListMedsPendingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListMedsPendingPoco poco = new ListMedsPendingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListMedsPending " +
                        "SET [PatientID] = @PatientID, [PrescribingProvider] = @PrescribingProvider, " +
                        "[MedName] = @MedName, [MedSig] = @MedSig, [MedNo] = @MedNo, [MedRefill] = @MedRefill, " +
                        "[MedDNS] = @MedDNS, [DateInitiated] = @DateInitiated, [DateLastRefilled] = @DateLastRefilled, " +
                        "[MedComments] = @MedComments, [PriorRefills] = @PriorRefills, [Refillable] = @Refillable, " +
                        "[Inactive] = @Inactive, [DrugID] = @DrugID, [MedSource] = @MedSource, " +
                        "[ExternalID] = @ExternalID, [PharmacyID] = @PharmacyID, " +
                        "[PharmacyTransactionID] = @PharmacyTransactionID, " +
                        "[QuickAddWhoPrescribed] = @QuickAddWhoPrescribed, " +
                        "[QuickAddReasonPrescribed] = @QuickAddReasonPrescribed, [Deleted] = @Deleted, " +
                        "[DateInactivated] = @DateInactivated, [RxGUID] = @RxGUID, [DateStarted] = @DateStarted, " +
                        "[DispenseQualifier] = @DispenseQualifier, [ERXstatus] = @ERXstatus, [DAW] = @DAW, " +
                        "[IsFormularyChecked] = @IsFormularyChecked, [SentBySureScripts] = @SentBySureScripts, " +
                        "[PharmacyTransmitFailed] = @PharmacyTransmitFailed, [InactivateReason] = @InactivateReason, " +
                        "[ScriptPrinted] = @ScriptPrinted, [ScriptFaxed] = @ScriptFaxed, [PendingFlag] = @PendingFlag, " +
                        "[ImportedDate] = @ImportedDate, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [Source] = @Source " +
                        "WHERE ScriptID = @ScriptID;";

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
