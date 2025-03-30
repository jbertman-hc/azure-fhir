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
    public class miniTemplateRepository : IminiTemplateRepository
    {
        private readonly string _connectionString;

        public miniTemplateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public miniTemplateDomain GetminiTemplate(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM miniTemplate WHERE miniKey = @id";

                    var miniTemplatePoco = cn.QueryFirstOrDefault<miniTemplatePoco>(query, new { id = id }) ?? new miniTemplatePoco();

                    return miniTemplatePoco.MapToDomainModel();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<miniTemplateDomain> GetminiTemplates(string criteria)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM miniTemplate WHERE @criteria";
                    List<miniTemplatePoco> pocos = cn.Query<miniTemplatePoco>(sql).ToList();
                    List<miniTemplateDomain> domains = new List<miniTemplateDomain>();

                    foreach (miniTemplatePoco poco in pocos)
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

        public int DeleteminiTemplate(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "DELETE * FROM miniTemplate WHERE miniKey = @id";

                    rowsAffected = cn.Execute(sql, new { id = id });

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertminiTemplate(miniTemplateDomain domain)
        {
            int insertedId = 0;

            try
            {
                miniTemplatePoco poco = new miniTemplatePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "INSERT INTO [dbo].[miniTemplate] ([miniKey], [ProviderName], [mTemplateName], " +
                        "[m0cap], [m1cap], [m2cap], [m3cap], [m4cap], [m5cap], [m6cap], [m7cap], [m8cap], [m9cap], " +
                        "[m10cap], [m11cap], [m12cap], [m13cap], [m14cap], [m15cap], [m16cap], [m17cap], [m18cap], " +
                        "[m19cap], [m0text], [m1text], [m2text], [m3text], [m4text], [m5text], [m6text], [m7text], " +
                        "[m8text], [m9text], [m10text], [m11text], [m12text], [m13text], [m14text], [m15text], " +
                        "[m16text], [m17text], [m18text], [m19text], [m0location], [m1location], [m2location], " +
                        "[m31location], [m4location], [m5location], [m6location], [m7location], [m8location], " +
                        "[m9location], [m10location], [m11location], [m12location], [m13location], [m14location], " +
                        "[m15location], [m16location], [m17location], [m18location], [m19location], [DateLastTouched], " +
                        "[LastTouchedBy], [DateRowAdded]) " +
                        "VALUES " +
                        "(@miniKey, @ProviderName, @mTemplateName, @m0cap, @m1cap, @m2cap, @m3cap, @m4cap, @m5cap, " +
                        "@m6cap, @m7cap, @m8cap, @m9cap, @m10cap, @m11cap, @m12cap, @m13cap, @m14cap, @m15cap, " +
                        "@m16cap, @m17cap, @m18cap, @m19cap, @m0text, @m1text, @m2text, @m3text, @m4text, @m5text, " +
                        "@m6text, @m7text, @m8text, @m9text, @m10text, @m11text, @m12text, @m13text, @m14text, " +
                        "@m15text, @m16text, @m17text, @m18text, @m19text, @m0location, @m1location, @m2location, " +
                        "@m31location, @m4location, @m5location, @m6location, @m7location, @m8location, @m9location, " +
                        "@m10location, @m11location, @m12location, @m13location, @m14location, @m15location, " +
                        "@m16location, @m17location, @m18location, @m19location, @DateLastTouched, @LastTouchedBy, " +
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

        public int UpdateminiTemplate(miniTemplateDomain domain)
        {
            int rowsAffected = 0;

            try
            {
                miniTemplatePoco poco = new miniTemplatePoco(domain);

                using (var cn = new SqlConnection(_connectionString))
                {
                    string sql = "UPDATE miniTemplate " +
                        "SET [ProviderName] = @ProviderName, [mTemplateName] = @mTemplateName, [m0cap] = @m0cap, " +
                        "[m1cap] = @m1cap, [m2cap] = @m2cap, [m3cap] = @m3cap, [m4cap] = @m4cap, [m5cap] = @m5cap, " +
                        "[m6cap] = @m6cap, [m7cap] = @m7cap, [m8cap] = @m8cap, [m9cap] = @m9cap, [m10cap] = @m10cap, " +
                        "[m11cap] = @m11cap, [m12cap] = @m12cap, [m13cap] = @m13cap, [m14cap] = @m14cap, " +
                        "[m15cap] = @m15cap, [m16cap] = @m16cap, [m17cap] = @m17cap, [m18cap] = @m18cap, " +
                        "[m19cap] = @m19cap, [m0text] = @m0text, [m1text] = @m1text, [m2text] = @m2text, " +
                        "[m3text] = @m3text, [m4text] = @m4text, [m5text] = @m5text, [m6text] = @m6text, " +
                        "[m7text] = @m7text, [m8text] = @m8text, [m9text] = @m9text, [m10text] = @m10text, " +
                        "[m11text] = @m11text, [m12text] = @m12text, [m13text] = @m13text, [m14text] = @m14text, " +
                        "[m15text] = @m15text, [m16text] = @m16text, [m17text] = @m17text, [m18text] = @m18text, " +
                        "[m19text] = @m19text, [m0location] = @m0location, [m1location] = @m1location, " +
                        "[m2location] = @m2location, [m31location] = @m31location, [m4location] = @m4location, " +
                        "[m5location] = @m5location, [m6location] = @m6location, [m7location] = @m7location, " +
                        "[m8location] = @m8location, [m9location] = @m9location, [m10location] = @m10location, " +
                        "[m11location] = @m11location, [m12location] = @m12location, [m13location] = @m13location, " +
                        "[m14location] = @m14location, [m15location] = @m15location, [m16location] = @m16location, " +
                        "[m17location] = @m17location, [m18location] = @m18location, [m19location] = @m19location, " +
                        "[DateLastTouched] = @DateLastTouched, [LastTouchedBy] = @LastTouchedBy, " +
                        "[DateRowAdded] = @DateRowAdded " +
                        "WHERE miniKey = @miniKey;";

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
