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
    public class ClinicalAssessmentMreDataRepository : IClinicalAssessmentMreDataRepository
    {
        private readonly string _connectionString;

        public ClinicalAssessmentMreDataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ClinicalAssessmentMreDataDomain GetClinicalAssessmentMreData(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM ClinicalAssessmentMreData WHERE PatientId = @id";

                    var ClinicalAssessmentMreDataPoco = cn.QueryFirstOrDefault<ClinicalAssessmentMreDataPoco>(query, new { id = id }) ?? new ClinicalAssessmentMreDataPoco();

                    return ClinicalAssessmentMreDataPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ClinicalAssessmentMreDataDomain> GetClinicalAssessmentMreDatas(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM ClinicalAssessmentMreData WHERE @criteria";
                    List<ClinicalAssessmentMreDataPoco> pocos = cn.Query<ClinicalAssessmentMreDataPoco>(sql).ToList();
                    List<ClinicalAssessmentMreDataDomain> domains = new List<ClinicalAssessmentMreDataDomain>();

                    foreach (ClinicalAssessmentMreDataPoco poco in pocos)
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

        public int DeleteClinicalAssessmentMreData(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM ClinicalAssessmentMreData WHERE PatientId = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertClinicalAssessmentMreData(ClinicalAssessmentMreDataDomain domain)
        {
            int insertedId = 0;

            try
            {
                ClinicalAssessmentMreDataPoco poco = new ClinicalAssessmentMreDataPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[ClinicalAssessmentMreData] " +
                        "([PatientId], [O2Sat], [O2SatSuppAmount], [O2SatAirSource], [O2SatSuppType], " +
                        "[SystolicBp], [DiastolicBp], [Temperature], [RespRate], [Pulse], [Weight], " +
                        "[Height], [HeadCircumference], [Edd], [Lmp], [Bmi], [TobaccoStatusStartDate], " +
                        "[TobaccoStatusEndDate], [TobaccoPipeSmoker], [TobaccoPipeStartDate], " +
                        "[TobaccoPipeEndDate], [TobaccoCigarSmoker], [TobaccoCigarStartDate], " +
                        "[TobaccoCigarEndDate], [TobaccoChewing], [TobaccoChewingStartDate], " +
                        "[TobaccoChewingEndDate], [TobaccoCdcCode], [DateLastTouched], [LastTouchedBy], " +
                        "[DateRowAdded]) " +
                        "VALUES " +
                        "(@PatientId, @O2Sat, @O2SatSuppAmount, @O2SatAirSource, @O2SatSuppType, " +
                        "@SystolicBp, @DiastolicBp, @Temperature, @RespRate, @Pulse, @Weight, " +
                        "@Height, @HeadCircumference, @Edd, @Lmp, @Bmi, @TobaccoStatusStartDate, " +
                        "@TobaccoStatusEndDate, @TobaccoPipeSmoker, @TobaccoPipeStartDate, " +
                        "@TobaccoPipeEndDate, @TobaccoCigarSmoker, @TobaccoCigarStartDate, " +
                        "@TobaccoCigarEndDate, @TobaccoChewing, @TobaccoChewingStartDate, " +
                        "@TobaccoChewingEndDate, @TobaccoCdcCode, @DateLastTouched, @LastTouchedBy, " +
                        "@DateRowAdded); " +
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

        public int UpdateClinicalAssessmentMreData(ClinicalAssessmentMreDataDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                ClinicalAssessmentMreDataPoco poco = new ClinicalAssessmentMreDataPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE ClinicalAssessmentMreData " +
                        "SET [O2Sat] = @O2Sat, [O2SatSuppAmount] = @O2SatSuppAmount, " +
                        "[O2SatAirSource] = @O2SatAirSource, [O2SatSuppType] = @O2SatSuppType, " +
                        "[SystolicBp] = @SystolicBp, [DiastolicBp] = @DiastolicBp, " +
                        "[Temperature] = @Temperature, [RespRate] = @RespRate, [Pulse] = @Pulse, " +
                        "[Weight] = @Weight, [Height] = @Height, [HeadCircumference] = @HeadCircumference, " +
                        "[Edd] = @Edd, [Lmp] = @Lmp, [Bmi] = @Bmi, " +
                        "[TobaccoStatusStartDate] = @TobaccoStatusStartDate, " +
                        "[TobaccoStatusEndDate] = @TobaccoStatusEndDate, " +
                        "[TobaccoPipeSmoker] = @TobaccoPipeSmoker, " +
                        "[TobaccoPipeStartDate] = @TobaccoPipeStartDate, " +
                        "[TobaccoPipeEndDate] = @TobaccoPipeEndDate, " +
                        "[TobaccoCigarSmoker] = @TobaccoCigarSmoker, " +
                        "[TobaccoCigarStartDate] = @TobaccoCigarStartDate, " +
                        "[TobaccoCigarEndDate] = @TobaccoCigarEndDate, " +
                        "[TobaccoChewing] = @TobaccoChewing, " +
                        "[TobaccoChewingStartDate] = @TobaccoChewingStartDate, " +
                        "[TobaccoChewingEndDate] = @TobaccoChewingEndDate, " +
                        "[TobaccoCdcCode] = @TobaccoCdcCode, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE PatientId = @PatientId;";

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
