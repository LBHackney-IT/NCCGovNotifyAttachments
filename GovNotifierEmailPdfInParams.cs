﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LbhNCCApi.Models
{
    public class GovNotifierEmailPdfInParams
    {
        public string Id { get; set; }//this is needed to access
        public string ContactId { get; set; }
        public string TenancyAgreementRef { get; set; }
        public string StartDate { get; set; }
        public string EmailTo { get; set; }
        public string TemplateId { get; set; }
        public string TemplateData { get; set; }
    }
}
