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
    public class LabNotesRepository : ILabNotesRepository
    {
        private readonly string _connectionString;

        public LabNotesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LabNotesDomain GetLabNotes(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM LabNotes WHERE LabNoteID = @id";

                    var LabNotesPoco = cn.QueryFirstOrDefault<LabNotesPoco>(query, new { id = id }) ?? new LabNotesPoco();

                    return LabNotesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LabNotesDomain> GetLabNotes(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM LabNotes WHERE @criteria";
                    List<LabNotesPoco> pocos = cn.Query<LabNotesPoco>(sql).ToList();
                    List<LabNotesDomain> domains = new List<LabNotesDomain>();

                    foreach (LabNotesPoco poco in pocos)
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

        public int DeleteLabNotes(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM LabNotes WHERE LabNoteID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLabNotes(LabNotesDomain domain)
        {
            int insertedId = 0;

            try
            {
                LabNotesPoco poco = new LabNotesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[LabNotes] " +
                        "([LabNoteID], [LabTestID], [LabResultID], [LabResultDetailID], [LabOrderID], " +
                        "[LabOrderDetailID], [CreatedDt], [CreatedBy], [LastUpdDt], [LastUpdBy], " +
                        "[OwnerType], [OwnerID], [NoteSeqNbr], [NoteText], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [Replaced]) " +
                        "VALUES " +
                        "(@LabNoteID, @LabTestID, @LabResultID, @LabResultDetailID, @LabOrderID, " +
                        "@LabOrderDetailID, @CreatedDt, @CreatedBy, @LastUpdDt, @LastUpdBy, @OwnerType, " +
                        "@OwnerID, @NoteSeqNbr, @NoteText, @DateLastTouched, @LastTouchedBy, @DateRowAdded, @Replaced); " +
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

        public int UpdateLabNotes(LabNotesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                LabNotesPoco poco = new LabNotesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE LabNotes " +
                        "SET [LabTestID] = @LabTestID, [LabResultID] = @LabResultID, " +
                        "[LabResultDetailID] = @LabResultDetailID, [LabOrderID] = @LabOrderID, " +
                        "[LabOrderDetailID] = @LabOrderDetailID, [CreatedDt] = @CreatedDt, " +
                        "[CreatedBy] = @CreatedBy, [LastUpdDt] = @LastUpdDt, [LastUpdBy] = @LastUpdBy, " +
                        "[OwnerType] = @OwnerType, [OwnerID] = @OwnerID, [NoteSeqNbr] = @NoteSeqNbr, " +
                        "[NoteText] = @NoteText, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[Replaced] = @Replaced " +
                        "WHERE LabNoteID = @LabNoteID;";

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
