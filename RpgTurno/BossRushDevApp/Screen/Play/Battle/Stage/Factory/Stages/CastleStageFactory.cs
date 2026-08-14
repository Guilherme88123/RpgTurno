using Domain.Enum.Unit;
using RpgTurno.Screen.Play.Battle.Wave;
using Service.Unit;

namespace RpgTurno.Screen.Play.Battle.Stage.Factory.Stages;

public static class CastleStageFactory
{
    public static StageData Create()
    {
        var waveGenerator = new WaveGenerator();

        var boss = UnitFactory.Create(UnitCode.SupremeWarrior, level: 20);
        var evilPawn = UnitFactory.Create(UnitCode.EvilPawn);
        var evilWarrior = UnitFactory.Create(UnitCode.EvilWarrior);

        return new StageData(
        [
            waveGenerator.Generate(1, 5),
            waveGenerator.Generate(2, 8),
            new WaveData([evilWarrior, boss, evilPawn], boss),
        ]);
    }
}
