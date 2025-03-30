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
    public class ListMEDSRepository : IListMEDSRepository
    {
        private readonly string _connectionString;

        public ListMEDSRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListMEDSDomain GetListMEDS(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListMEDS WHERE SciptID = @id";

                    var ListMEDSPoco = cn.QueryFirstOrDefault<ListMEDSPoco>(query, new { id = id }) ?? new ListMEDSPoco();

                    return ListMEDSPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListMEDSDomain> GetListMEDs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListMEDS WHERE @criteria";
                    List<ListMEDSPoco> pocos = cn.Query<ListMEDSPoco>(sql).ToList();
                    List<ListMEDSDomain> domains = new List<ListMEDSDomain>();

                    foreach (ListMEDSPoco poco in pocos)
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

        public int DeleteListMEDS(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListMEDS WHERE SciptID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListMEDS(ListMEDSDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListMEDSPoco poco = new ListMEDSPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListMEDS] " +
                        "([PatientID], [PatientName], [PrescribingProvider], [MedName], [MedSig], [MedNo], " +
                        "[MedRefill], [MedDNS], [DateInitiated], [DateLastRefilled], [MedComments], [SciptID], " +
                        "[PriorRefills], [Refillable], [Inactive], [DrugID], [MedSource], [ExternalID], " +
                        "[PharmacyID], [DateLastTouched], [LastTouchedBy], [DateRowAdded], [PharmacyTransactionID], " +
                        "[QuickAddWhoPrescribed], [QuickAddReasonPrescribed], [Deleted], [DateInactivated], " +
                        "[RxGUID], [DateStarted], [DispenseQualifier], [ERXstatus], [DAW], [IsFormularyChecked], " +
                        "[SentBySureScripts], [PharmacyTransmitFailed], [InactivateReason], [ScriptPrinted], " +
                        "[ScriptFaxed], [Source], [AdministeredDuringVisit], [Course]) " +
                        "VALUES " +
                        "(@PatientID, @PatientName, @PrescribingProvider, @MedName, @MedSig, @MedNo, @MedRefill, " +
                        "@MedDNS, @DateInitiated, @DateLastRefilled, @MedComments, @SciptID, @PriorRefills, " +
                        "@Refillable, @Inactive, @DrugID, @MedSource, @ExternalID, @PharmacyID, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @PharmacyTransactionID, @QuickAddWhoPrescribed, " +
                        "@QuickAddReasonPrescribed, @Deleted, @DateInactivated, @RxGUID, @DateStarted, " +
                        "@DispenseQualifier, @ERXstatus, @DAW, @IsFormularyChecked, @SentBySureScripts, " +
                        "@PharmacyTransmitFailed, @InactivateReason, @ScriptPrinted, @ScriptFaxed, @Source, " +
                        "@AdministeredDuringVisit, @Course); " +
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

        public int UpdateListMEDS(ListMEDSDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListMEDSPoco poco = new ListMEDSPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListMEDS " +
                        "SET [PatientName] = @PatientName, [PrescribingProvider] = @PrescribingProvider, " +
                        "[MedName] = @MedName, [MedSig] = @MedSig, [MedNo] = @MedNo, [MedRefill] = @MedRefill, " +
                        "[MedDNS] = @MedDNS, [DateInitiated] = @DateInitiated, " +
                        "[DateLastRefilled] = @DateLastRefilled, [MedComments] = @MedComments, " +
                        "[SciptID] = @SciptID, [PriorRefills] = @PriorRefills, [Refillable] = @Refillable, " +
                        "[Inactive] = @Inactive, [DrugID] = @DrugID, [MedSource] = @MedSource, " +
                        "[ExternalID] = @ExternalID, [PharmacyID] = @PharmacyID, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[PharmacyTransactionID] = @PharmacyTransactionID, " +
                        "[QuickAddWhoPrescribed] = @QuickAddWhoPrescribed, " +
                        "[QuickAddReasonPrescribed] = @QuickAddReasonPrescribed, [Deleted] = @Deleted, " +
                        "[DateInactivated] = @DateInactivated, [RxGUID] = @RxGUID, [DateStarted] = @DateStarted, " +
                        "[DispenseQualifier] = @DispenseQualifier, [ERXstatus] = @ERXstatus, [DAW] = @DAW, " +
                        "[IsFormularyChecked] = @IsFormularyChecked, [SentBySureScripts] = @SentBySureScripts, " +
                        "[PharmacyTransmitFailed] = @PharmacyTransmitFailed, [InactivateReason] = @InactivateReason, " +
                        "[ScriptPrinted] = @ScriptPrinted, [ScriptFaxed] = @ScriptFaxed, [Source] = @Source, " +
                        "[AdministeredDuringVisit] = @AdministeredDuringVisit, [Course] = @Course " +
                        "WHERE PatientID = @PatientID;";

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
