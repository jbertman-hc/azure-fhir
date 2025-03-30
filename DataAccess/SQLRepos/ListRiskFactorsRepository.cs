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
    public class ListRiskFactorsRepository : IListRiskFactorsRepository
    {
        private readonly string _connectionString;

        public ListRiskFactorsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListRiskFactorsDomain GetListRiskFactors(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListRiskFactors WHERE id = @id";

                    var ListRiskFactorsPoco = cn.QueryFirstOrDefault<ListRiskFactorsPoco>(query, new { id = id }) ?? new ListRiskFactorsPoco();

                    return ListRiskFactorsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListRiskFactorsDomain> GetListRiskFactors(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListRiskFactors WHERE @criteria";
                    List<ListRiskFactorsPoco> pocos = cn.Query<ListRiskFactorsPoco>(sql).ToList();
                    List<ListRiskFactorsDomain> domains = new List<ListRiskFactorsDomain>();

                    foreach (ListRiskFactorsPoco poco in pocos)
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

        public int DeleteListRiskFactors(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListRiskFactors WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListRiskFactors(ListRiskFactorsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListRiskFactorsPoco poco = new ListRiskFactorsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListRiskFactors] " +
                        "([ID], [PatientID], [RiskFactorID], [RiskFactorName], [Provider], [LastTouchedBy], " +
                        "[DateLastTouched], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @PatientID, @RiskFactorID, @RiskFactorName, @Provider, @LastTouchedBy, " +
                        "@DateLastTouched, @DateRowAdded); " +
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

        public int UpdateListRiskFactors(ListRiskFactorsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListRiskFactorsPoco poco = new ListRiskFactorsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListRiskFactors " +
                        "SET [PatientID] = @PatientID, [RiskFactorID] = @RiskFactorID, " +
                        "[RiskFactorName] = @RiskFactorName, [Provider] = @Provider, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateLastTouched] = @DateLastTouched, " +
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
