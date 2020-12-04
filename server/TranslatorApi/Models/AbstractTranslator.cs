using TranslatorApi.Models;

namespace TranslatorApi.Models
{
    public interface AbstractTranslator
    {
        string Translate(string text, string from, string to);
    }
}