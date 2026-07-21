using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Enemy.SuperWarrior;

public class EnemySuperWarriorAvatarSprite : SpriteData
{
    public EnemySuperWarriorAvatarSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemySuperWarriorAvatar),
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
