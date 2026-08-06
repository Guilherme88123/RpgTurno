using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Application.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.SuperWarrior;

public class EnemySuperWarriorAvatarSprite : SpriteData
{
    public EnemySuperWarriorAvatarSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemySuperWarriorAvatar),
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
