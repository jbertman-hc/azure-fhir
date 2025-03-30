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
    public class ImmunizationReactionsRepository : IImmunizationReactionsRepository
    {
        private readonly string _connectionString;

        public ImmunizationReactionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ImmunizationReactionsDomain GetImmunizationReactions(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ImmunizationReactions WHERE ImmunizationReactionID = @id";

                    var ImmunizationReactionsPoco = cn.QueryFirstOrDefault<ImmunizationReactionsPoco>(query, new { id = id }) ?? new ImmunizationReactionsPoco();

                    return ImmunizationReactionsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ImmunizationReactionsDomain> GetImmunizationReactions(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ImmunizationReactions WHERE @criteria";
                    List<ImmunizationReactionsPoco> pocos = cn.Query<ImmunizationReactionsPoco>(sql).ToList();
                    List<ImmunizationReactionsDomain> domains = new List<ImmunizationReactionsDomain>();

                    foreach (ImmunizationReactionsPoco poco in pocos)
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

        public int DeleteImmunizationReactions(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ImmunizationReactions WHERE ImmunizationReactionID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertImmunizationReactions(ImmunizationReactionsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ImmunizationReactionsPoco poco = new ImmunizationReactionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ImmunizationReactions] " +
                        "([ImmunizationReactionID], [Reaction], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ImmunizationReactionID, @Reaction, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateImmunizationReactions(ImmunizationReactionsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ImmunizationReactionsPoco poco = new ImmunizationReactionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ImmunizationReactions " +
                        "SET [Reaction] = @Reaction, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE ImmunizationReactionID = @ImmunizationReactionID;";

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
