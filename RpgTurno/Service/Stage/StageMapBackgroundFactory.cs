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
            StageCode.EvilTower => new TowerMapBackgroundSprite(),
            StageCode.BarracksOfValor => new BarrackMapBackgroundSprite(),
            StageCode.TheCastle => new CastleMapBackgroundSprite(),

            _ => throw new ArgumentException("Invalid stage code for background sprite!")
        };
    }
}
