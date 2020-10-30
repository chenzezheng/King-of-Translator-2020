using TranslatorApi.Models;

namespace TranslatorApi.Models
{
    interface ITranslate
    {
        string Translate(string text, string from, string to);
    }
    public class TranslationResults
    {
        public string BaiduResult = "";
        public string YoudaoResult = "";
        public string TencentResult = "";
    }
}