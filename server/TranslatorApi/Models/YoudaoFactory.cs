using TranslatorApi.Models;

namespace TranslatorApi.Models
{
    public class YoudaoFactory : AbstractFactory
    {
        public AbstractTranslator CreateTranslator() {
            YoudaoTranslator youdao = new YoudaoTranslator();
            return youdao;
        }
    }
}