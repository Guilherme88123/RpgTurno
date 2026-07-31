using Domain.Dto.Global;
using Domain.Interface.Language;

namespace Domain.Dto.Language;

public static class LanguageManager
{
    private static readonly ILanguageService _service = GlobalVariablesDto.GetService<ILanguageService>();

    public static string Get(string key) => _service.Get(key);
}
