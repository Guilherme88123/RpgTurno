using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.SupremeWarrior;

public class SupremeWarriorAvatarSprite : SpriteData
{
    public SupremeWarriorAvatarSprite() : base(
        SpriteConst.SupremeWarriorAvatar, border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
