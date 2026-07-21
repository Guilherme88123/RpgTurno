using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.ParticleFx;

public class SmallDustSprite : AnimationClip
{
    public SmallDustSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallDustEffect), 8, 1, 0.1f, 1)
    {
    }
}
