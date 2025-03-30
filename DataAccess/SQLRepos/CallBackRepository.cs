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
    public class CallBackRepository : ICallBackRepository
    {
        private readonly string _connectionString;

        public CallBackRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CallBackDomain GetCallBack(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM CallBack WHERE PrimaryKeyID = @id";

                    var CallBackPoco = cn.QueryFirstOrDefault<CallBackPoco>(query, new { id = id }) ?? new CallBackPoco();

                    return CallBackPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CallBackDomain> GetCallBacks(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM CallBack WHERE @criteria";
                    List<CallBackPoco> pocos = cn.Query<CallBackPoco>(sql).ToList();
                    List<CallBackDomain> domains = new List<CallBackDomain>();

                    foreach (CallBackPoco poco in pocos)
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

        public int DeleteCallBack(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM CallBack WHERE PrimaryKeyID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertCallBack(CallBackDomain domain)
        {
            int insertedId = 0;

            try
            {
                CallBackPoco poco = new CallBackPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[CallBack] " +
                        "([PrimaryKeyID], [PatientID], [CallBackDate], [CallBackComment], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@PrimaryKeyID, @PatientID, @CallBackDate, @CallBackComment, @DateLastTouched, " +
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

        public int UpdateCallBack(CallBackDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                CallBackPoco poco = new CallBackPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE CallBack " +
                        "SET [PatientID] = @PatientID, [CallBackDate] = @CallBackDate, " +
                        "[CallBackComment] = @CallBackComment, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE PrimaryKeyID = @PrimaryKeyID;";

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
