using Domain.Dto.Sprite;
using Domain.Application.Texture.Sprite;
using Infrastructure.Tiled.Dto;

namespace Domain.Dto.Map.Building;

public record BuildingMapDto
{
    private TiledMapDto _rawData;

    public int Width => _rawData.RealWidth;
    public int Height => _rawData.RealHeight;

    public SpriteData Background { get; private set; }
    public List<PositionSpriteRecord> Decorations { get; private set; }
    public List<AnimationClip> Sprites { get; private set; }

    public BuildingMapDto(TiledMapDto rawData, SpriteData background, List<PositionSpriteRecord> decorations, List<AnimationClip> sprites)
    {
        _rawData = rawData;
        Background = background;
        Decorations = decorations;
        Sprites = sprites;
    }
}
