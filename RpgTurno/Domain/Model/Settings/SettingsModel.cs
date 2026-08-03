using Domain.Enum.Language;
using Domain.Model.Base;

namespace Domain.Model.Settings;

public class SettingsModel : BaseModel
{
    public int MusicVolume { get; set; }
    public int EffectsVolume { get; set; }

    public bool Fullscreen { get; set; }
    public bool ShowFps { get; set; }

    public int ResolutionWidth { get; set; }
    public int ResolutionHeight { get; set; }

    public LanguageType Language { get; set; }
}
