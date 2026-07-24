namespace RpgTurno.Screen.Play.Battle.Stage.Factory.Stages;

public static class TowerStageFactory
{
    public static StageData Create()
    {
        var waveGenerator = new WaveGenerator();

        return new StageData(
        [
            waveGenerator.Generate(1, 2),
            waveGenerator.Generate(2, 6),
        ]);
    }
}
