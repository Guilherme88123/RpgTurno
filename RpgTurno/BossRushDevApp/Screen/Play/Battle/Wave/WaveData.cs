using Domain.Application.Entity.Units.Base;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Screen.Play.Battle.Wave;

public class WaveData
{
    public List<BaseUnitEntity> Enemies { get; }
    public List<BaseUnitEntity> AliveEnemies => Enemies.Where(x => !x.IsDead).ToList();

    public BaseUnitEntity Boss { get; }
    public bool IsBossWave => Boss is not null;

    public WaveData(List<BaseUnitEntity> enemies)
    {
        Enemies = enemies;
    }

    public WaveData(List<BaseUnitEntity> enemies, BaseUnitEntity boss)
    {
        Enemies = enemies;
        Boss = boss;
    }

    public bool IsCompleted()
    {
        return AliveEnemies.Count == 0;
    }
}
