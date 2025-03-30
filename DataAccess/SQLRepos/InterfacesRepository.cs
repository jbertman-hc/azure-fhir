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
    public class InterfacesRepository : IInterfacesRepository
    {
        private readonly string _connectionString;

        public InterfacesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public InterfacesDomain GetInterfaces(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Interfaces WHERE UniqueTableID = @id";

                    var InterfacesPoco = cn.QueryFirstOrDefault<InterfacesPoco>(query, new { id = id }) ?? new InterfacesPoco();

                    return InterfacesPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<InterfacesDomain> GetInterfaces(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Interfaces WHERE @criteria";
                    List<InterfacesPoco> pocos = cn.Query<InterfacesPoco>(sql).ToList();
                    List<InterfacesDomain> domains = new List<InterfacesDomain>();

                    foreach (InterfacesPoco poco in pocos)
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

        public int DeleteInterfaces(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM Interfaces WHERE UniqueTableID = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertInterfaces(InterfacesDomain domain)
        {
            int insertedId = 0;

            try
            {
                InterfacesPoco poco = new InterfacesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Interfaces] " +
                        "([UniqueTableID], [InterfaceID], [InterfaceName], [DateOn], [DateOff], " +
                        "[PracticeCode], [ActivationCode], [IsInterfaceActive], [Comments], [PathToSend], " +
                        "[PathToReceive], [OnlineOverride], [OnlineOverrideDate], [LastOnlineCheckDate], " +
                        "[InterfaceType], [FileFormat], [PatientInfoDir], [PatientInfoAuto], [BillingInfoDir], " +
                        "[BillingInfoAuto], [DateLastTouched], [LastTouchedBy], [DateRowAdded], " +
                        "[IncludeAllDXinFT], [HidePreviousLabs], [ComponentData], [ACEnabled], " +
                        "[ACMessage], [InterfaceLicenseStatusId], [LastCallToAC], [HideXlinkGUI], " +
                        "[DateTimeLastSync], [BillingExportMidLevels]) " +
                        "VALUES " +
                        "(@UniqueTableID, @InterfaceID, @InterfaceName, @DateOn, @DateOff, @PracticeCode, " +
                        "@ActivationCode, @IsInterfaceActive, @Comments, @PathToSend, @PathToReceive, " +
                        "@OnlineOverride, @OnlineOverrideDate, @LastOnlineCheckDate, @InterfaceType, " +
                        "@FileFormat, @PatientInfoDir, @PatientInfoAuto, @BillingInfoDir, @BillingInfoAuto, " +
                        "@DateLastTouched, @LastTouchedBy, @DateRowAdded, @IncludeAllDXinFT, @HidePreviousLabs, " +
                        "@ComponentData, @ACEnabled, @ACMessage, @InterfaceLicenseStatusId, @LastCallToAC, " +
                        "@HideXlinkGUI, @DateTimeLastSync, @BillingExportMidLevels); " +
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

        public int UpdateInterfaces(InterfacesDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                InterfacesPoco poco = new InterfacesPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE Interfaces " +
                        "SET [InterfaceID] = @InterfaceID, [InterfaceName] = @InterfaceName, " +
                        "[DateOn] = @DateOn, [DateOff] = @DateOff, [PracticeCode] = @PracticeCode, " +
                        "[ActivationCode] = @ActivationCode, [IsInterfaceActive] = @IsInterfaceActive, " +
                        "[Comments] = @Comments, [PathToSend] = @PathToSend, [PathToReceive] = @PathToReceive, " +
                        "[OnlineOverride] = @OnlineOverride, [OnlineOverrideDate] = @OnlineOverrideDate, " +
                        "[LastOnlineCheckDate] = @LastOnlineCheckDate, [InterfaceType] = @InterfaceType, " +
                        "[FileFormat] = @FileFormat, [PatientInfoDir] = @PatientInfoDir, " +
                        "[PatientInfoAuto] = @PatientInfoAuto, [BillingInfoDir] = @BillingInfoDir, " +
                        "[BillingInfoAuto] = @BillingInfoAuto, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded, " +
                        "[IncludeAllDXinFT] = @IncludeAllDXinFT, [HidePreviousLabs] = @HidePreviousLabs, " +
                        "[ComponentData] = @ComponentData, [ACEnabled] = @ACEnabled, [ACMessage] = @ACMessage, " +
                        "[InterfaceLicenseStatusId] = @InterfaceLicenseStatusId, " +
                        "[LastCallToAC] = @LastCallToAC, [HideXlinkGUI] = @HideXlinkGUI, " +
                        "[DateTimeLastSync] = @DateTimeLastSync, [BillingExportMidLevels] = @BillingExportMidLevels " +
                        "WHERE UniqueTableID = @UniqueTableID;";

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
