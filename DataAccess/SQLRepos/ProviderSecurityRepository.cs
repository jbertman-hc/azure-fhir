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
    public class ProviderSecurityRepository : IProviderSecurityRepository
    {
        private readonly string _connectionString;

        public ProviderSecurityRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProviderSecurityDomain GetProviderSecurity(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ProviderSecurity WHERE ProviderID = @id";

                    var ProviderSecurityPoco = cn.QueryFirstOrDefault<ProviderSecurityPoco>(query, new { id = id }) ?? new ProviderSecurityPoco();

                    return ProviderSecurityPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ProviderSecurityDomain> GetProviderSecuritys(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ProviderSecurity WHERE @criteria";
                    List<ProviderSecurityPoco> pocos = cn.Query<ProviderSecurityPoco>(sql).ToList();
                    List<ProviderSecurityDomain> domains = new List<ProviderSecurityDomain>();

                    foreach (ProviderSecurityPoco poco in pocos)
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

        public int DeleteProviderSecurity(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ProviderSecurity WHERE ProviderID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertProviderSecurity(ProviderSecurityDomain domain)
        {
            int insertedId = 0;

            try
            {
                ProviderSecurityPoco poco = new ProviderSecurityPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ProviderSecurity] " +
                        "([ProviderID], [ProviderName], [ProviderPassword], [DatePassLastChanged], [FirstName], " +
                        "[MiddleName], [LastName], [Degree], [DEA], [StateLicenseNumber], [State], [ProviderLevel], " +
                        "[CoSignReq], [Supervisor], [NotifySupervisor], [Inactive], [UPIN], [EINoverride], [Specialty], " +
                        "[XLinkProviderID], [NPI], [ProviderSig], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[LockedOut], [ResetPasswordRequired], [AllowStaffToTransmit], [PrescribeFor], " +
                        "[AllowOverrideInteraction], [AllowEmergencyOverride], [TaxonomyCode1], [TaxonomyCode2], " +
                        "[AcceptsMedicareAssignment], [AllowBillingAccess], [AllowAccessToPatientHealthInfo]) " +
                        "VALUES " +
                        "(@ProviderID, @ProviderName, @ProviderPassword, @DatePassLastChanged, @FirstName, " +
                        "@MiddleName, @LastName, @Degree, @DEA, @StateLicenseNumber, @State, @ProviderLevel, " +
                        "@CoSignReq, @Supervisor, @NotifySupervisor, @Inactive, @UPIN, @EINoverride, @Specialty, " +
                        "@XLinkProviderID, @NPI, @ProviderSig, @DateLastTouched, @LastTouchedBy, @DateRowAdded, " +
                        "@LockedOut, @ResetPasswordRequired, @AllowStaffToTransmit, @PrescribeFor, " +
                        "@AllowOverrideInteraction, @AllowEmergencyOverride, @TaxonomyCode1, @TaxonomyCode2, " +
                        "@AcceptsMedicareAssignment, @AllowBillingAccess, @AllowAccessToPatientHealthInfo); " +
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

        public int UpdateProviderSecurity(ProviderSecurityDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ProviderSecurityPoco poco = new ProviderSecurityPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ProviderSecurity " +
                        "SET [ProviderName] = @ProviderName, [ProviderPassword] = @ProviderPassword, " +
                        "[DatePassLastChanged] = @DatePassLastChanged, [FirstName] = @FirstName, " +
                        "[MiddleName] = @MiddleName, [LastName] = @LastName, [Degree] = @Degree, [DEA] = @DEA, " +
                        "[StateLicenseNumber] = @StateLicenseNumber, [State] = @State, [ProviderLevel] = @ProviderLevel, " +
                        "[CoSignReq] = @CoSignReq, [Supervisor] = @Supervisor, [NotifySupervisor] = @NotifySupervisor, " +
                        "[Inactive] = @Inactive, [UPIN] = @UPIN, [EINoverride] = @EINoverride, [Specialty] = @Specialty, " +
                        "[XLinkProviderID] = @XLinkProviderID, [NPI] = @NPI, [ProviderSig] = @ProviderSig, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded, [LockedOut] = @LockedOut, " +
                        "[ResetPasswordRequired] = @ResetPasswordRequired, [AllowStaffToTransmit] = @AllowStaffToTransmit, " +
                        "[PrescribeFor] = @PrescribeFor, [AllowOverrideInteraction] = @AllowOverrideInteraction, " +
                        "[AllowEmergencyOverride] = @AllowEmergencyOverride, [TaxonomyCode1] = @TaxonomyCode1, " +
                        "[TaxonomyCode2] = @TaxonomyCode2, [AcceptsMedicareAssignment] = @AcceptsMedicareAssignment, " +
                        "[AllowBillingAccess] = @AllowBillingAccess, " +
                        "[AllowAccessToPatientHealthInfo] = @AllowAccessToPatientHealthInfo " +
                        "WHERE ProviderID = @ProviderID;";

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
