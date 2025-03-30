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
    public class VaccineLotNumbersRepository : IVaccineLotNumbersRepository
    {
        private readonly string _connectionString;

        public VaccineLotNumbersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public VaccineLotNumbersDomain GetVaccineLotNumbers(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM VaccineLotNumbers WHERE id = @id";

                    var VaccineLotNumbersPoco = cn.QueryFirstOrDefault<VaccineLotNumbersPoco>(query, new { id = id }) ?? new VaccineLotNumbersPoco();

                    return VaccineLotNumbersPoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<VaccineLotNumbersDomain> GetVaccineLotNumbers(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM VaccineLotNumbers WHERE @criteria";
                    List<VaccineLotNumbersPoco> pocos = cn.Query<VaccineLotNumbersPoco>(sql).ToList();
                    List<VaccineLotNumbersDomain> domains = new List<VaccineLotNumbersDomain>();

                    foreach (VaccineLotNumbersPoco poco in pocos)
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

        public int DeleteVaccineLotNumbers(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM VaccineLotNumbers WHERE id = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertVaccineLotNumbers(VaccineLotNumbersDomain domain)
        {
            int insertedId = 0;

            try
            {
                VaccineLotNumbersPoco poco = new VaccineLotNumbersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[VaccineLotNumbers] " +
                        "([ID], [LotNo], [Expiration], [DateLastTouched], [LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@ID, @LotNo, @Expiration, @DateLastTouched, @LastTouchedBy, @DateRowAdded); " +
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

        public int UpdateVaccineLotNumbers(VaccineLotNumbersDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                VaccineLotNumbersPoco poco = new VaccineLotNumbersPoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE VaccineLotNumbers " +
                        "SET [LotNo] = @LotNo, [Expiration] = @Expiration, [DateLastTouched] = @DateLastTouched, " +
                        "[LastTouchedBy] = @LastTouchedBy, [DateRowAdded] = @DateRowAdded " +
                        "WHERE ID = @ID;";

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
