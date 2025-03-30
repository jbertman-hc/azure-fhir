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
    public class LabResultsRepository : ILabResultsRepository
    {
        private readonly string _connectionString;

        public LabResultsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LabResultsDomain GetLabResults(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM LabResults WHERE LabResultID = @id";

                    var LabResultsPoco = cn.QueryFirstOrDefault<LabResultsPoco>(query, new { id = id }) ?? new LabResultsPoco();

                    return LabResultsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LabResultsDomain> GetLabResults(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM LabResults WHERE @criteria";
                    List<LabResultsPoco> pocos = cn.Query<LabResultsPoco>(sql).ToList();
                    List<LabResultsDomain> domains = new List<LabResultsDomain>();

                    foreach (LabResultsPoco poco in pocos)
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

        public int DeleteLabResults(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM LabResults WHERE LabResultID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLabResults(LabResultsDomain domain)
        {
            int insertedId = 0;

            try
            {
                LabResultsPoco poco = new LabResultsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[LabResults] " +
                        "([LabResultID], [LabTestID], [AccessionNbrAC], [CreatedDt], [CreatedBy], [LastUpdDt], " +
                        "[LastUpdBy], [OrderingProviderID], [ElectronicOrderCreationDt], [SpecimenNbr], " +
                        "[LabTestCode], [SpecimenVolume], [SpecimenCollectedDt], [ActionCode], [ClinicalInfo], " +
                        "[SpecimenReceiptDt], [SpecimenSource], [AlternateID1], [AlternateID2], [ResultsSentDt], " +
                        "[FacilityPerformingTest], [LabTestStatus], [ParentForReflexOBX], [ParentForReflexOBR], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [OrderingProviderNotInAC], " +
                        "[SpecimenCondition], [SpecimenUniversalId], [SpecimenUniversalIdType], " +
                        "[SpecimenCollectedEndDate], [TimingStartDate], [TimingEndDate], [SpecimenType], " +
                        "[CourtesyCopyToProviderId], [SpecimenRejectReason], [AccessionNbrNamespaceId], " +
                        "[SpecimenNbrNamespaceId], [PlacerGroupId], [PlacerGroupNamespaceId], [ParentForReflexObxSubId], " +
                        "[LoincTestCode], [LabOrderingProvIDNumber], [LabOrderingProvNameTypeCode], " +
                        "[LabOrderingProvIDTypeCode], [ResultSeqNo]) " +
                        "VALUES " +
                        "(@LabResultID, @LabTestID, @AccessionNbrAC, @CreatedDt, @CreatedBy, @LastUpdDt, " +
                        "@LastUpdBy, @OrderingProviderID, @ElectronicOrderCreationDt, @SpecimenNbr, @LabTestCode, " +
                        "@SpecimenVolume, @SpecimenCollectedDt, @ActionCode, @ClinicalInfo, @SpecimenReceiptDt, " +
                        "@SpecimenSource, @AlternateID1, @AlternateID2, @ResultsSentDt, @FacilityPerformingTest, " +
                        "@LabTestStatus, @ParentForReflexOBX, @ParentForReflexOBR, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @OrderingProviderNotInAC, @SpecimenCondition, @SpecimenUniversalId, " +
                        "@SpecimenUniversalIdType, @SpecimenCollectedEndDate, @TimingStartDate, @TimingEndDate, " +
                        "@SpecimenType, @CourtesyCopyToProviderId, @SpecimenRejectReason, @AccessionNbrNamespaceId, " +
                        "@SpecimenNbrNamespaceId, @PlacerGroupId, @PlacerGroupNamespaceId, @ParentForReflexObxSubId, " +
                        "@LoincTestCode, @LabOrderingProvIDNumber, @LabOrderingProvNameTypeCode, " +
                        "@LabOrderingProvIDTypeCode, @ResultSeqNo); " +
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

        public int UpdateLabResults(LabResultsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LabResultsPoco poco = new LabResultsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE LabResults " +
                        "SET [LabTestID] = @LabTestID, [AccessionNbrAC] = @AccessionNbrAC, [CreatedDt] = @CreatedDt, " +
                        "[CreatedBy] = @CreatedBy, [LastUpdDt] = @LastUpdDt, [LastUpdBy] = @LastUpdBy, " +
                        "[OrderingProviderID] = @OrderingProviderID, " +
                        "[ElectronicOrderCreationDt] = @ElectronicOrderCreationDt, " +
                        "[SpecimenNbr] = @SpecimenNbr, [LabTestCode] = @LabTestCode, " +
                        "[SpecimenVolume] = @SpecimenVolume, [SpecimenCollectedDt] = @SpecimenCollectedDt, " +
                        "[ActionCode] = @ActionCode, [ClinicalInfo] = @ClinicalInfo, " +
                        "[SpecimenReceiptDt] = @SpecimenReceiptDt, [SpecimenSource] = @SpecimenSource, " +
                        "[AlternateID1] = @AlternateID1, [AlternateID2] = @AlternateID2, " +
                        "[ResultsSentDt] = @ResultsSentDt, [FacilityPerformingTest] = @FacilityPerformingTest, " +
                        "[LabTestStatus] = @LabTestStatus, [ParentForReflexOBX] = @ParentForReflexOBX, " +
                        "[ParentForReflexOBR] = @ParentForReflexOBR, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[OrderingProviderNotInAC] = @OrderingProviderNotInAC, [SpecimenCondition] = @SpecimenCondition, " +
                        "[SpecimenUniversalId] = @SpecimenUniversalId, [SpecimenUniversalIdType] = @SpecimenUniversalIdType, " +
                        "[SpecimenCollectedEndDate] = @SpecimenCollectedEndDate, [TimingStartDate] = @TimingStartDate, " +
                        "[TimingEndDate] = @TimingEndDate, [SpecimenType] = @SpecimenType, " +
                        "[CourtesyCopyToProviderId] = @CourtesyCopyToProviderId, " +
                        "[SpecimenRejectReason] = @SpecimenRejectReason, " +
                        "[AccessionNbrNamespaceId] = @AccessionNbrNamespaceId, " +
                        "[SpecimenNbrNamespaceId] = @SpecimenNbrNamespaceId, [PlacerGroupId] = @PlacerGroupId, " +
                        "[PlacerGroupNamespaceId] = @PlacerGroupNamespaceId, " +
                        "[ParentForReflexObxSubId] = @ParentForReflexObxSubId, [LoincTestCode] = @LoincTestCode, " +
                        "[LabOrderingProvIDNumber] = @LabOrderingProvIDNumber, " +
                        "[LabOrderingProvNameTypeCode] = @LabOrderingProvNameTypeCode, " +
                        "[LabOrderingProvIDTypeCode] = @LabOrderingProvIDTypeCode, [ResultSeqNo] = @ResultSeqNo " +
                        "WHERE LabResultID = @LabResultID;";

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
