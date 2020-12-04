using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace TranslatorApi.Models.YoudaoResult
{
    public class TranslationResult
    {
        //错误码，翻译结果无法正常返回
        public string errorCode { get; set; }
        public string query { get; set; }
        public string[] translation { get; set; }
    }

}