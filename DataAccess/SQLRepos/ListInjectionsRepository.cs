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
    public class ListInjectionsRepository : IListInjectionsRepository
    {
        private readonly string _connectionString;

        public ListInjectionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListInjectionsDomain GetListInjections(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListInjections WHERE InjectionID = @id";

                    var ListInjectionsPoco = cn.QueryFirstOrDefault<ListInjectionsPoco>(query, new { id = id }) ?? new ListInjectionsPoco();

                    return ListInjectionsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListInjectionsDomain> GetListInjections(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListInjections WHERE @criteria";
                    List<ListInjectionsPoco> pocos = cn.Query<ListInjectionsPoco>(sql).ToList();
                    List<ListInjectionsDomain> domains = new List<ListInjectionsDomain>();

                    foreach (ListInjectionsPoco poco in pocos)
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

        public int DeleteListInjections(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListInjections WHERE InjectionID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListInjections(ListInjectionsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListInjectionsPoco poco = new ListInjectionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListInjections] " +
                        "([InjectionID], [PatientID], [InjectionName], [LotNo], [DateGiven], [RecordedBy], " +
                        "[Volume], [Route], [Site], [Manufacturer], [Expiration], [Comment], [CPT], " +
                        "[LastTouchedBy], [DateLastTouched], [DateRowAdded], [IsGivenElsewhere], [Deleted], [NDCCode]) " +
                        "VALUES (@InjectionID, @PatientID, @InjectionName, @LotNo, @DateGiven, @RecordedBy, " +
                        "@Volume, @Route, @Site, @Manufacturer, @Expiration, @Comment, @CPT, " +
                        "@LastTouchedBy, @DateLastTouched, @DateRowAdded, @IsGivenElsewhere, @Deleted, @NDCCode); " +
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

        public int UpdateListInjections(ListInjectionsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListInjectionsPoco poco = new ListInjectionsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListInjections " +
                        "SET [PatientID] = @PatientID, [InjectionName] = @InjectionName, [LotNo] = @LotNo, " +
                        "[DateGiven] = @DateGiven, [RecordedBy] = @RecordedBy, [Volume] = @Volume, " +
                        "[Route] = @Route, [Site] = @Site, [Manufacturer] = @Manufacturer, " +
                        "[Expiration] = @Expiration, [Comment] = @Comment, [CPT] = @CPT, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateLastTouched] = @DateLastTouched, " +
                        "[DateRowAdded] = @DateRowAdded, [IsGivenElsewhere] = @IsGivenElsewhere, " +
                        "[Deleted] = @Deleted, [NDCCode] = @NDCCode " +
                        "WHERE InjectionID = @InjectionID;";

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
