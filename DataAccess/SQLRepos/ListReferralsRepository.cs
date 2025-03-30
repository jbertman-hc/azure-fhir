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
    public class ListReferralsRepository : IListReferralsRepository
    {
        private readonly string _connectionString;

        public ListReferralsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListReferralsDomain GetListReferrals(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListReferrals WHERE id = @id";

                    var ListReferralsPoco = cn.QueryFirstOrDefault<ListReferralsPoco>(query, new { id = id }) ?? new ListReferralsPoco();

                    return ListReferralsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListReferralsDomain> GetListReferrals(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListReferrals WHERE @criteria";
                    List<ListReferralsPoco> pocos = cn.Query<ListReferralsPoco>(sql).ToList();
                    List<ListReferralsDomain> domains = new List<ListReferralsDomain>();

                    foreach (ListReferralsPoco poco in pocos)
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

        public int DeleteListReferrals(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListReferrals WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListReferrals(ListReferralsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListReferralsPoco poco = new ListReferralsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListReferrals] " +
                        "([ID], [PatientID], [ReferralName], [ReferProviderID], [DateStarts], [DateEnds], " +
                        "[NumberOfVisits], [Comment], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[Outgoing], [OnBehalfOfProvider]) " +
                        "VALUES " +
                        "(@ID, @PatientID, @ReferralName, @ReferProviderID, @DateStarts, @DateEnds, " +
                        "@NumberOfVisits, @Comment, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @Outgoing, " +
                        "@OnBehalfOfProvider); " +
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

        public int UpdateListReferrals(ListReferralsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListReferralsPoco poco = new ListReferralsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListReferrals " +
                        "SET [PatientID] = @PatientID, [ReferralName] = @ReferralName, " +
                        "[ReferProviderID] = @ReferProviderID, [DateStarts] = @DateStarts, " +
                        "[DateEnds] = @DateEnds, [NumberOfVisits] = @NumberOfVisits, [Comment] = @Comment, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [Outgoing] = @Outgoing, [OnBehalfOfProvider] = @OnBehalfOfProvider " +
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
