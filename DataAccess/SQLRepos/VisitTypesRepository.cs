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
    public class VisitTypesRepository : IVisitTypesRepository
    {
        private readonly string _connectionString;

        public VisitTypesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public VisitTypesDomain GetVisitType(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM VisitTypes WHERE id = @id";

                    var VisitTypesPoco = cn.QueryFirstOrDefault<VisitTypesPoco>(query, new { id = id }) ?? new VisitTypesPoco();

                    return VisitTypesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<VisitTypesDomain> GetVisitTypes(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = $"SELECT * FROM VisitTypes WHERE {@criteria}";

                    List<VisitTypesPoco> pocos = cn.Query<VisitTypesPoco>(sql).ToList();
                    List<VisitTypesDomain> domains = new List<VisitTypesDomain>();

                    foreach (VisitTypesPoco poco in pocos)
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

        public int DeleteVisitTypes(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM VisitTypes WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertVisitTypes(VisitTypesDomain domain)
        {
            int insertedId = 0;

            try
            {
                VisitTypesPoco poco = new VisitTypesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[VisitTypes] " +
                        "([ID], [VisitType], [TimeForVisit], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @VisitType, @TimeForVisit, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateVisitTypes(VisitTypesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                VisitTypesPoco poco = new VisitTypesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE VisitTypes " +
                        "SET [VisitType] = @VisitType, [TimeForVisit] = @TimeForVisit, " +
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
