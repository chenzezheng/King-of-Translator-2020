using TranslatorApi.Models;

namespace TranslatorApi.Models
{
    public class TencentFactory : AbstractFactory
    {
        public AbstractTranslator CreateTranslator() {
            TencentTranslator tencent = new TencentTranslator();
            return tencent;
        }
    }
}