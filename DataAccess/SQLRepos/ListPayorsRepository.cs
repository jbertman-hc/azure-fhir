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
    public class ListPayorsRepository : IListPayorsRepository
    {
        private readonly string _connectionString;

        public ListPayorsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListPayorsDomain GetListPayors(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListPayors WHERE ListPayorID = @id";

                    var ListPayorsPoco = cn.QueryFirstOrDefault<ListPayorsPoco>(query, new { id = id }) ?? new ListPayorsPoco();

                    return ListPayorsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListPayorsDomain> GetListPayors(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListPayors WHERE @criteria";
                    List<ListPayorsPoco> pocos = cn.Query<ListPayorsPoco>(sql).ToList();
                    List<ListPayorsDomain> domains = new List<ListPayorsDomain>();

                    foreach (ListPayorsPoco poco in pocos)
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

        public int DeleteListPayors(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListPayors WHERE ListPayorID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListPayors(ListPayorsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListPayorsPoco poco = new ListPayorsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPayors] " +
                        "([ListPayorID], [PatientID], [PayorsID], [SubscriberID], [GuarantorID], [InsuranceType], " +
                        "[SubMemberNo], [PtMemberNo], [GroupNo], [GroupName], [CoPay], [StartDate], [EndDate], " +
                        "[Notes], [MedicareSecondaryCode], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[Active], [AcceptsAssignment]) " +
                        "VALUES " +
                        "(@ListPayorID, @PatientID, @PayorsID, @SubscriberID, @GuarantorID, @InsuranceType, " +
                        "@SubMemberNo, @PtMemberNo, @GroupNo, @GroupName, @CoPay, @StartDate, @EndDate, @Notes, " +
                        "@MedicareSecondaryCode, @DateLastTouched, @LastTouchedBy, @DateRowAdded, " +
                        "@Active, @AcceptsAssignment); " +
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

        public int UpdateListPayors(ListPayorsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListPayorsPoco poco = new ListPayorsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPayors " +
                        "SET [PatientID] = @PatientID, [PayorsID] = @PayorsID, [SubscriberID] = @SubscriberID, " +
                        "[GuarantorID] = @GuarantorID, [InsuranceType] = @InsuranceType, [SubMemberNo] = @SubMemberNo, " +
                        "[PtMemberNo] = @PtMemberNo, [GroupNo] = @GroupNo, [GroupName] = @GroupName, " +
                        "[CoPay] = @CoPay, [StartDate] = @StartDate, [EndDate] = @EndDate, [Notes] = @Notes, " +
                        "[MedicareSecondaryCode] = @MedicareSecondaryCode, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, [Active] = @Active, " +
                        "[AcceptsAssignment] = @AcceptsAssignment " +
                        "WHERE ListPayorID = @ListPayorID;";

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
