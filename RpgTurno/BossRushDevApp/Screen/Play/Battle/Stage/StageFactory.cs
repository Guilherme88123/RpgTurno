using Domain.Model.Entity.Units.Enemy.Archer;
using Domain.Model.Entity.Units.Enemy.Warrior;
using Domain.Model.Entity.Units.Enemy.Cleric;
using Domain.Model.Entity.Units.Enemy.Lancer;
using RpgTurno.Screen.Play.Battle.Wave;
using System.Linq.Expressions;

namespace RpgTurno.Screen.Play.Battle.Stage;

public static class StageFactory
{
    public static StageData Create()
    {
        return new StageData(
        [
            new WaveData(
            [
                new EnemyWarriorEntity(),
                new EnemyArcherEntity(),
            ]),
            new WaveData(
            [
                new EnemyWarriorEntity(),
                new EnemyLancerEntity(),
                new EnemyArcherEntity(),
            ]),
            new WaveData(
            [
                new EnemyWarriorEntity(),
                new EnemyClericEntity(),
                new EnemyArcherEntity(),
            ]),
        ]);
    }
}
