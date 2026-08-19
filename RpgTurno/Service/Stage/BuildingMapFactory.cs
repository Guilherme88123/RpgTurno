using Domain.Application.Texture.Sprite;
using Domain.Dto.Map.Building;
using Domain.Enum.Stage;
using Infrastructure.Tiled.Dto;

namespace Service.Stage;

public static class BuildingMapFactory
{
    public static BuildingMapDto Create(StageCode stageCode)
    {
        var tiledDto = GetTiledDto(stageCode);
        var background = GetBackgroundSprite(stageCode);
        var decorations = GetDecorations(tiledDto);

        return new BuildingMapDto(tiledDto, background, decorations.Decorations, decorations.Sprites);
    }

    private static TiledMapDto GetTiledDto(StageCode stageCode)
    {
        return StageMapDtoFactory.Create(stageCode);
    }

    private static SpriteData GetBackgroundSprite(StageCode stageCode)
    {
        return StageMapBackgroundFactory.GetMapBackground(stageCode);
    }

    private static MapDecorationsRecord GetDecorations(TiledMapDto tiledDto)
    {
        return MapDecorationsFactory.GetDecorations(tiledDto);
    }
}
