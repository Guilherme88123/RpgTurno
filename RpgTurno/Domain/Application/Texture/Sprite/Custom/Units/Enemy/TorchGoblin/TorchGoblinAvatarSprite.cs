using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.TorchGoblin;

public class TorchGoblinAvatarSprite : SpriteData
{
    public TorchGoblinAvatarSprite() : base(
        SpriteConst.TorchGoblinAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
