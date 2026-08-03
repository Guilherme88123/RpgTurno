using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Application.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.ParticleFx;

public class HealSprite : AnimationClip
{
    public HealSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.HealEffect), 
        framesX: 11, 
        framesY: 1, 
        frameTime: 0.1f, 
        row: 1, 
        border: new BorderDefinition(16, 48, 40, 40))
    {
    }
}
