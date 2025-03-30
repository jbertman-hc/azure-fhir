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
    public class ListPatientLanguagesRepository : IListPatientLanguagesRepository
    {
        private readonly string _connectionString;

        public ListPatientLanguagesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListPatientLanguagesDomain GetListPatientLanguages(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListPatientLanguages WHERE ListPatientLanguagesId = @id";

                    var ListPatientLanguagesPoco = cn.QueryFirstOrDefault<ListPatientLanguagesPoco>(query, new { id = id }) ?? new ListPatientLanguagesPoco();

                    return ListPatientLanguagesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListPatientLanguagesDomain> GetListPatientLanguages(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListPatientLanguages WHERE @criteria";
                    List<ListPatientLanguagesPoco> pocos = cn.Query<ListPatientLanguagesPoco>(sql).ToList();
                    List<ListPatientLanguagesDomain> domains = new List<ListPatientLanguagesDomain>();

                    foreach (ListPatientLanguagesPoco poco in pocos)
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

        public int DeleteListPatientLanguages(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListPatientLanguages WHERE ListPatientLanguagesId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListPatientLanguages(ListPatientLanguagesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListPatientLanguagesPoco poco = new ListPatientLanguagesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPatientLanguages] " +
                        "([ListPatientLanguagesId], [PatientID], [LanguageID], [FreeText], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ListPatientLanguagesId, @PatientID, @LanguageID, @FreeText, @DateLastTouched, " +
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

        public int UpdateListPatientLanguages(ListPatientLanguagesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListPatientLanguagesPoco poco = new ListPatientLanguagesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPatientLanguages " +
                        "SET [PatientID] = @PatientID, [LanguageID] = @LanguageID, [FreeText] = @FreeText, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE ListPatientLanguagesId = @ListPatientLanguagesId;";

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
