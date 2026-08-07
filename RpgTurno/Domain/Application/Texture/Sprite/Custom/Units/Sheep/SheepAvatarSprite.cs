using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Sheep;

public class SheepAvatarSprite : SpriteData
{
    public SheepAvatarSprite() : base(SpriteConst.EnemySuperWarriorAvatar, border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
