using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.ParticleFx;

public class HealSprite : AnimationClip
{
    public HealSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.HealEffect), 11, 1, 0.1f, 1, new BorderDefinition(16, 48, 40, 40))
    {
    }
}
