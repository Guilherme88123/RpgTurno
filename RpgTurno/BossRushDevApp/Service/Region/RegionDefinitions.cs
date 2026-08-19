using Domain.Enum.Region;

namespace RpgTurno.Service.Map.Region;

public static class RegionDefinitions
{
    public static RegionDefinition ForgottenFields = new(RegionCode.ForgottenFields, minEnemyLevel: 1, maxEnemyLevel: 5);

    public static RegionDefinition GoblinLands = new(RegionCode.GoblinLands, minEnemyLevel: 6, maxEnemyLevel: 11);

    public static RegionDefinition PirateCoast = new(RegionCode.PirateCoast, minEnemyLevel: 12, maxEnemyLevel: 17);

    public static RegionDefinition ShadowSwamp = new(RegionCode.ShadowSwamp, minEnemyLevel: 18, maxEnemyLevel: 24);

    public static RegionDefinition TheKingdom = new(RegionCode.TheKingdom, minEnemyLevel: 25, maxEnemyLevel: 30);
}
