using RpgTurno.Screen.Play.Battle.Stage;
using RpgTurno.Screen.Play.Battle.Stage.Factory;

namespace Service.Stage;

public static class StageFactory
{
    public static StageData Create()
    {
        var waveGenerator = new WaveGenerator();

        return new StageData(
        [
            waveGenerator.Generate(1, 5),
            waveGenerator.Generate(2, 8),
            waveGenerator.Generate(3, 12),
        ]);
    }
}
