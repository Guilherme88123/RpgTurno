namespace RpgTurno.Screen.Play.Battle.Stage.Factory.Stages;

public static class BarrackStageFactory
{
    public static StageData Create()
    {
        var waveGenerator = new WaveGenerator();

        return new StageData(
        [
            waveGenerator.Generate(1, 4),
            waveGenerator.Generate(2, 7),
        ]);
    }
}
