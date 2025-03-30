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
    public class ListPmAccountsRepository : IListPmAccountsRepository
    {
        private readonly string _connectionString;

        public ListPmAccountsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListPmAccountsDomain GetListPmAccounts(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListPmAccounts WHERE PmAccountId = @id";

                    var ListPmAccountsPoco = cn.QueryFirstOrDefault<ListPmAccountsPoco>(query, new { id = id }) ?? new ListPmAccountsPoco();

                    return ListPmAccountsPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListPmAccountsDomain> GetListPmAccounts(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListPmAccounts WHERE @criteria";
                    List<ListPmAccountsPoco> pocos = cn.Query<ListPmAccountsPoco>(sql).ToList();
                    List<ListPmAccountsDomain> domains = new List<ListPmAccountsDomain>();

                    foreach (ListPmAccountsPoco poco in pocos)
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

        public int DeleteListPmAccounts(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListPmAccounts WHERE PmAccountId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListPmAccounts(ListPmAccountsDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListPmAccountsPoco poco = new ListPmAccountsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPmAccounts] " +
                        "([PmAccountId], [PatientID], [AccountId], [AccountNumber], [AccountName], [AccountSeqNo], " +
                        "[PatientRelToRespParty], [RespPartyNo], [RespPartySalutation], [RespPartyFirstName], " +
                        "[RespPartyMiddleName], [RespPartyLastName], [RespPartySuffix], [RespPartyAddressLine1], " +
                        "[RespPartyAddressLine2], [RespPartyCity], [RespPartyState], [RespPartyZip], [RespPartyPhone], " +
                        "[RespPartyDob], [RespPartySex], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[EffectiveDate], [ExpirationDate]) " +
                        "VALUES " +
                        "(@PmAccountId, @PatientID, @AccountId, @AccountNumber, @AccountName, @AccountSeqNo, " +
                        "@PatientRelToRespParty, @RespPartyNo, @RespPartySalutation, @RespPartyFirstName, " +
                        "@RespPartyMiddleName, @RespPartyLastName, @RespPartySuffix, @RespPartyAddressLine1, " +
                        "@RespPartyAddressLine2, @RespPartyCity, @RespPartyState, @RespPartyZip, @RespPartyPhone, " +
                        "@RespPartyDob, @RespPartySex, @DateLastTouched, @LastTouchedBy, @DateRowAdded, " +
                        "@EffectiveDate, @ExpirationDate); " +
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

        public int UpdateListPmAccounts(ListPmAccountsDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListPmAccountsPoco poco = new ListPmAccountsPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPmAccounts " +
                        "SET [PatientID] = @PatientID, [AccountId] = @AccountId, [AccountNumber] = @AccountNumber, " +
                        "[AccountName] = @AccountName, [AccountSeqNo] = @AccountSeqNo, " +
                        "[PatientRelToRespParty] = @PatientRelToRespParty, [RespPartyNo] = @RespPartyNo, " +
                        "[RespPartySalutation] = @RespPartySalutation, [RespPartyFirstName] = @RespPartyFirstName, " +
                        "[RespPartyMiddleName] = @RespPartyMiddleName, [RespPartyLastName] = @RespPartyLastName, " +
                        "[RespPartySuffix] = @RespPartySuffix, [RespPartyAddressLine1] = @RespPartyAddressLine1, " +
                        "[RespPartyAddressLine2] = @RespPartyAddressLine2, [RespPartyCity] = @RespPartyCity, " +
                        "[RespPartyState] = @RespPartyState, [RespPartyZip] = @RespPartyZip, " +
                        "[RespPartyPhone] = @RespPartyPhone, [RespPartyDob] = @RespPartyDob, " +
                        "[RespPartySex] = @RespPartySex, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[EffectiveDate] = @EffectiveDate, [ExpirationDate] = @ExpirationDate " +
                        "WHERE PmAccountId = @PmAccountId;";

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
