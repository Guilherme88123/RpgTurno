using Domain.Enum.Unit;
using System.Collections.Generic;

namespace RpgTurno.Screen.Play.Battle.Stage.Factory;

public static class EnemyPool
{
    public static List<EnemyDefinition> Available =
    [
        new EnemyDefinition()
        {
            UnitCode = UnitCode.EvilWarrior,
            WaveCost = 3,
            SpawnWeight = 40,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.EvilArcher,
            WaveCost = 2,
            SpawnWeight = 60,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.EvilLancer,
            WaveCost = 4,
            SpawnWeight = 25,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.EvilCleric,
            WaveCost = 4,
            SpawnWeight = 20,
            MaxCopies = 1,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.EvilPawn,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.Bear,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.BombFish,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.Gnoll,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.Gnome,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.HarpoonShark,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.HexShaman,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.Lizard,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.Minotaur,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.PaddleShark,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.PigRider,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.Panda,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.Turtle,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.Skull,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.Snake,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.SpearGoblin,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.Spider,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.Thief,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.TorchGoblin,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.Troll,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
    ];
}
