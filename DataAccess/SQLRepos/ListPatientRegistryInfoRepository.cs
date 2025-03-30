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
    public class ListPatientRegistryInfoRepository : IListPatientRegistryInfoRepository
    {
        private readonly string _connectionString;

        public ListPatientRegistryInfoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ListPatientRegistryInfoDomain GetListPatientRegistryInfo(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ListPatientRegistryInfo WHERE ListPatientRegistryInfoId = @id";

                    var ListPatientRegistryInfoPoco = cn.QueryFirstOrDefault<ListPatientRegistryInfoPoco>(query, new { id = id }) ?? new ListPatientRegistryInfoPoco();

                    return ListPatientRegistryInfoPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ListPatientRegistryInfoDomain> GetListPatientRegistryInfos(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ListPatientRegistryInfo WHERE @criteria";
                    List<ListPatientRegistryInfoPoco> pocos = cn.Query<ListPatientRegistryInfoPoco>(sql).ToList();
                    List<ListPatientRegistryInfoDomain> domains = new List<ListPatientRegistryInfoDomain>();

                    foreach (ListPatientRegistryInfoPoco poco in pocos)
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

        public int DeleteListPatientRegistryInfo(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ListPatientRegistryInfo WHERE ListPatientRegistryInfoId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertListPatientRegistryInfo(ListPatientRegistryInfoDomain domain)
        {
            int insertedId = 0;

            try
            {
                ListPatientRegistryInfoPoco poco = new ListPatientRegistryInfoPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ListPatientRegistryInfo] " +
                        "([ListPatientRegistryInfoId], [PatientId], [RegistryInterfaceId], [VFCReasonId], " +
                        "[DateVFCInitialScreen], [ProtectionIndicator], [DateProtectionIndicator], " +
                        "[DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ListPatientRegistryInfoId, @PatientId, @RegistryInterfaceId, @VFCReasonId, " +
                        "@DateVFCInitialScreen, @ProtectionIndicator, @DateProtectionIndicator, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateListPatientRegistryInfo(ListPatientRegistryInfoDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ListPatientRegistryInfoPoco poco = new ListPatientRegistryInfoPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ListPatientRegistryInfo " +
                        "SET [PatientId] = @PatientId, [RegistryInterfaceId] = @RegistryInterfaceId, " +
                        "[VFCReasonId] = @VFCReasonId, [DateVFCInitialScreen] = @DateVFCInitialScreen, " +
                        "[ProtectionIndicator] = @ProtectionIndicator, " +
                        "[DateProtectionIndicator] = @DateProtectionIndicator, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE ListPatientRegistryInfoId = @ListPatientRegistryInfoId;";

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
