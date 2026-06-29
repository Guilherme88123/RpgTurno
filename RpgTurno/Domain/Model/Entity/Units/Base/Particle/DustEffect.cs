using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Base.Particle;

public class DustEffect : AnimationClip
{
    public DustEffect() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.DustEffect), 10, 1, 0.1f, 1)
    {
    }
}
