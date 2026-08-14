using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilWarrior;

public class EvilWarriorAvatarSprite : SpriteData
{
    public EvilWarriorAvatarSprite() 
        : base(SpriteConst.EvilWarriorAvatar, border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
