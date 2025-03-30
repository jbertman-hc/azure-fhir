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
    public class RemitMedicareOutpatientRepository : IRemitMedicareOutpatientRepository
    {
        private readonly string _connectionString;

        public RemitMedicareOutpatientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RemitMedicareOutpatientDomain GetRemitMedicareOutpatient(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM RemitMedicareOutpatient WHERE id = @id";

                    var RemitMedicareOutpatientPoco = cn.QueryFirstOrDefault<RemitMedicareOutpatientPoco>(query, new { id = id }) ?? new RemitMedicareOutpatientPoco();

                    return RemitMedicareOutpatientPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RemitMedicareOutpatientDomain> GetRemitMedicareOutpatients(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM RemitMedicareOutpatient WHERE @criteria";
                    List<RemitMedicareOutpatientPoco> pocos = cn.Query<RemitMedicareOutpatientPoco>(sql).ToList();
                    List<RemitMedicareOutpatientDomain> domains = new List<RemitMedicareOutpatientDomain>();

                    foreach (RemitMedicareOutpatientPoco poco in pocos)
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

        public int DeleteRemitMedicareOutpatient(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM RemitMedicareOutpatient WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertRemitMedicareOutpatient(RemitMedicareOutpatientDomain domain)
        {
            int insertedId = 0;

            try
            {
                RemitMedicareOutpatientPoco poco = new RemitMedicareOutpatientPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[RemitMedicareOutpatient] " +
                        "([ID], [RemitClaimsID], [MOA01], [MOA02], [MOA03], [MOA04], [MOA05], [MOA06], [MOA07], " +
                        "[MOA08], [MOA09], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @RemitClaimsID, @MOA01, @MOA02, @MOA03, @MOA04, @MOA05, @MOA06, @MOA07, " +
                        "@MOA08, @MOA09, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateRemitMedicareOutpatient(RemitMedicareOutpatientDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                RemitMedicareOutpatientPoco poco = new RemitMedicareOutpatientPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE RemitMedicareOutpatient " +
                        "SET [RemitClaimsID] = @RemitClaimsID, [MOA01] = @MOA01, [MOA02] = @MOA02, [MOA03] = @MOA03, " +
                        "[MOA04] = @MOA04, [MOA05] = @MOA05, [MOA06] = @MOA06, [MOA07] = @MOA07, [MOA08] = @MOA08, " +
                        "[MOA09] = @MOA09, [DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
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
