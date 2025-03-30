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
    public class ListPayors_HistoryRepository : IListPayors_HistoryRepository
    {
        private readonly string _connectionString;

        public ListPayors_HistoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListPayors_HistoryDomain GetListPayors_History(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListPayors_History WHERE ListPayorHistoryID = @id";

                    var ListPayors_HistoryPoco = cn.QueryFirstOrDefault<ListPayors_HistoryPoco>(query, new { id = id }) ?? new ListPayors_HistoryPoco();

                    return ListPayors_HistoryPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListPayors_HistoryDomain> GetListPayors_Historys(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListPayors_History WHERE @criteria";
                    List<ListPayors_HistoryPoco> pocos = cn.Query<ListPayors_HistoryPoco>(sql).ToList();
                    List<ListPayors_HistoryDomain> domains = new List<ListPayors_HistoryDomain>();

                    foreach (ListPayors_HistoryPoco poco in pocos)
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

        public int DeleteListPayors_History(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListPayors_History WHERE ListPayorHistoryID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListPayors_History(ListPayors_HistoryDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListPayors_HistoryPoco poco = new ListPayors_HistoryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPayors_History] " +
                        "([ListPayorHistoryID], [ListPayorID], [Saved], [SubscriberID], [GuarantorID], " +
                        "[InsuranceType], [SubMemberNo], [PtMemberNo], [GroupNo], [GroupName], [CoPay], " +
                        "[StartDate], [EndDate], [Notes], [MedicareSecondaryCode], [Active], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [AcceptsAssignment]) " +
                        "VALUES " +
                        "(@ListPayorHistoryID, @ListPayorID, @Saved, @SubscriberID, @GuarantorID, " +
                        "@InsuranceType, @SubMemberNo, @PtMemberNo, @GroupNo, @GroupName, @CoPay, @StartDate, " +
                        "@EndDate, @Notes, @MedicareSecondaryCode, @Active, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @AcceptsAssignment); " +
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

        public int UpdateListPayors_History(ListPayors_HistoryDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListPayors_HistoryPoco poco = new ListPayors_HistoryPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPayors_History " +
                        "SET [ListPayorID] = @ListPayorID, [Saved] = @Saved, [SubscriberID] = @SubscriberID, " +
                        "[GuarantorID] = @GuarantorID, [InsuranceType] = @InsuranceType, " +
                        "[SubMemberNo] = @SubMemberNo, [PtMemberNo] = @PtMemberNo, [GroupNo] = @GroupNo, " +
                        "[GroupName] = @GroupName, [CoPay] = @CoPay, [StartDate] = @StartDate, " +
                        "[EndDate] = @EndDate, [Notes] = @Notes, [MedicareSecondaryCode] = @MedicareSecondaryCode, " +
                        "[Active] = @Active, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[AcceptsAssignment] = @AcceptsAssignment " +
                        "WHERE ListPayorHistoryID = @ListPayorHistoryID;";

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
