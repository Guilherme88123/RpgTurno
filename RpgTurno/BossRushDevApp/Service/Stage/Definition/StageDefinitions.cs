using Domain.Enum.Region;
using Domain.Enum.Stage;
using Domain.Enum.Unit;

namespace RpgTurno.Service.Map.Stage.Definition;

public static class StageDefinitions
{
    public static StageDefinition ForgottenFields01 { get; } = new()
    {
        StageCode = StageCode.ForgottenFields01,
        RegionCode = RegionCode.ForgottenFields,

        WaveCount = 2,
        BaseWaveBudget = 2,
        WaveBudgetIncrease = 2,
    };

    public static StageDefinition ForgottenFields02 { get; } = new()
    {
        StageCode = StageCode.ForgottenFields02,
        RegionCode = RegionCode.ForgottenFields,

        WaveCount = 2,
        BaseWaveBudget = 3,
        WaveBudgetIncrease = 2,
    };

    public static StageDefinition ForgottenFields03 { get; } = new()
    {
        StageCode = StageCode.ForgottenFields03,
        RegionCode = RegionCode.ForgottenFields,

        WaveCount = 3,
        BaseWaveBudget = 4,
        WaveBudgetIncrease = 2,
    };

    public static StageDefinition ForgottenFieldsBoss { get; } = new()
    {
        StageCode = StageCode.ForgottenFieldsBoss,
        RegionCode = RegionCode.ForgottenFields,

        WaveCount = 3,
        BaseWaveBudget = 5,
        WaveBudgetIncrease = 2,

        IsBossStage = true,

        BossCode = UnitCode.Minotaur,
        BossLevel = 20,

        BossSupportUnits =
        [
            UnitCode.Gnome,
            UnitCode.Bear
        ],
    };

    public static StageDefinition GoblinLands01 { get; } = new()
    {
        StageCode = StageCode.GoblinLands01,
        RegionCode = RegionCode.GoblinLands,

        WaveCount = 2,
        BaseWaveBudget = 5,
        WaveBudgetIncrease = 2,
    };

    public static StageDefinition GoblinLands02 { get; } = new()
    {
        StageCode = StageCode.GoblinLands02,
        RegionCode = RegionCode.GoblinLands,

        WaveCount = 2,
        BaseWaveBudget = 6,
        WaveBudgetIncrease = 3,
    };

    public static StageDefinition GoblinLands03 { get; } = new()
    {
        StageCode = StageCode.GoblinLands03,
        RegionCode = RegionCode.GoblinLands,

        WaveCount = 3,
        BaseWaveBudget = 7,
        WaveBudgetIncrease = 3,
    };

    public static StageDefinition GoblinLandsBoss { get; } = new()
    {
        StageCode = StageCode.GoblinLandsBoss,
        RegionCode = RegionCode.GoblinLands,

        WaveCount = 3,
        BaseWaveBudget = 8,
        WaveBudgetIncrease = 3,

        IsBossStage = true,

        BossCode = UnitCode.PigRider,
        BossLevel = 40,

        BossSupportUnits =
        [
            UnitCode.SpearGoblin,
            UnitCode.TorchGoblin,
            UnitCode.HexShaman
        ],
    };

    public static StageDefinition ShadowSwamp01 { get; } = new()
    {
        StageCode = StageCode.ShadowSwamp01,
        RegionCode = RegionCode.ShadowSwamp,

        WaveCount = 2,
        BaseWaveBudget = 8,
        WaveBudgetIncrease = 2,
    };

    public static StageDefinition ShadowSwamp02 { get; } = new()
    {
        StageCode = StageCode.ShadowSwamp02,
        RegionCode = RegionCode.ShadowSwamp,

        WaveCount = 2,
        BaseWaveBudget = 9,
        WaveBudgetIncrease = 3,
    };

    public static StageDefinition ShadowSwamp03 { get; } = new()
    {
        StageCode = StageCode.ShadowSwamp03,
        RegionCode = RegionCode.ShadowSwamp,

        WaveCount = 3,
        BaseWaveBudget = 10,
        WaveBudgetIncrease = 3,
    };

    public static StageDefinition ShadowSwampBoss { get; } = new()
    {
        StageCode = StageCode.ShadowSwampBoss,
        RegionCode = RegionCode.ShadowSwamp,

        WaveCount = 3,
        BaseWaveBudget = 11,
        WaveBudgetIncrease = 3,

        IsBossStage = true,

        BossCode = UnitCode.Troll,
        BossLevel = 60,

        BossSupportUnits =
        [
            UnitCode.Spider,
            UnitCode.Snake,
            UnitCode.Gnoll
        ],
    };

    public static StageDefinition PirateCoast01 { get; } = new()
    {
        StageCode = StageCode.PirateCoast01,
        RegionCode = RegionCode.PirateCoast,

        WaveCount = 2,
        BaseWaveBudget = 11,
        WaveBudgetIncrease = 3,
    };

    public static StageDefinition PirateCoast02 { get; } = new()
    {
        StageCode = StageCode.PirateCoast02,
        RegionCode = RegionCode.PirateCoast,

        WaveCount = 2,
        BaseWaveBudget = 12,
        WaveBudgetIncrease = 3,
    };

    public static StageDefinition PirateCoast03 { get; } = new()
    {
        StageCode = StageCode.PirateCoast03,
        RegionCode = RegionCode.PirateCoast,

        WaveCount = 3,
        BaseWaveBudget = 13,
        WaveBudgetIncrease = 3,
    };

    public static StageDefinition PirateCoastBoss { get; } = new()
    {
        StageCode = StageCode.PirateCoastBoss,
        RegionCode = RegionCode.PirateCoast,

        WaveCount = 3,
        BaseWaveBudget = 14,
        WaveBudgetIncrease = 3,

        IsBossStage = true,

        // TODO: definir boss da Pirate Coast
        BossCode = null,
        BossLevel = 80,

        BossSupportUnits =
        [
            UnitCode.BombFish,
            UnitCode.HarpoonShark,
            UnitCode.PaddleShark
        ],
    };


    // =========================================================
    // THE KINGDOM
    // Level range: 21–25
    // Boss: Supreme Warrior
    // =========================================================

    public static StageDefinition Kingdom01 { get; } = new()
    {
        StageCode = StageCode.Kingdom01,
        RegionCode = RegionCode.TheKingdom,

        WaveCount = 2,
        BaseWaveBudget = 7,
        WaveBudgetIncrease = 2,
    };

    public static StageDefinition Kingdom02 { get; } = new()
    {
        StageCode = StageCode.Kingdom02,
        RegionCode = RegionCode.TheKingdom,

        WaveCount = 2,
        BaseWaveBudget = 8,
        WaveBudgetIncrease = 3,
    };

    public static StageDefinition Kingdom03 { get; } = new()
    {
        StageCode = StageCode.Kingdom03,
        RegionCode = RegionCode.TheKingdom,

        WaveCount = 3,
        BaseWaveBudget = 8,
        WaveBudgetIncrease = 3,
    };

    public static StageDefinition Kingdom04 { get; } = new()
    {
        StageCode = StageCode.Kingdom04,
        RegionCode = RegionCode.TheKingdom,

        WaveCount = 3,
        BaseWaveBudget = 10,
        WaveBudgetIncrease = 3,
    };

    public static StageDefinition KingdomBoss { get; } = new()
    {
        StageCode = StageCode.KingdomBoss,
        RegionCode = RegionCode.TheKingdom,

        WaveCount = 3,
        BaseWaveBudget = 10,
        WaveBudgetIncrease = 3,

        IsBossStage = true,

        BossCode = UnitCode.SupremeWarrior,
        BossLevel = 100,

        BossSupportUnits =
        [
            UnitCode.EvilCleric,
            UnitCode.EvilPawn
        ],
    };
}