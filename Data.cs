using LbhNCCApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Configuration;
using NLog;
namespace NCCGovNotifyAttachments
{
    class Data
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string _connstring = ConfigurationManager.AppSettings["CRM365BISQLConnString"];

        public static List<GovNotifierEmailPdfInParams> FetchAll()
        {
            try
            {
                using (var conn = new SqlConnection(_connstring))
                {
                    logger.Debug($@"Calling query for Fetch all");
                    var results = conn.Query<GovNotifierEmailPdfInParams>(
                        $@"select [Id], [ContactId], [TenancyAgreementRef], [StartDate],
                        [GovTemplateId] TemplateId, [GovTemplateData] TemplateData,[EmailId] EmailTo from [LBH_Ext_GovNotifyEmailStatements]
                        where [Status] = 1 or [Status] <> 0"
                    ).ToList();
                    logger.Debug($@"Coming back with result {results.Count}");
                    return results;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in FetchAll " + ex.Message);
                logger.Error(ex, "Error occured in Fetchall $$$$$ " + ex.Message);
                return null;
            }

        }


        public static void StatusUpdate(string Id, string Status, string StatusDescription, string DebugDescription)
        {
            try
            {
                string query = $@"Update  [dbo].[LBH_Ext_GovNotifyEmailStatements] SET
                                             [Status] ='{Status}', [StatusDescription] = '{StatusDescription}', [DebugErrorMessage]='{DebugDescription}' 
                                        Where Id = '{Id}'";
                int result = -1;
                using (var conn = new SqlConnection(_connstring))
                {
                    result = conn.Execute(query);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in StatusUpdate "+ ex.Message);
                logger.Error(ex, "Error occured in StatusUpdate $$$$$ " + ex.Message);
            }
        }

    }
}
