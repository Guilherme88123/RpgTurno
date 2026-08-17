using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.HarpoonShark;

public class HarpoonSharkAvatarSprite : SpriteData
{
    public HarpoonSharkAvatarSprite() : base(
        SpriteConst.HarpoonSharkAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
