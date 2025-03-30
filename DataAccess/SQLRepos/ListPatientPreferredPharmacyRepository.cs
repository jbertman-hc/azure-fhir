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
    public class ListPatientPreferredPharmacyRepository : IListPatientPreferredPharmacyRepository
    {
        private readonly string _connectionString;

        public ListPatientPreferredPharmacyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListPatientPreferredPharmacyDomain GetListPatientPreferredPharmacy(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListPatientPreferredPharmacy WHERE ListPatientPreferredPharmacyId = @id";

                    var ListPatientPreferredPharmacyPoco = cn.QueryFirstOrDefault<ListPatientPreferredPharmacyPoco>(query, new { id = id }) ?? new ListPatientPreferredPharmacyPoco();

                    return ListPatientPreferredPharmacyPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListPatientPreferredPharmacyDomain> GetListPatientPreferredPharmacys(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListPatientPreferredPharmacy WHERE @criteria";
                    List<ListPatientPreferredPharmacyPoco> pocos = cn.Query<ListPatientPreferredPharmacyPoco>(sql).ToList();
                    List<ListPatientPreferredPharmacyDomain> domains = new List<ListPatientPreferredPharmacyDomain>();

                    foreach (ListPatientPreferredPharmacyPoco poco in pocos)
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

        public int DeleteListPatientPreferredPharmacy(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListPatientPreferredPharmacy WHERE ListPatientPreferredPharmacyId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListPatientPreferredPharmacy(ListPatientPreferredPharmacyDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListPatientPreferredPharmacyPoco poco = new ListPatientPreferredPharmacyPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPatientPreferredPharmacy] " +
                        "([ListPatientPreferredPharmacyId], [PatientId], [PharmacyId], [FreeText], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ListPatientPreferredPharmacyId, @PatientId, @PharmacyId, @FreeText, @DateLastTouched, " +
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

        public int UpdateListPatientPreferredPharmacy(ListPatientPreferredPharmacyDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListPatientPreferredPharmacyPoco poco = new ListPatientPreferredPharmacyPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPatientPreferredPharmacy " +
                        "SET [PatientId] = @PatientId, [PharmacyId] = @PharmacyId, [FreeText] = @FreeText, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE ListPatientPreferredPharmacyId = @ListPatientPreferredPharmacyId;";

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
