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
    public class StatementSettingsRepository : IStatementSettingsRepository
    {
        private readonly string _connectionString;

        public StatementSettingsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public StatementSettingsDomain GetStatementSettings(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM StatementSettings WHERE StatementSettingsID = @id";

                    var StatementSettingsPoco = cn.QueryFirstOrDefault<StatementSettingsPoco>(query, new { id = id }) ?? new StatementSettingsPoco();

                    return StatementSettingsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<StatementSettingsDomain> GetStatementSettings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM StatementSettings WHERE @criteria";
                    List<StatementSettingsPoco> pocos = cn.Query<StatementSettingsPoco>(sql).ToList();
                    List<StatementSettingsDomain> domains = new List<StatementSettingsDomain>();

                    foreach (StatementSettingsPoco poco in pocos)
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

        public int DeleteStatementSettings(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM StatementSettings WHERE StatementSettingsID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertStatementSettings(StatementSettingsDomain domain)
        {
            int insertedId = 0;

            try
            {
                StatementSettingsPoco poco = new StatementSettingsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[StatementSettings] " +
                        "([StatementSettingsID], [MinimunDueAmount], [HowManyBeforeCollections], [PracticeMessage], " +
                        "[DaysLateFeeAssessed], [ChecksPayableTo], [BillByDate], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded], [RemitName], [RemitAttention], [RemitAddress1], [RemitAddress2], " +
                        "[RemitCity], [RemitState], [RemitZipCode], [BillingPhone], [DefaultEmailSubject], " +
                        "[DefaultEmailMessage], [PracticeEmailAddress], [DisplayName], [BillingPhoneExt], " +
                        "[PracticeLogo], [LateFee], [OnlinePayments]) " +
                        "VALUES " +
                        "(@StatementSettingsID, @MinimunDueAmount, @HowManyBeforeCollections, @PracticeMessage, " +
                        "@DaysLateFeeAssessed, @ChecksPayableTo, @BillByDate, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @RemitName, @RemitAttention, @RemitAddress1, @RemitAddress2, @RemitCity, " +
                        "@RemitState, @RemitZipCode, @BillingPhone, @DefaultEmailSubject, @DefaultEmailMessage, " +
                        "@PracticeEmailAddress, @DisplayName, @BillingPhoneExt, @PracticeLogo, @LateFee, @OnlinePayments); " +
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

        public int UpdateStatementSettings(StatementSettingsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                StatementSettingsPoco poco = new StatementSettingsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE StatementSettings " +
                        "SET [MinimunDueAmount] = @MinimunDueAmount, [HowManyBeforeCollections] = @HowManyBeforeCollections, " +
                        "[PracticeMessage] = @PracticeMessage, [DaysLateFeeAssessed] = @DaysLateFeeAssessed, " +
                        "[ChecksPayableTo] = @ChecksPayableTo, [BillByDate] = @BillByDate, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [RemitName] = @RemitName, [RemitAttention] = @RemitAttention, " +
                        "[RemitAddress1] = @RemitAddress1, [RemitAddress2] = @RemitAddress2, [RemitCity] = @RemitCity, " +
                        "[RemitState] = @RemitState, [RemitZipCode] = @RemitZipCode, [BillingPhone] = @BillingPhone, " +
                        "[DefaultEmailSubject] = @DefaultEmailSubject, [DefaultEmailMessage] = @DefaultEmailMessage, " +
                        "[PracticeEmailAddress] = @PracticeEmailAddress, [DisplayName] = @DisplayName, " +
                        "[BillingPhoneExt] = @BillingPhoneExt, [PracticeLogo] = @PracticeLogo, [LateFee] = @LateFee, " +
                        "[OnlinePayments] = @OnlinePayments " +
                        "WHERE StatementSettingsID = @StatementSettingsID;";

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
