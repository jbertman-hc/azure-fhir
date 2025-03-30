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
    public class SuperbillMappingRepository : ISuperbillMappingRepository
    {
        private readonly string _connectionString;

        public SuperbillMappingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SuperbillMappingDomain GetSuperbillMapping(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM SuperbillMapping WHERE UniqueTableId = @id";

                    var SuperbillMappingPoco = cn.QueryFirstOrDefault<SuperbillMappingPoco>(query, new { id = id }) ?? new SuperbillMappingPoco();

                    return SuperbillMappingPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SuperbillMappingDomain> GetSuperbillMappings(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM SuperbillMapping WHERE @criteria";
                    List<SuperbillMappingPoco> pocos = cn.Query<SuperbillMappingPoco>(sql).ToList();
                    List<SuperbillMappingDomain> domains = new List<SuperbillMappingDomain>();

                    foreach (SuperbillMappingPoco poco in pocos)
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

        public int DeleteSuperbillMapping(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM SuperbillMapping WHERE UniqueTableId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertSuperbillMapping(SuperbillMappingDomain domain)
        {
            int insertedId = 0;

            try
            {
                SuperbillMappingPoco poco = new SuperbillMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[SuperbillMapping] " +
                        "([UniqueTableId], [BillingGuid], [ExternalId], [SourceId], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded], [PartnerTransactionId], [DateLastSent], [LastResultMessage], [IsFailure]) " +
                        "VALUES " +
                        "(@UniqueTableId, @BillingGuid, @ExternalId, @SourceId, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @PartnerTransactionId, @DateLastSent, @LastResultMessage, @IsFailure); " +
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

        public int UpdateSuperbillMapping(SuperbillMappingDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                SuperbillMappingPoco poco = new SuperbillMappingPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE SuperbillMapping " +
                        "SET [BillingGuid] = @BillingGuid, [ExternalId] = @ExternalId, [SourceId] = @SourceId, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [PartnerTransactionId] = @PartnerTransactionId, " +
                        "[DateLastSent] = @DateLastSent, [LastResultMessage] = @LastResultMessage, " +
                        "[IsFailure] = @IsFailure " +
                        "WHERE UniqueTableId = @UniqueTableId;";

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
