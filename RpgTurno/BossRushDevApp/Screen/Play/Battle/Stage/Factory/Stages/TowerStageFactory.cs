using Domain.Enum.Unit;
using Service.Unit;

namespace RpgTurno.Screen.Play.Battle.Stage.Factory.Stages;

public static class TowerStageFactory
{
    public static StageData Create()
    {
        var waveGenerator = new WaveGenerator();

        var unitCode = UnitCode.BombFish;
        var unit = UnitFactory.Create(unitCode, level: 1);

        return new StageData(
        [
            new Wave.WaveData([unit]), //waveGenerator.Generate(1, 2),
            waveGenerator.Generate(2, 6),
        ]);
    }
}
