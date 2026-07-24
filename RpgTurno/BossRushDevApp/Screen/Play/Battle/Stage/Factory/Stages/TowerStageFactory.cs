using Domain.Model.Entity.Units.Enemy.SuperWarrior;
using RpgTurno.Screen.Play.Battle.Wave;

namespace RpgTurno.Screen.Play.Battle.Stage.Factory.Stages;

public static class TowerStageFactory
{
    public static StageData Create()
    {
        var waveGenerator = new WaveGenerator();

        var boss = new EnemySuperWarriorEntity();

        return new StageData(
        [
            new WaveData([boss], boss),
            waveGenerator.Generate(1, 2),
            waveGenerator.Generate(2, 6),
        ]);
    }
}
