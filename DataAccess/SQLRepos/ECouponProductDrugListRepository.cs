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
    public class ECouponProductDrugListRepository : IECouponProductDrugListRepository
    {
        private readonly string _connectionString;

        public ECouponProductDrugListRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ECouponProductDrugListDomain GetECouponProductDrugList(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ECouponProductDrugList WHERE PDL_Id = @id";

                    var ECouponProductDrugListPoco = cn.QueryFirstOrDefault<ECouponProductDrugListPoco>(query, new { id = id }) ?? new ECouponProductDrugListPoco();

                    return ECouponProductDrugListPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ECouponProductDrugListDomain> GetECouponProductDrugLists(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ECouponProductDrugList WHERE @criteria";
                    List<ECouponProductDrugListPoco> pocos = cn.Query<ECouponProductDrugListPoco>(sql).ToList();
                    List<ECouponProductDrugListDomain> domains = new List<ECouponProductDrugListDomain>();

                    foreach (ECouponProductDrugListPoco poco in pocos)
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

        public int DeleteECouponProductDrugList(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ECouponProductDrugList WHERE PDL_Id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertECouponProductDrugList(ECouponProductDrugListDomain domain)
        {
            int insertedId = 0;

            try
            {
                ECouponProductDrugListPoco poco = new ECouponProductDrugListPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ECouponProductDrugList] " +
                        "([PDL_Id], [ProductType], [CodeSet], [Code], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@PDL_Id, @ProductType, @CodeSet, @Code, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateECouponProductDrugList(ECouponProductDrugListDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ECouponProductDrugListPoco poco = new ECouponProductDrugListPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ECouponProductDrugList " +
                        "SET [ProductType] = @ProductType, [CodeSet] = @CodeSet, [Code] = @Code, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE PDL_Id = @PDL_Id;";

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
