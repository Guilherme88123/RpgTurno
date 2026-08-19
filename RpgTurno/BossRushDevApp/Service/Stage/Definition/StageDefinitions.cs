using Domain.Enum.Region;
using Domain.Enum.Stage;
using Domain.Enum.Unit;

namespace RpgTurno.Service.Map.Stage.Definition;

public static class StageDefinitions
{
    public static StageDefinition EvilTower = new()
    {
        StageCode = StageCode.EvilTower,
        RegionCode = RegionCode.ForgottenFields,
        WaveCount = 2,
        BaseWaveBudget = 2,
        WaveBudgetIncrease = 4,
    };

    public static StageDefinition BarracksOfValor = new()
    {
        StageCode = StageCode.BarracksOfValor,
        RegionCode = RegionCode.ForgottenFields,
        WaveCount = 2,
        BaseWaveBudget = 4,
        WaveBudgetIncrease = 3,
    };

    public static StageDefinition TheCastle = new()
    {
        StageCode = StageCode.TheCastle,
        RegionCode = RegionCode.ForgottenFields,
        WaveCount = 3,
        BaseWaveBudget = 5,
        WaveBudgetIncrease = 3,

        IsBossStage = true,
        BossCode = UnitCode.SupremeWarrior,
        BossLevel = 20,
        BossSupportUnits = [UnitCode.EvilCleric, UnitCode.EvilPawn],
    };
}
