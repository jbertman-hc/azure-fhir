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
    public class BillingRepository : IBillingRepository
    {
        private readonly string _connectionString;

        public BillingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BillingDomain GetBilling(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Billing WHERE BillingID = @id";

                    var BillingPoco = cn.QueryFirstOrDefault<BillingPoco>(query, new { id = id }) ?? new BillingPoco();

                    return BillingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BillingDomain> GetBillings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Billing WHERE @criteria";
                    List<BillingPoco> pocos = cn.Query<BillingPoco>(sql).ToList();
                    List<BillingDomain> domains = new List<BillingDomain>();

                    foreach (BillingPoco poco in pocos)
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

        public int DeleteBilling(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Billing WHERE BillingID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertBilling(BillingDomain domain)
        {
            int insertedId = 0;

            try
            {
                BillingPoco poco = new BillingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Billing] " +
                        "([BillingID], [PatientID], [PlaceOfService], [LocationID], [FacilityID], " +
                        "[ProviderCode], [Complexity], [CC], [Comments], [SignedOff], [BillingState], " +
                        "[TargetInsurType], [OldBillingID], [UseCurrentInsurInfo], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [DateOfService], [Historical], [IsOpen], " +
                        "[MsgID], [PmAccountId], [IcdType]) " +
                        "VALUES " +
                        "(@BillingID, @PatientID, @PlaceOfService, @LocationID, @FacilityID, " +
                        "@ProviderCode, @Complexity, @CC, @Comments, @SignedOff, @BillingState, " +
                        "@TargetInsurType, @OldBillingID, @UseCurrentInsurInfo, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded, @DateOfService, @Historical, @IsOpen, " +
                        "@MsgID, @PmAccountId, @IcdType); " +
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

        public int UpdateBilling(BillingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                BillingPoco poco = new BillingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Billing SET [PatientID] = @PatientID, " +
                        "[PlaceOfService] = @PlaceOfService, [LocationID] = @LocationID, " +
                        "[FacilityID] = @FacilityID, [ProviderCode] = @ProviderCode, " +
                        "[Complexity] = @Complexity, [CC] = @CC, [Comments] = @Comments, " +
                        "[SignedOff] = @SignedOff, [BillingState] = @BillingState, " +
                        "[TargetInsurType] = @TargetInsurType, [OldBillingID] = @OldBillingID, " +
                        "[UseCurrentInsurInfo] = @UseCurrentInsurInfo, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [DateOfService] = @DateOfService, " +
                        "[Historical] = @Historical, [IsOpen] = @IsOpen, [MsgID] = @MsgID, " +
                        "[PmAccountId] = @PmAccountId, [IcdType] = @IcdType " +
                        "WHERE BillingID = @BillingID;";

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
