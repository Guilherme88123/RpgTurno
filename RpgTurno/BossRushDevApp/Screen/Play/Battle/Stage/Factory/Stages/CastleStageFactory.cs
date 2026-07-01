namespace RpgTurno.Screen.Play.Battle.Stage.Factory.Stages;

public static class CastleStageFactory
{
    public static StageData Create()
    {
        var waveGenerator = new WaveGenerator();

        return new StageData(
        [
            waveGenerator.Generate(1, 5),
            waveGenerator.Generate(2, 8),
            waveGenerator.Generate(3, 13),
        ]);
    }
}
