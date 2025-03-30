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
    public class SuperbillRepository : ISuperbillRepository
    {
        private readonly string _connectionString;

        public SuperbillRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SuperbillDomain GetSuperbill(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Superbill WHERE BillingID = @id";

                    var SuperbillPoco = cn.QueryFirstOrDefault<SuperbillPoco>(query, new { id = id }) ?? new SuperbillPoco();

                    return SuperbillPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SuperbillDomain> GetSuperbills(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Superbill WHERE @criteria";
                    List<SuperbillPoco> pocos = cn.Query<SuperbillPoco>(sql).ToList();
                    List<SuperbillDomain> domains = new List<SuperbillDomain>();

                    foreach (SuperbillPoco poco in pocos)
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

        public int DeleteSuperbill(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Superbill WHERE BillingID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSuperbill(SuperbillDomain domain)
        {
            int insertedId = 0;

            try
            {
                SuperbillPoco poco = new SuperbillPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[superbill] " +
                        "([BillingID], [PatientID], [DoS], [PoS], [Provider], [Complexity], [CC], [ICD0], [ICD1], " +
                        "[ICD2], [ICD3], [ICD4], [ICD5], [ICD6], [ICD7], [CPT0], [CPT1], [CPT2], [CPT3], [CPT4], " +
                        "[CPT5], [CPT6], [CPT7], [Charge0], [Charge1], [Charge2], [Charge3], [Charge4], [Charge5], " +
                        "[Charge6], [Charge7], [sbBillingComments], [ProviderID], [Miscellaneous1], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [LocFac]) " +
                        "VALUES " +
                        "(@BillingID, @PatientID, @DoS, @PoS, @Provider, @Complexity, @CC, @ICD0, @ICD1, @ICD2, " +
                        "@ICD3, @ICD4, @ICD5, @ICD6, @ICD7, @CPT0, @CPT1, @CPT2, @CPT3, @CPT4, @CPT5, @CPT6, @CPT7, " +
                        "@Charge0, @Charge1, @Charge2, @Charge3, @Charge4, @Charge5, @Charge6, @Charge7, " +
                        "@sbBillingComments, @ProviderID, @Miscellaneous1, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @LocFac); " +
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

        public int UpdateSuperbill(SuperbillDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SuperbillPoco poco = new SuperbillPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE superbill " +
                        "SET [PatientID] = @PatientID, [DoS] = @DoS, [PoS] = @PoS, [Provider] = @Provider, " +
                        "[Complexity] = @Complexity, [CC] = @CC, [ICD0] = @ICD0, [ICD1] = @ICD1, [ICD2] = @ICD2, " +
                        "[ICD3] = @ICD3, [ICD4] = @ICD4, [ICD5] = @ICD5, [ICD6] = @ICD6, [ICD7] = @ICD7, " +
                        "[CPT0] = @CPT0, [CPT1] = @CPT1, [CPT2] = @CPT2, [CPT3] = @CPT3, [CPT4] = @CPT4, " +
                        "[CPT5] = @CPT5, [CPT6] = @CPT6, [CPT7] = @CPT7, [Charge0] = @Charge0, [Charge1] = @Charge1, " +
                        "[Charge2] = @Charge2, [Charge3] = @Charge3, [Charge4] = @Charge4, [Charge5] = @Charge5, " +
                        "[Charge6] = @Charge6, [Charge7] = @Charge7, [sbBillingComments] = @sbBillingComments, " +
                        "[ProviderID] = @ProviderID, [Miscellaneous1] = @Miscellaneous1, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [LocFac] = @LocFac " +
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
