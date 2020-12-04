using TranslatorApi.Models;

namespace TranslatorApi.Models
{
    public interface AbstractFactory
    {
        AbstractTranslator CreateTranslator();
    }
}