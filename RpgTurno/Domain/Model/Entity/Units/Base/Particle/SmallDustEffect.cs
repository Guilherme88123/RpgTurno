using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Base.Particle;

public class SmallDustEffect : AnimationClip
{
    public SmallDustEffect() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallDustEffect), 8, 1, 0.1f, 1)
    {
    }
}
