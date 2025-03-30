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
    public class PracticeFeesRepository : IPracticeFeesRepository
    {
        private readonly string _connectionString;

        public PracticeFeesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PracticeFeesDomain GetPracticeFees(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM PracticeFees WHERE PracticeFeesID = @id";

                    var PracticeFeesPoco = cn.QueryFirstOrDefault<PracticeFeesPoco>(query, new { id = id }) ?? new PracticeFeesPoco();

                    return PracticeFeesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PracticeFeesDomain> GetPracticeFees(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM PracticeFees WHERE @criteria";
                    List<PracticeFeesPoco> pocos = cn.Query<PracticeFeesPoco>(sql).ToList();
                    List<PracticeFeesDomain> domains = new List<PracticeFeesDomain>();

                    foreach (PracticeFeesPoco poco in pocos)
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

        public int DeletePracticeFees(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM PracticeFees WHERE PracticeFeesID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertPracticeFees(PracticeFeesDomain domain)
        {
            int insertedId = 0;

            try
            {
                PracticeFeesPoco poco = new PracticeFeesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[PracticeFees] " +
                        "([PracticeFeesID], [StatementSettingsID], [ChargeName], [ChargeText], [Charge], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@PracticeFeesID, @StatementSettingsID, @ChargeName, @ChargeText, @Charge, " +
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

        public int UpdatePracticeFees(PracticeFeesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                PracticeFeesPoco poco = new PracticeFeesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE PracticeFees " +
                        "SET [StatementSettingsID] = @StatementSettingsID, [ChargeName] = @ChargeName, " +
                        "[ChargeText] = @ChargeText, [Charge] = @Charge, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE PracticeFeesID = @PracticeFeesID;";

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
