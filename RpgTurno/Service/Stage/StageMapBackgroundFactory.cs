using Domain.Enum.Stage;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Maps;

namespace Service.Stage;

public static class StageMapBackgroundFactory
{
    public static SpriteData GetMapBackground(StageCode stageCode)
    {
        return stageCode switch
        {
            StageCode.Tower => new TowerMapBackgroundSprite(),
            StageCode.Barrack => new BarrackMapBackgroundSprite(),
            StageCode.Castle => new CastleMapBackgroundSprite(),

            _ => throw new ArgumentException("Invalid stage code for background sprite!")
        };
    }
}
