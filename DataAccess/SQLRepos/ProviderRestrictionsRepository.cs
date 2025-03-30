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
    public class ProviderRestrictionsRepository : IProviderRestrictionsRepository
    {
        private readonly string _connectionString;

        public ProviderRestrictionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProviderRestrictionsDomain GetProviderRestrictions(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ProviderRestrictions WHERE id = @id";

                    var ProviderRestrictionsPoco = cn.QueryFirstOrDefault<ProviderRestrictionsPoco>(query, new { id = id }) ?? new ProviderRestrictionsPoco();

                    return ProviderRestrictionsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ProviderRestrictionsDomain> GetProviderRestrictions(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ProviderRestrictions WHERE @criteria";
                    List<ProviderRestrictionsPoco> pocos = cn.Query<ProviderRestrictionsPoco>(sql).ToList();
                    List<ProviderRestrictionsDomain> domains = new List<ProviderRestrictionsDomain>();

                    foreach (ProviderRestrictionsPoco poco in pocos)
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

        public int DeleteProviderRestrictions(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ProviderRestrictions WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertProviderRestrictions(ProviderRestrictionsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ProviderRestrictionsPoco poco = new ProviderRestrictionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ProviderRestrictions] " +
                        "([id], [PatientID], [ProviderID], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@id, @PatientID, @ProviderID, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateProviderRestrictions(ProviderRestrictionsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ProviderRestrictionsPoco poco = new ProviderRestrictionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ProviderRestrictions " +
                        "SET [PatientID] = @PatientID, [ProviderID] = @ProviderID, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE id = @id;";

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
