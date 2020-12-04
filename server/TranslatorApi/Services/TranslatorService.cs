using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using Newtonsoft.Json;
using TranslatorApi.Models;

namespace TranslatorApi.Services
{
    public class TranslatorService
    {
        Dictionary <string, string> BaiduTable = new Dictionary<string, string>{
            {"Chinese", "zh"},
            {"English", "en"},
            {"Japanese", "jp"},
            {"Spanish", "spa"},
            {"French", "fra"},
            {"auto", "auto"}
        };
        Dictionary <string, string> YoudaoTable = new Dictionary<string, string>{
            {"Chinese", "zh-CHS"},
            {"English", "en"},
            {"Japanese", "ja"},
            {"Spanish", "es"},
            {"French", "fr"}
        };
        Dictionary <string, string> TencentTable = new Dictionary<string, string>{
            {"Chinese", "zh"},
            {"English", "en"},
            {"Japanese", "jp"},
            {"Spanish", "es"},
            {"French", "fr"}
        };
        string text;
        string from;
        string to;
        public TranslatorService(string text, string from = "auto", string to = "auto")
        {
            this.to = BaiduTable[to];
            this.from = BaiduTable[from];
            this.text = text;
        }

        public TranslationResults GetTranslationResult()
        {
            foreach(string key in BaiduTable.Keys)
            {
                if (BaiduTable[key] == from) from = key;
                if (BaiduTable[key] == to) to = key;
            }
            TranslationResults results = new TranslationResults();
            // 获取对应产品工厂
            BaiduFactory baiduFactory = new BaiduFactory();
            YoudaoFactory youdaoFactory = new YoudaoFactory();
            TencentFactory tencentFactory = new TencentFactory();

            // 生产对应产品
            AbstractTranslator Translator = baiduFactory.CreateTranslator();
            results.BaiduResult = Translator.Translate(text, BaiduTable[from], BaiduTable[to]);

            Translator = youdaoFactory.CreateTranslator();
            results.YoudaoResult = Translator.Translate(text, YoudaoTable[from], YoudaoTable[to]);

            Translator = tencentFactory.CreateTranslator();
            results.TencentResult = Translator.Translate(text, TencentTable[from], TencentTable[to]);
            return results;
        }
    }
}