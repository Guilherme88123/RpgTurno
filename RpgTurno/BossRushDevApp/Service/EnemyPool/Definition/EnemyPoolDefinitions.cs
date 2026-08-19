using Domain.Enum.Unit;

namespace RpgTurno.Service.Map.EnemyPool.Definition;

public static class EnemyPoolDefinitions
{
    public static EnemyDefinition EvilWarrior => new()
    {
        UnitCode = UnitCode.EvilWarrior,
        WaveCost = 3,
        SpawnWeight = 40,
        MaxCopies = 2
    };

    public static EnemyDefinition EvilArcher => new()
    {
        UnitCode = UnitCode.EvilArcher,
        WaveCost = 2,
        SpawnWeight = 55,
        MaxCopies = 2
    };

    public static EnemyDefinition EvilLancer => new()
    {
        UnitCode = UnitCode.EvilLancer,
        WaveCost = 4,
        SpawnWeight = 25,
        MaxCopies = 2
    };

    public static EnemyDefinition EvilCleric => new()
    {
        UnitCode = UnitCode.EvilCleric,
        WaveCost = 4,
        SpawnWeight = 15,
        MaxCopies = 1
    };

    public static EnemyDefinition EvilPawn => new()
    {
        UnitCode = UnitCode.EvilPawn,
        WaveCost = 2,
        SpawnWeight = 35,
        MaxCopies = 3
    };

    public static EnemyDefinition Thief => new()
    {
        UnitCode = UnitCode.Thief,
        WaveCost = 3,
        SpawnWeight = 25,
        MaxCopies = 2
    };

    public static EnemyDefinition Skull => new()
    {
        UnitCode = UnitCode.Skull,
        WaveCost = 2,
        SpawnWeight = 35,
        MaxCopies = 3
    };

    public static EnemyDefinition Gnome => new()
    {
        UnitCode = UnitCode.Gnome,
        WaveCost = 2,
        SpawnWeight = 35,
        MaxCopies = 3
    };

    public static EnemyDefinition Bear => new()
    {
        UnitCode = UnitCode.Bear,
        WaveCost = 5,
        SpawnWeight = 12,
        MaxCopies = 1
    };

    public static EnemyDefinition HexShaman => new()
    {
        UnitCode = UnitCode.HexShaman,
        WaveCost = 4,
        SpawnWeight = 18,
        MaxCopies = 1
    };

    public static EnemyDefinition SpearGoblin => new()
    {
        UnitCode = UnitCode.SpearGoblin,
        WaveCost = 3,
        SpawnWeight = 35,
        MaxCopies = 2
    };

    public static EnemyDefinition TorchGoblin => new()
    {
        UnitCode = UnitCode.TorchGoblin,
        WaveCost = 3,
        SpawnWeight = 30,
        MaxCopies = 2
    };

    public static EnemyDefinition Snake => new()
    {
        UnitCode = UnitCode.Snake,
        WaveCost = 3,
        SpawnWeight = 30,
        MaxCopies = 2
    };

    public static EnemyDefinition Spider => new()
    {
        UnitCode = UnitCode.Spider,
        WaveCost = 3,
        SpawnWeight = 25,
        MaxCopies = 2
    };

    public static EnemyDefinition Turtle => new()
    {
        UnitCode = UnitCode.Turtle,
        WaveCost = 5,
        SpawnWeight = 15,
        MaxCopies = 1
    };

    public static EnemyDefinition Lizard => new()
    {
        UnitCode = UnitCode.Lizard,
        WaveCost = 2,
        SpawnWeight = 35,
        MaxCopies = 3
    };

    public static EnemyDefinition Gnoll => new()
    {
        UnitCode = UnitCode.Gnoll,
        WaveCost = 4,
        SpawnWeight = 20,
        MaxCopies = 2
    };

    public static EnemyDefinition BombFish => new()
    {
        UnitCode = UnitCode.BombFish,
        WaveCost = 4,
        SpawnWeight = 25,
        MaxCopies = 2
    };

    public static EnemyDefinition HarpoonShark => new()
    {
        UnitCode = UnitCode.HarpoonShark,
        WaveCost = 4,
        SpawnWeight = 25,
        MaxCopies = 2
    };

    public static EnemyDefinition PaddleShark => new()
    {
        UnitCode = UnitCode.PaddleShark,
        WaveCost = 3,
        SpawnWeight = 30,
        MaxCopies = 2
    };
}