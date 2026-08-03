using Domain.Const.Text;
using Domain.Dto.Language;
using Domain.Application.Components.Text;

namespace RpgTurno.Custom.CustomComponents.Play.Wave;

public class WaveIndicatorComponent : TextComponent
{
    public void SetWavesNumber(int currentWave, int totalWaves)
    {
        SetText(GetWavesText(currentWave, totalWaves));
    }

    private string GetWavesText(int currentWave, int totalWaves)
    {
        return $"{LanguageManager.Get(TextConst.Wave)}: {currentWave}/{totalWaves}";
    }
}
