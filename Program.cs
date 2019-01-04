using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
namespace NCCGovNotifyAttachments
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("NCC Gov Notify Attachment Scheduler Start************************");
            Scheduler.Run();
            logger.Info("NCC Gov Notify Attachment Scheduler End++++++++++++++++++++++++++");
        }
    }
}
