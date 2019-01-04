using ceTe.DynamicPDF;
using LbhNCCApi.Models;
using NCCPdfReports;
using Newtonsoft.Json;
using Notify.Client;
using Notify.Models.Responses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using NLog;

namespace NCCGovNotifyAttachments
{
    class GovNotify
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static string _apiKey = ConfigurationManager.AppSettings["GovNotifyAPIKey"];
        /// <summary>
        /// Send Email with specified template using template data
        /// </summary>
        /// <param name="EmailTo">To email id</param>
        /// <param name="TemplateId">Gov Notifier Template Id</param>
        /// <param name="TemplateData">key value pair of the data in template</param>
        /// <returns></returns>

        public static void SendEmail(GovNotifierEmailPdfInParams inparam)
        {
            logger.Debug($@"Inside SendEmail for Id = {inparam.Id}");
            StringBuilder DebugInfo = new StringBuilder("Initiated for Pdf generation.\r\n");
            try
            {
                Data.StatusUpdate(inparam.Id, "2", "In progress", DebugInfo.Append("Inside SendEmail to start Generating pdf.\r\n").ToString());
                logger.Debug($@"In progress with for GeneratePdfDocument");
                Document document = new BuildDoc().GeneratePdfDocument(inparam.ContactId, inparam.StartDate, inparam.EndDate);
                
                if (document!=null)
                {
                    Data.StatusUpdate(inparam.Id, "3", "Document Created", DebugInfo.Append("Document created.\r\n").ToString());
                    logger.Debug($@"Document Created");
                    byte[] docbytes = document.Draw();
                    logger.Debug($@"Document Size = {docbytes.Length}");
                    NotificationClient client = new NotificationClient(_apiKey);
                    var TemplateDataDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(inparam.TemplateData);
                    logger.Debug($@"Creating Personalization");
                    Dictionary<string, dynamic> personalisation = new Dictionary<string, dynamic>();
                    foreach (KeyValuePair<string, string> pair in TemplateDataDict)
                    {
                        personalisation.Add(pair.Key.ToString(), pair.Value.ToString());
                        logger.Debug($@"Adding Personalization for key = {pair.Key.ToString()} value = {pair.Value.ToString()}");
                    }
                    personalisation.Add("link_to_document", NotificationClient.PrepareUpload(docbytes));
                    logger.Debug($@"Linked document to template");
                    Data.StatusUpdate(inparam.Id, "4", "Sending email", DebugInfo.Append("Emailing with pdf attachment.\r\n").ToString());

                    EmailNotificationResponse response = client.SendEmail(inparam.EmailTo, inparam.TemplateId, personalisation);
                    logger.Debug($@"Email Sent successfully to Gov Notifier with attachments {response.id}");
                    Data.StatusUpdate(inparam.Id, "0", "Email Sent Successfully", DebugInfo.Append("Completed.\r\n").ToString());
                }
            }
            catch (Exception ex)
            {
                Data.StatusUpdate(inparam.Id, "-1", "Error Occurred", DebugInfo.Append("Error occured : \r\n").ToString() + ex.Message);
                logger.Error(ex, "Error occured in Send Email $$$$$ "+ ex.Message);
            }
        }

    }
}
