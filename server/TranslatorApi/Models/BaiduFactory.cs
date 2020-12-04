using TranslatorApi.Models;

namespace TranslatorApi.Models
{
    public class BaiduFactory : AbstractFactory
    {
        public AbstractTranslator CreateTranslator() {
            BaiduTranslator baidu = new BaiduTranslator();
            return baidu;
        }
    }
}