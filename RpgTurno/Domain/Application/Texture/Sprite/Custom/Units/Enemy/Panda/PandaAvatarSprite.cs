using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Panda;

public class PandaAvatarSprite : SpriteData
{
    public PandaAvatarSprite() : base(
        SpriteConst.PandaAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
