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
    public class SuperbillAccountRepository : ISuperbillAccountRepository
    {
        private readonly string _connectionString;

        public SuperbillAccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SuperbillAccountDomain GetSuperbillAccount(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SuperbillAccount WHERE BillingID = @id";

                    var SuperbillAccountPoco = cn.QueryFirstOrDefault<SuperbillAccountPoco>(query, new { id = id }) ?? new SuperbillAccountPoco();

                    return SuperbillAccountPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SuperbillAccountDomain> GetSuperbillAccounts(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SuperbillAccount WHERE @criteria";
                    List<SuperbillAccountPoco> pocos = cn.Query<SuperbillAccountPoco>(sql).ToList();
                    List<SuperbillAccountDomain> domains = new List<SuperbillAccountDomain>();

                    foreach (SuperbillAccountPoco poco in pocos)
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

        public int DeleteSuperbillAccount(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SuperbillAccount WHERE BillingID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSuperbillAccount(SuperbillAccountDomain domain)
        {
            int insertedId = 0;

            try
            {
                SuperbillAccountPoco poco = new SuperbillAccountPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SuperbillAccount] " +
                        "([ID], [BillingID], [PatientID], [TotalCharges], [Copay], [Insur1], [Insur2], [Other], " +
                        "[Adjustments], [Balance], [BillingComments], [SavedBy], [SavedDate], [commentCopay], " +
                        "[commentIns1], [commentIns2], [commentOther], [commentAdjust], [commentBalance], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @BillingID, @PatientID, @TotalCharges, @Copay, @Insur1, @Insur2, @Other, @Adjustments, " +
                        "@Balance, @BillingComments, @SavedBy, @SavedDate, @commentCopay, @commentIns1, @commentIns2, " +
                        "@commentOther, @commentAdjust, @commentBalance, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateSuperbillAccount(SuperbillAccountDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SuperbillAccountPoco poco = new SuperbillAccountPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SuperbillAccount " +
                        "SET [BillingID] = @BillingID, [PatientID] = @PatientID, [TotalCharges] = @TotalCharges, " +
                        "[Copay] = @Copay, [Insur1] = @Insur1, [Insur2] = @Insur2, [Other] = @Other, " +
                        "[Adjustments] = @Adjustments, [Balance] = @Balance, [BillingComments] = @BillingComments, " +
                        "[SavedBy] = @SavedBy, [SavedDate] = @SavedDate, [commentCopay] = @commentCopay, " +
                        "[commentIns1] = @commentIns1, [commentIns2] = @commentIns2, [commentOther] = @commentOther, " +
                        "[commentAdjust] = @commentAdjust, [commentBalance] = @commentBalance, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE ID = @ID;";

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
