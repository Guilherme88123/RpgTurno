using Domain.Model.Components.Text;

namespace RpgTurno.CustomComponents.Wave;

public class WaveIndicatorComponent : TextComponent
{
    public void SetWavesNumber(int currentWave, int totalWaves)
    {
        SetText(GetWavesText(currentWave, totalWaves));
    }

    private string GetWavesText(int currentWave, int totalWaves)
    {
        return $"Wave: {currentWave}/{totalWaves}";
    }
}
