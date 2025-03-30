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
    public class ListPatientRacesRepository : IListPatientRacesRepository
    {
        private readonly string _connectionString;

        public ListPatientRacesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListPatientRacesDomain GetListPatientRaces(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListPatientRaces WHERE ListPatientRacesId = @id";

                    var ListPatientRacesPoco = cn.QueryFirstOrDefault<ListPatientRacesPoco>(query, new { id = id }) ?? new ListPatientRacesPoco();

                    return ListPatientRacesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListPatientRacesDomain> GetListPatientRaces(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListPatientRaces WHERE @criteria";
                    List<ListPatientRacesPoco> pocos = cn.Query<ListPatientRacesPoco>(sql).ToList();
                    List<ListPatientRacesDomain> domains = new List<ListPatientRacesDomain>();

                    foreach (ListPatientRacesPoco poco in pocos)
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

        public int DeleteListPatientRaces(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListPatientRaces WHERE ListPatientRacesId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListPatientRaces(ListPatientRacesDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListPatientRacesPoco poco = new ListPatientRacesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPatientRaces] " +
                        "([ListPatientRacesId], [PatientID], [RaceID], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ListPatientRacesId, @PatientID, @RaceID, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListPatientRaces(ListPatientRacesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListPatientRacesPoco poco = new ListPatientRacesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPatientRaces " +
                        "SET [PatientID] = @PatientID, [RaceID] = @RaceID, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE ListPatientRacesId = @ListPatientRacesId;";

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
