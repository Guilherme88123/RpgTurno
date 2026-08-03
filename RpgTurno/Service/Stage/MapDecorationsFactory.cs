using Domain.Const.Tiled;
using Domain.Dto.Map.Building;
using Domain.Dto.Sprite;
using Domain.Application.Texture.Sprite;
using Infrastructure.Tiled.Dto;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata.Ecma335;

namespace Service.Stage;

public static class MapDecorationsFactory
{
    private const string LayerSpriteIdentificatorColumn = "IntegrationCode";

    public static MapDecorationsRecord GetDecorations(TiledMapDto tiledDto)
    {
        List<AnimationClip> spritesList = new();
        List<PositionSpriteRecord> decorationsList = new();

        foreach (var layerDto in tiledDto.AllLayers)
        {
            if (!IsAnValidDecorationLayer(layerDto))
                continue;

            var layerIntegrationCodeProp = GetIntegrationCodeCustomProperty(layerDto);

            var sprite = GetSpriteByTiledIntegrationCode(layerIntegrationCodeProp.Value.ToString());

            var decorations = GetLayerDecorations(tiledDto, layerDto, sprite);

            spritesList.Add(sprite);
            decorationsList.AddRange(decorations);
        }

        return new MapDecorationsRecord(decorationsList, spritesList);
    }

    private static bool IsAnValidDecorationLayer(TiledLayerDto layerDto)
    {
        if (layerDto.Properties is null || layerDto.Properties.Count == 0)
            return false;

        var integrationCodeProperty = GetIntegrationCodeCustomProperty(layerDto);

        if (integrationCodeProperty is null)
            return false;

        if (!TiledIntegrationCodesConst.ValidIntegrationCodes.Contains(integrationCodeProperty.Value.ToString()))
            return false;

        return true;
    }

    private static TiledLayerCustomPropertyDto GetIntegrationCodeCustomProperty(TiledLayerDto layerDto)
    {
        return layerDto.Properties.FirstOrDefault(x => x.Name == LayerSpriteIdentificatorColumn);
    }

    private static AnimationClip GetSpriteByTiledIntegrationCode(string integrationCode)
    {
        return TiledIntegrationSpritesFactory.GetSprite(integrationCode);
    }

    private static List<PositionSpriteRecord> GetLayerDecorations(TiledMapDto tiledDto, TiledLayerDto layerDto, AnimationClip sprite)
    {
        var decorations = new List<PositionSpriteRecord>();

        for (var y = 0; y < layerDto.Height; y++)
            for (var x = 0; x < layerDto.Width; x++)
            {
                if (layerDto.Matrix[y, x] == 0)
                    continue;

                var positionSprite = new PositionSpriteRecord(sprite, new Point(x * tiledDto.TileWidth, y * tiledDto.TileHeight + tiledDto.TileHeight));
                decorations.Add(positionSprite);
            }

        return decorations;
    }
}
