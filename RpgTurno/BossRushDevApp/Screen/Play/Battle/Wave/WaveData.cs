using Domain.Model.Entity.Units.Base;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Screen.Play.Battle.Wave;

public class WaveData
{
    public List<BaseUnitEntity> Enemies { get; }

    public WaveData(List<BaseUnitEntity> enemies)
    {
        Enemies = enemies;
    }

    public bool IsCompleted()
    {
        return !Enemies.Any();
    }
}
