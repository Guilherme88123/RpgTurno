using Domain.Application.Entity.Units.Enemy.Minotaur;
using RpgTurno.Screen.Play.Battle.Wave;

namespace RpgTurno.Screen.Play.Battle.Stage.Factory.Stages;

public static class TowerStageFactory
{
    public static StageData Create()
    {
        var waveGenerator = new WaveGenerator();

        return new StageData(
        [
            new WaveData([new MinotaurEntity(level: 5)]),//waveGenerator.Generate(1, 2),
            waveGenerator.Generate(2, 6),
        ]);
    }
}
