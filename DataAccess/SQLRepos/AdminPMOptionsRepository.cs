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
    public class AdminPMOptionsRepository : IAdminPMOptionsRepository
    {
        private readonly string _connectionString;

        public AdminPMOptionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AdminPMOptionsDomain GetAdminPMOptions(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM AdminPMOptions WHERE AdminPMOptionsID = @id";

                    var AdminPMOptionsPoco = cn.QueryFirstOrDefault<AdminPMOptionsPoco>(query, new { id = id }) ?? new AdminPMOptionsPoco();

                    return AdminPMOptionsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AdminPMOptionsDomain> GetAdminPMOptions(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM AdminPMOptions WHERE @criteria";
                    List<AdminPMOptionsPoco> pocos = cn.Query<AdminPMOptionsPoco>(sql).ToList();
                    List<AdminPMOptionsDomain> domains = new List<AdminPMOptionsDomain>();

                    foreach (AdminPMOptionsPoco poco in pocos)
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

        public int DeleteAdminPMOptions(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM AdminPMOptions WHERE AdminPMOptionsID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertAdminPMOptions(AdminPMOptionsDomain domain)
        {
            int insertedId = 0;

            try
            {
                AdminPMOptionsPoco poco = new AdminPMOptionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[AdminPMOptions] " +
                        "([AdminPMOptionsID], [IsIncomingPaymentToPractice], [PaymentLocationID], " +
                        "[PMStartDate], [WarnIfPMDataInvalid], [MigratedSuperbillAccount], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded], [PMToggledOff]) " +
                        "VALUES " +
                        "(@AdminPMOptionsID, @IsIncomingPaymentToPractice, @PaymentLocationID, " +
                        "@PMStartDate, @WarnIfPMDataInvalid, @MigratedSuperbillAccount, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @PMToggledOff); " +
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

        public int UpdateAdminPMOptions(AdminPMOptionsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                AdminPMOptionsPoco poco = new AdminPMOptionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE AdminPMOptions " +
                        "SET [IsIncomingPaymentToPractice] = @IsIncomingPaymentToPractice, " +
                        "[PaymentLocationID] = @PaymentLocationID, " +
                        "[PMStartDate] = @PMStartDate, [WarnIfPMDataInvalid] = @WarnIfPMDataInvalid, " +
                        "[MigratedSuperbillAccount] = @MigratedSuperbillAccount, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [PMToggledOff] = @PMToggledOff " +
                        "WHERE AdminPMOptionsID = @AdminPMOptionsID;";

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
