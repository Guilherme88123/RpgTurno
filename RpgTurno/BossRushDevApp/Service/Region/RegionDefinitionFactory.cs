using Domain.Enum.Region;

namespace RpgTurno.Service.Map.Region;

public static class RegionDefinitionFactory
{
    public static RegionDefinition Create(RegionCode regionCode)
    {
        return regionCode switch
        {
            RegionCode.ForgottenFields => RegionDefinitions.ForgottenFields,
            RegionCode.GoblinLands => RegionDefinitions.GoblinLands,
            RegionCode.PirateCoast => RegionDefinitions.PirateCoast,
            RegionCode.ShadowSwamp => RegionDefinitions.ShadowSwamp,
            RegionCode.TheKingdom => RegionDefinitions.TheKingdom,
        };
    }
}
