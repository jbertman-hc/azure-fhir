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
    public class RxSigsRepository : IRxSigsRepository
    {
        private readonly string _connectionString;

        public RxSigsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RxSigsDomain GetRxSigs(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM RxSigs WHERE RowID = @id";

                    var RxSigsPoco = cn.QueryFirstOrDefault<RxSigsPoco>(query, new { id = id }) ?? new RxSigsPoco();

                    return RxSigsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RxSigsDomain> GetRxSigs(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM RxSigs WHERE @criteria";
                    List<RxSigsPoco> pocos = cn.Query<RxSigsPoco>(sql).ToList();
                    List<RxSigsDomain> domains = new List<RxSigsDomain>();

                    foreach (RxSigsPoco poco in pocos)
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

        public int DeleteRxSigs(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM RxSigs WHERE RowID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertRxSigs(RxSigsDomain domain)
        {
            int insertedId = 0;

            try
            {
                RxSigsPoco poco = new RxSigsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[RxSigs] " +
                        "([RowID], [ListMedID], [ActionID], [FormID], [RouteID], [FrequencyID], [AmountID], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@RowID, @ListMedID, @ActionID, @FormID, @RouteID, @FrequencyID, @AmountID, " +
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

        public int UpdateRxSigs(RxSigsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                RxSigsPoco poco = new RxSigsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE RxSigs " +
                        "SET [ListMedID] = @ListMedID, [ActionID] = @ActionID, [FormID] = @FormID, " +
                        "[RouteID] = @RouteID, [FrequencyID] = @FrequencyID, [AmountID] = @AmountID, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE RowID = @RowID;";

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
