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
    public class ECouponCurrentProgramsRepository : IECouponCurrentProgramsRepository
    {
        private readonly string _connectionString;

        public ECouponCurrentProgramsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ECouponCurrentProgramsDomain GetECouponCurrentPrograms(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ECouponCurrentPrograms WHERE ECouponCurrentProgramsId = @id";

                    var ECouponCurrentProgramsPoco = cn.QueryFirstOrDefault<ECouponCurrentProgramsPoco>(query, new { id = id }) ?? new ECouponCurrentProgramsPoco();

                    return ECouponCurrentProgramsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ECouponCurrentProgramsDomain> GetECouponCurrentPrograms(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ECouponCurrentPrograms WHERE @criteria";
                    List<ECouponCurrentProgramsPoco> pocos = cn.Query<ECouponCurrentProgramsPoco>(sql).ToList();
                    List<ECouponCurrentProgramsDomain> domains = new List<ECouponCurrentProgramsDomain>();

                    foreach (ECouponCurrentProgramsPoco poco in pocos)
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

        public int DeleteECouponCurrentPrograms(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ECouponCurrentPrograms WHERE ECouponCurrentProgramsId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertECouponCurrentPrograms(ECouponCurrentProgramsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ECouponCurrentProgramsPoco poco = new ECouponCurrentProgramsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ECouponCurrentPrograms] " +
                        "([ECouponCurrentProgramsId], [ScriptId], [ProgramId], [TransactionId], [Name], " +
                        "[Type], [Paid], [Image], [PaymentNotes], [Bin], [PCN], [Group], [CardholderId], " +
                        "[DateLastPrinted], [DateProgramConfirmed], [PharmacyCode], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded], [ProgramType]) " +
                        "VALUES " +
                        "(@ECouponCurrentProgramsId, @ScriptId, @ProgramId, @TransactionId, @Name, @Type, " +
                        "@Paid, @Image, @PaymentNotes, @Bin, @PCN, @Group, @CardholderId, @DateLastPrinted, " +
                        "@DateProgramConfirmed, @PharmacyCode, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded, @ProgramType); " +
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

        public int UpdateECouponCurrentPrograms(ECouponCurrentProgramsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ECouponCurrentProgramsPoco poco = new ECouponCurrentProgramsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ECouponCurrentPrograms " +
                        "SET [ScriptId] = @ScriptId, [ProgramId] = @ProgramId, [TransactionId] = @TransactionId, " +
                        "[Name] = @Name, [Type] = @Type, [Paid] = @Paid, [Image] = @Image, " +
                        "[PaymentNotes] = @PaymentNotes, [Bin] = @Bin, [PCN] = @PCN, [Group] = @Group, " +
                        "[CardholderId] = @CardholderId, [DateLastPrinted] = @DateLastPrinted, " +
                        "[DateProgramConfirmed] = @DateProgramConfirmed, [PharmacyCode] = @PharmacyCode, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [ProgramType] = @ProgramType " +
                        "WHERE ECouponCurrentProgramsId = @ECouponCurrentProgramsId;";

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
