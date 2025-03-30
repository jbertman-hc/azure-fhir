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
    public class OrdersRepository : IOrdersRepository
    {
        private readonly string _connectionString;

        public OrdersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public OrdersDomain GetOrders(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Orders WHERE PatientChargesID = @id";

                    var OrdersPoco = cn.QueryFirstOrDefault<OrdersPoco>(query, new { id = id }) ?? new OrdersPoco();

                    return OrdersPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<OrdersDomain> GetOrders(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Orders WHERE @criteria";
                    List<OrdersPoco> pocos = cn.Query<OrdersPoco>(sql).ToList();
                    List<OrdersDomain> domains = new List<OrdersDomain>();

                    foreach (OrdersPoco poco in pocos)
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

        public int DeleteOrders(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Orders WHERE PatientChargesID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertOrders(OrdersDomain domain)
        {
            int insertedId = 0;

            try
            {
                OrdersPoco poco = new OrdersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Orders] " +
                        "([ID], [Type], [Item_Name], [Item_CPT], [Item_ICD], [LOINC], [Location], [InHouseTest], " +
                        "[LastTouchedBy], [DateLastTouched], [DateRowAdded], [URL], [Comments], [HmRuleID], " +
                        "[GUID], [Inactive], [IsImm], [IsChild], [AutoAddToBill], [IsNumericalResult]) " +
                        "VALUES " +
                        "(@ID, @Type, @Item_Name, @Item_CPT, @Item_ICD, @LOINC, @Location, @InHouseTest, " +
                        "@LastTouchedBy, @DateLastTouched, @DateRowAdded, @URL, @Comments, @HmRuleID, " +
                        "@GUID, @Inactive, @IsImm, @IsChild, @AutoAddToBill, @IsNumericalResult); " +
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

        public int UpdateOrders(OrdersDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                OrdersPoco poco = new OrdersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Orders SET [Type] = @Type, [Item_Name] = @Item_Name, " +
                        "[Item_CPT] = @Item_CPT, [Item_ICD] = @Item_ICD, [LOINC] = @LOINC, " +
                        "[Location] = @Location, [InHouseTest] = @InHouseTest, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateLastTouched] = @DateLastTouched, [DateRowAdded] = @DateRowAdded, [URL] = @URL, " +
                        "[Comments] = @Comments, [HmRuleID] = @HmRuleID, [GUID] = @GUID, [Inactive] = @Inactive, " +
                        "[IsImm] = @IsImm, [IsChild] = @IsChild, [AutoAddToBill] = @AutoAddToBill, " +
                        "[IsNumericalResult] = @IsNumericalResult " +
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
