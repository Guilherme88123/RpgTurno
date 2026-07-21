using Domain.Model.Entity.Units.Enemy.SuperWarrior;
using Domain.Model.Entity.Units.Enemy.Warrior;
using RpgTurno.Screen.Play.Battle.Wave;

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
            new WaveData([new EnemyWarriorEntity(), new EnemySuperWarriorEntity(), new EnemyWarriorEntity()]),
        ]);
    }
}
