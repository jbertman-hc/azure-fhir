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
    public class EMLineItemRepository : IEMLineItemRepository
    {
        private readonly string _connectionString;

        public EMLineItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public EMLineItemDomain GetEMLineItem(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM EMLineItem WHERE id = @id";

                    var EMLineItemPoco = cn.QueryFirstOrDefault<EMLineItemPoco>(query, new { id = id }) ?? new EMLineItemPoco();

                    return EMLineItemPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<EMLineItemDomain> GetEMLineItems(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM EMLineItem WHERE @criteria";
                    List<EMLineItemPoco> pocos = cn.Query<EMLineItemPoco>(sql).ToList();
                    List<EMLineItemDomain> domains = new List<EMLineItemDomain>();

                    foreach (EMLineItemPoco poco in pocos)
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

        public int DeleteEMLineItem(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM EMLineItem WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertEMLineItem(EMLineItemDomain domain)
        {
            int insertedId = 0;

            try
            {
                EMLineItemPoco poco = new EMLineItemPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[EMLineItem] " +
                        "([ID], [PatientID], [BillingID], [CPT], [ICDprimary], [ICDsecondary], " +
                        "[ICDtertiary], [ICDquaternary], [modifier], [Price], [BillingComments], " +
                        "[SavedBy], [SavedDate], [units], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @PatientID, @BillingID, @CPT, @ICDprimary, @ICDsecondary, @ICDtertiary, " +
                        "@ICDquaternary, @modifier, @Price, @BillingComments, @SavedBy, @SavedDate, @units, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateEMLineItem(EMLineItemDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                EMLineItemPoco poco = new EMLineItemPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE EMLineItem " +
                        "SET [PatientID] = @PatientID, [BillingID] = @BillingID, [CPT] = @CPT, " +
                        "[ICDprimary] = @ICDprimary, [ICDsecondary] = @ICDsecondary, " +
                        "[ICDtertiary] = @ICDtertiary, [ICDquaternary] = @ICDquaternary, " +
                        "[modifier] = @modifier, [Price] = @Price, [BillingComments] = @BillingComments, " +
                        "[SavedBy] = @SavedBy, [SavedDate] = @SavedDate, [units] = @units, " +
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
