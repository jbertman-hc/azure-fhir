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
    public class LabTestsRepository : ILabTestsRepository
    {
        private readonly string _connectionString;

        public LabTestsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LabTestsDomain GetLabTests(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM LabTests WHERE LabTestID = @id";

                    var LabTestsPoco = cn.QueryFirstOrDefault<LabTestsPoco>(query, new { id = id }) ?? new LabTestsPoco();

                    return LabTestsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LabTestsDomain> GetLabTests(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM LabTests WHERE @criteria";
                    List<LabTestsPoco> pocos = cn.Query<LabTestsPoco>(sql).ToList();
                    List<LabTestsDomain> domains = new List<LabTestsDomain>();

                    foreach (LabTestsPoco poco in pocos)
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

        public int DeleteLabTests(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM LabTests WHERE LabTestID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLabTests(LabTestsDomain domain)
        {
            int insertedId = 0;

            try
            {
                LabTestsPoco poco = new LabTestsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[LabTests] " +
                        "([LabTestID], [PatientID], [CreatedDt], [CreatedBy], [LastUpdDt], [LastUpdBy], " +
                        "[LabOrderSentDt], [LabResultMessageDt], [LabResultReceivedDt], [LabLocationCode], " +
                        "[LabCompany], [SpecimenNbr], [BillType], [SpecimenStatus], [Fasting], [LabTestStatus], " +
                        "[SignOffID], [SignOffDt], [Comments], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[LabPatientFamilyName], [LabPatientGivenName], [LabPatientMiddleName], " +
                        "[LabPatientSuffix], [LabPatientRace], [LabPatientRaceAlternate], [LabPatientDOB], " +
                        "[LabPatientSex], [LabPatientIdNumber], [LabPatientAANamespaceId], [LabPatientIdTypeCode]) " +
                        "VALUES " +
                        "(@LabTestID, @PatientID, @CreatedDt, @CreatedBy, @LastUpdDt, @LastUpdBy, @LabOrderSentDt, " +
                        "@LabResultMessageDt, @LabResultReceivedDt, @LabLocationCode, @LabCompany, @SpecimenNbr, " +
                        "@BillType, @SpecimenStatus, @Fasting, @LabTestStatus, @SignOffID, @SignOffDt, @Comments, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @LabPatientFamilyName, " +
                        "@LabPatientGivenName, @LabPatientMiddleName, @LabPatientSuffix, @LabPatientRace, " +
                        "@LabPatientRaceAlternate, @LabPatientDOB, @LabPatientSex, @LabPatientIdNumber, " +
                        "@LabPatientAANamespaceId, @LabPatientIdTypeCode); " +
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

        public int UpdateLabTests(LabTestsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LabTestsPoco poco = new LabTestsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE LabTests " +
                        "SET [PatientID] = @PatientID, [CreatedDt] = @CreatedDt, [CreatedBy] = @CreatedBy, " +
                        "[LastUpdDt] = @LastUpdDt, [LastUpdBy] = @LastUpdBy, [LabOrderSentDt] = @LabOrderSentDt, " +
                        "[LabResultMessageDt] = @LabResultMessageDt, [LabResultReceivedDt] = @LabResultReceivedDt, " +
                        "[LabLocationCode] = @LabLocationCode, [LabCompany] = @LabCompany, [SpecimenNbr] = @SpecimenNbr, " +
                        "[BillType] = @BillType, [SpecimenStatus] = @SpecimenStatus, [Fasting] = @Fasting, " +
                        "[LabTestStatus] = @LabTestStatus, [SignOffID] = @SignOffID, [SignOffDt] = @SignOffDt, " +
                        "[Comments] = @Comments, [DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [LabPatientFamilyName] = @LabPatientFamilyName, " +
                        "[LabPatientGivenName] = @LabPatientGivenName, [LabPatientMiddleName] = @LabPatientMiddleName, " +
                        "[LabPatientSuffix] = @LabPatientSuffix, [LabPatientRace] = @LabPatientRace, " +
                        "[LabPatientRaceAlternate] = @LabPatientRaceAlternate, [LabPatientDOB] = @LabPatientDOB, " +
                        "[LabPatientSex] = @LabPatientSex, [LabPatientIdNumber] = @LabPatientIdNumber, " +
                        "[LabPatientAANamespaceId] = @LabPatientAANamespaceId, [LabPatientIdTypeCode] = @LabPatientIdTypeCode " +
                        "WHERE LabTestID = @LabTestID;";

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
