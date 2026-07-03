using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Effect;

public class LevelUpEffect : AnimationClip
{
    public LevelUpEffect() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LevelUpEffect), 11, 1, 0.1f, 1, new BorderDefinition(16, 48, 40, 40))
    {
    }
}
