using LbhNCCApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCCGovNotifyAttachments
{
    class Scheduler
    {
        public static void Run()
        {
            List<GovNotifierEmailPdfInParams> list = Data.FetchAll();
            if(list!=null)
            {
                foreach (GovNotifierEmailPdfInParams param in list)
                {
                    GovNotify.SendEmail(param);
                }
            }
        }
    }
}
