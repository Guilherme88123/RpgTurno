using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.BombFish;

public class BombFishAvatarSprite : SpriteData
{
    public BombFishAvatarSprite() : base(
        SpriteConst.BombFishRun,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
