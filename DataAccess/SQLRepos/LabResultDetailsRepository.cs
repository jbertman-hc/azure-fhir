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
    public class LabResultDetailsRepository : ILabResultDetailsRepository
    {
        private readonly string _connectionString;

        public LabResultDetailsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LabResultDetailsDomain GetLabResultDetails(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM LabResultDetails WHERE LabResultDetailID = @id";

                    var LabResultDetailsPoco = cn.QueryFirstOrDefault<LabResultDetailsPoco>(query, new { id = id }) ?? new LabResultDetailsPoco();

                    return LabResultDetailsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LabResultDetailsDomain> GetLabResultDetails(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM LabResultDetails WHERE @criteria";
                    List<LabResultDetailsPoco> pocos = cn.Query<LabResultDetailsPoco>(sql).ToList();
                    List<LabResultDetailsDomain> domains = new List<LabResultDetailsDomain>();

                    foreach (LabResultDetailsPoco poco in pocos)
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

        public int DeleteLabResultDetails(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM LabResultDetails WHERE LabResultDetailID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLabResultDetails(LabResultDetailsDomain domain)
        {
            int insertedId = 0;

            try
            {
                LabResultDetailsPoco poco = new LabResultDetailsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[LabResultDetails] " +
                        "([LabResultDetailID], [LabResultID], [CreatedDt], [CreatedBy], [LastUpdDt], " +
                        "[LastUpdBy], [InactiveFlag], [CorrectsLabTestID], [CorrectedByLabTestID], " +
                        "[LabTestStatus], [LabTestCode], [LoincTestCode], [ObservationSubID], [ObservationValue], " +
                        "[UOM], [ReferenceRanges], [AbnormalFlag], [NormalAbnormalType], [ReferenceRangeChangeDt], " +
                        "[SecurityAccessChecks], [ObservationSentDt], [LabLocationCode], [LabCompany], " +
                        "[ValueType], [RptFileName], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[ResponsibleObserver], [DetailSeqNo], [IsLabReport]) " +
                        "VALUES " +
                        "(@LabResultDetailID, @LabResultID, @CreatedDt, @CreatedBy, @LastUpdDt, @LastUpdBy, " +
                        "@InactiveFlag, @CorrectsLabTestID, @CorrectedByLabTestID, @LabTestStatus, @LabTestCode, " +
                        "@LoincTestCode, @ObservationSubID, @ObservationValue, @UOM, @ReferenceRanges, " +
                        "@AbnormalFlag, @NormalAbnormalType, @ReferenceRangeChangeDt, @SecurityAccessChecks, " +
                        "@ObservationSentDt, @LabLocationCode, @LabCompany, @ValueType, @RptFileName, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @ResponsibleObserver, @DetailSeqNo, @IsLabReport); " +
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

        public int UpdateLabResultDetails(LabResultDetailsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LabResultDetailsPoco poco = new LabResultDetailsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE LabResultDetails " +
                        "SET [LabResultID] = @LabResultID, [CreatedDt] = @CreatedDt, [CreatedBy] = @CreatedBy, " +
                        "[LastUpdDt] = @LastUpdDt, [LastUpdBy] = @LastUpdBy, [InactiveFlag] = @InactiveFlag, " +
                        "[CorrectsLabTestID] = @CorrectsLabTestID, [CorrectedByLabTestID] = @CorrectedByLabTestID, " +
                        "[LabTestStatus] = @LabTestStatus, [LabTestCode] = @LabTestCode, " +
                        "[LoincTestCode] = @LoincTestCode, [ObservationSubID] = @ObservationSubID, " +
                        "[ObservationValue] = @ObservationValue, [UOM] = @UOM, [ReferenceRanges] = @ReferenceRanges, " +
                        "[AbnormalFlag] = @AbnormalFlag, [NormalAbnormalType] = @NormalAbnormalType, " +
                        "[ReferenceRangeChangeDt] = @ReferenceRangeChangeDt, " +
                        "[SecurityAccessChecks] = @SecurityAccessChecks, [ObservationSentDt] = @ObservationSentDt, " +
                        "[LabLocationCode] = @LabLocationCode, [LabCompany] = @LabCompany, [ValueType] = @ValueType, " +
                        "[RptFileName] = @RptFileName, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[ResponsibleObserver] = @ResponsibleObserver, [DetailSeqNo] = @DetailSeqNo, " +
                        "[IsLabReport] = @IsLabReport " +
                        "WHERE LabResultDetailID = @LabResultDetailID;";

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
