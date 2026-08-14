using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Warrior;

public class WarriorAvatarSprite : SpriteData
{
    public WarriorAvatarSprite()
        : base(SpriteConst.WarriorAvatar, border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
