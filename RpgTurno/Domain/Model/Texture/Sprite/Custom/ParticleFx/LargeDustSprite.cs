using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.ParticleFx;

public class LargeDustSprite : AnimationClip
{
    public LargeDustSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LargeDustEffect), 10, 1, 0.1f, 1)
    {
    }
}
