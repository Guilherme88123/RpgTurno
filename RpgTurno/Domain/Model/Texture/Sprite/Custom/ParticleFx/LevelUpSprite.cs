using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.ParticleFx;

public class LevelUpSprite : AnimationClip
{
    public LevelUpSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LevelUpEffect), 11, 1, 0.1f, 1, new BorderDefinition(16, 48, 40, 40))
    {
    }
}
