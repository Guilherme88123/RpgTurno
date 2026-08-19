using Domain.Enum.Region;
using RpgTurno.Service.Map.EnemyPool.Definition;
using System;

namespace RpgTurno.Service.Map.EnemyPool;

public static class EnemyPoolFactory
{
    public static EnemyPoolData Create(RegionCode regionCode)
    {
        return regionCode switch
        {
            RegionCode.ForgottenFields => CreateForgottenFields(),
            RegionCode.GoblinLands => CreateGoblinLands(),
            RegionCode.ShadowSwamp => CreateShadowSwamp(),
            RegionCode.PirateCoast => CreatePirateCoast(),
            RegionCode.TheKingdom => CreateTheKingdom(),

            _ => throw new ArgumentException("Invalid region code for enemy pool!")
        };
    }

    private static EnemyPoolData CreateForgottenFields() => new([
        EnemyPoolDefinitions.EvilPawn,
        EnemyPoolDefinitions.Thief,
        EnemyPoolDefinitions.Skull,
        EnemyPoolDefinitions.Gnome,
        EnemyPoolDefinitions.Bear
    ]);

    private static EnemyPoolData CreateGoblinLands() => new([
        EnemyPoolDefinitions.HexShaman,
        EnemyPoolDefinitions.SpearGoblin,
        EnemyPoolDefinitions.TorchGoblin,
        EnemyPoolDefinitions.Gnome
    ]);

    private static EnemyPoolData CreateShadowSwamp() => new([
        EnemyPoolDefinitions.Snake,
        EnemyPoolDefinitions.Spider,
        EnemyPoolDefinitions.Turtle,
        EnemyPoolDefinitions.Lizard,
        EnemyPoolDefinitions.Gnoll,
        EnemyPoolDefinitions.Bear
    ]);

    private static EnemyPoolData CreatePirateCoast() => new([
        EnemyPoolDefinitions.BombFish,
        EnemyPoolDefinitions.HarpoonShark,
        EnemyPoolDefinitions.PaddleShark,
        EnemyPoolDefinitions.Turtle
    ]);

    private static EnemyPoolData CreateTheKingdom() => new([
        EnemyPoolDefinitions.EvilArcher,
        EnemyPoolDefinitions.EvilCleric,
        EnemyPoolDefinitions.EvilLancer,
        EnemyPoolDefinitions.EvilWarrior,
        EnemyPoolDefinitions.EvilPawn
    ]);
}
