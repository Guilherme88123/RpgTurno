using Domain.Enum.Stage;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Maps;

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
