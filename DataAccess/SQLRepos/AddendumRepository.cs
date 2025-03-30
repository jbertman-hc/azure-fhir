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
    public class AddendumRepository : IAddendumRepository
    {
        private readonly string _connectionString;

        public AddendumRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AddendumDomain GetAddendum(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Addendum WHERE id = @id";

                    var AddendumPoco = cn.QueryFirstOrDefault<AddendumPoco>(query, new { id = id }) ?? new AddendumPoco();

                    return AddendumPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AddendumDomain> GetAddendums(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Addendum WHERE @criteria";
                    List<AddendumPoco> pocos = cn.Query<AddendumPoco>(sql).ToList();
                    List<AddendumDomain> domains = new List<AddendumDomain>();

                    foreach (AddendumPoco poco in pocos)
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

        public int DeleteAddendum(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Addendum WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertAddendum(AddendumDomain domain)
        {
            int insertedId = 0;

            try
            {
                AddendumPoco poco = new AddendumPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Addendum] ([PatID], [PatientName], " +
                        "[Date], [NoteType], [NoteSubject], [NoteBody], [SavedBy], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@PatID, @PatientName, @Date, @NoteType, @NoteSubject, " +
                        "@NoteBody, @SavedBy, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateAddendum(AddendumDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                AddendumPoco poco = new AddendumPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Addendum " +
                        "SET [PatID] = @PatID, [PatientName] = @PatientName, [Date] = @Date, " +
                        "[NoteType] = @NoteType, [NoteSubject] = @NoteSubject, " +
                        "[NoteBody] = @NoteBody, [SavedBy] = @SavedBy, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE id = @id";

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
