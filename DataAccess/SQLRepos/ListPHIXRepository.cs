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
    public class ListPHIXRepository : IListPHIXRepository
    {
        private readonly string _connectionString;

        public ListPHIXRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListPHIXDomain GetListPHIX(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListPHIX WHERE ListPhixId = @id";

                    var ListPHIXPoco = cn.QueryFirstOrDefault<ListPHIXPoco>(query, new { id = id }) ?? new ListPHIXPoco();

                    return ListPHIXPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListPHIXDomain> GetListPHIXs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListPHIX WHERE @criteria";
                    List<ListPHIXPoco> pocos = cn.Query<ListPHIXPoco>(sql).ToList();
                    List<ListPHIXDomain> domains = new List<ListPHIXDomain>();

                    foreach (ListPHIXPoco poco in pocos)
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

        public int DeleteListPHIX(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListPHIX WHERE ListPhixId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListPHIX(ListPHIXDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListPHIXPoco poco = new ListPHIXPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPHIX] " +
                        "([ListPhixId], [ViewPhixId], [PatientId], [IsActive], [SubscriptionStatusId], " +
                        "[DateActivate], [DateInactive], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ListPhixId, @ViewPhixId, @PatientId, @IsActive, @SubscriptionStatusId, " +
                        "@DateActivate, @DateInactive, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListPHIX(ListPHIXDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListPHIXPoco poco = new ListPHIXPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPHIX " +
                        "SET [ViewPhixId] = @ViewPhixId, [PatientId] = @PatientId, [IsActive] = @IsActive, " +
                        "[SubscriptionStatusId] = @SubscriptionStatusId, [DateActivate] = @DateActivate, " +
                        "[DateInactive] = @DateInactive, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE ListPhixId = @ListPhixId;";

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
