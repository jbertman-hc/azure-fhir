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
    public class ListImmunizationsRepository : IListImmunizationsRepository
    {
        private readonly string _connectionString;

        public ListImmunizationsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListImmunizationsDomain GetListImmunizations(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListImmunizations WHERE ListImmID = @id";

                    var ListImmunizationsPoco = cn.QueryFirstOrDefault<ListImmunizationsPoco>(query, new { id = id }) ?? new ListImmunizationsPoco();

                    return ListImmunizationsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListImmunizationsDomain> GetListImmunizations(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListImmunizations WHERE @criteria";
                    List<ListImmunizationsPoco> pocos = cn.Query<ListImmunizationsPoco>(sql).ToList();
                    List<ListImmunizationsDomain> domains = new List<ListImmunizationsDomain>();

                    foreach (ListImmunizationsPoco poco in pocos)
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

        public int DeleteListImmunizations(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListImmunizations WHERE ListImmID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListImmunizations(ListImmunizationsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListImmunizationsPoco poco = new ListImmunizationsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListImmunizations] " +
                        "([ListImmID], [PatientID], [Immunization], [LotNo], [DateGiven], [RecordedBy], " +
                        "[Volume], [Route], [Site], [Manufacturer], [Expiration], [Comment], [Sequence], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ListImmID, @PatientID, @Immunization, @LotNo, @DateGiven, @RecordedBy, @Volume, " +
                        "@Route, @Site, @Manufacturer, @Expiration, @Comment, @Sequence, @DateLastTouched, " +
                        "@LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListImmunizations(ListImmunizationsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListImmunizationsPoco poco = new ListImmunizationsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListImmunizations " +
                        "SET [PatientID] = @PatientID, [Immunization] = @Immunization, [LotNo] = @LotNo, " +
                        "[DateGiven] = @DateGiven, [RecordedBy] = @RecordedBy, [Volume] = @Volume, " +
                        "[Route] = @Route, [Site] = @Site, [Manufacturer] = @Manufacturer, " +
                        "[Expiration] = @Expiration, [Comment] = @Comment, [Sequence] = @Sequence, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE ListImmID = @ListImmID;";

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
