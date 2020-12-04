using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace TranslatorApi.Models.TencentResult
{
    public class Translation
    {
        public string source_text { get; set; }
        public string target_text { get; set; }
    }

    public class TranslationResult
    {
        public string ret { get; set; }
        public string msg { get; set; }
        public Translation data { get; set; }
    }
}