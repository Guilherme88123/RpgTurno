using Domain.Const.Language;
using Domain.Enum.Language;
using System.Globalization;

namespace Service.Language;

public static class LanguageCultureFactory
{
    public static CultureInfo Create(LanguageType language)
    {
        return language switch
        {
            LanguageType.English => LanguageConst.CultureEnglish,
            LanguageType.Portuguese => LanguageConst.CulturePortuguese,
            LanguageType.Spanish => LanguageConst.CultureSpanish,

            _ => throw new ArgumentException("Language not supported!"),
        };
    }
}
